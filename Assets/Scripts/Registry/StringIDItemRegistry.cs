using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Loader;
using Assets.Scripts.Table;
using Assets.Scripts.ColumnDefinitions;

namespace Assets.Scripts.Registry
{
    abstract class StringIDItemRegistry<T> : ItemRegistry<T, string>, IGameLoader
        where T : StringIdentifiedRegistryItem
    {
        protected static readonly StringColumnDefinition IdentifierColumn = new StringColumnDefinition("Identifier");

        public StringIDItemRegistry(string tableName) : base(tableName)
        {
            this.columnDefinitions.Add(IdentifierColumn);
        }

        protected override T Register(T item)
        {
            this.AddItem(item.InnerName, item);
            return item;
        }

        public void Load(string filesDirectory, Domain domain)
        {
            // initialize CSV reader/writer
            CSVTable table = new CSVTable(filesDirectory, this.TableName, this.columnDefinitions);

            // initialize postload cache
            this.loadCache = new List<PostLoadData<T>>();

            // read the CSV
            List<CSVTableRow> rows = table.Load();

            foreach (CSVTableRow row in rows)
            {
                T item = this.LoadItem(row);
                string id = ItemIdentifier.CreateIdentifierString(domain, this.TableName, item.InnerName);

                // add to postload cache
                this.loadCache.Add(new PostLoadData<T>(row, item));

                this.AddItem(id, item);
            }
        }

        public void Save(string filesDirectory)
        {
            // initialize CSV reader/writer
            CSVTable table = new CSVTable(filesDirectory, this.TableName, this.columnDefinitions);

            // initialize list to save the serialized records into
            List<CSVTableRow> rows = new List<CSVTableRow>();

            foreach (KeyValuePair<string, T> pair in this.itemsByIDs)
            {
                CSVTableRow row = table.CreateNewRowObject();
                this.SaveItem(pair.Value, row);
                rows.Add(row);
            }

            table.Save(rows);
        }

        public T FindItem(ItemIdentifier identifier)
        {
            T item;
            if (this.itemsByIDs.TryGetValue(identifier.IdentifierString, out item))
            {
                return item;
            }
            else
            {
                return null;
            }
        }

        StringIdentifiedRegistryItem IGameLoader.FindItem(ItemIdentifier identifier)
        {
            return this.FindItem(identifier);
        }
    }
}
