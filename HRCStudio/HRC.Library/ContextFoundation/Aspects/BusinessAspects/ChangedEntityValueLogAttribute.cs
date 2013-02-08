using System;
using System.Collections.Generic;
using HRC.Foundation.LogLibrary;
using HRC.Library.DatabaseObject.EntityLibrary.EntityBase;

namespace HRC.Library.ContextFoundation.Aspects.BusinessAspects
{
    public class ChangedEntityValueLogAttribute : BusinessAspectBase
    {
        [WorksBefore]
        public int LogChangedValue(AspectContext context)
        {
            var entity = (BaseEntity)context.Method.Args[0];
            Dictionary<string, EntityColumn> changedColumns = entity.GetChangedColumns();
            if (changedColumns.Count == 0)
                return 0;

            var logContext = new LogContext {EntityName = entity.EntityName, TableName = entity.TableName};

            foreach (var c in changedColumns)
            {
                logContext.Values.Add(c.Key, new ChangedEntityColumn(c.Value.OldValue, c.Value.CurrentValue));
            }

            try
            {
                LogManager.Instance.Write(logContext, true);
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return changedColumns.Count;
        }

    }
}