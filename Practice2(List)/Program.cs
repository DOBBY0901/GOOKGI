using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice2_List_
{
    internal class Program
    {
        static void AddNumberList(List<int> numList)
        {
            Console.Write("추가할 숫자 입력: ");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                numList.Add(value);
                Console.WriteLine("추가 완료!");
            }
            else
            {
                Console.WriteLine("숫자를 입력하세요.");
            }

            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            List<int> numList = new List<int>();
            AddNumberList(numList);
        }
    }
}
