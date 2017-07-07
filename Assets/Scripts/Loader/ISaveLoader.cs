namespace Assets.Scripts.Loader
{
    public interface ISaveLoader
    {
        void Load(string filesDirectory);
        void PostLoad();
        void Save(string filesDirectory);
    }
}