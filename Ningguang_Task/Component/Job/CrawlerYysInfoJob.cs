using System;
using System.Collections.Generic;
using Quartz;

namespace Ningguang.Component.Job
{
    public class CrawlerYysInfoJob:BaseJob
    {
        protected override void JobHandle(IJobExecutionContext context)
        {
            
            bool checkPreTaskStatus = CheckPreTaskStatus(new List<int>());
            // context.JobDetail.JobDataMap.GetString()
            for (int i = 0; i < 10; i++)
            {
                Console.Out.WriteLineAsync(nameof(CrawlerYysInfoJob)+"running");
            }
        }
    }
}