using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c34104032_practice_5_2
{
    class Chess
    {
        public string character;
        public int hp;
        public int mp;
        public int atk;
        public int atkRange;
        public int moveRange;
        public int position; //棋子在棋盤上的index

        public void attack(Chess Enemy)
        {
            Enemy.hp -= atk;
        }

        public virtual void Skill()
        {

        }
    }
}
