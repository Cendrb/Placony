using Assets.Scripts.ColumnDefinitions;

namespace Assets.Scripts.Table
{
    interface ITableRowDataReader
    {
        T Read<T>(ColumnDefinition<T> column);
    }
}
