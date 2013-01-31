using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRC.Foundation.ConvertionLibrary;

namespace HRC.Library.EntityLibrary.EntityBase
{
    public class BaseEntity
    {
        public BaseEntity()
        {

        }

        public BaseEntity(string name)
        {
            EntityName = name;
            TableName = name;
        }

        public string EntityName { get; set; }
        public string TableName { get; set; }



        Dictionary<string, EntityColumn> _columns = new Dictionary<string, EntityColumn>();
        public void SetValue<T>(string columnName, T a)
        {
            if (_columns.ContainsKey(columnName))
            {
                EntityColumn column = _columns[columnName];
                column.OldValue = column.CurrentValue;
                column.CurrentValue = a;
                if (!column.OldValue.Equals(column.CurrentValue))
                    column.IsChanged = true;
            }
            else
            {
                EntityColumn cv = new EntityColumn();
                cv.CurrentValue = a;
                _columns.Add(columnName, cv);
            }
        }

        public T GetValue<T>(string columnName)
        {
            if (_columns.ContainsKey(columnName))
            {
                if (_columns[columnName].CurrentValue != null && _columns[columnName].CurrentValue != DBNull.Value)
                    return ConvertionHelper.ConvertValue<T>(_columns[columnName].CurrentValue);
            }
            return default(T);
        }

        internal Dictionary<string, EntityColumn> GetChangedColumns()
        {
            Dictionary<string, EntityColumn> columns = new Dictionary<string, EntityColumn>();
            foreach (var col in _columns)
            {
                if (col.Value.IsChanged)
                    columns.Add(col.Key, col.Value);
            }

            return columns;
        }
    }
}
