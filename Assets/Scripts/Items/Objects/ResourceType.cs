using System;
using Assets.Scripts.Items.Interfaces;
using Assets.Scripts.Loader;
using Assets.Scripts.Registry;
using Assets.Scripts.Table;

namespace Assets.Scripts.Items.Objects
{
    class ResourceType : GameDefinedRegistryItem, IResourceTypeFilter
    {
        public static readonly ThisRegistry Registry = new ThisRegistry();

        private ResourceType(ItemIdentifier identifier)
            : base(identifier)
        {
            
        }

        public bool Is(ResourceType resourceType)
        {
            return this == resourceType;
        }

        public class ThisRegistry : GameDefinedItemRegistry<ResourceType>
        {
            public ThisRegistry() 
                : base("ResourceTypes")
            {
            }

            public ResourceType CreateNew(ItemIdentifier identifier)
            {
                return this.RegisterItem(new ResourceType(identifier)
                {
                });
            }

            protected override ResourceType LoadItem(ITableRowDataReader reader)
            {
                return new ResourceType(ItemIdentifier.Parse(reader.Read(IdentifierColumn)))
                {
                };
            }

            protected override void LoadReferencesForItem(PostLoadData<ResourceType> row)
            {
            }

            protected override void SaveItem(ResourceType item, ITableRowDataWriter writer)
            {
                writer.Write(IdentifierColumn, item.Identifier.IdentifierString);
            }
        }
    }
}
