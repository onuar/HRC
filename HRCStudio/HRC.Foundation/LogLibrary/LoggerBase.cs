using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRC.Foundation.LogLibrary
{
    public abstract class LoggerBase
    {
        public LoggerBase NextLogger { get; set; }

        protected abstract void WriteInternal(LogContext context);

        //template method
        public void Write(LogContext context)
        {
            try
            {
                WriteInternal(context);
            }
            catch
            {
                if (NextLogger != null)
                    NextLogger.Write(context);
            }
        }
    }
}