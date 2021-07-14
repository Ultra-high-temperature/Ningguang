using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ningguang.Component.Job;
using Ningguang.DataAccess;
using Ningguang.Models;
using Ningguang.Models.Enum;
using Ningguang.Utils;
using Quartz;

namespace Ningguang.Component.Job
{
    public class SystemJob : BaseJob
    {
        //dbContext注意作用域
        private NingTaskContext taskContext;

        //已经被加载进内存的任务id
        private static HashSet<int> memoryTaskSet = new HashSet<int>();

        //首次启动，需要加载处于waiting wbt running finished pause 的任务
        //后续运行时，需要加载setup状态的任务
        private static bool FIRST_SET_UP = true;

        //todo 需要开启事务 以保证数据的一致性
        protected override void JobHandle(IJobExecutionContext context)
        {

            TaskDbContextFactory dbContextFactory = new TaskDbContextFactory();
            taskContext = dbContextFactory.CreateDbContext(null);
            DbSet<NingTask> dbTasks = taskContext.NingTasks;

            //定时扫表加载新任务,首次执行加载处于waiting wbt running finished pause的任务
            List<NingTask> newTaskList = new List<NingTask>();
            if (FIRST_SET_UP == false)
            {
                newTaskList = dbTasks.Where(t => !memoryTaskSet.Contains(t.id)
                                                 && t.taskState == (int) TaskStateEnum.SETUP).ToList();
            }
            else
            {
                newTaskList = dbTasks.Where(t =>
                    t.taskState == (int) TaskStateEnum.WAITING
                    || t.taskState == (int) TaskStateEnum.RUNNING
                    || t.taskState == (int) TaskStateEnum.WAIT_BEFORE_TASK
                    || t.taskState == (int) TaskStateEnum.FINISHED
                    || t.taskState == (int) TaskStateEnum.PAUSE).ToList();
                FIRST_SET_UP = false;
            }

            //将任务id添加到 memoryTaskSet 中 代表该任务已经被加载到内存
            foreach (NingTask task in newTaskList)
            {
                memoryTaskSet.Add(task.id);
                handleNewTask(task);
            }

            //同时要移除已经停用的任务
            List<NingTask> deleteTasks = dbTasks.Where(t => t.taskState == (int) TaskStateEnum.DELETE)
                .ToList();
            foreach (NingTask task in deleteTasks)
            {
                memoryTaskSet.Remove(task.id);
                handleDeleteTask(task.id);
            }
        }

        //处理一个新增task
        //将task映射成多个job
        private void handleNewTask(NingTask task)
        {
            List<NingItem> items =
                taskContext.NingItems.Where(t => t.taskId == task.id).ToList();
            if (items == null || items.Count == 0)
            {
                throw new Exception("task对应的items为空");
            }

            List<IGrouping<int, NingItem>> groups =
                items.GroupBy(item => item.itemType).ToList();

            //将同一个item下的同类任务合并，按类别创建job
            foreach (var group in groups)
            {
                int itemType = group.Key;
                string jobName = QuartzNameUtils.BuildJobName(task.id, itemType);
                string triggerName = QuartzNameUtils.BuildJobName(task.id, itemType);

                //将按type和taskId划分出来的NingItemList 以k-v的形式放到cache中
                //key 即 jobName
                IEnumerator<NingItem> list = @group.GetEnumerator();
                QuartzParameter.PutJobParameterCacheSyn(jobName, list);

                Type clazz;
                //todo 优化掉硬编码
                switch (itemType)
                {
                    case (int) ItemTypeEnum.crawlerYysInfo:
                        clazz = typeof(CrawlerYysInfoJob);
                        break;
                    case (int) ItemTypeEnum.crawlerYysList:
                        clazz = typeof(CrawlerYysListJob);
                        break;
                    case (int) ItemTypeEnum.mlAutoUpdateModel:
                        clazz = typeof(MlAutoUpdateModelJob);
                        break;
                    default:
                        throw new Exception("unKnowType");
                }

                TaskQuartzManager.addJob(jobName, triggerName, clazz, task.preTask, task.autoTriggerTime.ToString());
            }
        }

        //删除一个task 
        //从db中load出type，依据type进行修改
        //todo 引入cache有必要吗？
        private void handleDeleteTask(int taskId)
        {
            NingTask ningTask = taskContext.NingTasks.Find(taskId);
            DbSet<NingItem> ningItems = taskContext.NingItems;
            if (ningTask == null)
            {
                throw new Exception("taskId不存在");
            }

            if (ningTask.taskState != (int) TaskStateEnum.PAUSE)
            {
                //获取task下所有的type
                var types = ningItems.Where(t => t.taskId == taskId)
                    .Select(t => t.itemType)
                    .GroupBy(t => t)
                    .Select(t => t.Key);
                foreach (var type in types)
                {
                    string jobName = QuartzNameUtils.BuildJobName(taskId, type);
                    string jobTriggerName = QuartzNameUtils.BuildTriggerName(taskId, type);
                    TaskQuartzManager.deleteJob(jobName, jobTriggerName);
                }
            }
            else
            {
                throw new Exception("task未暂停");
            }
        }
    }
}