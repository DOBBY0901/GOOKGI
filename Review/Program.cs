using System;
using System.Runtime.ExceptionServices;


namespace Review
{
    public class Program
    {
        // 빌딩 만들기
        static void Building()
        {
            // 내가 입력한 숫자의 크기만큼
            // ㅁㅁㅁㅁㅁ의 개수가 늘어남
            // 대신 가로 길이도 조정 가능하게 
            // 한번 입력에 가로 세로
            // ex) 3, 4 하면 가로3줄 세로 4줄
            
            Console.WriteLine("세로, 가로 순으로 입력해주세요");
            string input = Console.ReadLine();
            string[] str = input.Split(' ');

            int height = int.Parse(str[0]);
            int width = int.Parse(str[1]);

            
           
            for (int i = 0; i < height; i++)
            {
               for(int j = 0; j < width; j++)
               {
                   Console.Write("ㅁ");
               }
               Console.WriteLine();
            }

            Console.ReadKey();
        }

        //피라미드 만들기
        static void Triangle()
        {
            Console.Write("피라미드의 높이를 입력해주세요 : ");
            int height = int.Parse(Console.ReadLine());
            Console.Write("피라미드의 넓이를 입력해주세요 : ");
            int width = int.Parse(Console.ReadLine());


            string strTriangle = "";
            string strSpacebar = "";
           
            int index = 0;
            while (index < height)
            {
                strSpacebar = "";
                for (int i = index; i < height - 1; i++)
                {
                    for( int j = 0; j < width; j++)
                    {
                        strSpacebar += " ";
                    }
                    
                }
   
                if (index == 0)
                {
                    strTriangle = "▲";
                }
              
                else
                {
                    for (int i = 0; i < width; i++)
                    {
                        strTriangle += "▲";
                    }
                }
               
                Console.WriteLine(strSpacebar + strTriangle);
                index++;
                
            }

            Console.ReadKey();
        }

        //역삼각형 만들기
        static void ReverseTriangle()
        {
            int height = 0;
            
            if (int.TryParse(Console.ReadLine(), out height)==false)
            {
                Console.WriteLine("파싱! 실패..ㅠ");
            }

            int spaceCount = 0;

            for (int i = height - 1; i >= 0; i--)
            {
                for (int j = 0; j < spaceCount; j++)
                {
                    Console.Write(" ");
                }
                for (int j = 0; j <= i; j++)
                {
                    Console.Write("▼");
                }
                Console.WriteLine();
                spaceCount++;
            }
  
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            string str1 = Console.ReadLine();
            string str2 = Console.ReadLine();
            
            Triangle();
            ReverseTriangle();
        }

    }

}