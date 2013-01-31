using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRC.Foundation.ConvertionLibrary;
using HRC.Foundation.LogLibrary;
using HRC.Library.DBAccessLayer;
using HRC.Library.EntityLibrary.EntityBase;
using HRC.Library.EntityLibrary.EntityOperations;

namespace HRC.Library.LoggerPlugins
{
    public class SqlLogger : LoggerBase
    {
        protected override void WriteInternal(LogContext context)
        {
            //todo@onuar: 110812 userContext buraya taşınacak. yani login olan user'ın bilgileri
            //aklımda yarım yamalak şeyler var ama henüz oturamadım.
            List<BaseEntity> logEntities = new List<BaseEntity>();
            foreach (var c in context.Values)
            {
                BaseEntity logEntity = new BaseEntity() { EntityName = "Log" };
                EntityColumn ec = (EntityColumn)c.Value;
                logEntity.SetValue<string>("OldValue", ConvertionHelper.ConvertValue<string>(ec.OldValue));
                logEntity.SetValue<string>("NewValue", ConvertionHelper.ConvertValue<string>(ec.CurrentValue));
                logEntities.Add(logEntity);
            }
            //todo@onuar: 110812 transaction ile database'e yazdırılacak
            //entityManager ile olmuyor çünkü loggerPlugins projesi, hrc.library'i direkt referans almıyor.
            //referans gösteremiyoruz çünki circleDependency hatası yiyoruz. ya dbAccessLayer dışarı alınacak ya da başka bir şey
        }
    }
}