using System;
using System.Linq;
using Assets.Scripts.Items.Interfaces;
using Assets.Scripts.Loader;
using Assets.Scripts.Registry;
using Assets.Scripts.Table;
using Assets.Scripts.Util;
using Assets.Scripts.ColumnDefinitions;
using System.Collections.Generic;

namespace Assets.Scripts.Items.Objects
{
    class ResourceTypeGroup : StringIdentifiedRegistryItem, IResourceTypeFilter
    {
        public static readonly ThisRegistry Registry = new ThisRegistry();

        public List<ResourceType> ResourceTypes { get; private set; }

        public ResourceTypeGroup(Domain domain, string innerName) : base(domain, innerName)
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

        public class ThisRegistry : StringIDItemRegistry<ResourceTypeGroup>
        {
            protected static readonly ListColumnDefinition<ItemIdentifier> ResourcesColumn = new ListColumnDefinition<ItemIdentifier>(
                new ItemIdentifierColumnDefinition("IncludedResource"),
                "IncludedResources");

            public ThisRegistry()
                : base("ResourceTypeGroups")
            {
            }

            public ResourceTypeGroup CreateNew(Domain domain, string innerName)
            {
                return this.Register(new ResourceTypeGroup(domain, innerName)
                {
                });
            }

            protected override ResourceTypeGroup LoadItem(ITableRowDataReader reader)
            {
                return new ResourceTypeGroup(Domain.FindByName(reader.DomainName), reader.Read(IdentifierColumn))
                {
                };
            }

            protected override void LoadReferencesForItem(PostLoadData<ResourceTypeGroup> row)
            {
                List<ItemIdentifier> identifiers = row.Source.Read(ResourcesColumn);
                foreach (ItemIdentifier identifier in identifiers)
                {
                    row.LoadedItem.ResourceTypes.Add((ResourceType)TheOnlyLoader.Instance.FindItem(identifier));
                }
            }

            protected override void SaveItem(ResourceTypeGroup item, ITableRowDataWriter writer)
            {
                writer.Write(IdentifierColumn, item.InnerName);
                writer.Write<List<ResourceType>>(ResourcesColumn, item.ResourceTypes);
            }
        }
    }
}
