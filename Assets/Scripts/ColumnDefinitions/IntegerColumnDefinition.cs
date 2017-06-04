using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ColumnDefinitions
{
    class IntegerColumnDefinition : ColumnDefinition<int>
    {
        public IntegerColumnDefinition(string columnName) : base("Integer", columnName)
        {
        }

        public override int Deserialize(string sourceData)
        {
            int result;
            if (int.TryParse(sourceData, out result))
            {
                return result;
            }
            else
            {
                throw new ColumnParsingException(this, sourceData);
            }
        }

        public override string Serialize(int sourceData)
        {
            return sourceData.ToString();
        }
    }
}
