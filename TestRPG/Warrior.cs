using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TestRPG.Unit;

namespace TestRPG
{
    public class Warrior : PlayerUnit
    {
       public Warrior(string _name, int _lv , int _atk, int _def)
        {
            m_name = _name;
            m_lv = _lv;
            m_exp = 33;
            m_attValue = _atk;
            m_defValue = _def;
            m_hp = 100;
            m_mp = 30;
            m_gold = 1000;

            CurY = 1;
            CurX = 1;
        }

       
    }
}
