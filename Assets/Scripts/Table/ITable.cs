using System.Collections.Generic;
using Assets.Scripts.ColumnDefinitions;

namespace Assets.Scripts.Table
{
    interface ITable<TRow>
        where TRow : ITableRow
    {
        TRow CreateNewRowObject();

        List<TRow> Load();

        Dictionary<T, TRow> Load<T>(ColumnDefinition<T> indexColumn);

        void Save(List<TRow> sourceData);
    }
}
