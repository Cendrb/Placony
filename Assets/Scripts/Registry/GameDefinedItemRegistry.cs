using System.Collections.Generic;
using Assets.Scripts.ColumnDefinitions;
using Assets.Scripts.Loader;
using Assets.Scripts.Table;

namespace Assets.Scripts.Registry
{
    abstract class GameDefinedItemRegistry<T> : ItemRegistry<T>, IGameDefinedStuffLoader
             where T : GameDefinedRegistryItem
    {
        protected static readonly StringColumnDefinition IdentifierColumn = new StringColumnDefinition("Identifier");

        public GameDefinedItemRegistry(string tableName)
            : base(tableName)
        {
            this.columnDefinitions.Add(IdentifierColumn);
        }

        public override void Init()
        {
            base.Init();
            TheOnlyLoader.Instance.RegisterGameDefinedLoader(this);
        }

        public void ReplaceIDsFromFile(string filesDirectory)
        {
            CSVTable table = new CSVTable(filesDirectory, this.TableName, new List<IColumnDefinition>() { IDColumn, IdentifierColumn });
            Dictionary<int, int> newIDs = new Dictionary<int, int>();
            Dictionary<string, CSVTableRow> rows = table.Load(IdentifierColumn);
            foreach (KeyValuePair<int, T> pair in this.itemsByIDs)
            {
                CSVTableRow row;
                if (rows.TryGetValue(pair.Value.Identifier.IdentifierString, out row))
                {
                    int newID = row.Read(IDColumn);
                    newIDs.Add(pair.Key, newID);

                    if (newID > this.lastID)
                    {
                        this.lastID = newID;
                    }
                }
            }

            List<T> itemsToReAdd = new List<T>();
            foreach (KeyValuePair<int, int> pair in newIDs)
            {
                T existingItemWithNewID;
                if (this.itemsByIDs.TryGetValue(pair.Value, out existingItemWithNewID))
                {
                    itemsToReAdd.Add(existingItemWithNewID);
                    this.itemsByIDs.Remove(pair.Value);
                    this.idsByItems.Remove(existingItemWithNewID);
                }

                T existingItemWithOldID = this.itemsByIDs[pair.Key];
                this.itemsByIDs.Remove(pair.Key);
                this.idsByItems.Remove(existingItemWithOldID);

                this.AddItem(pair.Value, existingItemWithOldID);
            }

            foreach (T item in itemsToReAdd)
            {
                this.lastID++;
                this.AddItem(this.lastID, item);
            }
        }

        public void SaveIDsToFile(string saveDirectory)
        {
            CSVTable table = new CSVTable(saveDirectory, this.TableName, new List<IColumnDefinition>() { IDColumn, IdentifierColumn });
            List<CSVTableRow> rows = new List<CSVTableRow>();
            foreach(KeyValuePair<int, T> pair in this.itemsByIDs)
            {
                CSVTableRow row = table.CreateNewRowObject();
                row.Write(IDColumn, pair.Key);
                row.Write(IdentifierColumn, pair.Value.Identifier.IdentifierString);
                rows.Add(row);
            }

            table.Save(rows);
        }
    }
}
