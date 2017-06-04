using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ColumnDefinitions
{
    class ColumnParsingException : Exception
    {
        public IColumnDefinition ColumnDefinition { get; private set; }
        public string SuppliedValue { get; private set; }

        public ColumnParsingException(IColumnDefinition columnDefinition, string suppliedValue)
        {
            this.ColumnDefinition = columnDefinition;
            this.SuppliedValue = suppliedValue;
        }
    }
}
