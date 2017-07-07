using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Loader;
using Assets.Scripts.Table;
using Assets.Scripts.ColumnDefinitions;

namespace Assets.Scripts.Registry
{
    abstract class IntIDItemRegistry<T> : ItemRegistry<T, int>, ISaveLoader
        where T : IntIdentifiedRegistryItem
    {
        protected static readonly IntegerColumnDefinition IDColumn = new IntegerColumnDefinition("ID");

        int lastID = 0;

        public IntIDItemRegistry(string tableName) : base(tableName)
        {
            this.columnDefinitions.Add(IDColumn);
        }

        protected override T Register(T item)
        {
            this.AddItem(++this.lastID, item);
            return item;
        }

        public void Load(string filesDirectory)
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
                int id = item.ID;

                // add to postload cache
                this.loadCache.Add(new PostLoadData<T>(row, item));

                this.AddItem(id, item);

                if (this.lastID < id)
                {
                    this.lastID = id;
                }
            }
        }

        public void Save(string filesDirectory)
        {
            // initialize CSV reader/writer
            CSVTable table = new CSVTable(filesDirectory, this.TableName, this.columnDefinitions);

            // initialize list to save the serialized records into
            List<CSVTableRow> rows = new List<CSVTableRow>();

            foreach (KeyValuePair<int, T> pair in this.itemsByIDs)
            {
                CSVTableRow row = table.CreateNewRowObject();
                this.SaveItem(pair.Value, row);
                rows.Add(row);
            }

            table.Save(rows);
        }
    }
}
