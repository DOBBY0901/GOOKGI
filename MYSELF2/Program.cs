using System;
using System.Security.Cryptography;
namespace MYSELF2
{
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
            상단 ,
            중단 ,
            하단
        }

        static Random random = new Random();
        
        public class Unit : Character
        {
            public Unit() : base("전사", 100, 100, 30)
            {

            }
        }

        public class Orc : Character
        {
            public Orc() : base("오크", 100, 100, 30)
            {

            }

        }
       
        static void PrintStartScene(Unit player, Orc monster) //초기화면 출력
        {
            Console.Clear();
            player.PrintStatus();
            monster.PrintStatus();

            Console.WriteLine("-----------------------------");
            Console.WriteLine("적과 조우했습니다. 행동을 결정해주세요.");
            Console.WriteLine("1.싸운다.");
            Console.WriteLine("2.대기한다.");
            Console.WriteLine("-----------------------------");
        }
     
        static Scene StartScene() //시작씬
        {
            while (true)
            {
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                
                switch(keyinfo.Key)
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

        static Scene FightScene(Unit player, Orc monster) //전투씬
        {
            while(true)
            {
                // 플레이어 공격 턴
                Console.Clear();
              
                player.PrintStatus();
                monster.PrintStatus() ;
                Console.WriteLine("-----------------------------");
                Console.WriteLine("플레이어의 턴입니다.");
                Console.WriteLine("1.공격");
                Console.WriteLine("2.대기");
                Console.WriteLine("-----------------------------");
                player.attckCount++;
                
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                switch (keyinfo.Key)
                {
                        case ConsoleKey.D1:
                        Position playerAttackPos = ChoosePosition("공격할 위치를 선택해주세요.");
                        Position monsterDefensePos = RandomPosition();

                        ApplyDamageToMonster(player, monster, playerAttackPos, monsterDefensePos);  
                        Console.ReadKey();

                        
                        //몬스터 사망처리
                        if(monster.IsDead())
                        {
                            Console.WriteLine("몬스터가 사망했습니다!");
                            Console.ReadKey();
                            return Scene.end;
                        }

                        else
                        {
                            //몬스터 공격 턴
                            Position monsterAttackPos = ChoosePosition("방어할 위치를 선택해주세요.");

                            Position playerDefensePos = RandomPosition();

                            ApplyDamageToPlayer(player, monster, playerDefensePos, monsterAttackPos);
                            Console.ReadKey();

                            //플레이어 사망처리
                            if(player.IsDead())
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

                        default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        break; 
                }   

            }

        }
        static Scene PauseScene() //대기씬
        {
            Console.Clear();
            Console.WriteLine("-----------------------------");
            Console.WriteLine("대기 중...");
            Console.WriteLine("아무 키나 누르면 처음으로 돌아갑니다.");
            Console.WriteLine("-----------------------------");
            Console.ReadKey();

            return Scene.start;

        }

        static void PlayScene(Unit player, Orc monster)
        {
            Scene scene = Scene.start;
            while (true)
            {

                switch (scene)
                {
                    case Scene.start:
                        PrintStartScene(player, monster);
                        scene = StartScene();
                        break;

                    case Scene.fight:
                        scene = FightScene(player, monster);
                        break;

                    case Scene.pause:
                        scene = PauseScene();
                        break;

                    case Scene.end:
                        scene = EndScene(player, monster);
                        break;
                }
            }
        }

        static Scene EndScene(Unit player,Orc monster) //종료씬
        {
            Console.Clear();
            Console.WriteLine("-----------------------------");
            Console.WriteLine("게임 종료!");
            Console.WriteLine($"플레이어 총 공격 횟수 : {player.AttackCount()}");
            Console.WriteLine($"플레이어 공격 성공 횟수 : {player.SuccessAttackCount()}");
            Console.WriteLine($"플레이어 명중률 :{(player.SuccessAttackCount() * 100.0) / player.AttackCount():F1} %");
            Console.WriteLine("-----------------------------");
            Console.ReadKey();

            return Scene.end;

        }

        static Position ChoosePosition(string message) //상,중,하단 선택
        {
            while (true)
            {
                Console.Clear() ;
                Console.WriteLine("-----------------------------");
                Console.WriteLine(message);
                Console.WriteLine("1. 상단");
                Console.WriteLine("2. 중단");
                Console.WriteLine("3. 하단");
                Console.WriteLine("-----------------------------");

                ConsoleKeyInfo keyinfo = Console.ReadKey();

                switch (keyinfo.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("1.상단");
                        return Position.상단;
                    case ConsoleKey.D2:
                        Console.WriteLine("2.중단");
                        return Position.중단;
                    case ConsoleKey.D3:
                        Console.WriteLine("3.하단");
                        return Position.하단;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey ();
                        break;
                }
            }
        }

        static Position RandomPosition() //무작위 위치
        {
          int rand = random.Next(0,3);
            
            return (Position)rand;
        }

        static void ApplyDamageToPlayer(Unit player, Orc monster, Position monsterAttckPos, Position playerDefensePos) //몬스터가 플레이어 공격
        {
            Console.Clear();
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"몬스터 공격 위치 : {monsterAttckPos}");
            Console.WriteLine($"플레이어 방어 위치 : {playerDefensePos}");
            Console.WriteLine("-----------------------------");

            if (monsterAttckPos == playerDefensePos)
            {
                Console.WriteLine("방어 성공! 공격이 무효화됩니다.");
            }
            else
            {
                player.hp -= monster.att;
                Console.WriteLine($"방어 실패..{monster.att}의 데미지!");
            }
        }

        static void ApplyDamageToMonster(Unit player, Orc monster, Position playerAttckPos, Position monsterDefensePos) //플레이어가 몬스터 공격
        {
            Console.Clear();
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"플레이어 공격 위치 : {playerAttckPos}");
            Console.WriteLine($"몬스터 방어 위치 : {monsterDefensePos}");
            Console.WriteLine("-----------------------------");

            if (playerAttckPos == monsterDefensePos)
            {
                
                Console.WriteLine("공격 실패.. 공격이 무효화됩니다.");
            }
            else
            {
                monster.hp -= player.att;
                player.attackSucces++;
                Console.WriteLine($"공격 성공! {player.att}의 데미지!");
            }
        }

        static void Main()
        {
            Unit player = new Unit();
            Orc monster = new Orc();  
            
            PlayScene(player, monster);
        
        }
    }
}
