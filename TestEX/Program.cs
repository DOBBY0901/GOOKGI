using System;

       
//스탯창
//화면에 플레이어 이름, hp, att(공격력) 출력 - 완료
//입력 받을 수 있도록                        -완료
// 1. 몬스터와 싸운다                        -완료
// 2. 대기한다                               -완료
// 3. 이외의 경우는 잘못된입력임을 알려줌.   -완료
//
//화면에 창을 띄운다. - 완료
//입력을 받아서 결과창이 나와야한다. - 완료
//반복되어야한다. - 완료
//
//추가
//클래스로 만든다. - 완료
//싸운다 - 이후 플레이어는 상,중,하단 중 선택하여 공격가능
//몬스터는 세 부위중 랜덤하게 방어
//방어 성공시 공격무효, 방어 실패시 데미지 들어감
//몬스터가 죽을 때 까지
//
//+ 턴제방식으로 플레이어 공격시 몬스터 공격
// 플레이어도 상,중,하단의 공격을 막는다
// 한쪽이 죽을떄까지 반복한다.

public class Program
{
   enum Scene
    {
        start,
        fight,
        pause
    }
   

    public class Player
    {
        string name = "전사";
        int hp = 100;
        int att = 30;

        public void PrintStatus()
        {
            Player player = new Player();
            Console.WriteLine("------------------------------");
            Console.WriteLine($"플레이어의 이름   : {player.name}");
            Console.WriteLine($"플레이어의 체력   : {player.hp}");
            Console.WriteLine($"플레이어의 공격력 : {player.att}");
            Console.WriteLine("------------------------------");
        }
   
    }

   
    static void PrintMessage(Player player)
    {
        player. PrintStatus();
        Console.WriteLine("------------------------------");
        Console.WriteLine("몬스터와 조우했습니다. 행동을 선택해주세요.");
        Console.WriteLine("1. 싸운다.");
        Console.WriteLine("2. 대기한다.");
        Console.WriteLine("------------------------------");
    }

    static Scene StartScene(Player player)
    {
        
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("몬스터와 전투를 시작합니다.");
                    return Scene.fight;
                case ConsoleKey.D2:
                    Console.WriteLine("대기합니다.");            
                    return Scene.pause;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    return Scene.start;
            }
        
          
    }

    static Scene HitPosition()
    {
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            Console.WriteLine("------------------------------");
            Console.WriteLine("공격할 위치를 선택해주세요");
            Console.WriteLine("1. 상단.");
            Console.WriteLine("2. 중단.");
            Console.WriteLine("2. 하단.");
            Console.WriteLine("------------------------------");
            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("상단.");
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("중단.");
                    break;
                default:
                    Console.WriteLine("하단");
                    break;
            }
        }
    }

    static void Main()
    {
        Player player1 = new Player();
        Scene scene = new Scene();
        
        while (true)
        {
            if(scene == Scene.start)
            {
                PrintMessage(player1);
                scene = StartScene(player1);
            }
            switch (scene)
            {
                case Scene.start:
                    break;
                case Scene.fight:
                    
                    break;
                case Scene.pause:
                    break;
            }
        }
        

        Console.ReadKey();
        
    }

}
