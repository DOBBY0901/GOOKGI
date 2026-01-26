using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //정수 저장 10개
            int[] intArray = new int[10];
            int[] intArray1 = new int[] { 1, 2, 3, 4, 5 };
            int[] intArray2 = { 1, 2, 3, 4, 5 };

            long[] intArray3 = new long[10];

            //정수 저장 100개 - 정적이다. 즉, 바뀌지않는다.
            //값이 바뀌면 안되는 것임을 다른 작업자에게 알린다.

            int[] arrayHundred = new int[100];

            for (int i = 0; i < intArray.Length; i++) //foreach를 쓸수도 있지만 나쁜방식. foreach는 인덱스에 접근할 수 없다.
            {
                arrayHundred[i] = i * 10;
            }

            //별개 - 앞의 데이터가 다 날아감
            arrayHundred = new int[200];

            //만일 이렇게 쓰고싶다면 새로운 배열을 하나 만들어서 
            //기존 배열의 데이터를 저장한 후에 써야 한다.



            //배열인데 동시에 확장된 기능 - 동적이다. 즉, 크기가 바뀐다.
            List<int> listHundred = new List<int>();
            int listcap = listHundred.Capacity;

            for(int i = 0;i < 100;i++)
            {
                listHundred.Add(i);
            }

            //2차원 배열
            int[,] twoDemension = new int[,]
            {
                { 1, 2,3,4,5 },
                { 2, 3,4,6,7 },
            };

            //c++의 auto와 같은 역할 - 자동으로 변환된다.
            var test = 10;

            listHundred.Remove(50); //50의 값을 가진것을 제거
            listHundred.RemoveAt(50); //50번째 index를 제거한다.
            listHundred.Clear();
          
            foreach (int value in listHundred)
            {
                Console.WriteLine(value);
            }

            Dictionary<int, int> dic = new Dictionary<int, int>();
            
            listHundred.Contains(50); //50번째 값이 있는지?
            dic.ContainsKey(55); //ContainsValue로도 찾을수있다.


            listHundred.Sort(); //오름차순 정렬 - 작 -> 큰
            listHundred.Reverse(); //반대로 뒤집기 - 내림차순

            Dictionary<string, int> dicTest = new Dictionary<string, int>();
            dicTest.Add("Apple", 5);
            dicTest.Add("Banana", 10);

           

        }
    }
}
