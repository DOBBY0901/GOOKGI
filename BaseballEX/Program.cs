using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace BaseballEX
{

   
    //컴퓨터가 무작위 3개의 숫자(중복되지 않은 숫자 ex 111, 222)
    internal class Program
    {
        static void ShowStartScreens()
        {
            Console.WriteLine("야구게임에 오신것을 환영합니다.");
            Console.WriteLine("규칙 : 중복되지 않는 숫자 3개를 입력해주세요.");        }

        static int[] GetNumber()
        {
            
            Random rand = new Random();
            int[] arrayRandNum = new int[10];
            for (int i = 0; i < 10; i++)
                arrayRandNum[i] = i;

            for (int i = 0; i < 3; i++)
            {
                int result = rand.Next(i, 10);
                int temp = arrayRandNum[i];
                arrayRandNum[i] = arrayRandNum[result];
                arrayRandNum[result] = temp;
            }

            return arrayRandNum;
        }

        static bool IsNumberCheck(string str)
        {
            
            //문자열의 길이 체크
            if (str.Length != 3)
            {
                Console.WriteLine("잘못된 입력입니다. 숫자 3개를 입력해주세요.");
            }
            else
            {
                bool _isNum = true;
                bool _isduplication = true;
                for (int i = 0; i < 3; i++)
                {
                    //받은 문자열이 숫자인지 체크
                    char temp = str[i];
                    if (48 <= temp && temp <= 57)
                    {
                        //숫자들이 중복되지 않는지 체크
                        for (int a = 0; a < str.Length; a++)
                        {
                            for (int b = a + 1; b < str.Length; b++)
                            {
                                if (str[a] == str[b])
                                {
                                    _isduplication = false;

                                }
                                else
                                {

                                }
                            }
                        }

                    }
                    else
                    {
                        _isNum = false;
                    }
                }

                if (_isNum)
                {
                    if (_isduplication)
                    {
                        Console.WriteLine("숫자를 입력했습니다.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("중복되는 숫자가 있습니다.");
                    }
                }
                else
                {
                    Console.WriteLine("문자를 입력했습니다");
                }
            }

            return false;
        }
        static void Main(string[] args)
        {
            ShowStartScreens();
            int[] arrayCorrectNum = new int[3];
            int[] arrayResult = GetNumber();

            for (int i = 0; i < arrayCorrectNum.Length; i++)
            { 
                arrayCorrectNum[i] = arrayResult[i];
            }

           while (true)
            {
                string str = Console.ReadLine();
                if (IsNumberCheck(str))
                {
                    int[] arrayEnterNum = new int[3];
                    for (int i = 0; i < arrayEnterNum.Length; i++)
                        arrayEnterNum[i] = Convert.ToInt32(str[i]) - 48;

                    int _strike = 0;
                    int _ball = 0;

                    for (int i = 0; i < arrayCorrectNum.Length; i++)
                    {
                        for (int j = 0; j < arrayEnterNum.Length; j++)
                        {
                            if (arrayCorrectNum[i] == arrayEnterNum[j])
                            {
                                if (i == j)
                                    _strike++;
                                else
                                    _ball++;
                            }

                        }
                    }

                    Console.WriteLine($"{_strike}S");
                    Console.WriteLine($"{_ball}B");
                    Console.WriteLine("");
                }
                else
                {

                }
            }

        }
    }
}

