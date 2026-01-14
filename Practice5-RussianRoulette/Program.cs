using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Practice5_RussianRoulette
{
    //총기
    public class Gun
    {
        Player player = new Player();
        Bullet bullet = new Bullet();

        public int ChamberCount { get; set; }
        public bool[] Bullets { get;  }
        public int CurrentPos { get; set; }

        private Random _rand;

        public Gun(int chamber,int bullet, Random rand)
        {
           
            ChamberCount = chamber;
            _rand = rand;

            Bullets = new bool[ChamberCount]; // 총알위치
            CurrentPos = 0; // 시작 위치(원하면 랜덤도 가능) 
            LoadBullets(bullet);
            
        }
       
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

        // 방아쇠 당기기 : true면 발사(사망), false면 생존
        public bool PullTrigger()
        {
            bool fired = Bullets[CurrentPos];
            
            if (fired) 
            {
                player.TakeDamege(bullet.BulletDamage()); //총알피격
                Bullets[CurrentPos] = false;
            }
               
            //다음 약실로 회전(회피)
            CurrentPos = (CurrentPos + 1) % ChamberCount;
            return fired;
        }

        //총알 발사
        public void Shoot()
        {
            int tryShoot = 0;

            Console.WriteLine("Enter를 누르면 방아쇠를 당깁니다.");
            Console.WriteLine("Q를 누르면 종료합니다.");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Clear();

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
                    Console.WriteLine("Enter를 누르면 방아쇠를 당깁니다.");
                    Console.WriteLine("Q를 누르면 종료합니다.");
                    continue;
                }

                tryShoot++;
                ShootScript();
                
                bool fired = PullTrigger();

                if (fired)
                {
                        Console.WriteLine("탕!");
                        Console.WriteLine($"총알을 맞았습니다... (시도 : {ChamberCount}/{tryShoot}회)");
                        Console.WriteLine($"남은목숨 : {player.life}");
                    
                    //사망처리
                    if (player.life <= 0)
                    {
                        Console.WriteLine($"사망했습니다...");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine($"생존! (시도 : {ChamberCount}/{tryShoot}회)");
                    Console.WriteLine($"남은목숨 : {player.life}");
                }
                
                Console.WriteLine();
                Console.WriteLine("Enter : 계속 / Q : 종료");
            }

            Console.ReadKey();
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
    }
    //총알
    public class Bullet
    {
        public int damage = 1;

        public int BulletDamage()
        {
            return damage;
        }
    }
    //유닛
    public abstract class Unit
    {
        public int life = 2;
        
        //데미지 받음
       public void TakeDamege(int damage)
        {
            life -= damage;
        }
       
    }
    //플레이어
    public class Player : Unit 
    {     
    

    }
    //적
    public class Enemy : Unit
         
    {

    }

    internal class Program
    {
        //약실의 개수 지정
        static int SetChamber()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("러시안 룰렛입니다.");
                Console.Write("지정할 약실의 개수를 입력해주세요: ");

                if (int.TryParse(Console.ReadLine(), out int chamber) && chamber > 0)
                {
                    Console.WriteLine($"지정한 약실 개수 : {chamber}");
                    return chamber;
                }
                Console.WriteLine("잘못입력하셨습니다. 약실 개수는 1 이상의 숫자여야 합니다.");

                Console.ReadKey();
            }
            
        }

        //총알 개수 지정
        static int SetBulletCount(int chamber)
        {
            while (true)
            {
                Console.Write($"넣을 총알 개수를 입력하세요 (1 ~ {chamber}): ");

                if (int.TryParse(Console.ReadLine(), out int bulletCount) &&
                    bulletCount >= 1 && bulletCount <= chamber)
                {
                    Console.WriteLine($"지정한 총알 개수 : {bulletCount}");
                    return bulletCount;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        static void Main()
        {
            Random rand = new Random();
            int chamber = SetChamber();
            int bulletCount = SetBulletCount(chamber);
            Gun gun = new Gun(chamber, bulletCount,rand);

            gun.Shoot();
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

3. 혼자서, 1:1, 여러명 가능하게
-여러명일 경우(ex 5명)
총알이 발사되어 1명이 사망 - 4명
약실에 총알을 다시 넣고
최후 생존자가 나올때까지 or 내가 죽을 때 까지

4. 러시안 룰렛인데 아이템이 있게 (ex 게임 벅샷룰렛)
-플레이어가 가진 목숨
-아이템 (약실에 총알이 있는지 없는지 확인 등등)
-특정 난이도의 AI는 라이프가 더 많다. (아이템 사용도 함)

5. 내가 스스로 방아쇠를 당긴 후
-한번 더 당길지 or 턴을 넘길지 선택

 */