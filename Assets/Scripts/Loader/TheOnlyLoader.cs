using System.Collections.Generic;
using Assets.Scripts.Items.Objects;
using Assets.Scripts.Registry;

namespace Assets.Scripts.Loader
{
    public partial class TheOnlyLoader
    {
        public static readonly TheOnlyLoader Instance = new TheOnlyLoader();

        private List<IStuffLoader> saveLoaders = new List<IStuffLoader>();
        private List<IGameDefinedStuffLoader> gameDefinedLoaders = new List<IGameDefinedStuffLoader>();

        private TheOnlyLoader()
        {
            
        }

        public void DoDumbStuff()
        {
            ResourceType.Registry.CreateNew(new ItemIdentifier(Domain.Vanilla, "IronOre"));
        }

        public void LoadGame(string filesDirectory)
        {
            foreach (IGameDefinedStuffLoader loader in this.gameDefinedLoaders)
            {
                loader.Load(filesDirectory);
            }

            foreach (IGameDefinedStuffLoader loader in this.gameDefinedLoaders)
            {
                loader.PostLoad();
            }
        }

        public void LoadSave(string saveDirectory)
        {
            foreach(IStuffLoader loader in this.saveLoaders)
            {
                if(loader is IGameDefinedStuffLoader)
                {
                    // only replace ids
                    (loader as IGameDefinedStuffLoader).ReplaceIDsFromFile(saveDirectory);
                }
                else
                {
                    // load all data
                    loader.Load(saveDirectory);
                }
            }

            foreach (IStuffLoader loader in this.saveLoaders)
            {
                if(!(loader is IGameDefinedStuffLoader))
                {
                    loader.PostLoad();
                }
            }
        }

        public void SaveGame(string filesDirectory)
        {
            foreach (IGameDefinedStuffLoader loader in this.gameDefinedLoaders)
            {
                loader.Save(filesDirectory);
            }
        }

        public void SaveSave(string saveDirectory)
        {
            foreach (IStuffLoader loader in this.saveLoaders)
            {
                if (loader is IGameDefinedStuffLoader)
                {
                    // only replace ids
                    (loader as IGameDefinedStuffLoader).SaveIDsToFile(saveDirectory);
                }
                else
                {
                    // load all data
                    loader.Save(saveDirectory);
                }
            }
        }

        public void RegisterGameDefinedLoader(IGameDefinedStuffLoader loader)
        {
            this.gameDefinedLoaders.Add(loader);
        }

        public void RegisterSaveLoader(IStuffLoader loader)
        {
            this.saveLoaders.Add(loader);
        }
    }
}
