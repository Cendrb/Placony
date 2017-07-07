using Assets.Scripts.ColumnDefinitions;

namespace Assets.Scripts.Table
{
    interface ITableRowDataReader
    {
        string DomainName { get; }
        T Read<T>(ColumnDefinition<T> column);
    }
}
