using System.Collections.Generic;
using Assets.Scripts.Items.Objects;
using Assets.Scripts.Registry;

namespace Assets.Scripts.Loader
{
    public partial class TheOnlyLoader
    {
        public static readonly TheOnlyLoader Instance = new TheOnlyLoader();

        private List<IGameLoader> gameLoaders = new List<IGameLoader>();
        private List<ISaveLoader> saveLoaders = new List<ISaveLoader>();

        private TheOnlyLoader()
        {

        }

        public StringIdentifiedRegistryItem FindItem(ItemIdentifier identifier)
        {
            foreach (IGameLoader loader in this.gameLoaders)
            {
                if (loader.TableName == identifier.TableName)
                {
                    StringIdentifiedRegistryItem item = loader.FindItem(identifier);
                    if (item != null)
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        public string FindIdentifierString(StringIdentifiedRegistryItem item)
        {
            foreach (IGameLoader loader in this.gameLoaders)
            {
                if (loader.TableName == item)
                {
                    StringIdentifiedRegistryItem item = loader.FindItem(identifier);
                    if (item != null)
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        public void DoDumbStuff()
        {
            
        }

        public void LoadGame(string filesDirectory, Domain domain)
        {
            foreach (IGameLoader loader in this.gameLoaders)
            {
                loader.Load(filesDirectory, domain);
            }

            foreach (IGameLoader loader in this.gameLoaders)
            {
                loader.PostLoad();
            }
        }

        public void SaveGame(string filesDirectory)
        {
            foreach (IGameLoader loader in this.gameLoaders)
            {
                loader.Save(filesDirectory);
            }
        }

        public void LoadSave(string filesDirectory)
        {
            foreach (ISaveLoader loader in this.saveLoaders)
            {
                loader.Load(filesDirectory);
            }

            foreach (ISaveLoader loader in this.saveLoaders)
            {
                loader.PostLoad();
            }
        }

        public void SaveSave(string filesDirectory)
        {
            foreach (ISaveLoader loader in this.saveLoaders)
            {
                loader.Save(filesDirectory);
            }
        }

        public void RegisterSaveLoader(ISaveLoader loader)
        {
            this.saveLoaders.Add(loader);
        }

        public void RegisterGameLoader(IGameLoader loader)
        {
            this.gameLoaders.Add(loader);
        }
    }
}
