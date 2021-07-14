// using System;
// using Ningguang.Component.Job;
// using Quartz;
// using Quartz.Impl;
//
// namespace Ningguang.Component
// {
//     public class JobHelp
//     {
//         private static volatile IScheduler scheduler;
//
//
//         //job类 cron 定时器所需的cron表达式
//         public static async void loadJob(BaseJob job, string cron,string str)
//         {
//             if (JobHelp.scheduler == null)
//             {
//                 StdSchedulerFactory factory = new StdSchedulerFactory();
//                 IScheduler temp = await factory.GetScheduler();
//                 lock (typeof(IScheduler))
//                 {
//                     if (scheduler == null)
//                     {
//                         scheduler = temp;
//                     }
//                 }
//                 scheduler.Start();
//             }
//
//             ITrigger trigger = TriggerBuilder.Create()
//                 .WithIdentity("trigger3", "group1"+str)
//                 .WithCronSchedule(cron)
//                 .ForJob("myJob", "group1"+str)
//                 .Build();
//
//             IJobDetail jobDetail = JobBuilder.Create<TestJob>()
//                 .WithIdentity("myJob", "group1"+str)
//                 .UsingJobData("key",str)
//                 .Build();
//
//             await scheduler.ScheduleJob(jobDetail, trigger);
//             
//         }
//     }
// }