using Assets.Scripts.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Loader
{
    public interface IGameLoader
    {
        string TableName { get; }
        StringIdentifiedRegistryItem FindItem(ItemIdentifier identifier);
        void Load(string filesDirectory, Domain domain);
        void PostLoad();
        void Save(string filesDirectory);
    }
}
