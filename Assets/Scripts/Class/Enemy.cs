using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Class
{
    class Enemy
    {
        public string Name { get; set; }
        public float Health { get; set; }
        public List<EnemyAttack> Attacks  { get; set; }
        public float XP { get; set; }
    }
}
