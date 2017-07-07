using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Items.Objects;
using Assets.Scripts.Loader;

namespace Assets.Scripts
{
    public static class Initializer
    {
        public static void Init()
        {
            TheOnlyLoader.Instance.RegisterGameLoader(ResourceType.Registry);
            TheOnlyLoader.Instance.RegisterGameLoader(ResourceTypeGroup.Registry);
        }
    }
}
