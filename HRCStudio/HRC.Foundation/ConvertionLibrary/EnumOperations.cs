using System;

namespace HRC.Foundation.ConvertionLibrary
{
    public class EnumOperations
    {
        public static T GetEnumValue<T>(string value)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch
            {
                //TODO: onur Exception
                throw new Exception("enum convert edilemiyor.");
            }
        }
    }
}