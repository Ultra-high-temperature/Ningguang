using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ningguang.DataAccess;
using Ningguang.Models;
using Ningguang.Models.Enum;
using Ningguang.Models.VO;
using Ningguang.Utils;

namespace Ningguang.Service
{
    public class TaskService
    {
        private readonly NingTaskContext context;

        public TaskService(NingTaskContext context)
        {
            this.context = context;
        }

        //根据taskId查找task和对应的item
        public TaskConfigVO getConfigVo(int taskId)
        {
            TaskConfigVO result = new TaskConfigVO();
            NingTask ningTask = context.NingTasks.Find(taskId);
            var ningItems = from item in context.NingItems
                where item.taskId == taskId
                select item;
            List<NingItem> items = ningItems.ToList();
            result.Items = items;
            BeanUtils.copyProperties(ningTask, result);

            return result;
        }

        //创建task 返回成功次数
        public int createTask(TaskConfigVO taskConfigVo)
        {
            NingTask task = new NingTask();
            BeanUtils.copyProperties(taskConfigVo, task);
            List<NingItem> items = taskConfigVo.Items;
            context.NingTasks.Add(task);
            context.SaveChanges();
            int taskId = task.id;

            foreach (NingItem item in items)
            {
                item.taskId = taskId;
                context.NingItems.Add(item);
            }

            int num = context.SaveChanges();
            return num;
        }

        //更新task及其对应的items
        public int updateTaskAndItems(TaskConfigVO taskConfigVo)
        {
            NingTask ningTask = context.NingTasks.Find(taskConfigVo.id);
            //检查任务状态，只能更改处于就绪的任务
            if ((int)TaskStateEnum.SETUP != ningTask.taskState)
            {
                return 0;
            }
            
            NingTask task = new NingTask();
            BeanUtils.copyProperties(taskConfigVo, task);
            context.NingTasks.Update(task);
            
            //检查item是否存在
            List<NingItem> items = taskConfigVo.Items;
            if (items != null && items.Count != 0)
            {
                foreach (NingItem item in items)
                {
                    item.updateTime = null;
                    item.createTime = null;
                    if (item.taskId != taskConfigVo.id)
                        throw new Exception("taskId 与 item内taskId不一致");
                    context.NingItems.Update(item);
                }
            }

            int num = context.SaveChanges();
            return num;
        }

        //获取任务Task列表
        public List<NingTask> getTasks()
        {
            DbSet<NingTask> tasks = context.NingTasks;
            var ningTasks = from ningTask in tasks
                select ningTask;
            List<NingTask> list = ningTasks.ToList();
            return list;
        }
        
        //获取task下属的item列表
        public List<NingItem> getItemsByTaskId(int taskId)
        {
            DbSet<NingItem> items = context.NingItems;
            var ningItems = from ningItem in items
                where ningItem.taskId == taskId
                select ningItem;
            List<NingItem> list = ningItems.ToList();
            return list;
        }
        
    }
}