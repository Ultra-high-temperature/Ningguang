using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ningguang.Models
{
    [Table("ning_task")]
    public class NingTask
    {
        [Column("id")]
        public int id { get; set; }
        
        [Column("task_name")]
        public string taskName { get; set; }
        
        [Column("task_state")]
        public int taskState{ get; set; }
        
        [Column("pre_task")]
        public string preTask{ get; set; }
        
        [Column("task_count")]
        public int taskCount{ get; set; }
        
        [Column("task_finish_count")]
        public int taskFinishCount{ get; set; }
        
        [Column("auto_trigger_time")]
        public DateTime autoTriggerTime{ get; set; }
        
        [Column("create_time")]
        public DateTime createTime{ get; set; }
        
        [Column("update_time")]
        public DateTime updateTime{ get; set; }
    }
}