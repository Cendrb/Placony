using Assets.Scripts.ColumnDefinitions;

namespace Assets.Scripts.Table
{
    interface ITableRowDataWriter
    {
        string DomainName { get; }
        void Write<T>(ColumnDefinition<T> column, T value);
    }
}
