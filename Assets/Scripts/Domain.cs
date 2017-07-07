using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Domain
    {
        public static readonly Domain Vanilla = new Domain("Vanilla");
        public static readonly Domain Custom = new Domain("Custom");

        public string Name { get; set; }

        private static readonly List<Domain> domains = new List<Domain>()
        {
            Vanilla, Custom
        };

        public Domain(string name)
        {
            this.Name = name;
        }

        public static Domain FindByName(string name)
        {
            return domains.FirstOrDefault(domain => domain.Name.Equals(name, StringComparison.Ordinal));
        }
    }
}
