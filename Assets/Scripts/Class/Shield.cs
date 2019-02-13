using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Class
{
    public class Shield
    {
        public string Name { get; set; }
        List<Defense> Defenses { get; set; }
        public bool Equipped { get; set; }

        public Shield(string name)
        {
            //obviously need a better way to do this.
            Name = name;
            if (name == "Wooden Shield")
            {
                var newDefense = new Defense { Type = "all", ResistancePercent = 50 };
            }
        }
    }

}
