using System;
using System.Reflection;
using MySqlConnector;

namespace Ningguang.Utils
{
    public class BeanUtils
    {
        public static void copyProperties(Object source,Object target)
        {
            var tInType = source.GetType();
            foreach (var itemOut in target.GetType().GetProperties())
            {
                var itemIn = tInType.GetProperty(itemOut.Name); ;
                if (itemIn != null)
                {
                    itemOut.SetValue(target, itemIn.GetValue(source));
                }
            }
        }
        
    }
}