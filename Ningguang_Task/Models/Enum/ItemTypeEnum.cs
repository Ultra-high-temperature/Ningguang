using System;

//https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/internal
namespace Ningguang.Models.Enum
{
    //c#只支持单个值,所以需要扩展方法
    public enum ItemTypeEnum
    {
        crawlerYysList = 0,
        crawlerYysInfo = 1,
        mlAutoUpdateModel = 2
    }
    

    //c#只支持单个值,所以需要扩展方法
    // public enum ItemType
    // {
    //     [ItemTypeAttr(1,"crawler_yys_list")]
    //     crawlerYysList,
    //     [ItemTypeAttr(2,"crawler_yys_info")]
    //     crawlerYysInfo,
    //     [ItemTypeAttr(3,"ml_auto_update_model")]
    //     mlAutoUpdateModel
    // }
    //
    // class ItemTypeAttr : Attribute
    // {
    //     //这个关键字没弄懂
    //     internal ItemTypeAttr(int dbType, string typeName)
    //     {
    //         DbType = dbType;
    //         TypeName = typeName;
    //     }
    //
    //     //数据库类型，1，2，3，4
    //     private int DbType { get; set; }
    //
    //     //对应到db的实际类型
    //     private String TypeName { get; set; }
    // }
}