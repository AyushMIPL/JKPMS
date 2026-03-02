using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Web;

namespace App.Web.Repository
{
  public class RateLimit
  {
    private static readonly ConcurrentDictionary<string, (int Attempts, DateTime LastAttempt)> _attempts = new ConcurrentDictionary<string, (int, DateTime)>();
    private static readonly int MaxAttempts;
    private static readonly int BlockDurationInMinutes;

    static RateLimit()
    {
      // Retrieve configuration values
      MaxAttempts = int.TryParse(ConfigurationManager.AppSettings["MaxAttempts"], out var attempts) ? attempts : 3;
      BlockDurationInMinutes = int.TryParse(ConfigurationManager.AppSettings["BlockDurationInMinutes"], out var duration) ? duration : 5;
    }
    public static bool IsRequestAllowed()
    {
      var ipAddres = GetLocalIPAddress();
      var now = DateTime.Now;
      var userAttempts = _attempts.GetOrAdd(ipAddres, (0, now));

      if (userAttempts.Attempts >= MaxAttempts)
      {
        var blockEndTime = userAttempts.LastAttempt.AddMinutes(BlockDurationInMinutes);
        if (now < blockEndTime)
        {
          return false;
        }

        // Reset attempts after block duration
        _attempts[ipAddres] = (0, now);
      }

      // Update attempts and time of last attempt
      _attempts[ipAddres] = (userAttempts.Attempts + 1, now);
      return true;
    }

    public static TimeSpan? GetRetryAfterTime()
    {
      var ipAddres =GetLocalIPAddress();
      if (_attempts.TryGetValue(ipAddres, out var userAttempts))
      {
        var now = DateTime.Now;
        var blockEndTime = userAttempts.LastAttempt.AddMinutes(BlockDurationInMinutes);
        if (now < blockEndTime)
        {
          return blockEndTime - now;
        }
      }
      return null;
    }
    public static void ClearRateLimit()
    {
      // Remove the user's entry from the dictionary
      _attempts.TryRemove(GetLocalIPAddress(), out _);
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