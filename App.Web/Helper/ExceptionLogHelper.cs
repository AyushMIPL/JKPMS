using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using App.Data.Entities;
using App.Data;
using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using App.Web.Controllers;
using System.Net;
using App.Web.Repository;
using System.Net.Sockets;

namespace App.Web.Helper
{
  //***** Code By Himanshu Rajput  ****

  public class ExceptionLogHelper
  {

    private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
    private AppDbContext db;
    private string connectionString;

    // Method to log error manually into the ErrorLogs table

    public ExceptionLogHelper()
    {
      db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
      connectionString = ConnectionStringProvider.GetConnectionString();
    }
    public void LogErrorToDatabase(Exception ex)
    {
      // Prepare the error details
      string errorMessage = ex.Message;
      string stackTrace = ex.StackTrace;
      DateTime dateOccurred = DateTime.Now;
      int UserId = AppUserManager.GetUserId();

      // Get machine info
      string MachineName = Environment.MachineName;
      string IpAddress = GetLocalIPAddress();

      // SQL Query to insert the error
      string query = "INSERT INTO ErrorLogs (ErrorMessage, StackTrace, DateOccurred,UserId,MachineName,IpAddress) VALUES (@ErrorMessage, @StackTrace, @DateOccurred,@UserId,@MachineName,@IpAddress)";

      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        using (SqlCommand command = new SqlCommand(query, connection))
        {
          // Add parameters to avoid SQL injection
          command.Parameters.AddWithValue("@ErrorMessage", errorMessage);
          command.Parameters.AddWithValue("@StackTrace", stackTrace);
          command.Parameters.AddWithValue("@DateOccurred", dateOccurred);
          command.Parameters.AddWithValue("@UserId", UserId);
          command.Parameters.AddWithValue("@MachineName", MachineName);
          command.Parameters.AddWithValue("@IpAddress", IpAddress);

          // Open the connection and execute the query
          connection.Open();
          command.ExecuteNonQuery();
          connection.Close();
        }
      }
    }
    static string GetLocalIPAddress()
    {
      string ipAddress = string.Empty;
      try
      {
        // Get host entry for local machine
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

        // Loop through IP addresses
        foreach (IPAddress ip in host.AddressList)
        {
          // Check for IPv4 address (optional, as you might want IPv6 too)
          if (ip.AddressFamily == AddressFamily.InterNetwork)
          {
            ipAddress = ip.ToString();
            break;
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error: " + ex.Message);
      }

      return ipAddress;
    }

  }
}