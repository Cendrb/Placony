using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ColumnDefinitions
{
    class BoolColumnDefinition : ColumnDefinition<bool>
    {
        public BoolColumnDefinition(string typeName, string columnName) : base(typeName, columnName)
        {
        }

        public override bool Deserialize(string sourceData)
        {
            if (sourceData.Equals("true", StringComparison.Ordinal))
            {
                return true;
            }
            else if (sourceData.Equals("false", StringComparison.Ordinal))
            {
                return false;
            }
            else
            {
                throw new ColumnParsingException(this, sourceData);
            }
        }

        public override string Serialize(bool sourceData)
        {
            return sourceData.ToString().ToLowerInvariant();
        }
    }
}
