using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Loader;
using Assets.Scripts.Table;
using Assets.Scripts.ColumnDefinitions;

namespace Assets.Scripts.Registry
{
    abstract class IntIDItemRegistry<T> : ItemRegistry<T, int>
        where T : RegistryItem
    {
        protected static readonly IntegerColumnDefinition IDColumn = new IntegerColumnDefinition("ID");

        public IntIDItemRegistry(string tableName) : base(tableName)
        {
            this.columnDefinitions.Add(IDColumn);
        }

        protected override ColumnDefinition<int> GetIndexColumn()
        {
            return IDColumn;
        }
    }
}
