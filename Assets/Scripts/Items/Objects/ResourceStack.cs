using System;
using Assets.Scripts.Registry;
using Assets.Scripts.Table;
using Assets.Scripts.ColumnDefinitions;
using Assets.Scripts.Items.Interfaces;
using Assets.Scripts.Loader;

namespace Assets.Scripts.Items.Objects
{
    class ResourceStack : IntIdentifiedRegistryItem
    {
        public static readonly ThisRegistry Registry = new ThisRegistry();

        public int Quantity { get; private set; }
        public IResourceTypeFilter ResourceFilter { get; private set; }

        private ResourceStack(int id)
            : base(id)
        {

        }

        public class ThisRegistry : IntIDItemRegistry<ResourceStack>
        {
            protected static readonly IntegerColumnDefinition QuantityColumn = new IntegerColumnDefinition("Quantity");

            public ThisRegistry() 
                : base("ResourceStacks")
            {
            }

            protected override ResourceStack LoadItem(ITableRowDataReader reader)
            {
                return new ResourceStack(reader.Read(IDColumn))
                {
                    Quantity = reader.Read(QuantityColumn)
                };
            }

            protected override void LoadReferencesForItem(PostLoadData<ResourceStack> row)
            {
                throw new NotImplementedException();
            }

            protected override void SaveItem(ResourceStack item, ITableRowDataWriter writer)
            {
                writer.Write(IDColumn, item.ID);
                writer.Write(QuantityColumn, item.Quantity);
            }
        }
    }
}
