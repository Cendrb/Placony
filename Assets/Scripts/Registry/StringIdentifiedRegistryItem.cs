namespace Assets.Scripts.Registry
{
    public abstract class StringIdentifiedRegistryItem : RegistryItem
    {
        public readonly Domain Domain;
        public readonly string InnerName;

        protected StringIdentifiedRegistryItem(Domain domain, string innerName)
        {
            this.Domain = domain;
            this.InnerName = innerName;
        }
    }
}
