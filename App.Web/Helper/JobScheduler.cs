using Quartz;
using Quartz.Impl;
using System;


namespace App.Web.Helper
{
    public class JobScheduler
    {
        public static void Start()
        {
          try
          {
            IScheduler ExpSubNotificationScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            ExpSubNotificationScheduler.Start();

            // Bank Disbursement Utility
            IJobDetail job = JobBuilder.Create<BankDisbursementUtilityJobScheduler>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(5)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(DateTime.Now.Hour, DateTime.Now.Minute))
                  )
                .Build();
            ExpSubNotificationScheduler.ScheduleJob(job, trigger);  
            
            // Bank Validation Utility

            IScheduler BankValidationScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            BankValidationScheduler.Start();
            IJobDetail bankValidationJob = JobBuilder.Create<BankValidationUtility>().Build();
            ITrigger bankValidationTrigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(5)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(DateTime.Now.Hour, DateTime.Now.Minute))
                  )
                .Build();
             BankValidationScheduler.ScheduleJob(bankValidationJob, bankValidationTrigger); 

          }
          catch (Exception ex)
          {
            throw;
          }
              
        }
    }
}

