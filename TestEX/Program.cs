using System;

public class Program
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

    public class Player
    {
        public string name = "전사";
        public int hp = 100;
        public int maxhp = 100;
        public int att = 30;

        public void PrintStatus()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"플레이어 이름   : {name}");
            Console.WriteLine($"플레이어 체력   : {hp}/{maxhp}");
            Console.WriteLine($"플레이어 공격력 : {att}");
            Console.WriteLine("------------------------------");
        }

        public bool IsDead() 
        {           
            return hp <= 0; 
        }
    }

    public class Monster
    {
        public string name = "슬라임";
        public int hp = 100;
        public int maxhp = 100;
        public int att = 30;

        public void PrintStatus()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"몬스터 이름     : {name}");
            Console.WriteLine($"몬스터 체력     : {hp}/{maxhp}");
            Console.WriteLine($"몬스터 공격력   : {att}");
            Console.WriteLine("------------------------------");
        }

        public bool IsDead() 
        {           
            return hp <= 0; 
        }
    }

    // 시작 화면 출력
    static void PrintMessage(Player player, Monster monster)
    {
        Console.Clear();
        player.PrintStatus();
        monster.PrintStatus();

        Console.WriteLine("몬스터와 조우했습니다. 행동을 선택해주세요.");
        Console.WriteLine("1. 싸운다.");
        Console.WriteLine("2. 대기한다.");
        Console.WriteLine("------------------------------");
    }

    static Scene StartScene() 
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey();

        switch (keyInfo.Key)
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

    // 상단/중단/하단 선택(공격/방어 공용)
    static Position ChoosePosition(string message)
    {     
        while (true)
        {
            Console.Clear();
            Console.WriteLine("------------------------------");
            Console.WriteLine(message);
            Console.WriteLine("1. 상단");
            Console.WriteLine("2. 중단");
            Console.WriteLine("3. 하단");
            Console.WriteLine("------------------------------");

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.D1: 
                    return Position.상단;
                case ConsoleKey.D2: 
                    return Position.중단;
                case ConsoleKey.D3: 
                    return Position.하단;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static Position RandomPosition()
    {   
        int rand = random.Next(1, 4);
        return (Position)rand;
    }


    static void ApplyDamageToMonster(Player player, Monster monster, Position attackPos, Position monsterDefPos)
    {
        Console.WriteLine("------------------------------");
        Console.WriteLine($"플레이어 공격 위치: {attackPos}");
        Console.WriteLine($"몬스터 방어 위치  : {monsterDefPos}");

        if (attackPos == monsterDefPos)
        {
            Console.WriteLine("몬스터 방어 성공! 공격이 무효가 되었습니다.");
        }
        else
        {
            monster.hp -= player.att;
            if (monster.hp < 0) monster.hp = 0;
            Console.WriteLine($"공격 성공! {player.att} 데미지!");
            Console.WriteLine($"몬스터 체력: {monster.hp}/{monster.maxhp}");
        }
    }

    static void ApplyDamageToPlayer(Player player, Monster monster, Position attackPos, Position playerDefPos)
    {
        Console.WriteLine($"몬스터 공격 위치: {attackPos}");
        Console.WriteLine($"플레이어 방어 위치: {playerDefPos}");

        if (attackPos == playerDefPos)
        {
            Console.WriteLine("플레이어 방어 성공! 공격이 무효가 되었습니다.");
        }
        else
        {
            player.hp -= monster.att;
            if (player.hp < 0) player.hp = 0;
            Console.WriteLine($"피격! {monster.att} 데미지!");
            Console.WriteLine($"플레이어 체력: {player.hp}/{player.maxhp}");
        }
    }

    static Scene FightScene(Player player, Monster monster)
    {
        while (true)
        {
            // 승패 체크
            if (monster.IsDead())
            {
                Console.Clear();
                Console.WriteLine("몬스터가 쓰러졌습니다! 승리!");
                Console.ReadKey();
                return Scene.end;
            }
            if (player.IsDead())
            {
                Console.Clear();
                Console.WriteLine("플레이어가 쓰러졌습니다... 패배...");
                Console.ReadKey();
                return Scene.end;
            }

            // 플레이어 턴
            Console.Clear();
            player.PrintStatus();
            monster.PrintStatus();
            Console.WriteLine("플레이어 턴입니다.");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 대기");
            Console.WriteLine("------------------------------");

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                    {
                        // 플레이어 공격 위치 선택
                        Position playerAttackPos = ChoosePosition("공격할 위치를 선택해주세요");

                        // 몬스터는 랜덤 방어
                        Position monsterDefPos = RandomPosition();

                        Console.Clear();
                        ApplyDamageToMonster(player, monster, playerAttackPos, monsterDefPos);
                        Console.WriteLine("------------------------------");
                        Console.ReadKey();

                        // 몬스터 죽으면 여기서 종료
                        if (monster.IsDead())
                        {
                            Console.Clear();
                            Console.WriteLine("몬스터가 쓰러졌습니다! 승리!");
                            Console.ReadKey();
                            return Scene.end;
                        }

                        // 몬스터 턴(턴제: 플레이어 공격 후 몬스터 공격)
                        Console.Clear();
                        player.PrintStatus();
                        monster.PrintStatus();
                        Console.WriteLine("몬스터 턴입니다.");

                        // 몬스터는 랜덤 공격
                        Position monsterAttackPos = RandomPosition();

                        // 플레이어는 방어 위치 선택
                        Position playerDefPos = ChoosePosition("방어할 위치를 선택해주세요");

                        Console.Clear();
                        ApplyDamageToPlayer(player, monster, monsterAttackPos, playerDefPos);
                        Console.WriteLine("------------------------------");
                        Console.ReadKey();

                        // 플레이어 죽으면 종료
                        if (player.IsDead())
                        {
                            Console.Clear();
                            Console.WriteLine("플레이어가 쓰러졌습니다... 패배...");
                            Console.ReadKey();
                            return Scene.end;
                        }

                        break;
                    }

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

    static Scene PauseScene()
    {
        Console.Clear();
        Console.WriteLine("대기 중...");
        Console.WriteLine("아무 키나 누르면 시작 화면으로 돌아갑니다.");
        Console.ReadKey();
        return Scene.start;
    }

    static void Main()
    {
        Player player1 = new Player();
        Monster monster1 = new Monster(); 

        Scene scene = Scene.start;

        while (true)
        {
            switch (scene)
            {
                case Scene.start:
                    PrintMessage(player1, monster1);
                    scene = StartScene();
                    break;

                case Scene.fight:
                    scene = FightScene(player1, monster1);
                    break;

                case Scene.pause:
                    scene = PauseScene();
                    break;

                case Scene.end:
                    Console.Clear();
                    Console.WriteLine("게임 종료!");
                    Console.ReadKey();
                    return;
            }
        }
    }
}

