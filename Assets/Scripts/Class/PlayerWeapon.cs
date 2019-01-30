using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Class
{
    public class PlayerWeapon
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public string DamageType { get; set; }
        public bool Equipped { get; set; }
        //animation?



        public PlayerWeapon(string name)
        {
            //obviously need a better way to do this.
            Name = name;
            if(name == "Dagger")
            {
                Damage = 1;
                DamageType = "Pierce";
            }
            
            if (name == "Rusy Sword")
            {
                Damage = 1;
                DamageType = "Slash";
            }
        }


    }
}
