using System;
using Quartz;

namespace Ningguang.Utils
{
    public static class QuartzNameUtils
    {

        private const string triggerNameTemplate = "{TaskId}_{ItemType}_USER_TRIGGER";
        private const string jobNameTemplate = "{TaskId}_{ItemType}_USER_JOB";
        
        public const string USER_JOB_GROUP = "USER_JOB_GROUP";
        public const string USER_TRIGGER_GROUP = "USER_TRIGGER_GROUP";
        
        public const string SYSTEM_JOB_GROUP = "SYSTEM_JOB_GROUP";
        public const string SYSTEM_TRIGGER_GROUP = "SYSTEM_TRIGGER_GROUP";
        
        public static JobKey BuildJobKey(int taskId,int itemTypeEnumValue)
        {
            string  jobName = jobNameTemplate
                .Replace("{TaskId}", taskId+"")
                .Replace("{ItemType}", itemTypeEnumValue+"");
            JobKey key = JobKey.Create(USER_JOB_GROUP,jobName);
            return key;
        }
        
        
        public static string BuildTriggerName(int taskId,int itemTypeEnumValue)
        {
            string  triggerName = triggerNameTemplate
                .Replace("{TaskId}", taskId+"")
                .Replace("{ItemType}", itemTypeEnumValue+"");
            return triggerName;
        }
        
        public static string BuildJobName(int taskId,int itemTypeEnumValue)
        {
            string  jobName = jobNameTemplate
                .Replace("{TaskId}", taskId+"")
                .Replace("{ItemType}", itemTypeEnumValue+"");
            return jobName;
        }

        public static void Main0(string[] args)
        {
            var buildJobKey = BuildJobKey(1, 1);
            Console.Out.WriteLine(buildJobKey.ToString());
        }
    }
}