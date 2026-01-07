using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Practice1
{
    internal class Program
    { 
        //덧셈
        static void AddCalculator()
        {
            Console.Clear();
            Console.WriteLine("============================================");
            Console.WriteLine("덧셈 계산기 입니다. 원하는 숫자 2개를 입력해주세요");
            Console.WriteLine("============================================");

            // 한 줄에 숫자 2개 입력
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            double a = double.Parse(parts[0]);
            double b = double.Parse(parts[1]);

            double result = a + b;

            double resultCum = result;

            

            Console.WriteLine($"결과 {a} + {b} = {result}");
        }
        
        //뺄셈
        static void MinusCalculator()
        {
            Console.Clear();
            Console.WriteLine("============================================");
            Console.WriteLine("뺼셈 계산기 입니다. 원하는 숫자 2개를 입력해주세요.");
            Console.WriteLine("============================================");

            // 한 줄에 숫자 2개 입력
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            double a = double.Parse(parts[0]);
            double b = double.Parse(parts[1]);

            double result = a - b;

            Console.WriteLine($"결과 {a} - {b} = {result}");
        }

        //곱셈
        static void MultipleCalculator()
        {
            Console.Clear();
            Console.WriteLine("============================================");
            Console.WriteLine("곱셈 계산기 입니다. 원하는 숫자 2개를 입력해주세요.");
            Console.WriteLine("============================================");

            // 한 줄에 숫자 2개 입력
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            double a = double.Parse(parts[0]);
            double b = double.Parse(parts[1]);

            double result = a * b;

            Console.WriteLine($"결과 {a} * {b} = {result}");
        }

        //나눗셈
        static void DivideCalculator()
        {
            Console.Clear();
            Console.WriteLine("============================================");
            Console.WriteLine("나눗셈 계산기 입니다. 원하는 숫자 2개를 입력해주세요.");
            Console.WriteLine("============================================");

            // 한 줄에 숫자 2개 입력
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            double a = double.Parse(parts[0]);
            double b = double.Parse(parts[1]);

            double result = a/b;

            if(a== 0 || b== 0)
            {
                Console.WriteLine("0은 나눌수 없습니다. 다른 숫자를 입력해주세요");
               
            }
            else
            {
                Console.WriteLine($"결과 {a} / {b} = {result}");
            }
            
        }

        //계산기 선택
        static void ChooseCalculator()
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.D1:
                        AddCalculator();
                        break;
                    case ConsoleKey.D2:
                        MinusCalculator();

                        break;
                    case ConsoleKey.D3:
                        MultipleCalculator();
                        break;
                    case ConsoleKey.D4:
                        DivideCalculator();
                        break;
                    default:
                        break;
                
                
                }
            Console.ReadKey();

        }

        //안내문 출력
        static void PrintNotice()
        {
            Console.WriteLine("==================================================================================");
            Console.WriteLine("계산기 입니다. 원하는 계산기의 번호를 눌러주세요.");
            Console.WriteLine("숫자 1 입력, 스페이스바(Spacebar), 숫자 2 입력, 엔터(Enter)순서로 입력하면 됩니다.");
            Console.WriteLine("==================================================================================");
            Console.WriteLine("                                 1. 덧셈(+) 계산기"                                );
            Console.WriteLine();                                                                                  
            Console.WriteLine("                                 2. 뺄셈(-) 계산기"                                );
            Console.WriteLine();                                                                                  
            Console.WriteLine("                                 3. 곱셈(*) 계산기"                                );
            Console.WriteLine();
            Console.WriteLine("                                 4. 나눗셈(/)계산기"                               );
            Console.WriteLine("==================================================================================");

        }

        //계산기 출력
        static void PrintCalculator()
        {
            bool isExit = true;

            Console.WriteLine("=================================================");
            Console.WriteLine("사칙연산 계산기입니다.");
            Console.WriteLine("1. 계산기 시작");
            Console.WriteLine("2. 나가기");
            Console.WriteLine("=================================================");

            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            if (consoleKeyInfo.Key == ConsoleKey.D1)
            {
                isExit = true;

                while (isExit == true)
                {
                    Console.Clear();
                    PrintNotice();
                    ChooseCalculator();
                }
            }
            else
            {
                isExit = false;
                Console.WriteLine("계산기가 종료되었습니다.");
                return;
            }

        }

        static void Main(string[] args)
        {
            PrintCalculator();
        }
    }
}

/*
 * 계산기
 * 1. 덧셈
 * 정수 2개 입력 -> 덧셈 출력
 * 
 *
 * 
 * 
 * 
 */ 
