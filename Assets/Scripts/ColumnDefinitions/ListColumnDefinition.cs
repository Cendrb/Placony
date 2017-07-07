using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ColumnDefinitions
{
    class ListColumnDefinition<T> : ColumnDefinition<List<T>>
    {
        private readonly ColumnDefinition<T> itemColumnDefinition;

        public ListColumnDefinition(ColumnDefinition<T> itemColumnDefinition, string columnName)
            : base("Semicolon list of " + itemColumnDefinition.TypeName, columnName)
        {
            this.itemColumnDefinition = itemColumnDefinition;
        }

        public override List<T> Deserialize(string sourceData)
        {
            if (string.IsNullOrEmpty(sourceData))
            {
                return new List<T>();
            }

            string[] stringValues = sourceData.Split(';');
            List<T> parsedValues = new List<T>();
            foreach (string stringValue in stringValues)
            {
                parsedValues.Add(this.itemColumnDefinition.Deserialize(stringValue));
            }

            return parsedValues;
        }

        public override string Serialize(List<T> sourceData)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < sourceData.Count; i++)
            {
                stringBuilder.Append(this.itemColumnDefinition.Serialize(sourceData[i]));
                if (i < sourceData.Count - 1)
                {
                    stringBuilder.Append(';');
                }
            }

            return stringBuilder.ToString();
        }
    }
}
