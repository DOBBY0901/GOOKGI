using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayEX
{
    internal class Program
    {
        static void Main(string[] args)
        {
          //  int[] array = new int[20];
          //     for (int i = 0; i < array.Length; i++)
          //  {
          //      array[i] = i;
          //  }
          //  
          //  for (int i = 0; i < array.Length; i++)
          //  {
          //      Console.WriteLine(array[i]);
          //  }
          //
            List<int> listA = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                listA.Add(i);
            }

            for (int i = 0; i < listA.Count; i++)
            {
                Console.WriteLine(listA[i]);
            }
            
            Console.ReadLine();
        }

        //배열은 처음 정한 크기를 벗어나지못하지만, 리스트는 ADD()할때마다 늘어날수있음. - 그렇다면 배열은 왜 사용?
        //배열은 그 메모리가 정확히 할당되어있지만, 리스트는 알수없음. 
        //즉, 정해진 수가 있는 경우는 배열 , 동적할당 되어있는 경우는 리스트를.
    }
}
