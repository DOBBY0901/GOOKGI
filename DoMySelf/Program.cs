using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoMySelf
{ 
   

    internal class Program
    {
        
        static void Main()
        {
            int[] arrayA = new int[10];
            int[] arrayB = new int[10];

            int[] arrayResult = new int[arrayA.Length + arrayB.Length];

            for (int i = 0; i < arrayA.Length; i++)
            {
                arrayA[i] = i;
                arrayB[i] = i + 10;
            }

            for (int i = 0; i < arrayA.Length; ++i)
            {
                arrayResult[i] = arrayA[i]; 
            }

            for(int i  = 0; i < arrayB.Length; i++)
            {
                arrayResult[arrayA.Length + i] = arrayB[i];
            }
            

            for (int i = 0; i < arrayResult.Length; i++)
            {
               Console.WriteLine(arrayResult[i] + " ");
            }

            int _length = arrayResult.Length/2;
            int[] arrayResult_A = new int[_length];
            int[] arrayResult_B = new int[_length];

            for(int i = 0; i < _length; i++)
            {
                arrayResult_A[i] = arrayResult[i];
                arrayResult_B[i] = arrayResult[_length + i];
            }

            for (int i = 0; i < arrayResult_A.Length; i++)
            {
                Console.WriteLine(arrayResult_A[i] + " ");
                Console.WriteLine(arrayResult_B[i] + " ");
            }

            Console.ReadKey();
        }
    }
}
