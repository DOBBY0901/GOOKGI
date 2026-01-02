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
        public int[,] m_seat;
    }

    public class WorldMap
    {    
        Dictionary<Map, StageMap> m_dicMap = new Dictionary<Map, StageMap>();

        private int[,] m_startMap;
        private int[,] m_townMap;
        private int[,] m_river;

        public WorldMap() 
        {

            InitMap();
       
        }

        private void InitMap()
        {
            StageMap startMap = new StageMap();
            startMap.m_seat = new int[,]
            {

               { 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,0 },
               { 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,0 },
               { 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,0 },
               { 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,0 },
               { 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,0 },
               { 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,0 },
               { 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,0 },
               { 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,0 },
               { 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,0 },
               { 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,0 },
               { 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,0 },
               { 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0, 0 ,0 },

            };
            
            StageMap townmap = new StageMap();
            townmap.m_seat = new int[,]
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

             };
            
            StageMap rivermap = new StageMap();
            rivermap.m_seat = new int[,]
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
            };
            
            m_dicMap.Add(Map.Start, startMap);
            m_dicMap.Add(Map.Town, townmap);
            m_dicMap.Add(Map.River, rivermap);

        }   


        private int[,] GetMap(Map _map)
        {
           if(m_dicMap.ContainsKey(_map))
            {
                StageMap getMap;
                m_dicMap.TryGetValue(_map, out getMap);
                return getMap.m_seat;
            }
          
            return null;
        }
        public void ShowScreenMap(Map _map)
        {
            int[,] curMap = GetMap(_map);
            for (int y = 0; y < curMap.GetLength(0); y++)
            {
                for(int x = 0; x< curMap.GetLength(1); x++)
                {
                    if (curMap[y, x] == 0)
                    {
                        Console.Write("'");
                    }

                    else if(curMap[y, x] == 1)
                    {
                        Console.Write("*");
                    }

                    else if (curMap[y, x] == 2)
                    {
                        Console.Write("#");
                    }

                    
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
}
