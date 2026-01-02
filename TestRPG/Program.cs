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
                map.ShowScreenMap(Map.Start);

                playerWarrior.PrintStatus();

                Console.ReadLine();
            }
            
           
           
        }
    }
}

/*
1. 맵만들기
2. 캐릭터 클래스
3. 인벤토리
4. 아이템 만들기

 */