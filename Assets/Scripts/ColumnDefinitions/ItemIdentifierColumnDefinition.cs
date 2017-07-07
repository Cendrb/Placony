using Assets.Scripts.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ColumnDefinitions
{
    class ItemIdentifierColumnDefinition : ColumnDefinition<ItemIdentifier>
    {
        public ItemIdentifierColumnDefinition(string columnName) : base("Identifier string", columnName)
        {
        }

        public override ItemIdentifier Deserialize(string sourceData)
        {
            return ItemIdentifier.Parse(sourceData);
        }

        public override string Serialize(ItemIdentifier sourceData)
        {
            return sourceData.IdentifierString;
        }
    }
}
