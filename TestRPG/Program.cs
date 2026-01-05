using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRPG
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Warrior playerWarrior = new Warrior("전사", 5, 30, 30);

            WorldMap map = new WorldMap();

            while (true)
            {
                Console.Clear();
                Unit _unit = playerWarrior;
                map.ShowScreenMap(_unit); //업캐스팅 (부모에 대한 정보만 넘어감)
                playerWarrior.PrintStatus();
                playerWarrior.InputMove(Console.ReadKey(), map);
                
            }
            
           
           
        }
    }
}

/*
1. 맵만들기
2. 캐릭터 클래스
3. 적 몬스터 클래스
-적을 만났을때 전투 가능
3. 인벤토리
4. 아이템 만들기

 */