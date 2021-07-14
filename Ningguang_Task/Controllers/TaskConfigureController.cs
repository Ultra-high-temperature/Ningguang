using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Ningguang.Component;
using Ningguang.DataAccess;
using Ningguang.Models;
using Ningguang.Models.NingItemObject;
using Ningguang.Models.VO;
using Ningguang.Service;

namespace Ningguang.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskConfigureController:ControllerBase
    {
        private readonly NingTaskContext context;
        private readonly TaskService taskService;
        private readonly IConfiguration configuration;

        public TaskConfigureController(NingTaskContext context,TaskService taskService,
            IConfiguration configuration)
        {
            this.context = context;
            this.taskService = taskService;
            this.configuration = configuration;
        }

        [HttpGet]
        public OkObjectResult GetTaskList()
        {
            List<NingTask> tasks = taskService.getTasks();
            return Ok(tasks);
        }
        
        [HttpGet]
        [Route("items/{taskId}")]
        public OkObjectResult GetItemListByTaskId([FromRoute] int taskId)
        {
            List<NingTask> tasks = taskService.getTasks();
            return Ok(tasks);
        }

        [HttpPut]
        public OkObjectResult Update(TaskConfigVO taskConfigVo)
        {
            int updateTaskAndItems = taskService.updateTaskAndItems(taskConfigVo);
            return Ok(updateTaskAndItems);
        }

        [HttpPost]
        public OkObjectResult Create([FromBody]TaskConfigVO taskConfigVo)
        {
            int success = taskService.createTask(taskConfigVo);
            return Ok(success);
        }


        //todo 需限制访问ip
        [HttpGet]
        [Route("connectionString")]
        public string GetDbConnectionString()
        {
            string connectionString = configuration.GetConnectionString("string");
            return connectionString;
        }
        
        // //不建议使用
        // [HttpGet]
        // [Route("test")]
        // public void t()
        // {
        //     var taskDbContextFactory = new TaskDbContextFactory();
        //     var ba = taskDbContextFactory.CreateDbContext(null);
        //     return;
        // }

    }
}