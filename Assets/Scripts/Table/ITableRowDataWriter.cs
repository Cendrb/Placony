using Assets.Scripts.ColumnDefinitions;

namespace Assets.Scripts.Table
{
    interface ITableRowDataWriter
    {
        void Write<T>(ColumnDefinition<T> column, T value);
    }
}
