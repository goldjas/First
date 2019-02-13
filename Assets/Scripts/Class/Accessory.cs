using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Class
{
    public class Accessory
    {
        public string Name { get; set; }
        List<Defense> Defenses { get; set; }
        public bool Equipped { get; set; }
    }
}
