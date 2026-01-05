using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace TestRPG
{
    public class PlayerUnit : Unit, IMoveController
    {

        private int[,] dirs = new int[,]
        {
            { 0, -1}, //위
            { 0, 1}, //아래
            { -1 ,0}, //왼쪽
            { 1, 0}, //오른쪽
        };

        public void InputMove(ConsoleKeyInfo _key, WorldMap _worldMap)
        {
            int dir = 0;
            switch (_key.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    dir = 0;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    dir = 1;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    dir = 2;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    dir = 3;
                    break;
                
                case ConsoleKey.Spacebar:
                    CallInteraction(_worldMap);
                    break;

                default:
                    return;
            }

            MoveFunc(dirs[dir, 0], dirs[dir, 1], _worldMap);
        }

        
        // 앞에 벽이 있거나 장애물이 있으면 움직이지 못해야함.
        public void MoveFunc(int _dtx, int _dty, WorldMap _worldMap)
        {
            CurX += _dtx;
            CurY += _dty;

            //플레이어가 화면 밖으로 나가지 못하게 함.
            int sizeX = _worldMap.GetStageMap().GetSeatLength(1); 
            int sizeY = _worldMap.GetStageMap().GetSeatLength(0);

            if (CurX < 0)
            {
                CurX = 0;
            }

            else if (CurX >= sizeX)
            {
                CurX = sizeX - 1;
            }

            if (CurY < 0)
            {
                CurY = 0;
            }
            else if (CurY >= sizeY)
            {
                CurY = sizeY - 1;
            }

            //플레이어가 이동했을때 해당 좌표가 벽이라면 다시 원위치로 되돌아감.
            if (_worldMap.GetStageMap().GetSeatInfo(CurY, CurX) == 1)
            {
                CurX -= _dtx;
                CurY -= _dty;
            }

            
        }

        public void CallInteraction(WorldMap _worldMap)
        {
            int info = _worldMap.GetStageMap().GetSeatInfo(CurY,CurX);

            if (info == 0 || info == 1)
            {
                return;
            }
               _worldMap.ChageMap(info);       

        }
     
    }     
}

