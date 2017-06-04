using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ColumnDefinitions
{
    interface IColumnDefinition
    {
        string ColumnName { get; }
        string TypeName { get; }
    }
}
