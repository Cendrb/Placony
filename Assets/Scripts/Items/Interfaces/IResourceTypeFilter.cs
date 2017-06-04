using Assets.Scripts.Items.Objects;

namespace Assets.Scripts.Items.Interfaces
{
    interface IResourceTypeFilter
    {
        bool Is(ResourceType resourceType);
    }
}
