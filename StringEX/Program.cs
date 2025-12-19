using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace StringEX
{
    internal class Program
    {
        // 가장 기초 알고리즘

        // 제일 큰애, 제일 작은애, 같은애 위치, 배열값 복사하기, 배열 뒤집기

        // 0 ~ 9 까지 숫자가 랜덤하게 배열에 들어가 있어야 한다.

        // 숫자를 오름차순 0 ~ 9, 내림차순 9 ~ 0


        static void Main(string[] args)
        {
            char[] arrayC = new char[10];
            
            Random rand = new Random();
             for (int i = 0; i < 10; i++)
            {
                arrayC[i] = (char)(97+i);            
            }

            for (int i = 0; i < 10; i++)
            {
                int result = rand.Next(i, 10);
                char temp = arrayC[0];
                arrayC[i] = arrayC[result];
                arrayC[result] = temp;
            }

            string str = "";
            for (int i = 0; i < arrayC.Length; i++)
            {
                str += arrayC[i];
            }
            
            Console.WriteLine(str);
            Console.ReadKey();
        }
    }
}
