using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace TestRPG
{
   public enum Map
    {
        Start,
        Town,
        River
    }

    public class StageMap
    {
        private int[,] m_seat;

        private int startX;
       
        public StageMap() 
        {
        
        }

        public StageMap(int[,] _seat)
        {
            m_seat = new int[_seat.GetLength(0), _seat.GetLength(1)];
            
            for (int y = 0; y < _seat.GetLength(0); y++)
            {
                for(int x = 0;  x < _seat.GetLength(1); x++)
                {
                    m_seat[y, x] = _seat[y, x];
                }
            }
        }

        public int GetSeatLength(int demension)
        {
            return m_seat.GetLength(demension);
        }

        public int GetSeatInfo(int _y, int _x)
        {
            return m_seat[_y, _x];
        }
    }

    public class WorldMap
    {    
        Dictionary<Map, StageMap> m_dicMap = new Dictionary<Map, StageMap>();

        private Map m_curMapSeat;
        private StageMap m_curStageMap;

        
        public WorldMap(Map _map = Map.Start) 
        {

            InitMap(_map);
       
        }

        private void InitMap(Map _map)
        {
            StageMap startMap = new StageMap(new int[,]
            {

               { 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,3 },
               { 0, 0, 0, 0, 0, 0, 1, 0 ,0 ,0, 0 ,3 },
               { 0, 0, 0, 0, 0, 0, 1, 0 ,0 ,0, 0 ,3 },
               { 0, 0, 0, 0, 0, 0, 1, 0 ,0 ,0, 0 ,3 },
               { 0, 0, 0, 0, 0, 0, 1, 0 ,0 ,0, 0 ,3 },
               { 0, 0, 0, 0, 0, 0, 1, 0 ,0 ,0, 0 ,3 },
               { 0, 0, 0, 0, 0, 0, 1, 0 ,0 ,0, 0 ,3 },
               { 0, 0, 0, 0, 0, 0, 1, 0 ,0 ,2, 0 ,3 },
               { 0, 0, 0, 0, 0, 0, 1, 0 ,0 ,0, 0 ,3 },
               { 0, 0, 0, 0, 0, 0, 1, 0 ,0 ,0, 0 ,3 },
               { 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,3 },
               { 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,3 },

            });        
            StageMap townmap = new StageMap(new int[,]
             {
               { 1, 1, 1, 1, 1, 1, 1, 1 ,1 ,1, 1 ,1},
               { 1, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,1},
               { 1, 1, 1, 1, 1, 1, 1, 1 ,1 ,1, 1 ,1},

             });
            StageMap rivermap = new StageMap(new int[,]
            {
               {1, 1, 1, 1, 1, 1, 1, 1 ,1 ,1, 1 ,1},
               { 1, 0, 0, 0, 0, 2, 2, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 2, 2, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 2, 2, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 2, 2, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 2, 2, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 2, 2, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 2, 2, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 2, 2, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 2, 2, 0 ,0 ,0, 0 ,1},
               { 1, 0, 0, 0, 0, 2, 2, 0 ,0 ,0, 0 ,1},
               { 1, 1, 1, 1, 1, 1, 1, 1 ,1 ,1, 1 ,1},
            });
            
            m_dicMap.Add(Map.Start, startMap);
            m_dicMap.Add(Map.Town, townmap);
            m_dicMap.Add(Map.River, rivermap);

            SetMap(_map);

        }   


        private void SetMap(Map _map)
        {
            if (m_dicMap.TryGetValue(Map.Start, out m_curStageMap))
            {
                m_curMapSeat = _map;
            }
            else
            {
                Console.WriteLine("처음 초기화에서 맵 불러오기 실패");
            }
        }

    //   private int[,] GetMap(Map _map)
    //   {
    //       
    //       if(m_dicMap.ContainsKey(_map))
    //       {
    //           StageMap getMap;
    //           m_dicMap.TryGetValue(_map, out getMap);
    //           return getMap.GetSeatInfo();
    //       }
    //       //맵 전달 실패
    //       return null;
    //   }

        public void ShowScreenMap( Unit _unit)
        {
            for (int y = 0; y < m_curStageMap.GetSeatLength(0); y++)
            {
                for(int x = 0; x< m_curStageMap.GetSeatLength(1); x++)
                {
                       //플레이어의 위치 표시
                        if (_unit.CurX == x && _unit.CurY == y)
                        { 
                           Console.Write("P"); 
                        }
                        
                        //움직일수 있는 위치
                        else if (m_curStageMap.GetSeatInfo(y,x) == 0)
                        {
                            Console.Write("'");
                        }

                        //움직일 수 없는 위치
                        else if (m_curStageMap.GetSeatInfo(y, x) == 1)
                        {
                            Console.Write("*");
                        }

                        else if (m_curStageMap.GetSeatInfo(y, x) == 2)
                        {
                            Console.Write("T");
                        }
                        else if (m_curStageMap.GetSeatInfo(y, x) == 3)
                        {
                            Console.Write("->");
                        }
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 현재 맵 사이즈 가져오는 매서드
        /// </summary>
        /// <param name="dimension"> 0, 1만 넣으세요</param>
        /// <returns></returns>
        public int GetCurMapsize(int dimension)
        {
            return m_curStageMap.GetSeatLength(dimension);
        }

        public StageMap GetStageMap()
        {
            return m_curStageMap;
        }

        public void ChageMap(int _info)
        {
            switch(_info)
            {
                case 2:
                    SetMap(Map.Town);
                    break;

                default:
                    break;
            }
        }
    }
}
