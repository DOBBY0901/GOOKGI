using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace WriteEx //누가 만들었는지 명시
{
   enum Place
    {
        start,
        town,
        river,
        plain,
        fight
    }

    class UnitParents
    {
        protected string name = "";
        protected int lv = 3;
        protected int hp = 30;
        protected int maxhp = 30;
        protected int att = 3;
        protected int currnethp = 30;
        
        public void Damage()
        {
            hp -= att;
            Console.WriteLine($"공격성공! {att}의 데미지!");
            Console.WriteLine($"{name}의 체력 : {hp}/{maxhp}");    
        }
        public int GetAtt()
        {
            return att;
        }
        public void GetCurrenthp()
        {
            hp = currnethp;
        }
        public int Currenthp()
        {
            return hp;
        }

        public void SetName(string newname)
        {
           name = newname;
        }

        public string GetName()
        {
            return name;
        }
    }

    class Player : UnitParents
    {
       
        public void PrintStatus()
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"플레이어 레벨 : {lv}");
            Console.WriteLine($"플레이어 체력 : {hp}/{maxhp}");
            Console.WriteLine($"플레이어 공격력 : {att}");
            Console.WriteLine("---------------------------------------------");
        }

        
    }

    class Monster : UnitParents
    {
     

    }


    internal class Program
    {
        static void StartTextPrint(Player player)
        {
            Console.Clear();
            player.PrintStatus();
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("어디로 가시겠습니까?");
            Console.WriteLine("1. 마을");
            Console.WriteLine("2. 강");
            Console.WriteLine("3. 평원");
            Console.WriteLine("---------------------------------------------");

        }
        static Place StartPoint()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("마을로 향합니다.");
                    Console.ReadKey();
                    return Place.town;

                case ConsoleKey.D2:
                    Console.WriteLine("강으로 향합니다.");
                    Console.ReadKey();
                    return Place.river;

                case ConsoleKey.D3:
                    Console.WriteLine("평원으로 향합니다.");
                    Console.ReadKey();
                    return Place.plain;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                    return Place.start;
            }
        }

        static Place Town(Player player)
        {
            while (true)
            {
                Console.Clear();
                player.PrintStatus();
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("마을에 도착했습니다. 무엇을 하시겠습니까?");
                Console.WriteLine("1. 휴식");
                Console.WriteLine("2. 훈련");
                Console.WriteLine("3. 떠나기");
                Console.WriteLine("---------------------------------------------");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("휴식을 취합니다.");
                        Console.ReadKey();
                        return Place.town;

                    case ConsoleKey.D2:
                        Console.WriteLine("훈련을 진행합니다.");
                        Console.ReadKey();
                        return Place.town;

                    case ConsoleKey.D3:
                        Console.WriteLine("마을을 떠납니다.");
                        Console.ReadKey();
                        return Place.start;
                    
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        return Place.town;
                }
            }      
        }

        static Place River(Player player)
        {
           while(true)
            {
                Console.Clear();
                player.PrintStatus();
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("강에 도착했습니다. 무엇을 하시겠습니까?");
                Console.WriteLine("1. 수영으로 강을 건넌다.");
                Console.WriteLine("2. 배를 기다린다.");
                Console.WriteLine("3. 떠나기");
                Console.WriteLine("---------------------------------------------");

                ConsoleKeyInfo keyInfo = Console.ReadKey();


                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("수영으로 강을 건넙니다.");
                        Console.ReadKey();
                        return Place.river;

                    case ConsoleKey.D2:
                        Console.WriteLine("배를 기다립니다.");
                        Console.ReadKey();
                        return Place.river;

                    case ConsoleKey.D3:
                        Console.WriteLine("강을 떠납니다.");
                        Console.ReadKey();
                        return Place.start;

                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        return Place.river;
                }

            }

          
          }

        static Place Plain(Player player)
        {
          
            while (true)
            {
                Console.Clear();
                player.PrintStatus();
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("평원에 도착했습니다. 적과 마주쳤습니다.");
                Console.WriteLine("1. 싸운다.");
                Console.WriteLine("2. 아이템을 사용한다.");
                Console.WriteLine("3. 도망친다");
                Console.WriteLine("---------------------------------------------");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("적과 전투를 시작합니다.");
                        Console.ReadKey();
                        return Place.fight;

                    case ConsoleKey.D2:
                        Console.WriteLine("아이템을 사용합니다.");
                        Console.ReadKey();
                        return Place.plain;

                    case ConsoleKey.D3:
                        Console.WriteLine("도망칩니다.");
                        Console.ReadKey();
                        return Place.start;

                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        return Place.plain;
                }
            }     
        }

        static void Fight(Player player, Monster monster)
        {
            Monster monster1 = monster;
            
            
                Console.Clear();
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("적과의 전투가 시작되었습니다.");
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 방어");
                Console.WriteLine("3. 도망친다");
                Console.WriteLine("---------------------------------------------");
                player.PrintStatus();

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("공격합니다.");
                        monster.Damage();
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D2:
                        Console.WriteLine("방어합니다.");
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D3:
                        Console.WriteLine("도망칩니다.");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        break;
                }

            }
        

        static void Main(string[] args)
        {
            Place place = Place.start;
            Player player1 = new Player();
            Monster monster1  = new Monster();
            player1.GetAtt();
            monster1.SetName("고블린");
            
            
            while (true)
            {
             
               if(place == Place.start)
                {
                    StartTextPrint(player1);
                    place = StartPoint();
                }

                switch (place)
                {
                    case Place.start:
                        break;

                    case Place.town:
                        place = Town(player1);
                        break;

                    case Place.river:
                        place = River(player1);
                        break;

                    case Place.plain:
                        place = Plain(player1);
                        break;
                    
                    case Place.fight:
                        Fight(player1,monster1);
                        break;

                    default:
                        Console.WriteLine("잘못된 입력입니다");
                        break;
                }
                              
            }
            
        }
        }
}
