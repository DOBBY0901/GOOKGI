using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Threading;
using System.Xml.Serialization;

namespace Practice5_RussianRoulette
{
    enum ItemType
    {
        Heal,
        DoubleDamage
    }

    //총알
    public class Bullet
    {
        public int damage = 1;

        //총알 데미지
        public int BulletDamage()
        {
            return damage;
        }
      
    }

    //약실
    public class Chamber
    {
        public int ChamberCount { get; private set; }
        public bool[] Bullets { get; private set; }
        public int CurrentPos { get; private set; }

        private Random _rand;

        public Chamber(int chamberCount, int bulletCount, Random rand)
        {
            ChamberCount = chamberCount;
            _rand = rand;

            Bullets = new bool[ChamberCount];
            CurrentPos = 0;

            LoadBullets(bulletCount);
        }
        //약실 개수 지정
       

        //총알 위치 설정
        private void LoadBullets(int bulletCount)
        {
            int loaded = 0;
            while (loaded < bulletCount)
            {
                int pos = _rand.Next(0, ChamberCount);
                if (!Bullets[pos])
                {
                    Bullets[pos] = true;
                    loaded++;
                }
            }
        }

        // 발사 판정/탄 제거/약실 회전
        public bool TryFire()
        {
            bool fired = Bullets[CurrentPos];

            if (fired)
                Bullets[CurrentPos] = false;

            CurrentPos = (CurrentPos + 1) % ChamberCount;
            return fired;
        }
    }

    //유닛
    public abstract class Unit
    {
        Dictionary<ItemType, int> inventory = new Dictionary<ItemType, int>();
        public int life = 2;

        //데미지 받음
        public void TakeDamege(int damage)
        {
            life -= damage;
        }

        //아이템 추가
        public void AddItem()
        {

        }

        //아이템 사용
        public void UseItem()
        {

        }

    }

    //플레이어
    public class Player : Unit { }

    //적
    public class Enemy : Unit { }


    //총
    public class Gun
    {
        private Chamber _chamber;
       
        public Gun(Chamber chamber)
        {
            _chamber = chamber;
        }

        //방아쇠 당기기 : Chamber에게 발사 여부를 물어보고, Bullet 데미지를 적용
        public bool PullTrigger(Unit target, Bullet bullet)
        {
            bool fired = _chamber.TryFire();

            if (fired)
            {
                target.TakeDamege(bullet.BulletDamage());
            }

            return fired;
        }

        //총알 발사 약실/총알 리셋을 위해 chamber 교체
        public void ResetChamber(Random rand)
        {
            
            int chamberCount = 6;
            int bulletCount = rand.Next(1, 5); // 1~4
            _chamber = new Chamber(chamberCount, bulletCount, rand);
        }

        //총알 발사, 벅샷룰렛 룰
        public void Shoot(UI ui, Random rand)
        {
            Player player = new Player();
            Enemy enemy = new Enemy();
            Bullet bullet = new Bullet();

            int tryShoot = 0;
            bool playerTurn = true;

            ui.PrintInfo();
            Console.WriteLine("Enter를 누르면 진행합니다.");
            Console.WriteLine("Q를 누르면 종료합니다.");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Clear();

                ui.PrintStatus(player,enemy);  
                //종료
                if (key.Key == ConsoleKey.Q)
                {
                    Console.WriteLine("게임 종료...");
                    break;
                }

                //엔터 이외의 키를 누르면 출력
                if (key.Key != ConsoleKey.Enter)
                {
                    Console.WriteLine("잘못된 키를 누르셨습니다.");
                    Console.WriteLine("Enter를 누르면 진행합니다.");
                    Console.WriteLine("Q를 누르면 종료합니다.");
                    continue;
                }
                
                // 현재 턴 
                string who = playerTurn ? "플레이어" : "적";
                Unit self = playerTurn ? (Unit)player : (Unit)enemy;
                Unit other = playerTurn ? (Unit)enemy : (Unit)player;

                ui.PrintTurnHeader(who);

                //총구 방향 선택
                bool aimToSelf;
                if (playerTurn)
                    aimToSelf = ui.SelectTargetForPlayer();      // true면 플레이어에게
                else
                    aimToSelf = ui.SelectTargetForEnemy(rand);   // true면 적 자신에게

                Unit target = aimToSelf ? self : other;
                string targetText = aimToSelf ? "자신" : "상대";
                Console.WriteLine($"{who}(이/가) {targetText}에게 총구를 겨눴습니다.");

                bool fired = PullTrigger(target, bullet);
                tryShoot++;
                ui.ShootScript();

                if (fired)
                {
                    Console.WriteLine("탕!");
                    Console.WriteLine($"{targetText}(이/가) 총알을 맞았습니다... (시도 : {tryShoot}회)");

                    //누가 맞든 즉시 리셋
                    tryShoot = 0;
                    ui.PrintReset();
                    ResetChamber(rand);
                }
                else
                {
                    Console.WriteLine("약실이 비었습니다.");
                    Console.WriteLine($"(시도 : {tryShoot}회)");
                }

                //사망처리
                if (player.life <= 0)
                {
                    Console.WriteLine("플레이어 사망... 패배!");
                    break;
                }
                if (enemy.life <= 0)
                {
                    Console.WriteLine("상대 사망... 승리!");
                    break;
                }

                // 추가턴 규칙: 자기에게 쐈는데 빈 약실이면 턴 유지
                if (aimToSelf && fired == false)
                {
                    Console.WriteLine($">>> {who}(이/가) {who}에게 쐈지만 약실이 비었습니다! 한 번 더 진행합니다.");
                }
                else
                {
                    playerTurn = !playerTurn; // 그 외에는 턴 교대
                }

                Console.WriteLine("Enter : 계속 / Q : 종료");
            }

            Console.ReadKey();
        }
    


}

    //UI
    public class UI
    {
        public void PrintInfo()
        {
            Console.WriteLine("====================================================================================");
            Console.WriteLine("러시안 룰렛입니다.");
            Console.WriteLine("플레이어와 적은 번갈아 격발하며 6개의 약실중 1~4개의 총알이 무작위로 들어가있습니다.");
            Console.WriteLine("각 턴마다 총구 방향을 선택할 수 있습니다. (플레이어 / 적)");
            Console.WriteLine("자기에게 쐈는데 비어있으면, 턴이 1회 더 진행됩니다.");
            Console.WriteLine("플레이어와 적 중 한명이라도 총알을 맞으면 약실과 총알은 리셋됩니다.");
            Console.WriteLine("플레이어와 적 중 한명이 죽을때까지 게임은 반복됩니다.");
            Console.WriteLine("====================================================================================");
            Console.WriteLine("아무 키나 누르면 시작합니다...");
            Console.ReadKey();
            Console.Clear();
        }
        //총알 발사 스크립트 출력
        public void ShootScript()
        {
            Console.Write("찰칵");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Console.WriteLine();
        }

        // 플레이어 총구 방향 선택
        // return true면 "나에게", false면 "상대에게"
        public bool SelectTargetForPlayer()
        {
            while (true)
            {
                Console.Write("총구 방향 선택 : ");  
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("1. 나 ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("2. 상대 ");
                Console.ResetColor();
                Console.WriteLine();

                string input = Console.ReadLine();

                if (input == "1") return true;
                if (input == "2") return false;

                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        // 적 총구 방향 선택
        // return true면 "자기에게", false면 "플레이어에게"
        public bool SelectTargetForEnemy(Random rand)
        {
            // 50% 확률로 자기/상대
            return rand.Next(0, 2) == 0;
        }

        //차례 출력
        public void PrintTurnHeader(string who)
        {
            Console.WriteLine($"[{who} 차례]");
        }

        //플레이어, 적 체력 UI (우측상단 항상출력)
        public void PrintStatus(Player player, Enemy enemy)
        {
            int x = Console.WindowWidth - 20;
            int y = 0;
            
            if (x < 0)
            { 
                x = 0; 
            }


            Console.SetCursorPosition(x, y);
            Console.Write("플레이어 ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("♥");
            Console.ResetColor();
            Console.WriteLine($" : {player.life}");


            Console.SetCursorPosition(x, y + 1);
            Console.Write("  적  ");
            Console.ForegroundColor = ConsoleColor.Red;
            
            Console.Write("   ♥");
            Console.ResetColor();
            Console.WriteLine($" : {enemy.life}");

            // 커서를 다시 본문 시작 위치로
            Console.SetCursorPosition(0, 4);
        }

        //리셋 UI
        public void PrintReset()
        {
            Console.WriteLine();
            Console.WriteLine(">>> 총알이 발사되었습니다! 약실과 총알을 리셋합니다.");
            Console.WriteLine();
        }
    }
    internal class Program
    {
        static void GameManager()
        {
            Random rand = new Random();
            int chamberCount = 6;
            int bulletCount = rand.Next(1, 5);

            Chamber chamber = new Chamber(chamberCount, bulletCount, rand);
            Gun gun = new Gun(chamber);
            UI ui = new UI();

            gun.Shoot(ui, rand);

        }
        static void Main()
        {
            GameManager();
        }
    }
}


/*
 리볼버 - 6개의 약실 중 1개의 약실에만 총알이 들어가있다. ㅇ
          총알은 특정 약실 위치에 고정 ㅇ 
          플레이어는 방아쇠를 당기며 약실에 총알이 없을 때 방아쇠를 당기면 죽지 않음 ㅇ
          총알이 있으면 사망. ㅇ

추가사항
1.약실을 플레이어가 초기에 늘릴수있음 ㅇ
-변수를 통해 약실 개수 변형 가능 ㅇ

2. 총알을 여러발 넣는게 가능하게 ㅇ
-ex) 20개의 약실에 총알 5발 등 ㅇ

3. 혼자서, 1:1,ㅇ 여러명 가능하게 
-여러명일 경우(ex 5명)
총알이 발사되어 1명이 사망 - 4명
약실에 총알을 다시 넣고
최후 생존자가 나올때까지 or 내가 죽을 때 까지

4. 러시안 룰렛인데 아이템이 있게 (ex 게임 벅샷룰렛)
-플레이어가 가진 목숨 ㅇ
-아이템 (약실에 총알이 있는지 없는지 확인 등등)
-특정 난이도의 AI는 라이프가 더 많다. (아이템 사용도 함)

5. 내가 스스로 방아쇠를 당긴 후
-한번 더 당길지 or 턴을 넘길지 선택

 */