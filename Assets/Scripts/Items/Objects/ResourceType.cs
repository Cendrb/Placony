using System;
using Assets.Scripts.Items.Interfaces;
using Assets.Scripts.Loader;
using Assets.Scripts.Registry;
using Assets.Scripts.Table;

namespace Assets.Scripts.Items.Objects
{
    class ResourceType : StringIdentifiedRegistryItem, IResourceTypeFilter
    {
        public static readonly ThisRegistry Registry = new ThisRegistry();

        private ResourceType(Domain domain, string innerName)
            : base(domain, innerName)
        {
            
        }

        public override IItemRegistry Registry
        {
            get
            {
                return Re
            }
        }

        public bool Is(ResourceType resourceType)
        {
            return this == resourceType;
        }

        public class ThisRegistry : StringIDItemRegistry<ResourceType>
        {
            public ThisRegistry() 
                : base("ResourceTypes")
            {
            }

            public ResourceType CreateNew(Domain domain, string identifier)
            {
                return this.Register(new ResourceType(domain, identifier)
                {
                });
            }

            protected override ResourceType LoadItem(ITableRowDataReader reader)
            {
                return new ResourceType(Domain.FindByName(reader.DomainName), reader.Read(IdentifierColumn))
                {
                };
            }

            protected override void LoadReferencesForItem(PostLoadData<ResourceType> row)
            {
            }

            protected override void SaveItem(ResourceType item, ITableRowDataWriter writer)
            {
                writer.Write(IdentifierColumn, item.InnerName);
            }
        }
    }
}
