using Quartz;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace JKPS.Schedular.Scheduler
{
    public class UpdateDashboardTable : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            string[] connectionNames = { "AppConnection", "AppConnection1" };
            string storedProcedureName = "USP_PaymentCount_Cache"; 

            foreach (string connectionName in connectionNames)
            {
               string connectionString = GetConnectionString(connectionName);
                ExecuteStoredProcedure(connectionString, storedProcedureName);
            }

            return Task.CompletedTask;
        }

        private static void ExecuteStoredProcedure(string connectionString, string storedProcedureName)
        {
            try
            {
                using (System.Data.SqlClient.SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"Stored procedure executed successfully on database. Rows affected: {rowsAffected}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing stored procedure on database: {ex.Message}");
                // Log the exception as required
            }
        }

        private static string GetConnectionString(string connectionName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionName]?.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception($"Connection string '{connectionName}' not found in web.config.");
            }
            return connectionString;
        }
    }
}
