using Assets.Scripts.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Loader
{
    class PostLoadData<T>
    {
        public readonly ITableRowDataReader Source;
        public readonly T LoadedItem;

        public PostLoadData(ITableRowDataReader source, T loadedItem)
        {
            this.Source = source;
            this.LoadedItem = loadedItem;
        }
    }
}
