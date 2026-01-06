using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRPG
{
    public class PlayerUnit : Unit, IMoveController
    {

        private int[,] dirs = new int[,]
            {
                { 0, -1 }, // 위
                { 0, 1 },  // 아래
                { -1, 0 }, // 왼쪽
                { 1, 0}    // 오른쪽
            };

        public void InputMove(ConsoleKeyInfo _keyInfo, WorldMap _worldMap)
        {
            int dir = -1;
            switch (_keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    dir = 0;
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    dir = 1;
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    dir = 2;
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    dir = 3;
                    break;

                case ConsoleKey.R:
                    _worldMap.ChangeMap((int)Map.town);
                    PlayerChangeSeat(_worldMap.GetStageMap());
                    break;

                case ConsoleKey.T:
                    _worldMap.ChangeMap((int)Map.start);
                    PlayerChangeSeat(_worldMap.GetStageMap());
                    break;

                case ConsoleKey.Spacebar:
                    CallInteraction(_worldMap);
                    break;

                default:
                    return;
            }

            if (dir != -1)
                MoveFunc(dirs[dir, 0], dirs[dir, 1], _worldMap);

        }


        // 앞에 벽에 있거나 장애물이 있으면 움직이지 못해야한다.
        public void MoveFunc(int _dtX, int _dtY, WorldMap _worldMap)
        {
            CurX += _dtX;
            CurY += _dtY;


            // 플레이어가 화면 밖으로 나가지 못하게 하는 조건들
            int sizeX = _worldMap.GetStageMap().GetSeatLength(1);
            int sizeY = _worldMap.GetStageMap().GetSeatLength(0);

            if (CurX < 0)
                CurX = 0;
            else if (CurX >= sizeX)
                CurX = sizeX - 1;

            if (CurY < 0)
                CurY = 0;
            else if (CurY >= sizeY)
                CurY = sizeY - 1;


            // 플레이어가 이동했을때 거기 좌표가 벽이라면??
            // 다시 원위치로 되돌아가게한다.
            if (_worldMap.GetStageMap().GetSeatInfo(CurY, CurX) == 1)
            {
                CurX -= _dtX;
                CurY -= _dtY;
            }



        }


        public void CallInteraction(WorldMap _worldMap)
        {
            int info = _worldMap.GetStageMap().GetSeatInfo(CurY, CurX);

            // 아무 의미 없는 상호작용.
            if (info == 0 || info == 1 || info == -1 ||
                info == 3 || info == 4)
                return;


            _worldMap.ChangeMap(info);
            PlayerChangeSeat(_worldMap.GetStageMap());
        }

        private void PlayerChangeSeat(int _y, int _x)
        {
            CurY = _y;
            CurX = _x;
        }
        private void PlayerChangeSeat(StageMap _stageMap)
        {
            int[] startSeat = _stageMap.GetStartSeat(CurY, CurX);
            CurY = startSeat[0];
            CurX = startSeat[1];
        }




    }
}
