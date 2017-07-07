using System;
using System.Collections.Generic;
using Assets.Scripts.ColumnDefinitions;

namespace Assets.Scripts.Table
{
    class CSVTableRow : ITableRow
    {
        private string[] columnStrings;
        private List<IColumnDefinition> columnsInOrder;

        public string DomainName { get; private set; }

        public CSVTableRow(List<IColumnDefinition> columnsInOrder, string domainName)
        {
            this.columnsInOrder = columnsInOrder;
            this.columnStrings = new string[columnsInOrder.Count];
            this.DomainName = domainName;
        }

        public CSVTableRow(string lineText, List<IColumnDefinition> columnsInOrder, string domainName)
        {
            this.columnsInOrder = columnsInOrder;
            this.columnStrings = lineText.Split(',');
            this.DomainName = domainName;
        }

        public T Read<T>(ColumnDefinition<T> column)
        {
            int columnIndex = this.columnsInOrder.IndexOf(column);
            if(columnIndex == -1)
            {
                // TODO Notify about missing column?
                return default(T);
            }
            else
            {
                return column.Deserialize(this.columnStrings[columnIndex]);
            }
        }

        public void Write<T>(ColumnDefinition<T> column, T value)
        {
            int columnIndex = this.columnsInOrder.IndexOf(column);
            if (columnIndex == -1)
            {
                // TODO Notify about missing column?
            }
            else
            {
                this.columnStrings[columnIndex] = column.Serialize(value);
            }
        }

        public string GetLineString()
        {
            return string.Join(",", this.columnStrings);
        }
    }
}
