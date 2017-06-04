using System;
using System.Linq;
using Assets.Scripts.Items.Interfaces;
using Assets.Scripts.Loader;
using Assets.Scripts.Registry;
using Assets.Scripts.Table;
using Assets.Scripts.Util;

namespace Assets.Scripts.Items.Objects
{
    class ResourceTypeGroup : GameDefinedRegistryItem, IResourceTypeFilter
    {
        public static readonly ThisRegistry Registry = new ThisRegistry();

        public ImmutableCollection<ResourceType> ResourceTypes { get; private set; }

        public ResourceTypeGroup(ItemIdentifier identifier) : base(identifier)
        {
        }

        public bool Contains(ResourceType resourceType)
        {
            return this.ResourceTypes.Contains(resourceType);
        }

        public bool Is(ResourceType resourceType)
        {
            return this.Contains(resourceType);
        }

        public class ThisRegistry : GameDefinedItemRegistry<ResourceTypeGroup>
        {
            public ThisRegistry() 
                : base("ResourceTypeGroups")
            {
            }

            public ResourceTypeGroup CreateNew(ItemIdentifier identifier, string name)
            {
                return this.RegisterItem(new ResourceTypeGroup(identifier)
                {
                });
            }

            protected override ResourceTypeGroup LoadItem(ITableRowDataReader reader)
            {
                return new ResourceTypeGroup(ItemIdentifier.Parse(reader.Read(IdentifierColumn)))
                {
                };
            }

            protected override void LoadReferencesForItem(PostLoadData<ResourceTypeGroup> row)
            {
                throw new NotImplementedException();
            }

            protected override void SaveItem(ResourceTypeGroup item, ITableRowDataWriter writer)
            {
                writer.Write(IdentifierColumn, item.Identifier.IdentifierString);
            }
        }
    }
}
