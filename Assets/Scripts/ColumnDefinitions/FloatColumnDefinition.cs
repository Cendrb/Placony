using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ColumnDefinitions
{
    class FloatColumnDefinition : ColumnDefinition<float>
    {
        public FloatColumnDefinition(string columnName) : base("Float", columnName)
        {
        }

        public override float Deserialize(string sourceData)
        {
            float result;
            if (float.TryParse(sourceData, out result))
            {
                return result;
            }
            else
            {
                throw new ColumnParsingException(this, sourceData);
            }
        }

        public override string Serialize(float sourceData)
        {
            return sourceData.ToString();
        }
    }
}
