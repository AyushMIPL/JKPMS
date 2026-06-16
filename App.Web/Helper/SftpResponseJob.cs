using App.Data;
using log4net;
using Quartz;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Helper
{
    [DisallowConcurrentExecution]
    public class SftpResponseJob : IJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SftpResponseJob));

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                Logger.Info($"[SftpResponseJob] Started execution at {DateTime.Now}");
                var stopwatch = Stopwatch.StartNew();

                ConnectionStringProvider connectionStringProvider = new ConnectionStringProvider();
                using (var db = new AppDbContext(connectionStringProvider.GetConnectionString()))
                {
                    var activeDistricts = db.MasterDistrict
                        .Where(d => d.IsActive)
                        .Select(d => d.Name)
                        .ToList();

                    var processor = new SftpResponseProcessor(db);

                    foreach (var district in activeDistricts)
                    {
                        if (string.IsNullOrWhiteSpace(district))
                            continue;

                        // Process Validation
                        try
                        {
                            Logger.Info($"[SftpResponseJob] Fetching Validation responses for district: {district}");
                            var valResult = processor.FetchAndProcessResponses("Validation", district);
                            Logger.Info($"[SftpResponseJob] Validation Result for {district}: Success={valResult.Success}, Message={valResult.Message}");
                        }
                        catch (Exception ex)
                        {
                            Logger.Error($"[SftpResponseJob] Error processing Validation responses for district: {district}", ex);
                        }

                        // Process Disbursement
                        try
                        {
                            Logger.Info($"[SftpResponseJob] Fetching Disbursement responses for district: {district}");
                            var disbResult = processor.FetchAndProcessResponses("Disbursement", district);
                            Logger.Info($"[SftpResponseJob] Disbursement Result for {district}: Success={disbResult.Success}, Message={disbResult.Message}");
                        }
                        catch (Exception ex)
                        {
                            Logger.Error($"[SftpResponseJob] Error processing Disbursement responses for district: {district}", ex);
                        }
                    }
                }

                stopwatch.Stop();
                Logger.Info($"[SftpResponseJob] Finished execution at {DateTime.Now}. Duration: {stopwatch.ElapsedMilliseconds} ms.");
            }
            catch (Exception ex)
            {
                Logger.Error("[SftpResponseJob] Critical error during execution", ex);
            }

            return Task.CompletedTask;
        }
    }
}
