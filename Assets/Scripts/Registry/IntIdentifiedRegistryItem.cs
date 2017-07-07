namespace Assets.Scripts.Registry
{
    abstract class IntIdentifiedRegistryItem : RegistryItem
    {
        public readonly int ID;

        protected IntIdentifiedRegistryItem(int id)
        {
            this.ID = id;
        }
    }
}
