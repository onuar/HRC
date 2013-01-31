using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRC.Foundation.ConvertionLibrary;
using HRC.Foundation.LogLibrary;
using HRC.Library.EntityLibrary.EntityBase;
using HRC.Library.EntityLibrary.EntityOperations;
using HRC.Library.EntityLibrary.EntitySchemaOperations;

namespace HRC.Library.ContextFoundation.Aspects.BusinessAspects
{
    public class ChangedEntityValueLogAttribute : BusinessAspectBase
    {
        [WorksBefore]
        public int LogChangedValue(AspectContext context)
        {
            BaseEntity entity = (BaseEntity)context.Method.Args[0];
            Dictionary<string, EntityColumn> changedColumns = entity.GetChangedColumns();
            if (changedColumns.Count == 0)
                return 0;

            LogContext logContext = new LogContext();

            logContext.EntityName = entity.EntityName;
            logContext.TableName = entity.TableName;

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