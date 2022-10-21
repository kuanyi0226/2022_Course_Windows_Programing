using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c34104032_practice_5_2
{
    class Wizard : Chess
    {
        public Wizard(string role)
        {
            this.character = role; // p1 or p2
            this.hp = 70;
            this.mp = 25;
            this.atk = 20;
            this.atkRange = 2;
            this.moveRange = 2;
        }

        public bool Skill(Chess Enemy)
        {
            if (mp >= 10)
            {
                mp -= 10;
                Enemy.hp -= (atk * 2);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
