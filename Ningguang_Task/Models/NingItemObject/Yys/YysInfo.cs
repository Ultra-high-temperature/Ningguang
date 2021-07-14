namespace Ningguang.Models.NingItemObject.Yys
{
    //调用crawler_yys_info爬虫所需参数
    public class YysInfo:ItemObject
    {
        //回调接口
        public string callbackUrl { get; set; }
        //回调所用主键
        public int itemId{ get; set; }
        //参数: gmt_ordersn
        public string ordersn{ get; set; }
        
    }
}