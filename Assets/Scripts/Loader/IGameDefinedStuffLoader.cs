namespace Assets.Scripts.Loader
{
    public interface IGameDefinedStuffLoader : IStuffLoader
    {
        void ReplaceIDsFromFile(string filesDirectory);
        void SaveIDsToFile(string saveDirectory);
    }
}