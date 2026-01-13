using System;

namespace Practice4
{
    internal class Program
    {
        enum RSP 
        {
            가위 = 1,
            바위 = 2, 
            보 = 3 
        }

        //정보 출력
        static void PrintInfo()
        {
            Console.WriteLine("======================");
            Console.WriteLine("가위바위보 게임입니다.");
            Console.WriteLine("1. 가위");
            Console.WriteLine("2. 바위");
            Console.WriteLine("3. 보");
            Console.WriteLine("======================");
        }
        
        //승리 계산
        static int Win(RSP player, RSP computer)
        {
            // 0: 무, 1: 승, -1: 패
            if (player == computer)
            {
                return 0;
            }

            // 승리 조건 : 바위>가위, 가위>보, 보>바위
            else if ((player == RSP.바위 && computer == RSP.가위) ||
                     (player == RSP.가위 && computer == RSP.보) ||
                     (player == RSP.보 && computer == RSP.바위))
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        
        //전체출력
        static void ALL(Random random)
        {
            int win = 0;
            int lose = 0;
            int draw = 0;

            while (true)
            {
                PrintInfo();
                string input = Console.ReadLine();
                Console.Clear();

                if (!int.TryParse(input, out int num))
                {
                    Console.WriteLine("잘못된 입력입니다. 숫자를 입력해주세요.");
                    continue;
                }

                else if (num < 1 || num > 3)
                {
                    Console.WriteLine("범위가 잘못됐습니다. (1~3)");
                    continue;
                }

                RSP player = (RSP)num;
                RSP computer = (RSP)random.Next(1, 4);
                int result = Win(player, computer);

                Console.Clear();
                Console.WriteLine($"플레이어 : {player}, 컴퓨터 : {computer}");
                if (result == 0)
                {
                    draw++;
                    Console.WriteLine("결과 : 무승부");
                }
                else if (result == 1)
                {
                    win++;
                    Console.WriteLine("결과 : 승리!");
                }
                else
                {
                    lose++;
                    Console.WriteLine("결과 : 패배...");
                }
               
                Console.WriteLine($"승{win}, 무{draw}, 패{lose}");

                if (win >= 5)
                {
                    Console.WriteLine("승리했습니다!");
                    break;
                }
                else if (lose >= 5)
                {
                    Console.WriteLine("패배했습니다...");
                    break;
                }
            }


        }
        static void Main()
        {
            Random random = new Random();
            ALL(random);
        }

        

    }
}
