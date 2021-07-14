using System;
using System.Text.Json;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json;
using Ningguang.Models;
using Ningguang.Models.Enum;
using Ningguang.Models.NingItemObject.Ml;
using Ningguang.Models.NingItemObject.Yys;

namespace Ningguang.Utils
{
    public class NingItemUtils
    {
        public static void ParseItemJsonToObject(NingItem item)
        {
            int itemItemType = item.itemType;
            string itemItemJsonStr = item.itemJson;
            switch (itemItemType)
            {
                case (int) ItemTypeEnum.crawlerYysInfo:
                    item.itemJsonObject = JsonConvert.DeserializeObject<YysInfo>(itemItemJsonStr);
                    break;
                case (int) ItemTypeEnum.crawlerYysList:
                    item.itemJsonObject = JsonConvert.DeserializeObject<YysList>(itemItemJsonStr);
                    break;
                case (int) ItemTypeEnum.mlAutoUpdateModel:
                    item.itemJsonObject = JsonConvert.DeserializeObject<MlUpdateModel>(itemItemJsonStr);
                    break;
                default:
                    throw new Exception("未定义的字段类型");
            }
        }

        // public static void Main(string[] args)
        // {
        //     var ningItem = new NingItem();
        //     ningItem.taskId = 0;
        //     YysInfo yysInfo = new YysInfo();
        //     yysInfo.ordersn = "123";
        //     yysInfo.callbackUrl = "http://localhost:8080/123";
        //     
        //     string serializeObject = JsonConvert.SerializeObject(yysInfo);
        //     ningItem.itemJsonStr = serializeObject;
        //     
        //     ParseItemJsonToObject(ningItem);
        //     
        //     Console.WriteLine(ningItem.itemJson);
        // }
    }
}