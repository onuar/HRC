using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HRC.Foundation.LogLibrary;

namespace TextLoggerAddOn
{
    public class TextLogger : LoggerBase
    {
        protected override void WriteInternal(LogContext context)
        {
            using (TextWriter tw = File.AppendText("logs.txt"))
            {
                //direkt context'i basarak
                tw.WriteLine(DateTime.Now.ToString() + "=>" + context);

                //kolonlar arasında dolaşarak
                //foreach (var c in context.Values)
                //{
                //    tw.WriteLine(DateTime.Now.ToString() + "=>" + c.ToString());
                //}

                tw.WriteLine("#logged!!!");
            }
        }
    }
}
