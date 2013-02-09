using System;
using System.Collections.Generic;
using HRC.Foundation.ConvertionLibrary;
using HRC.Foundation.LogLibrary;
using HRC.Library.DatabaseObject.DatabaseSchema.Operations;
using HRC.Library.DatabaseObject.EntityLibrary.EntityBase;

namespace HRC.Library.ContextFoundation.LoggerPlugins
{
    public class SqlLogger : LoggerBase
    {
        protected override void WriteInternal(LogContext context)
        {
            var logEntities = new List<BaseEntity>();
            foreach (var c in context.Values)
            {
                BaseEntity logEntity = new BaseEntity() { EntityName = "DataLog" };
                var ec = (ChangedEntityColumn)c.Value;
                logEntity.SetValue<DateTime>("ModifiedDate", DateTime.Now);
                logEntity.SetValue<string>("EntityName", context.EntityName);
                logEntity.SetValue<string>("TableName", context.TableName);
                logEntity.SetValue<string>("ColumnName", c.Key);
                logEntity.SetValue<string>("OldValue", ConvertionHelper.ConvertValue<string>(ec.OldValue));
                logEntity.SetValue<string>("NewValue", ConvertionHelper.ConvertValue<string>(ec.CurrentValue));
                logEntities.Add(logEntity);

                //todo@onuar: transaction
                DbObjectOperationManager.Instance.Insert(logEntity);
            }
        }
    }
}