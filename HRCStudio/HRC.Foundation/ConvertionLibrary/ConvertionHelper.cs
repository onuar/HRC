using System;

namespace HRC.Foundation.ConvertionLibrary
{
    public class ConvertionHelper
    {

        public static T ConvertValue<T>(object value)
        {
            try
            {
                if (!(value is IConvertible))
                    return (T)value;
                if (typeof(T).BaseType == typeof(Enum))
                {
                    return EnumOperations.GetEnumValue<T>(value.ToString());
                }
                if (typeof(T) == typeof(bool))
                {
                    object returnValue = false;
                    if (value.ToString().Equals("1") || value.ToString().ToLower().Equals("true"))
                    {
                        returnValue = true;
                    }
                    return (T)returnValue;
                }
                return (T)Convert.ChangeType(value, typeof(T), new System.Globalization.CultureInfo("tr-TR"));
            }
            catch
            {
                //TODO: onur Exception
                throw new Exception("değer convert edilemiyor: convertionhelper. value:" + value.ToString());
            }
        }
    }
}
