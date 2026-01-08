using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
 
public class Program
 {
   
    // 시작 안내문 출력
    static void PrintNotice()
    {
        Console.WriteLine("1. 숫자 입력, 출력");
        Console.WriteLine("2. 미리 저장해둔 번호 출력");
        Console.WriteLine("3. 랜덤 숫자 5개 출력(0~1000)");
    }

    // 두번째 안내문 출력 (반복)
    static void PrintNextNotice(int[] numArray)
    {
        Console.Clear();
        Console.WriteLine($"배열에 저장된 숫자 : {numArray[0]}, {numArray[1]}, {numArray[2]}, {numArray[3]}, {numArray[4]}");
        Console.WriteLine("1. 합, 평균 출력");
        Console.WriteLine("2. 최댓값 / 최솟값 출력");
        Console.WriteLine("3. 짝수만 출력");
        Console.WriteLine("4. 홀수만 출력");
        Console.WriteLine("5. 크기 역순으로 출력");
        
    }
    
    //시작 배열 출력
    static void PrintArray(int[] numArray, Random rand)
    {
        ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.D1:
                    NumberInput(numArray);
                    break;

                case ConsoleKey.D2:
                    PrintScriptNumber(numArray);
                    break;

                case ConsoleKey.D3:
                    RandomNumber(numArray, rand);
                    break;

                default:
                    break;
            }
        
           
    }

    //조건에 따른 값 출력
    static void PrintNewarray(int[] numArray)
    {
        ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

        switch (consoleKeyInfo.Key)
        {
            case ConsoleKey.D1:
                AddNumber(numArray);
                break;

            case ConsoleKey.D2:
                MaxMinNumber(numArray);
                break;

            case ConsoleKey.D3:
                PrintEvenNumber(numArray);
                break;

            case ConsoleKey.D4:
                PrintOddNumber(numArray);
                break;

            case ConsoleKey.D5:
                PrintNumberReverse(numArray);
                break;

            default :
                break;
        }
    }

    //숫자 입력
    static void NumberInput(int[] numArray)
    {   
            Console.Clear();
            Console.WriteLine("숫자 5개를 입력해주세요.");

            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            int a = int.Parse(parts[0]);
            int b = int.Parse(parts[1]);
            int c = int.Parse(parts[2]);
            int d = int.Parse(parts[3]);
            int e = int.Parse(parts[4]);

            numArray[0] = a;
            numArray[1] = b;
            numArray[2] = c;
            numArray[3] = d;
            numArray[4] = e;

        Console.WriteLine($"{numArray[0]}, {numArray[1]}, {numArray[2]}, {numArray[3]}, {numArray[4]}");

        Console.ReadKey();

    }

    //미리 저장된 배열 출력
    static void PrintScriptNumber(int[] numArray)
    {
        Console.Clear();
        
        numArray[0] = 7;
        numArray[1] = 77;
        numArray[2] = 777;
        numArray[3] = 7777;
        numArray[4] = 77777;

        Console.WriteLine($"{numArray[0]}, {numArray[1]}, {numArray[2]}, {numArray[3]}, {numArray[4]}");
    }

    //0~1000까지의 랜덤 5개의 번호 출력
    static void RandomNumber(int[] numArray, Random rand)
    {
        Console.Clear();
        
        for (int i = 0; i < numArray.Length; i++)
        {
            int randNum = rand.Next(0, 1000);
            numArray [i] = randNum;
        }
        Console.WriteLine($"{numArray[0]}, {numArray[1]}, {numArray[2]}, {numArray[3]}, {numArray[4]}");
    }

    //배열에 저장된 숫자의 합
    static void AddNumber(int[] numArray)
    {
        int resultSum = numArray[0] + numArray[1] + numArray[2] + numArray[3] + numArray[4];
        int resultAverage = (numArray[0] + numArray[1] + numArray[2] + numArray[3] + numArray[4]) / 5;
        Console.WriteLine();
        Console.WriteLine($" 합  : {resultSum}");
        Console.WriteLine($"평균 : {resultAverage}");
        Console.ReadKey();
        Console.Clear();
    }

    //배열의 최대값 최솟값 출력
    static void MaxMinNumber(int[] numArray)
    {
        int max = numArray[0]; 
        int min = numArray[0];
        for (int i = 1; i < numArray.Length; i++)
        {
            if (numArray[i] > max)
            {
                max = numArray[i];
            }

        }

        for(int i = 1; i < numArray.Length; i ++)
        {
            if (numArray[i] < min)
            {
                min = numArray[i];
            }
        }
        Console.WriteLine() ;
        Console.WriteLine($"최댓값 : {max}, 최솟값 : {min}");
        Console.ReadKey();
        Console.Clear();
    }
    
    //짝수 출력
    static void PrintEvenNumber(int[] numArray)
    {
        int evenNum = 0;
        Console.WriteLine();
        Console.WriteLine("짝수");
        for (int i = 0; i < numArray.Length; i++)
        {
            if (numArray[i] % 2 == 0)
            {
                evenNum = numArray[i];

                Console.Write($"{evenNum} ");
            }
        }

        
        Console.ReadKey();
        Console.Clear();
    }

    //홀수 출력
    static void PrintOddNumber(int[] numArray)
    {
        int oddNum = 0;
        Console.WriteLine();
        Console.WriteLine("홀수");
        for (int i = 0; i < numArray.Length; i++)
        {
            if (numArray[i] % 2 != 0)
            {
                oddNum = numArray[i];

                Console.Write($"{oddNum} ");
            }
        }


        Console.ReadKey();
        Console.Clear();
    }

    //역순으로 출력
    static void PrintNumberReverse(int[] numArray)
    {
        int numMin  = 0;
        int numMin2 = 0;
        int numMin3 = 0;
        int numMin4 = 0;
        int numMin5 = 0;

        for (int i = 0; i < numArray.Length; i++)
        {
            if(numArray[0]< numArray[i])
            {
                numMin = numArray[0];
            }
        }

       

        Console.WriteLine();
        Console.WriteLine($"{numMin}, {numMin2}, {numMin3}, {numMin4}, {numMin5}");
        Console.ReadKey();

    }

    static void Main()
    {
         int[] numArray = new int[5];
        
         Random rand = new Random();

        PrintNotice();
        PrintArray(numArray, rand);

        while (true)
        {
           PrintNextNotice(numArray);
           PrintNewarray(numArray);
        }
        
    }
  
 }

/*
 * 숫자 관리
 * 배열로 - (int)
 * 슷지 5개 저장 후 출력
 * 1을 누르면 숫자 5개를 입력하게 만들고 저장.
 * 2를 누르면 스크립트에 미리 저장해둔 정보로 숫자를 저장.
 * 3을 누르면 랜덤한 숫자 5개가 입력되도록.
 * 
 * 매서드를 통해
 * 합
 * 최대/최소 출력
 * 짝수만 출력
 * 홀수만 출력
 * 크기 역순으로 출력
 * 
 */
