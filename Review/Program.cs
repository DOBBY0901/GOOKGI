using Microsoft.SqlServer.Server;
using System;


namespace Review
{
    public class Program
    {
        static void Main()
        {
            string str_1 = Console.ReadLine();
            string str_2 = Console.ReadLine();

            Triangle(str_1, str_2);
            ReverseTriangle(str_1, str_2);

            Console.ReadKey();
        }

        //빌딩 만들기 메서드
        static void Building()
        {

            // 7 11
            //string str = Console.ReadLine();
            //string str_width = "";
            //string str_height = "";

            //int index = 0;
            //for (int i = 0; i < str.Length; i++)
            //{
            //    if (str[i] == ' ')
            //    {
            //        index = i;
            //        break;
            //    }
            //}
            //for (int i = 0; i < index; i++)
            //    str_width += str[i];
            //for (int i = index; i < str.Length; i++)
            //    str_height += str[i];



            string[] str = Console.ReadLine().Split(' ');
            int width = 0;
            int height = 0;

            if (int.TryParse(str[0], out width) == false ||
                int.TryParse(str[1], out height) == false)
                Console.WriteLine("파싱 실패");

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write("□");
                }
                Console.WriteLine();
            }

            //Console.Write("가로 길이를 입력해주세요 : ");
            //string str = Console.ReadLine();
            //int width = 0;
            //if (int.TryParse(str, out width) == false)
            //    Console.WriteLine("파싱에 실패했습니다.");

            //Console.Write("세로 길이를 입력해주세요 : ");
            //str = Console.ReadLine();
            ////int height = int.Parse(str); //Convert.ToInt32(str);
            //int height = 0;
            ////파싱이 되면 True 그렇지 않으면 False                
            //if (int.TryParse(str, out height) == false)
            //    Console.WriteLine("파싱에 실패했습니다.");

            //str = "";
            //for (int x = 0; x < width; x++)
            //    str += "□";
            //for (int y = 0; y < height; y++)
            //    Console.WriteLine(str);

            //for (int y = 0; y < height; y++)
            //{
            //    for (int x = 0; x < width; x++)
            //    {
            //        Console.Write("□");
            //    }
            //    Console.WriteLine();
            //}

            Console.ReadKey();

            // 내가 입력한 숫자의 크기만큼
            // ㅁㅁㅁㅁㅁ
            // ㅁㅁㅁㅁㅁ
            // ㅁㅁㅁㅁㅁ
            // ㅁㅁㅁㅁㅁ
            // ㅁㅁㅁㅁㅁ
            // 건물 모양 나오게 하기.
            // 입력받은 수만큼 세로길이가 조절 가능하게.
            //
            // 두번입력부터
            //
            // 대신 가로길이도 조절 가능하게
            // 한번 입력으로 
            // 11 12
        }

        //피라미드 만들기
        static void Triangle(string _str_1, string _str_2)
        {
            int height = int.Parse(_str_1); // 이런 방식은 방어코드가 없기때문에 안좋은 코드 방식
            int width = int.Parse(_str_2); // 이런 방식은 방어코드가 없기때문에 안좋은 코드 방식

            Console.WriteLine();

            string strTriangle = "";
            string strSpaceBar = "";

            int index = 0;
            while (index < height)
            {
                strSpaceBar = "";
                for (int i = index; i < height - 1; i++)
                    for (int j = 0; j < width; j++)
                        strSpaceBar += " ";
                if (index == 0)
                {
                    strTriangle = "△";
                }
                else
                {
                    for (int i = 0; i < width; i++)
                        strTriangle += "△";
                }
                Console.WriteLine(strSpaceBar + strTriangle);
                index++;
            }



            //피라미드의 옆 넓이의 길이를 늘리고 싶다.
        }

        //역삼각형 만들기
        static void ReverseTriangle(string _str_1, string _str_2)
        {
            // 실제로는 이런 방식은 쓰면 안된다.
            //int height = int.Parse(Console.ReadLine());
            int height = 0;
            if (!int.TryParse(_str_1, out height))
                Console.WriteLine("파싱 실패");

            int width = 0;
            if (!int.TryParse(_str_2, out width))
                Console.WriteLine("파싱 실패");

            int spaceConut = 0;
            for (int i = height - 1; i >= 0; i--)
            {
                for (int j = 0; j < spaceConut; j++)
                    Console.Write(" ");
                for (int j = 0; j <= i; j++)
                    Console.Write("▽");
                Console.WriteLine();
                spaceConut++;
            }

        }


    }
}
