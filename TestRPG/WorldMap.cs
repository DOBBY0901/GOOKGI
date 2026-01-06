using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace TestRPG
{
    public enum Map
    {
        start = 10,
        town,
        river,
        plain,
    }

    public class StageMap
    {
        private int[,] m_seat;
        private List<int[]> m_startSeat = new List<int[]>();

        private Map m_myMap;

        public StageMap(int[,] _seat, Map _map)
        {
            //Console.WriteLine($"y : {_seat.GetLength(0)}");
            //Console.WriteLine($"x : {_seat.GetLength(1)}");

            m_seat = new int[_seat.GetLength(0), _seat.GetLength(1)];
            for (int y = 0; y < _seat.GetLength(0); y++)
            {
                for (int x = 0; x < _seat.GetLength(1); x++)
                {
                    m_seat[y, x] = _seat[y, x];
                    if (_seat[y, x] == -1)
                        m_startSeat.Add(new int[] { y, x });
                }
            }
            m_myMap = _map;
        }

        public int GetSeatLength(int _dimension)
        {
            return m_seat.GetLength(_dimension);
        }

        public int GetSeatInfo(int _y, int _x)
        {
            return m_seat[_y, _x];
        }

        public int[] GetStartSeat()
        {
            return m_startSeat[0];
        }

        public int[] GetStartSeat(int _y)
        {
            switch (m_myMap)
            {
                case Map.start:
                    return m_startSeat[0];

                case Map.town:
                    if (_y == 5)
                        return m_startSeat[0];
                    else if (_y == 6)
                        return m_startSeat[2];
                    else
                        return m_startSeat[1];

                case Map.river:
                    return m_startSeat[0];

                default:
                    return null;
            }


        }

        public int[] GetStartSeat(int _y, int _x)
        {
            int index = CheckStartSeat(_y, _x);
            return m_startSeat[index];
        }

        public Func<int, int, int> CheckStartSeat;

    }

    public class WorldMap
    {
        private Dictionary<Map, StageMap> m_dicMap = new Dictionary<Map, StageMap>();

        private Map m_curMap;
        private StageMap m_curStageMap;



        public WorldMap(Map _map = Map.start)
        {
            InitMap(_map);
        }

        private void InitMap(Map _map)
        {
            StageMap startMap = new StageMap(new int[,]
                {
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                    { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1},
                    { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1},
                    { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1},
                    { 0, 0, 0, 0, 0, 1, 0, 1, 11, 1, 0, 1},
                    { 0, 0, 0, 0, 0, 1, 0, 1, 11, 1, 0, 1},
                    { 0, 0, -1, 0, 0, 1, 0, 0, 0, 0, 0, 1},
                    { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1},
                    { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1},
                    { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1},
                    { 0, 0, 0, 0, 0, 0, 0, 0, 12, 0, 0, 1},
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                }, Map.start);
            StageMap townMap = new StageMap(new int[,]
                {
                {1, 1, 1, 1, 1, 1, 10, 1 },
                {1, 0, 0, 0, 0, 0, -1, 1 },
                {1, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, -1, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 1 },
                {1, -1, 0, 0, 0, 0, 0, 1 },
                {1, 10, 1, 1, 1, 1, 1, 1 },
                }, Map.town);
            StageMap riverMap = new StageMap(new int[,]
                {
                {0, 0, 0, 1, 1, 0, 0, 0 },
                {0, 0, 0, 1, 1, 0, 0, 0 },
                {0, 0, 0, 1, 1, 0, 0, 0 },
                {-1, 0, 0, 1, 1, 0, 0, 0 },
                {0, 0, 0, 1, 1, 0, 0, 0 },
                {0, 0, 0, 1, 1, 0, 0, 0 },
                {0, 0, 0, 1, 1, 0, 0, 0 },
                {0, 0, 0, 1, 1, 0, 0, 0 },
                }, Map.river);
            StageMap plainMap = new StageMap(new int[,]
                {
                    { 3, 0, 0, 0, 0, 0, 0, 0},
                    { 3, 0, 0, 0, 0, 0, 0, 0},
                    { 3, -1, 0, 0, 0, 0, 0, 0},
                    { 3, 0, 0, 0, 0, 0, 0, 0},
                    { 3, 0, 0, 0, 0, 0, 0, 0},
                    { 3, 0, 0, 0, 0, 0, 0, 0},
                }, Map.plain);

            startMap.CheckStartSeat = (_y, _x) =>
            {
                return 0;
            };

            townMap.CheckStartSeat = (_y, _x) =>
            {
                if (_y == 5 && _x == 8)
                    return 0;
                else if (_y == 6 && _x == 8)
                    return 2;
                return 1;
            };

            riverMap.CheckStartSeat = (_y, _x) =>
            {
                return 0;
            };

            plainMap.CheckStartSeat = (_y, _x) =>
            {
                return 0;
            };


            m_dicMap.Add(Map.start, startMap);
            m_dicMap.Add(Map.town, townMap);
            m_dicMap.Add(Map.river, riverMap);
            m_dicMap.Add(Map.plain, plainMap);


            SetCurMap(_map);
        }

        private void SetCurMap(Map _map)
        {
            if (m_dicMap.TryGetValue(_map, out m_curStageMap))
            {
                m_curMap = _map;
            }
            else
            {
                Console.WriteLine("처음 초기화 하는 부분에서 맵을 가져오다가 실패했습니다.");
            }
        }


        public void ShowScreenMap(Unit _player)
        {
            for (int y = 0; y < m_curStageMap.GetSeatLength(0); y++)
            {
                for (int x = 0; x < m_curStageMap.GetSeatLength(1); x++)
                {
                    // 플레이어의 위치를 표시
                    if (_player.CurX == x && _player.CurY == y)
                        Console.Write("P");

                    // 움직일수 있는 좌표를 표시
                    // -1 은 맵이동 됬을때 스타트 위치.
                    else if (m_curStageMap.GetSeatInfo(y, x) == 0 ||
                            m_curStageMap.GetSeatInfo(y, x) == -1)
                        Console.Write("'");

                    // 움직일수 없는 벽을 좌표로 표시
                    else if (m_curStageMap.GetSeatInfo(y, x) == 1)
                        Console.Write("+");

                    // 화살표 표시
                    else if (m_curStageMap.GetSeatInfo(y, x) == 3)
                        Console.Write("<");
                    else if (m_curStageMap.GetSeatInfo(y, x) == 4)
                        Console.Write(">");

                    else if (m_curStageMap.GetSeatInfo(y, x) == 10)
                        Console.Write("S");

                    else if (m_curStageMap.GetSeatInfo(y, x) == 11)
                        Console.Write("T");

                    else if (m_curStageMap.GetSeatInfo(y, x) == 12)
                        Console.Write("R");


                    Console.Write(" ");
                }
                Console.WriteLine();
            }

        }

        /// <summary>
        /// 현재 맵 사이즈를 가져오는 매서드
        /// </summary>
        /// <param name="dimension">0과 1만 넣으세요. 2차원 배열이라서</param>
        /// <returns></returns>
        public int GetCurMapSize(int dimension)
        {
            return m_curStageMap.GetSeatLength(dimension);
        }


        public StageMap GetStageMap()
        {
            return m_curStageMap;
        }

        public void ChangeMap(int _info)
        {
            /*
            //switch (_info)
            //{
            //    case 10:
            //        SetCurMap(Map.start);
            //        break;
            //    case (int)Map.town:
            //        SetCurMap(Map.town);
            //        break;
            //    case 12:
            //        SetCurMap(Map.river);
            //        break;
            //}
            */
            SetCurMap((Map)_info);
        }


    }
}
