using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRPG
{
    public class Program
    {
        static void Main(string[] args)
        {
            WorldMap map = new WorldMap();
            int[] startSeat = map.GetStageMap().GetStartSeat();
            Warrior playerWarrior = new Warrior("전사", 1, 10, 3, startSeat[0], startSeat[1]);

            while (true)
            {
                Console.Clear();
                map.ShowScreenMap(playerWarrior);
                playerWarrior.PrintStatus();
                playerWarrior.InputMove(Console.ReadKey(), map);
            }

        }
    }
}


/*

1. 맵만들기
   - 완 - 나중에 수정을 할수도 있지만
   

2. 캐릭터 클래스 
   - 완 - 수정할수 있지만.
   - 완 - 캐릭터 움직임. - P - 라고 표시를 하면서 맵을 움직일 수 있어야한다. 
   - 맵에서 상호작용으로 맵 변경이 가능해졌다.
   - 아직 화살표를 통한 상호작용은 적용되지 않았다.

3. 적 몬스터 클래스
   - 적을 만났을때 적과 싸울수 있어야한다.

4. 인벤토리


5. 아이템 만들기


6. 인벤토리 열어서 아이템 사용
 
 */