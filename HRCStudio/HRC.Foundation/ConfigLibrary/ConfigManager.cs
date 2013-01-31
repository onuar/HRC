
namespace HRC.Foundation.ConfigLibrary
{
    public class ConfigManager
    {
        public static T GetConfigValue<T>(string key)
        {
            ConfigReaderBase reader = 
                ConfigReadersFactory.GetConfigReader();
            object value = reader.GetValue(key);
            return ConvertionLibrary.ConvertionHelper.ConvertValue<T>(value);
        }
    }
}
