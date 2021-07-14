using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Ningguang.DataAccess;
using Quartz;
using Quartz.Logging;

namespace Ningguang.Component.Job
{
    //job需要什么？
    //items 执行的参数List 从QuartzManager中获取
    //preParameter 前置任务的执行结果 采用JobName获取(前置任务会有多个type，所以会有多个参数，需要指定)
    //processor 调用的api job内部决定该怎么处理
    
    public abstract class BaseJob:IJob
    {
        public string name { get; set; }
        
        //todo 需要加入监控
        public Task Execute(IJobExecutionContext context)
        {
            Console.Out.WriteLineAsync($"{context.JobDetail.Key}开始执行");
            
            // object? ningTaskContext = ServiceProviderProvider.ServiceProvider.GetRequiredService(typeof(NingTaskContext));
            
            DateTime start = new DateTime();
            JobHandle(context);
            DateTime end = new DateTime();
            TimeSpan timeSpan = end - start;
            Console.Out.WriteLineAsync($"{context.JobDetail.Key}执行时间为{timeSpan}");
            return null;
        }

        //检查提供的preTask是否都执行过了
        protected bool CheckPreTaskStatus(List<int> preTaskIds)
        {
            return true;
        }

        protected abstract void JobHandle(IJobExecutionContext context);

    }
}