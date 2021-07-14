using System;
using System.Collections.Generic;

namespace Ningguang.Models.VO
{
    public class TaskConfigVO
    {
        public int id { get; set; }
        public string taskName { get; set; }
        public int taskState{ get; set; }
        public string preTask{ get; set; }
        public int taskCount{ get; set; }
        public int taskFinishCount{ get; set; }
        public DateTime autoTriggerTime{ get; set; }
        
        public List<NingItem> Items{ get; set; }
    }
}