namespace Ningguang.Models.NingItemObject
{
    //Item的接口
    public class ItemObject
    {
        //集群间通讯的key
        //todo 各模块调用安全性
        protected string key { get; set; }
    }
}