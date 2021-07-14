namespace Ningguang.Models.NingItemObject.Yys
{
    //调用crawler_yys_List爬虫所需参数
    public class YysList:ItemObject
    {
        //回调接口
        private string callbackUrl { get; set; }
        //回调所用主键
        private int itemId{ get; set; }
    }
}