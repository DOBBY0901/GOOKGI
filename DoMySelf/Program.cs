using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoMySelf
{ 
    class CharacterInfo
    {
       protected string name;
       protected int lv;
       protected int attck;
       protected int hp;
       protected int maxhp;

        public CharacterInfo(string name, int lv, int attck, int hp, int maxhp)
        {
            this.name = name;
            this.lv = lv;
            this.attck = attck;
            this.hp = hp;
            this.maxhp = maxhp;
        }

    }
    class Player : CharacterInfo
    {
        public Player() : base("플레이어", 1, 5, 50, 100)
        {

        }
    }

    class Enemy:CharacterInfo
    {
        public Enemy(string name, int lv, int attack,int hp, int maxhp) : base(name, lv, attack, hp, maxhp)
        {

        }
    }

    internal class Program
    {
        static void Main()
        {
           
        }
    }
}
