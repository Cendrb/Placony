using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Loader;
using Assets.Scripts.Table;
using Assets.Scripts.ColumnDefinitions;

namespace Assets.Scripts.Registry
{
    abstract class ItemIdentifierItemRegistry<T> : ItemRegistry<T, string>
        where T : RegistryItem
    {
        protected static readonly StringColumnDefinition IdentifierColumn = new StringColumnDefinition("Identifier");

        public ItemIdentifierItemRegistry(string tableName) : base(tableName)
        {
            this.columnDefinitions.Add(IdentifierColumn);
        }

        protected override ColumnDefinition<string> GetIndexColumn()
        {
            return IdentifierColumn;
        }
    }
}
