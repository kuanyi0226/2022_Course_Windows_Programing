using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c34104032_practice_5_2
{
    class Ranger : Chess
    {
        public Ranger(string role)
        {
            this.character = role; // p1 or p2
            this.hp = 90;
            this.mp = 10;
            this.atk = 30;
            this.atkRange = 3;
            this.moveRange = 1;
        }

        public bool Skill(Chess Enemy) //順便判斷是否成功使用技能
        {
            if (mp >= 10)
            {
                mp -= 10;
                attack(Enemy);
                return true;
            }
            else
                return false;
        }
    }
}
