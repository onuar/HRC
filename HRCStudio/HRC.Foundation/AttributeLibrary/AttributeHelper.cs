using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRC.Foundation.AttributeLibrary
{
    public static class AttributeHelper
    {
        public static List<TAttributeType> GetInterfacesMethodsAttributes<TAttributeType>(Type objType)
        {
            List<TAttributeType> attributeCollection;
            foreach (var item in objType.GetInterfaces())
            {
                foreach (var methodInfo in item.GetMethods())
                {

                    var methodAttributes = methodInfo.GetCustomAttributes(typeof(TAttributeType), true);
                }
            }

            return null;
        }

        public static IEnumerable<TAttributeType> GetMethodAttributes<TAttributeType>(System.Reflection.MethodBase methodBase)
        {
            object[] attributes = methodBase.GetCustomAttributes(typeof(TAttributeType), true);
            return attributes.Cast<TAttributeType>();
        }

        public static IEnumerable<TAttributeType> GetCustomAttributes<TAttributeType>(Type objectType)
        {
            object[] attributes = objectType.GetCustomAttributes(typeof(TAttributeType), true);
            return attributes.Cast<TAttributeType>().ToList();
        }
    }
}