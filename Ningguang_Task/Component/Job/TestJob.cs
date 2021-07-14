using System;
using System.Threading.Tasks;
using Ningguang.Component.Job;
using Quartz;

namespace Ningguang.Component
{
    public class TestJob:BaseJob
    {
        // public override async Task Execute(IJobExecutionContext context)
        // {
        //     string? s = context.JobDetail.JobDataMap.GetString("key");
        //     await Console.Out.WriteLineAsync(s);
        // }

        protected override void JobHandle(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}