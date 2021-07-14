using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ningguang.Models.NingItemObject;

namespace Ningguang.Models
{
    [Table("ning_item")]
    public class NingItem
    {
        [Column("id")] public int id { get; set; }

        [Column("task_id")] public int taskId { get; set; }

        [Column("item_type")] public int itemType { get; set; }

        //此字段存放itemJson序列化后数据
        [Column("item_json")] public string itemJson { get; set; }

        [NotMapped] public ItemObject itemJsonObject { get; set; }

        [Column("item_extend")] public string itemExtend { get; set; }

        [Column("create_time")] public DateTime? createTime { get; set; }

        [Column("update_time")] public DateTime? updateTime { get; set; }
    }
}