namespace Assets.Scripts.Loader
{
    public interface IStuffLoader
    {
        void Load(string filesDirectory);
        void PostLoad();
        void Save(string filesDirectory);
    }
}