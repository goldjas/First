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
        public WeaponSkill Skill { get; set; }
        //animation?



        public PlayerWeapon(string name)
        {
            //obviously need a better way to do this.
            Name = name;
            if(name == "Dagger")
            {
                Damage = 1;
                DamageType = "Pierce";
                Skill = new WeaponSkill
                {
                    Name = "1: Dagger Storm",
                    Damage = 2,
                    EnergyCost = 10
                };
            }
            
            if (name == "Rusty Sword")
            {
                Damage = 1;
                DamageType = "Slash";
                Skill = new WeaponSkill
                {
                    Name = "Weak Blade Beam",
                    Damage = 2,
                    EnergyCost = 10
                };
            }

            if (name == "Sword")
            {
                Damage = 3;
                DamageType = "Slash";
                Skill = new WeaponSkill
                {
                    Name = "Blade Beam",
                    Damage = 4,
                    EnergyCost = 10
                };
            }
            if (name == "Bow")
            {
                Damage = 3;
                DamageType = "Pierce";
                Skill = new WeaponSkill
                {
                    Name = "Multi Shot",
                    Damage = 3,
                    EnergyCost = 10
                };
            }
            if (name == "Wand")
            {
                Damage = 3;
                DamageType = "Lightning";
                Skill = new WeaponSkill
                {
                    Name = "Nova",
                    Damage = 4,
                    EnergyCost = 10
                };
            }
        }


    }
}
