using System.Collections.Generic;
using Assets.Scripts.ColumnDefinitions;
using Assets.Scripts.Loader;
using Assets.Scripts.Table;

namespace Assets.Scripts.Registry
{
    abstract class ItemRegistry<TItem, TIndex> : IStuffLoader
        where TItem : RegistryItem
    {
        public string TableName { get; private set; }

        protected List<IColumnDefinition> columnDefinitions = new List<IColumnDefinition>();

        protected Dictionary<TIndex, TItem> itemsByIDs = new Dictionary<TIndex, TItem>();
        protected Dictionary<TItem, TIndex> idsByItems = new Dictionary<TItem, TIndex>();

        private List<PostLoadData<TItem>> loadCache = null;

        public ItemRegistry(string tableName)
        {
            this.TableName = tableName;
        }

        public virtual void Init()
        {
            TheOnlyLoader.Instance.RegisterSaveLoader(this);
        }

        protected void AddItem(TIndex id, TItem item)
        {
            this.itemsByIDs.Add(id, item);
            this.idsByItems.Add(item, id);
        }

        public void Load(string filesDirectory)
        {
            // initialize CSV reader/writer
            CSVTable table = new CSVTable(filesDirectory, this.TableName, this.columnDefinitions);

            // initialize list for items without ids specified inside the file
            List<TItem> addLaterItems = new List<TItem>();

            // initialize postload cache
            this.loadCache = new List<PostLoadData<TItem>>();

            // read the CSV
            List<CSVTableRow> rows = table.Load();

            // get index column
            ColumnDefinition<TIndex> indexColumn = this.GetIndexColumn();

            foreach (CSVTableRow row in rows)
            {
                TItem item = this.LoadItem(row);
                TIndex id = row.Read(indexColumn);

                // add to postload cache
                this.loadCache.Add(new PostLoadData<TItem>(row, item));

                // id defined in file - add immediately
                this.AddItem(id, item);
            }
        }

        public void Save(string filesDirectory)
        {
            // initialize CSV reader/writer
            CSVTable table = new CSVTable(filesDirectory, this.TableName, this.columnDefinitions);

            // initialize list to save the serialized records into
            List<CSVTableRow> rows = new List<CSVTableRow>();

            // get index column
            ColumnDefinition<TIndex> indexColumn = this.GetIndexColumn();

            foreach (KeyValuePair<TIndex, TItem> pair in this.itemsByIDs)
            {
                CSVTableRow row = table.CreateNewRowObject();
                row.Write(indexColumn, pair.Key);
                this.SaveItem(pair.Value, row);
                rows.Add(row);
            }

            table.Save(rows);
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

        protected abstract ColumnDefinition<TIndex> GetIndexColumn();
    }
}
