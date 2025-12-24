using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace TEST1
{
    internal class Program
    {
        enum Scene
        {
            start,
            fight,
            pause,
            end
        }

        enum Position
        {
            상단,
            중단,
            하단
        }
       
        static Random random = new Random();

        public class Character
        {
            public string name;
            public int hp;
            public int maxhp;
            public int att;
            public bool isDead;
            public int attckCount = 0;
            public int attackSucces = 0;

            public Character(string name, int hp, int maxhp, int att)
            {
                this.name = name;
                this.hp = hp;
                this.maxhp = maxhp;
                this.att = att;
            }

            public void PrintStatus()
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"이름 : {name}");
                Console.WriteLine($"체력 : {hp}/{maxhp}");
                Console.WriteLine($"공격력: {att}");
                Console.WriteLine("-----------------------------");
            }

            public bool IsDead()
            {
                if (hp <= 0)
                {
                    isDead = true;
                }

                return isDead;
            }
            public int AttackCount()
            {
                return attckCount;
            }

            public int SuccessAttackCount()
            {
                return attackSucces;
            }
        }

        public class Unit:Character
        {
            public Unit() : base("유닛",100, 100, 30) 
            {

            }
        }

        public class Orc:Character
        {
            public Orc():base ("오크", 150, 150, 25)
            {

            }
        }

        static void PrintStart(Unit unit, Orc orc)
        {
            Console.Clear();
            unit.PrintStatus();
            orc.PrintStatus();  
            Console.WriteLine("------------------------------");
            Console.WriteLine("적과 조우했습니다. 행동을 선택해주세요.");
            Console.WriteLine("1. 싸운다.");
            Console.WriteLine("2. 대기한다.");
            Console.WriteLine("------------------------------");
        } 

        static void PrintScene(Unit unit, Orc orc)
        {
            Scene scene = Scene.start;
            while(true)
            {
                switch(scene)
                {
                    case Scene.start:
                        PrintStart(unit, orc);
                        scene = StartScene();
                        break;

                    case Scene.fight: 
                        scene = FightScene(unit,orc);
                        break;
                    
                    case Scene.pause:
                        scene = PauseScene();
                        break;

                    case Scene.end:
                        scene = EndScene(unit, orc);
                        break;

                }
           
            }
        }

        static Scene StartScene()
        {
            
           while(true) 
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.D1:
                       Console.WriteLine("몬스터와 전투를 시작합니다.");
                       Console.ReadKey();
                    return Scene.fight;
                   
                    case ConsoleKey.D2:
                        Console.WriteLine("대기합니다.");
                        Console.ReadKey();
                        return Scene.pause;
                   
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        return Scene.start;
                }
            }

        }

        static Scene FightScene(Unit unit, Orc orc)
        {
            while (true)
            {
               //플레이어 턴
                Console.Clear();
                unit.PrintStatus();
                orc.PrintStatus();
                Console.WriteLine("-----------------------------");
                Console.WriteLine("플레이어의 턴입니다.");
                Console.WriteLine("1.공격");
                Console.WriteLine("2.대기");
                Console.WriteLine("-----------------------------");
                unit.attckCount++;

                ConsoleKeyInfo consoleKeyInfo = new ConsoleKeyInfo();
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.D1:
                        
                        Position playerAttackPos = ChoosePosition("공격할 위치를 선택해주세요");
                        Position monsterDefensePos = RandomPosition();

                        DamageToMonster(unit,orc, playerAttackPos, monsterDefensePos);
                        Console.ReadKey();

                        if(orc.IsDead()) //몬스터 사망시 
                        {
                            Console.WriteLine("몬스터가 사망했습니다!");
                            Console.ReadKey();

                            return Scene.end;
                        }
                        else // 몬스터 턴
                        {
                            Position monsterAttackPos = ChoosePosition("방어할 위치를 선택해주세요");
                            Position playerDefensePos = RandomPosition();

                            DamageToPlayer(unit,orc, monsterAttackPos, playerDefensePos);
                            Console.ReadKey();

                            if(unit.IsDead())//플레이어 사망시
                            {
                                Console.WriteLine("플레이어가 사망했습니다...");
                                Console.ReadKey();

                                return Scene.end; 
                            }
                        }
                        break;

                    case ConsoleKey.D2:
                        Console.WriteLine("대기합니다.");
                        Console.ReadKey();
                        return Scene.start;

                   


                }
            }
        }

        static Scene PauseScene()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------");
            Console.WriteLine("대기 중...");
            Console.WriteLine("-----------------------------");
            Console.ReadKey();
            return Scene.start;
        }

        static Scene EndScene(Unit unit, Orc orc)
        {
            Console.Clear();
            Console.WriteLine("-----------------------------");
            Console.WriteLine("게임 종료!");
            Console.WriteLine($"플레이어 총 공격 횟수 : {unit.AttackCount()}");
            Console.WriteLine($"플레이어 공격 성공 횟수 : {orc.SuccessAttackCount()}");
            Console.WriteLine($"플레이어 명중률 :{(unit.SuccessAttackCount() * 100.0) / orc.AttackCount():F1} %");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("");
            Console.WriteLine("아무키나 누르면 처음으로 돌아갑니다.");
            return Scene.start;
        }

        
        static void DamageToMonster(Unit unit, Orc orc, Position playerAttackPos, Position monsterDefensePos) //플레이어가 몬스터 공격
        {
            Console.Clear();
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"플레이어 공격 위치 : {playerAttackPos}");
            Console.WriteLine($"몬스터 방어 위치   : {monsterDefensePos}");
            Console.WriteLine("-----------------------------");

            if(playerAttackPos == monsterDefensePos)
            {
                Console.WriteLine("공격 실패...몬스터가 방어했습니다.");
            }
            else
            {
                orc.hp -= unit.att;
                unit.attackSucces++;
                Console.WriteLine("공격 성공 !");
                Console.WriteLine($"{unit.att}의 데미지!");
            }
        }
        static void DamageToPlayer(Unit unit, Orc orc, Position monsterAttackPos, Position playerDefensePos) //몬스터가 플레이어 공격
        {
            Console.Clear();
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"몬스터 공격 위치   : {monsterAttackPos}");
            Console.WriteLine($"플레이어 방어 위치 : {playerDefensePos}");
            Console.WriteLine("-----------------------------");

            if(monsterAttackPos == playerDefensePos)
            {
                Console.WriteLine("방어 성공 ! 플레이어가 방어했습니다!");  
            }
            else
            {
                unit.hp -= orc.att;
                Console.WriteLine("방어실패...");
                Console.WriteLine($"{orc.att}의 데미지...");
            }

        }

        static Position ChoosePosition(string message)
        {
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-----------------------------");
                Console.WriteLine("공격할 위치를 선택해주세요");
                Console.WriteLine("1.상단");
                Console.WriteLine("2.중단");
                Console.WriteLine("3.하단");
                Console.WriteLine("-----------------------------");
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("상단");
                        return Position.상단;
                    case ConsoleKey.D2:
                        Console.WriteLine("중단");
                        return Position.중단;
                    case ConsoleKey.D3:
                        Console.WriteLine("하단");
                        return Position.하단;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        break;

                }

            }
        } // 상,중,하단 선택
        static Position RandomPosition()
        {
            int rand = random.Next(0, 3);
            return (Position)rand;
        }

        static void Main()
        {
            Unit unit = new Unit();
            Orc orc = new Orc();
           
            PrintScene(unit, orc);
        }
    }
}
