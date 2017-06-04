namespace Assets.Scripts.Registry
{
    abstract class GameDefinedRegistryItem : RegistryItem
    {
        public readonly ItemIdentifier Identifier;

        protected GameDefinedRegistryItem(ItemIdentifier identifier)
        {
            this.Identifier = identifier;
        }
    }
}
