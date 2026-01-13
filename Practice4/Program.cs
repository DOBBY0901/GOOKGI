using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice4
{
    internal class Program
    {
        //안내문 출력
        static void PrintInfo()
        {
            Console.WriteLine("================================================");
            Console.WriteLine("숫자 맞추기! UP & DOWN!");
            Console.WriteLine("무작위 숫자를 입력해주세요.");
            Console.WriteLine("정답을 틀릴 시 [UP], [Down]으로 힌트를 드립니다.");
            Console.WriteLine("================================================");
            Console.WriteLine("난이도를 결정해주세요.");
            Console.WriteLine("1. 쉬움 (횟수제한 없음, 1~100 까지의 숫자.)");
            Console.WriteLine("2. 보통 (횟수제한 20회, 1~300까지의 숫자.)");
            Console.WriteLine("3. 어려움 (횟수제한 10회, 1~500 까지의 숫자.)");
            Console.WriteLine("================================================");
        }

        //쉬움모드 -횟수제한 X, 1~100까지의 숫자
        static void EasyMode(Random random, int count)
        {
            int correctNum = random.Next(1, 101);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("쉬움 모드입니다. 숫자를 입력해주세요.");
                Console.WriteLine("횟수제한 없음, 1~100까지의 숫자");
                Console.WriteLine("====================================");

                int input;
                string wrongInput = Console.ReadLine();

                if (!int.TryParse(wrongInput, out input))
                {
                    Console.WriteLine("숫자가 아닌 문자를 입력했습니다. 숫자를 입력해주세요.");
                    Console.ReadKey();
                    continue;
                }
                else if (input <= 0 || input > 100)
                {
                    Console.WriteLine("범위 밖의 숫자를 입력했습니다. 범위 내의 숫자를 입력해주세요");
                    Console.ReadKey();
                    continue;
                }
                count++;

                if (correctNum == input)
                {
                    Console.WriteLine("정답!");
                    Console.WriteLine($"{count}번 시도");
                   
                    break;
                }
                else if (correctNum > input)
                {
                    Console.WriteLine("[UP]");
                    Console.ReadKey();
                }
                else if (correctNum < input)
                {
                    Console.WriteLine("[DOWN]");
                    Console.ReadKey();
                }

            }

           
        }

        //보통 모드 - 횟수제한 20회, 1~300까지의 숫자
        static void NormalMode(Random random, int count)
        {
            int correctNum = random.Next(1, 301);
            int chance = 20;
                      
            while (count < chance)
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("보통 모드입니다. 숫자를 입력해주세요.");
                Console.WriteLine($"횟수제한 {chance - count}회, 1~300까지의 숫자");
                Console.WriteLine("====================================");
                
                int input;
                string wrongInput = Console.ReadLine();

                if (!int.TryParse(wrongInput, out input))
                {
                    Console.WriteLine("숫자가 아닌 문자를 입력했습니다. 숫자를 입력해주세요.");
                    Console.ReadKey();
                    continue;
                }
                else if (input <= 0 || input > 300)
                {
                    Console.WriteLine("범위 밖의 숫자를 입력했습니다. 범위 내의 숫자를 입력해주세요");
                    Console.ReadKey();
                    continue;
                }
                count++;

                if (correctNum == input)
                {
                    Console.WriteLine("정답!");
                    Console.WriteLine($"{count}번 시도");
                    break;
                }
                else if (correctNum > input)
                {
                    Console.WriteLine("[UP]");
                    Console.ReadKey();
                }
                else if (correctNum < input)
                {
                    Console.WriteLine("[DOWN]");
                    Console.ReadKey();
                }
               
            }
            Console.WriteLine("횟수를 모두 사용했습니다.");
            Console.WriteLine("정답을 맞추지 못했습니다.");
            Console.WriteLine("게임을 종료합니다.");
            Console.ReadKey();

        }
        //어려움 모드 - 횟수제한 10회, 1~500까지의 숫자
        static void HardMode(Random random, int count)
        {
            int correctNum = random.Next(1, 501);
            int chance = 10;

            while (count < chance)
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("어려움 모드입니다. 숫자를 입력해주세요.");
                Console.WriteLine($"횟수제한 {chance - count}회, 1~500까지의 숫자");
                Console.WriteLine("====================================");
               
                int input;
                string wrongInput = Console.ReadLine();

                if (!int.TryParse(wrongInput, out input))
                {
                    Console.WriteLine("숫자가 아닌 문자를 입력했습니다. 숫자를 입력해주세요.");
                    Console.ReadKey();
                    continue;
                }
                else if(input <=0 || input > 500)
                {
                    Console.WriteLine("범위 밖의 숫자를 입력했습니다. 범위 내의 숫자를 입력해주세요");
                    Console.ReadKey();
                    continue;
                }
                count++;

                if (correctNum == input)
                {
                    Console.WriteLine("정답!");
                    Console.WriteLine($"{count}번 시도");
                    break;
                }
                else if (correctNum > input)
                {
                    Console.WriteLine("[UP]");
                    Console.ReadKey();
                }
                else if (correctNum < input)
                {
                    Console.WriteLine("[DOWN]");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("횟수를 모두 사용했습니다.");
            Console.WriteLine("정답을 맞추지 못했습니다.");
            Console.WriteLine("게임을 종료합니다.");
            Console.ReadKey();
        }

        //난이도 선택
        static void ChooseLevel(Random random, int count, bool isDone)
        {
            while (isDone)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        EasyMode(random, count);
                        isDone = false;
                        break;

                    case ConsoleKey.D2:
                        NormalMode(random, count);
                        isDone = false;
                        break;

                    case ConsoleKey.D3:
                        HardMode(random, count);
                        isDone = false;
                        break;

                    default:
                        break;
                }
            }
        }
        static void Main()
        {
            Random random = new Random();
            int count = 0;
            bool isDone = true;
           
            PrintInfo();          
            ChooseLevel(random, count, isDone);
        }
    }
}
/*
숫자 맞추기 업다운
1. 컴퓨터가 랜덤한 숫자 하나를 가지고 있어야 한다. - 완

2. 플레이어는 숫자를 입력한다. - 완
-문자가 입력되어도 프로그램이 터지면 안됨

3. 플레이어가 입력한 숫자가 크거나 작으면 - 완
업이나 다운으로 플레이어에게 알려줘야한다

4. 플레이어가 숫자를 맞출 때 까지 - 완

5. 숫자를 맞추면 몇번(Count)안에 맞췄는지 결과창에서 알려주기 -완

6. 난이도가 쉬움이면 - 횟수제한 없고, 숫자 범위도 작게 - 완
   난이도에 따라 횟수제한은 줄어들고 숫자 범위는 커짐.

7. 리팩토링 - 좀더 알아보기 쉽게 (매서드화, 클래스 적용 등)

※앞에가 빨리 끝나면 가위바위보 게임 만들어보기
 */
