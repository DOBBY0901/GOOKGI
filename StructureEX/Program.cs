using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureEX
{

    internal class Program
    {
        static void Main(string[] args)
        {
            //메모리 고정
            int[] arrayA = new int[100];

            //메모리 자유로움
            //근본은 배열
            List<int> listA = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                //배열길이 빈곳 가장 앞에 값이 들어감. 
                //배열의 길이를 넘으면 배열 크기를 늘림.
                listA.Add(i);
                Console.WriteLine($"Count : {listA.Count}");
                Console.WriteLine($"Capacity : {listA.Capacity}"); //배열의길이가 됨
            }
            Console.WriteLine("");
     
            foreach (int listValue in listA)
            {
                Console.WriteLine(listValue);
            }

            Console.WriteLine("");

            //쓸줄모르면 굉장히 위험, 알면 굉장히 유용
            //데이터 수정에 유리하나 일일히 하나씩 체크하기 때문에
            //메모리 낭비가 어마어마해질 수 있음
            LinkedList<int> linkedlistA = new LinkedList<int>();

            //Key값으로 Value값에 접근(아래경우는 string)
            //직관성이 높아 자주 사용
            Dictionary<string, int> dicA = new Dictionary<string, int>();
            dicA.Add("A", 1);
            dicA.Add("B", 2);
            dicA.Add("C", 3);

            //하지만 Key값으로 접근해야하니
            //for문은 사용불가 그럴때 foreach문사용
            foreach (KeyValuePair<string, int> A in dicA)
            {
                Console.WriteLine(A);
            }
            Console.WriteLine("");


            //보통 자료구조에는 리스트와 딕셔너리가 가장많이 사용된다.

            //가장 늦게 들어온 값이 가장먼저 나감
            //Push, Pop을 사용
            //Ctrl + Z가 대표적인 스택의 예시
            Stack<int> stackA = new Stack<int>();
            stackA.Push(1);
            stackA.Push(2);
            stackA.Push(3);
            stackA.Push(4);

            int a = stackA.Pop();

            foreach (int item in stackA)
            {
                Console.WriteLine(item);
            }
           
            Console.WriteLine($"a : {a}");
            Console.WriteLine("");

            //가장 먼저 들어온 값이 가장먼저 나감
            //Enqueue, Dequeue 사용
            Queue<int> queueA = new Queue<int>();



            int[] arrayB = new int[10];
            arrayB[0] = 1;
            List<int> listB = new List<int>();
            
            for(int i= 0; i < 10; i++)
            listB.Add(i);
            listB.Insert(5,-20);
            listB.Remove(3);
            listB.RemoveAt(5);

            for(int i = 0; i < listB.Count; i++)
            Console.Write(listB[i] + " ");
           
            Console.WriteLine();
            Console.WriteLine($"Listcount : {listB.Count}, ListCapacoty : {listB.Capacity}");
            
            MyLsit<int> myList = new MyLsit<int>();
            for(int i = 0; i < 10; i++) 
                myList.Add(i);
                myList.Insert(5, -20);
                myList.Remove(3);
                myList.RemoveAt(5);
            
           
          

            for (int i = 0; i < myList.Count; i++)
            Console.Write(myList.array[i] + " ");
            
            Console.WriteLine();
            Console.WriteLine($"Mylist count : {myList.Count}, MyListCapacoty : {myList.Capacity}");
            
            LinkedList<int> linkedList = new LinkedList<int>();
            linkedList.AddLast(5);

            MyLinkedList myLinkedList = new MyLinkedList();
            myLinkedList.AddLast(5);



        }
    }
}
