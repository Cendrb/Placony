using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Items.Objects;

namespace Assets.Scripts
{
    public static class Initializer
    {
        public static void Init()
        {
            ResourceType.Registry.Init();
            ResourceTypeGroup.Registry.Init();
        }
    }
}
