using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRPG
{
    public interface IMoveController
    {
       
        void InputMove(ConsoleKeyInfo _keyint, WorldMap _worldMap);
        void MoveFunc(int _dtx, int _dty, WorldMap _worldMap);
    }

    public class Unit
    {
        protected string m_name;
        public string Name { get { return m_name; } }

        protected int m_lv;
        public int Lv { get { return m_lv; } }

        private int[] m_requireExp = new int[]
        {
            0, 100, 200, 300, 500, 1000
        };
        
        protected int m_exp;
        public int Exp { get { return m_exp; } }

        protected double m_expPercentage => ((double)m_exp / m_requireExp[Lv]) * 100;

        protected int m_hp;
        public int Hp { get { return m_hp; } }

        protected int m_mp;
        public int Mp { get { return m_mp; } }

        protected int m_attValue;
        public int AttValue { get { return m_attValue; } }

        protected int m_defValue;
        public int DefValue { get { return m_defValue; } }

        protected int m_gold;
        public int Gold { get { return m_gold; } }

        public int CurX { get; set; }
       
        public int CurY { get;  set; } 

        public void PrintStatus()
        {
            Console.WriteLine("====================");
            Console.WriteLine($"  이름    : {Name}");
            Console.WriteLine($"  레벨    : {Lv}");
            Console.WriteLine($" 경험치   : {m_expPercentage}%");
            Console.WriteLine($"  체력    : {Hp}");
            Console.WriteLine($"  마나    : {Mp}");
            Console.WriteLine($" 공격력   : {AttValue}");
            Console.WriteLine($" 방어력   : {DefValue}");
            Console.WriteLine($"소지 골드 : {Gold}");
            Console.WriteLine("====================");
        }

       
    }
}
