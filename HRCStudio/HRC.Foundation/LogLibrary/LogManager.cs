using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace HRC.Foundation.LogLibrary
{
    public class LogManager
    {
        private LoggerBase previousLogger;
        private LoggerBase firstLogger;
        private static object lockobject = new object();


        //singleton
        static Lazy<LogManager> _instance =
            new Lazy<LogManager>(() => { return new LogManager(); }, true);
        public static LogManager Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private LogManager()
        {
            //todo@onuar: hrcLoggerSetting dosyası yoksa, yani adam loglama için bir configuration yapmamışsa hata vermemeli. ya da başka bi şey.
            System.Uri uri = new System.Uri(Assembly.GetExecutingAssembly().CodeBase);
            string foundationLibraryDirectory = Path.GetDirectoryName(uri.AbsolutePath);

            XmlDocument xdoc = XmlLibrary.XmlHelper.GetXmlDocument(foundationLibraryDirectory + "/" + "HRCLoggerSetting.xml");

            foreach (XmlNode item in xdoc.SelectNodes("loggers/logger"))
            {
                string typeName = item.Attributes["TypeName"].Value;

                string[] parts = typeName.Split(',');

                Assembly asm = Assembly.LoadFile(Path.Combine(foundationLibraryDirectory, parts[1] + ".dll"));

                Type pluginType = asm.GetType(parts[0]);

                LoggerBase logger = Activator.CreateInstance(pluginType) as LoggerBase;

                if (firstLogger == null)
                    firstLogger = logger;

                if (previousLogger != null)
                    previousLogger.NextLogger = logger;

                previousLogger = logger;
            }
        }

        public void Write(LogContext context, bool asenkron = false)
        {
            if (firstLogger != null)
            {
                if (asenkron)
                {
                    Action<LogContext> del = new Action<LogContext>(firstLogger.Write);
                    del.BeginInvoke(context, null, null);
                }
                else
                    firstLogger.Write(context);
            }
        }
    }
}
