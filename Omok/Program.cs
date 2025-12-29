using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Omok
{
    public class Omok
    {
        //프로퍼티와 매서드는 취향차이!

        private int[,] m_seat;
        //오목 판 사이즈
        private int m_size;
        public int Size
        {
            get { return m_size / 2;}
            set { m_size= value;}
        }

        //내가 놓을 수 있는 X좌표
        private int m_curX;
        public int Curx
        {
            get { return m_curX; }
            set { m_curX = value;

                if (m_curX < 0)
                {
                    m_curX = 0;
                }

                else if (m_curX > m_size - 1)
                {
                    m_curX = m_size - 1;
                }

                }
        }

        //내가 놓을 수 있는 Y좌표
        private int m_curY;
        public int Cury
        {
                get { return m_curY; }
               
            set {
                m_curY = value;

                if (m_curY < 0)
                {
                    m_curY = 0;

                }
                else if (m_curY > m_size - 1)
                { 
                    m_curY = m_size - 1; 
                }
            }
        }

        private bool m_isBlacktrun;

        public bool Isplaying { get; set; }

        /// <summary> 
        /// 오목의 생성자
        /// </summary>
        /// <param name="_size"> 오목판 사이즈 </param>
        public Omok(int _size = 19)
        {
            m_size = _size;
            Curx = Size;
            Cury = Size;
            m_seat = new int[m_size, m_size];

            for (int y = 0; y < m_size; y++)
            {
                for (int x = 0; x < m_size; x++)
                {
                    m_seat[y, x] = 0;
                }
            }
           
            m_isBlacktrun = true;
            Isplaying = true;
        }
        /// <summary> 
        /// 오목판 초기화
        /// </summary>
        /// <param name="_size"></param>
        /// <returns></returns>
        public int[,] InitOmok()
        {
            int[,] seat = new int[m_size, m_size];
            for (int y = 0; y < m_size; y++)
            {
                for (int x = 0; x < m_size; x++)
                {
                    seat[y, x] = 0;
                }
            }
            return seat;
        }

        /// <summary>
       /// 현재 오목판 출력
       /// </summary>
       /// <param name="_seat">만들어진 오목판의 시트</param> 
        public void ShowOmok()
        {
            Console.Clear();
            for (int y = 0; y < m_seat.GetLength(0); y++)
            {
                for (int x = 0; x < m_seat.GetLength(0); x++)
                {
                    if (Curx == x && Cury == y)
                    {
                        Console.Write("C");
                    }
                    
                    else if (m_seat[y, x] == 0)
                    {
                        Console.Write("'");
                    }

                    else if (m_seat[y, x] == 1)
                    {
                        Console.Write("B");
                    }
                    
                    else if (m_seat[y, x] == 2)
                    {
                        Console.Write("W");
                    }

                    Console.Write(" ");
                }

                Console.WriteLine();
            }

            if (m_isBlacktrun)
            {
                Console.WriteLine("흑돌 차례입니다.");
            }
            else
            {
                Console.WriteLine("백돌 차례입니다.");
            }
        }

        /// <summary>
        /// 키보드 입력으로 오목 조작
        /// </summary>
        public void InputKey()
        {
            ConsoleKeyInfo keyInfo =Console.ReadKey();

            switch(keyInfo.Key) //swich문의 or은 이렇게 표시
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    Cury--;
                    break;       
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    Cury++;
                    break;         
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    Curx++;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    Curx--;
                    break;

                case ConsoleKey.Spacebar:                  
                    PutStone();                
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 바둑돌 놓기
        /// </summary>
        private void PutStone()
        {
            int x = m_curX;
            int y = m_curY;

            if (m_seat[y,x] == 0)
            {
                if (m_isBlacktrun)
                {
                    m_seat[y, x] = 1;
                }         
                else
                {
                    m_seat[y, x] = 2;
                }
                    
            }
            // = m_seat[y,x] = m_isBlacktrun ? 1 : 2;
            // 두개가 같은 것.
           if(!CheckFiveStone())
            m_isBlacktrun = !m_isBlacktrun;
        }

       /// <summary>
       /// 돌이 연속으로 5개가 되었는지 확인
       /// </summary>
       /// <returns></returns>
        private  bool CheckFiveStone()
        {
            int checkNum = m_isBlacktrun == true ? 1 : 2;

            //   if (m_isBlacktrun)
            //       checkNum = 1;
            //   else
            //       checkNum = 2;
            int x = Curx;
            int y = Cury;

            //m_seat[y,x] 방금 둔 돌의 좌표

            //가로 검사
            for (int i = 0; i < 5; i++)
            {
                int count = 0;
                for (int j = 0; j < 5; j++)
                {
                    //둘이 다르게 연결됐을때
                    int temp = i + j - 4;
                    if (x - temp < 0 || x - temp >= m_size)
                        continue;

                    if (m_seat[y, x - temp] == checkNum)
                        count++;

                    if (count == 5)
                    {
                        Isplaying = false;
                        return true;
                    }
                }
            }

            //세로 검사
            for (int i = 0; i < 5; i++)
            {
                int count = 0;
                for (int j = 0; j < 5; j++)
                {
                    //둘이 다르게 연결됐을때
                    int temp = i + j - 4;
                    if (y - temp < 0 || y - temp >= m_size)
                        continue;

                    if (m_seat[y - temp, x] == checkNum)
                        count++;
                    else
                        break;

                    if (count == 5)
                    {
                        Isplaying = false;
                        return true;
                    }
                }
            }

            //대각선 내림차순 계산
            for (int i = 0; i < 5; i++)
            {
                int count = 0;
                for (int j = 0; j < 5; j++)
                {
                    //둘이 다르게 연결됐을때
                    int temp = i + j - 4;
                    if (x - temp < 0 || x - temp >= m_size||
                        y - temp < 0 || y - temp >= m_size)
                        continue;

                    if (m_seat[y - temp, x - temp] == checkNum)
                        count++;

                    if (count == 5)
                    {
                        Isplaying = false;
                        return true;
                    }
                }
            }

            //대각선 오름차순 계산
            for (int i = 0; i < 5; i++)
            {
                int count = 0;
                for (int j = 0; j < 5; j++)
                {
                    //둘이 다르게 연결됐을때
                    int temp = i + j - 4;
                    if (x + temp < 0 || x + temp >= m_size ||
                        y - temp < 0 || y - temp >= m_size)
                        continue;

                    if (m_seat[y - temp, x + temp] == checkNum)
                        count++;

                    if (count == 5)
                    {
                        Isplaying = false;
                        return true;
                    }
                }
            }

            return false;
        }

        public bool GetIsBlackTurn()
        {
            return m_isBlacktrun;
        }
    }
  

    internal class Program
    {
        static void Main(string[] args)
        {
            Omok omok = new Omok();

            while (omok.Isplaying)
            {
                omok.ShowOmok();
                omok.InputKey();
            }

            if (omok.GetIsBlackTurn())
            {
                Console.WriteLine("흑돌승리!");
            }

            else
            {
                Console.WriteLine("백돌승리!");
            }
        }

    }
}


// 오목 만들기
// 1. 흑돌, 백돌 필요 (완료)
//  1-1.흑 백이 번갈아 가면서 (완료)
// 2. 바둑판 - 좌표 필요 (완료)
// 3. 입력 받기 - 인풋 (완료)
// 4. 내가 돌을 놓을 자리 - 좌표(완료)
// 5. 룰 정리 
// 문서로 표현할 줄 알아야 한다.
// 같은 돌이 위 or 아래 or 대각선으로 5개가 놓여지면 승리
// 돌을 놓는 순간 해당 돌을 기준으로 5개가 연결되는지 체크

// 알고리즘
// 많은 데이터를 저장하고 효율적으로 처리
