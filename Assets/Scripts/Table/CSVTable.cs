using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.ColumnDefinitions;

namespace Assets.Scripts.Table
{
    class CSVTable : ITable<CSVTableRow>
    {
        public string FileDirectory { get; set; }
        public string TableName { get; set; }

        private string filePath;
        private List<IColumnDefinition> columnDefinitions;

        public CSVTable(string fileDirectory, string tableName, List<IColumnDefinition> columnDefinitions)
        {
            this.FileDirectory = fileDirectory;
            this.TableName = tableName;
            this.columnDefinitions = new List<IColumnDefinition>(columnDefinitions);
            this.filePath = Path.Combine(fileDirectory, tableName + ".csv");
        }

        public CSVTableRow CreateNewRowObject()
        {
            return new CSVTableRow(this.columnDefinitions);
        }

        public List<CSVTableRow> Load()
        {
            if (!File.Exists(this.filePath))
            {
                return new List<CSVTableRow>();
            }

            List<CSVTableRow> csvLines = new List<CSVTableRow>();

            using (StreamReader reader = new StreamReader(this.filePath))
            {
                this.TableName = reader.ReadLine();

                List<IColumnDefinition> fileColumnDefinitions = ParseColumnDefinitions(reader.ReadLine(), this.columnDefinitions);

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    csvLines.Add(new CSVTableRow(line, fileColumnDefinitions));
                }
            }
            return csvLines;
        }

        public Dictionary<T, CSVTableRow> Load<T>(ColumnDefinition<T> indexColumn)
        {
            if (!File.Exists(this.filePath))
            {
                return new Dictionary<T, CSVTableRow>();
            }

            Dictionary<T, CSVTableRow> csvLines = new Dictionary<T, CSVTableRow>();

            using (StreamReader reader = new StreamReader(this.filePath))
            {
                this.TableName = reader.ReadLine();

                List<IColumnDefinition> fileColumnDefinitions = ParseColumnDefinitions(reader.ReadLine(), this.columnDefinitions);

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    CSVTableRow csvTableLine = new CSVTableRow(line, fileColumnDefinitions);
                    csvLines.Add(csvTableLine.Read(indexColumn), csvTableLine);
                }
            }

            return csvLines;
        }

        public void Save(List<CSVTableRow> sourceData)
        {
            using (StreamWriter writer = new StreamWriter(this.filePath, false))
            {
                writer.WriteLine(this.TableName);

                for (int i = 0; i < this.columnDefinitions.Count; i++)
                {
                    IColumnDefinition columnDefinition = this.columnDefinitions[i];
                    writer.Write(columnDefinition.ColumnName + " (" + columnDefinition.TypeName + ")");
                    if (i != this.columnDefinitions.Count - 1)
                    {
                        writer.Write(",");
                    }
                }

                writer.WriteLine();

                foreach (CSVTableRow csvTableLine in sourceData)
                {
                    writer.WriteLine(csvTableLine.GetLineString());
                }
            }
        }

        private static List<IColumnDefinition> ParseColumnDefinitions(string sourceString, List<IColumnDefinition> availableColumnDefinitions)
        {
            List<IColumnDefinition> fileColumnDefinitions = new List<IColumnDefinition>();

            string[] columnTitles = sourceString.Split(',');
            foreach (string columnTitle in columnTitles)
            {
                int bracketIndex = columnTitle.IndexOf('(');
                string columnName = columnTitle.Substring(0, bracketIndex).TrimEnd();
                string columnType = columnTitle.Substring(bracketIndex + 1).Replace(")", string.Empty).Trim();
                IColumnDefinition columnDefinition;
                if ((columnDefinition = availableColumnDefinitions.FirstOrDefault(colDef => colDef.ColumnName.Equals(columnName, StringComparison.Ordinal))) != null)
                {
                    fileColumnDefinitions.Add(columnDefinition);
                }
            }

            return fileColumnDefinitions;
        }
    }
}
