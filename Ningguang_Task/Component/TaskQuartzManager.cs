using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Ningguang.Component.Job;
using Ningguang.Utils;
using Quartz;
using Quartz.Impl;

namespace Ningguang.Component
{
    //将task直接映射成用户job
    //jobName 代表 jobKey
    
    //job处理,加入后立即生效，假如已经启动了
    //scheduler.ScheduleJob(jobDetail, trigger); 
    
    
    public static class TaskQuartzManager
    {
        private static readonly StdSchedulerFactory SchedulerFactory = new StdSchedulerFactory();
        
        //添加用户Job
        //preKeyList是当前task的前置任务的key，执行时通过key从context取出数据使用
        //用于task间传递必要数据
        public static void addJob(string jobName,string triggerName,Type jobType
            ,string preKeyList, string cron)
        {
            // Type type = typeof(jobType);
            IScheduler scheduler = SchedulerFactory.GetScheduler().Result;
            IJobDetail jobDetail = JobBuilder.Create(jobType)
                .WithIdentity(jobName,QuartzNameUtils.USER_JOB_GROUP)
                .UsingJobData(nameof(preKeyList), JsonConvert.SerializeObject(preKeyList))
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(triggerName, QuartzNameUtils.USER_TRIGGER_GROUP)
                .WithSchedule(CronScheduleBuilder.CronSchedule(cron))
                .Build();
            scheduler.ScheduleJob(jobDetail, trigger);
        }
        
        //启动时添加系统任务
        public static void addSystemJob(string cron)
        {
            IScheduler scheduler = SchedulerFactory.GetScheduler().Result;
            IJobDetail jobDetail = JobBuilder.Create(typeof(SystemJob))
                .WithIdentity("SYSTEM_JOB",QuartzNameUtils.SYSTEM_JOB_GROUP)
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("SYSTEM_TRIGGER", QuartzNameUtils.SYSTEM_TRIGGER_GROUP)
                .WithSchedule(CronScheduleBuilder.CronSchedule(cron))
                .Build();
            scheduler.ScheduleJob(jobDetail, trigger);
        }

        //删除Job
        public static void deleteJob(string jobName,string triggerName)
        {
            IScheduler scheduler = SchedulerFactory.GetScheduler().Result;
            TriggerKey triggerKey = new TriggerKey(triggerName);
            //停止触发器
            scheduler.PauseTrigger(triggerKey);
            // //移除触发器，如果job没有触发器了，那么job也会被移除
            // scheduler.UnscheduleJob(triggerKey);
            //移除job，同时会移除关联的触发器
            scheduler.DeleteJob(JobKey.Create(jobName,QuartzNameUtils.USER_JOB_GROUP));
        }

        //todo 启动Job
        public static void startJob()
        {
            IScheduler scheduler = SchedulerFactory.GetScheduler().Result;
            if (!scheduler.IsShutdown)
            {
                scheduler.Start();
            }
        }

        //暂停Job
        public static void pauseJob(string jobName)
        {
            IScheduler scheduler = SchedulerFactory.GetScheduler().Result;
            scheduler.PauseJob(new JobKey(jobName,QuartzNameUtils.USER_JOB_GROUP));
        }
        

        //启动Scheduler
        public static void startScheduler()
        {
            IScheduler scheduler = SchedulerFactory.GetScheduler().Result;
            if (!scheduler.IsShutdown)
            {
                scheduler.Start();
            }
        }

        
        public static void startJob(string jobName)
        {
            IScheduler scheduler = SchedulerFactory.GetScheduler().Result;
            scheduler.ResumeJob(new JobKey(jobName ,QuartzNameUtils.USER_JOB_GROUP));
        }

        
        static List<string> getJobNames()
        {
            IScheduler scheduler = SchedulerFactory.GetScheduler().Result;
            scheduler.GetJobKeys(null, default);
            return null;
        }
    }
}