using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ColumnDefinitions
{
    class StringColumnDefinition : ColumnDefinition<string>
    {
        public StringColumnDefinition(string columnName) : base("String", columnName)
        {
        }

        public override string Deserialize(string sourceData)
        {
            return sourceData;
        }

        public override string Serialize(string sourceData)
        {
            return sourceData;
        }
    }
}
