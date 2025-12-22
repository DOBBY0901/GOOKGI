using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DoMySelf
{ 
   
    internal class Program
    {
        static void PrintMessage()
        {
            Console.WriteLine("야구 게임에 오신것을 환영합니다.");
            Console.WriteLine("중복되지 않은 3가지 숫자를 입력해주세요.");
        }

        static int[] GetRandom()
        {
            Random rand = new Random();
            int[] arrayRand = new int [10];
            for (int i = 0; i < arrayRand.Length; i++)
            {
                arrayRand[i] = i;
            }

            for(int i = 0; i < arrayRand.Length; i++)
            {
                int randResult = rand.Next(0,10);
                int temp = arrayRand[i];
                arrayRand[i] = arrayRand[randResult];
                arrayRand [randResult] = temp;
            }

            int[] arrayResult = new int [3];
            
            for(int i = 0;i < arrayResult.Length; i++)
            {
                arrayResult[i] = arrayRand[i];
            }

            return arrayResult;
           
        }

        static bool GetChenckNumber(string str)
        {
            
            bool _isNumber = true;
            bool _isLength = true;
            bool _isOverlap = true;

            if (str.Length != 3) // 문자열 길이 체크
            { 
               _isLength = false;
                if (str.Length < 3)
                {
                    _isLength = false;
                    Console.WriteLine("문자열의 길이가 3보다 짧습니다.");
                }
                else
                {
                    _isLength = false;
                    Console.WriteLine("문자열의 길이가 3보다 깁니다.");
                }
            }

            if (_isLength) // 숫자인지 체크
            {           
                for (int i = 0; i < str.Length; i++)
                {
                    char tempC = str[i];

                    if (tempC < 48 || tempC > 57)
                    { 
                        _isNumber = false; 
                    }
                }
            }
           
            if (!_isNumber)
            {
                Console.WriteLine("문자가 섞여있습니다.");
            }

            if (_isNumber && _isLength) //중복확인
            {
                for (int i = 0; i < str.Length; i++)
                {
                    for (int j = i + 1; j < str.Length; j++)
                    {
                        if (str[i] == str[j])
                        {
                            _isOverlap = false;

                        }
                    }
                }  
            }
            
            if(!_isOverlap)
            {
                Console.WriteLine("중복되는 숫자가 있습니다.");
            }

            if(_isLength && _isNumber && _isOverlap)
            {
                return true;
            }
            else
            { 
                return false; 
            }
        }

        static void Main()
        {
            PrintMessage();
            int[] arrayCorrect = GetRandom();
            int _count = 0;

            while (true)
            {
                string inputStr = Console.ReadLine();
                bool _bool = GetChenckNumber(inputStr);
              
                if (_bool)
                {
                    _count++;
                    int _strike = 0;
                    int _ball = 0;
              
                    for(int i =0 ; i < inputStr.Length; i++)
                    { 
                        for(int j = 0; j < arrayCorrect.Length; j++)
                        {
                            int tempNum = (int)inputStr[i] - 48;
                            if (tempNum == arrayCorrect[j])
                            {
                                if(i==j)
                                {
                                    _strike++;
                                }
                                else
                                {
                                    _ball++;
                                }
                            }
                        }
                    }

                    Console.WriteLine($"{_strike} 스트라이크");
                    Console.WriteLine($"{_ball} 볼");

                    //몇번만에 맞췄는가를 알려주고 결과창 출력
                    //다시 새로운 숫자를 집어서 다시 게임 시작

                    if (_strike == 3)
                    {
                        Console.WriteLine($"정답입니다. {_count}번 시도하셨습니다.");
                        Console.ReadKey();
                        Console.Clear();

                        PrintMessage();
                        arrayCorrect = GetRandom();
                        _count = 0;
                    }
                
                }
                else
                {

                }

            }
              
        }  
    }
}
