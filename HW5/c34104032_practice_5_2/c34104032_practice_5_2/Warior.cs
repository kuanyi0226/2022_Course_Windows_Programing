using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c34104032_practice_5_2
{
    class Warior : Chess
    {
        public Warior(string role)
        {
            this.character = role; // p1 or p2
            this.hp = 100;
            this.mp = 15;
            this.atk = 30;
            this.atkRange = 1;
            this.moveRange = 2;
        }

        public bool Skill(Chess Enemy)
        {
            if (mp >= 10)
            {
                mp -= 10;
                attack(Enemy);
                hp += (atk / 2);
                return true;
            }
            else
                return false;
        }

    }
}
