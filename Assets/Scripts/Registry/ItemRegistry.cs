using System.Collections.Generic;
using Assets.Scripts.ColumnDefinitions;
using Assets.Scripts.Loader;
using Assets.Scripts.Table;

namespace Assets.Scripts.Registry
{
    abstract class ItemRegistry<TItem, TIndex>
        where TItem : RegistryItem
    {
        public string TableName { get; private set; }

        protected List<IColumnDefinition> columnDefinitions = new List<IColumnDefinition>();

        protected Dictionary<TIndex, TItem> itemsByIDs = new Dictionary<TIndex, TItem>();
        protected Dictionary<TItem, TIndex> idsByItems = new Dictionary<TItem, TIndex>();

        protected List<PostLoadData<TItem>> loadCache = null;

        public ItemRegistry(string tableName)
        {
            this.TableName = tableName;
        }

        protected void AddItem(TIndex id, TItem item)
        {
            this.itemsByIDs.Add(id, item);
            this.idsByItems.Add(item, id);
        }

        public TItem GetItem(TIndex id)
        {
            return this.itemsByIDs[id];
        }

        public TIndex GetID(TItem item)
        {
            return this.idsByItems[item];
        }

        public void PostLoad()
        {
            if (this.loadCache == null)
            {
                throw new System.InvalidOperationException("PostLoad needs to be called after Load");
            }

            foreach (PostLoadData<TItem> row in this.loadCache)
            {
                this.LoadReferencesForItem(row);
            }

            // delete the cache
            this.loadCache = null;
        }

        protected abstract TItem LoadItem(ITableRowDataReader reader);

        protected abstract void SaveItem(TItem item, ITableRowDataWriter writer);

        protected abstract void LoadReferencesForItem(PostLoadData<TItem> row);

        protected abstract TItem Register(TItem item);
    }
}
