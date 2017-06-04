using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ColumnDefinitions
{
    abstract class ColumnDefinition<T> : IColumnDefinition
    {
        public string TypeName { get; private set; }
        public string ColumnName { get; private set; }

        public ColumnDefinition(string typeName, string columnName)
        {
            this.TypeName = typeName;
            this.ColumnName = columnName;
        }

        public abstract string Serialize(T sourceData);

        public abstract T Deserialize(string sourceData);
    }
}
