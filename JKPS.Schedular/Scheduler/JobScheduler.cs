using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace JKPS.Schedular.Scheduler
{
  public class JobScheduler
  {
    public static void Start()
    {
      try
      {

      
      IScheduler ExpSubNotificationScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
      ExpSubNotificationScheduler.Start();

      IJobDetail job = JobBuilder.Create<BankDisbursementUtility>().Build();

      ITrigger trigger = TriggerBuilder.Create()
          .WithDailyTimeIntervalSchedule
            (s =>
               s.WithIntervalInSeconds(10)
              .OnEveryDay()
              .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(DateTime.Now.Hour, DateTime.Now.Minute))
            )
          .Build();

      ExpSubNotificationScheduler.ScheduleJob(job, trigger);
      }
      catch (Exception ex)
      {

        throw;
      }
    }

    public static void UpdateDashBoardTableStart()
    {
      try
      {

      IScheduler ExpSubNotificationScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
      ExpSubNotificationScheduler.Start();

      IJobDetail job = JobBuilder.Create<UpdateDashboardTable>().Build();
                ITrigger trigger = TriggerBuilder.Create()
                    .WithDailyTimeIntervalSchedule(s => s
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(1, 0)) 
                    )
                    .Build();

                ExpSubNotificationScheduler.ScheduleJob(job, trigger);
      }
      catch (Exception ex)
      {

        throw;
      }
    }
  }
}
