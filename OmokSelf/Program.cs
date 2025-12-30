using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace OmokSelf
{
    public class Omok
    {
        private int[,] m_seat;
        private int m_size;

        private int m_curX;
        public int CurX
        {
            get { return m_curX; }
            set { 
                m_curX = value; 
                if(m_curX < 0)
                    m_curX = 0;
                else if(m_curX >= m_size)
                    m_curX = m_size - 1;
                }

        }

        private int m_curY;
        public int CurY
        {
            get { return m_curY; }
            set { m_curY = value;
                if (m_curY < 0)
                    m_curY = 0;
                else if (m_curY >= m_size)
                    m_curY = m_size - 1;
            }
        }

        private bool m_isBlackTurn;
        private bool m_isPlaying;

      /// <summary>
      /// 오목판 생성자
      /// </summary>
      /// <param name="_size"> 이값은 최소7 최대 19이다.</param>
        public Omok(int _size = 19) 
        {
            if (_size < 7)
            {
                _size = 7;
            }
            else if (_size > 19)
            {
                _size = 19;
            }

            m_seat = new int[_size, _size];
            m_size = _size;

            for (int y = 0; y < _size; y++)
            {
                for(int x = 0; x < _size; x++)
                {
                    m_seat[y, x] = 0;
                }
            }

            CurX = _size / 2;
            CurY = _size / 2;

            m_isBlackTurn = true;
            m_isPlaying = true;
        }

        /// <summary>
        /// 오목판 출력
        /// </summary>
        public void ShowScreen()
        {
            Console.Clear();
            
            for (int y = 0; y < m_seat.GetLength(0); y++)
            {
                for (int x = 0; x < m_seat.GetLength(0); x++)
                {
                    if (CurX == x && CurY == y)
                    {
                        Console.Write("C");
                    }
                    else if(m_seat[y, x] == 0)
                    {
                        Console.Write("'");
                    }
                    else if(m_seat[y, x] == 1)
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

            if (m_isBlackTurn)
            {
                Console.WriteLine("흑돌 차례");
            }
            else
            {
                Console.WriteLine("백돌 차례");
            }
        }

        /// <summary>
        /// 키보드 인풋
        /// </summary>
        public void InputOmok()
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    CurY--;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    CurY++;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    CurX--;
                    break;

                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    CurX++;
                    break;

                case ConsoleKey.Spacebar:
                    PutStone();
                    break;
            }
        }

        /// <summary>
        /// 스페이스바를 누르면 돌을 놓기
        /// </summary>
        private void PutStone()
        {
            int x = m_curX;
            int y = m_curY;

            if (m_seat[y, x] == 0)
            {
                if (m_isBlackTurn)
                {
                    m_seat[y, x] = 1;
                }
                else
                {
                    m_seat[y, x] = 2;
                }
                
                if(!CheckFiveStone())
                m_isBlackTurn = !m_isBlackTurn;
            }
        }

        /// <summary>
        /// 돌이 5개인지 확인
        /// </summary>
        /// <returns></returns>
        private bool CheckFiveStone()
        {
            int x = CurX;
            int y = CurY;
            int checkNum = m_isBlackTurn == true ? 1 : 2;
            

           for(int i = 0; i < 5; i++)
            {
                int count = 0;
                for (int j = 0; j < 5; j++)
                {
                    int xDt = x + i + j - 4;
                    if (xDt < 0 || xDt >= m_size)
                        continue;

                    if (m_seat[y,xDt] == checkNum)
                    {
                        count++;
                    }
                    if (count == 5)
                    {
                        m_isPlaying = false;
                        return true;
                    }
                }
            }
            
            {
                
                for (int i = 0; i < 5; i++)
                {
                    int count = 0;
                    for (int j = 0; j < 5; j++)
                    {
                        int yDt = y + i+ j - 4;
                        if (yDt < 0 || yDt >= m_size)
                            continue;
                        if (m_seat[yDt, x] == checkNum)
                        {
                            count++;
                        }
                        if (count == 5)
                        {
                            m_isPlaying = false;
                            return true;
                        }
                    }
                }
            }


            return false;
        }

        private bool CheckFiveStone2(int _x, int _y, int _checkNum, int _xDt, int _yDt)
        {
            for (int i = 0; i < 5; i++)
            {
                int count = 0;
                for (int j = 0; j < 5; j++)
                {
                    int xDt = 0;  //_x + i + j - 4;
                    int yDt = 0;  //_y;

                    if(_xDt == 1)
                        
                    if (xDt < 0 || xDt >= m_size ||
                        yDt < 0 || yDt >= m_size)
                        continue;

                    if (m_seat[yDt, xDt] == _checkNum)
                    {
                        count++;
                    }
                    if (count == 5)
                    {
                        m_isPlaying = false;
                        return true;
                    }
                }
            }

            return false;
        }

        public bool GetIsPlaying()
        {
            return m_isPlaying;
        }

        public void ShowResult()
        {
            if(m_isBlackTurn)
            {
                Console.WriteLine("흑돌 승리!");
            }
            else
            {
                Console.WriteLine("백돌 승리!");
            }
            Console.ReadKey();
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {         
            Omok omok = new Omok(13);
           
            while (omok.GetIsPlaying())
            {
                omok.ShowScreen();
                omok.InputOmok();
            }
            omok.ShowResult();
        }
    }
}

// 1. 바둑판을 2차원 배열로 출력한다.
// 2. 인풋처리 - wasd혹은 방향키