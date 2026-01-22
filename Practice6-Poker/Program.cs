using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Practice6
{
    //카드의 수트 (스페이드, 다이아몬드, 하트, 클로버)
    public enum Suit
    {
        Spades  = 0,
        Diamond = 1,
        Heart   = 2,
        Clover  = 3
    }

    //카드의 숫자 (A, 2~10, J, Q, K)
    public enum Rank
    {
        Ace   = 1,
        Two   = 2,
        Three = 3,
        Four  = 4,
        Five  = 5,
        Six   = 6,
        Seven = 7,
        Eight = 8,
        Nine  = 9,
        Ten   = 10,
        Jack  = 11,
        Queen = 12,
        King  = 13
    }

    //카드의 족보
    public enum HandRank
    {
        HighCard = 1,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush
    }

    //덱 관리
    public class Deck
    {
        Random rand = new Random();
        private List<Card> cards = new List<Card>();

        //읽기전용, Deck클래스 밖에서 수정불가
        public IReadOnlyList<Card> Cards => cards;

        public Deck()
        {
            CreateDeck();
        }

        // 52장 생성
        private void CreateDeck()
        {
            cards.Clear(); //덱 생성시 기존 덱 초기화

            foreach (Suit suit in Enum.GetValues(typeof(Suit))) //수트 eunm 추가
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank))) //랭크 eunm 추가
                {
                    cards.Add(new Card(suit, rank)); //카드 추가
                }
            }

        }

        //카드 섞기 Fisher - Yates 알고리즘 사용 (무작위로 섞는 알고리즘, 카드섞기, 음악재생프로그램에서 무작위 선택등에 사용)
        public void Shuffle()
        {
            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);

                // 섞기
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        //카드 뽑기
        public Card Draw()
        {
            // 덱이 비었을 때 예외 처리
            if (cards.Count == 0)
            {
                return null;
            }

            Card topCard = cards[0]; //맨 위의 카드 뽑기
            cards.RemoveAt(0); //덱에서 카드 제거
            return topCard; // 뽑은 카드 return
        }


    }

    //카드 관리
    public class Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }
        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }
        public override string ToString()
        {
            return $"{GetSuitSymbol()} {GetRankString()}";
        }
        //문양으로 변경
        private string GetSuitSymbol()
        {
            switch (Suit)
            {
                case Suit.Spades : return "♠";
                case Suit.Diamond: return "◆";
                case Suit.Heart: return "♥";
                case Suit.Clover: return "♣";
                default: return "?";
            }
        }

        //A,J,Q,K로 변경
        private string GetRankString()
        {
            switch (Rank)
            {
                case Rank.Ace: return "A";
                case Rank.Jack: return "J";
                case Rank.Queen: return "Q";
                case Rank.King: return "K";
                default: return ((int)Rank).ToString();
            }
        }


    }

    //플레이어 관리
    public class Player
    {
        public string Name { get; private set; }
        private List<Card> hand = new List<Card>();

        public List<Card> Hand => hand;

        public Player(string name)
        {
            Name = name;
        }

        // 덱에서 받은 카드를 손패에 추가
        public void ReceiveCard(Card card)
        {
            hand.Add(card);
        }

        // 다음 라운드 대비 초기화
        public void ClearHand()
        {
            hand.Clear();
        }
    }


    //게임 시스템 관리
    public class GameManager
    {
        private Deck deck;
        private List<Player> players = new List<Player>();
        private CheckHandRanking checkHandRanking = new CheckHandRanking();

        public GameManager()
        {
            deck = new Deck();
        }

        //새 플레이어 추가
        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        //게임 시작
        public void StartGame(int cardsPerPlayer)
        {
            deck = new Deck(); //덱 생성
            deck.Shuffle(); // 덱 섞기

            foreach (var p in players)
            { 
                p.ClearHand(); //플레이어 손 비우기
            }

            Deal(cardsPerPlayer); //플레이어 카드 배분
            ShowHands(); //패 보여주기
            ShowWinner(); //승자 출력
        }

        //플레이어들에게 카드 배분
        private void Deal(int cardsPerPlayer)
        {
            // 1장씩 카드를 골고루 배분
            for (int i = 0; i < cardsPerPlayer; i++)
            {
                foreach (var p in players)
                {
                    Card card = deck.Draw();
                    if (card == null) return; // 덱이 비면 종료(간단 처리)
                    
                    p.ReceiveCard(card);
                }
            }
        }

        //손패 보기
        private void ShowHands()
        {
            foreach (var p in players)
            {
                Console.WriteLine($"[{p.Name}]");
                
                foreach (var c in p.Hand)
                {
                    Console.WriteLine(c);
                }
                Console.WriteLine();
            }
        }

        //승자 비교
        private void ShowWinner()
        {
            Player winner = players[0]; 
            HandValue bestValue = checkHandRanking.Evaluate5(players[0].Hand);

            for (int i = 1; i < players.Count; i++)
            {
                HandValue curValue = checkHandRanking.Evaluate5(players[i].Hand);

                int result = checkHandRanking.CompareRanking(curValue, bestValue);

                if(result > 0)
                {
                    bestValue = curValue;
                    winner = players[i];
                }
            }

            Console.WriteLine("===== 결과 =====");
            Console.WriteLine($"승자: {winner.Name}");
            Console.WriteLine($"족보: {bestValue.RankName} ({bestValue.Detail})");
            Console.WriteLine("===============");
        }

        // 플레이어 초기화
        public void ResetPlayers()
        {
            players.Clear();
        }

        // count명 만큼 플레이어 생성
        public void CreatePlayers(int count)
        {
            players.Clear(); // 중요: 누적 방지
            for (int i = 1; i <= count; i++)
            {
                players.Add(new Player($"플레이어 {i}"));
            }
        }

    }

    //패의 가치를 비교하는 클래스
    public class HandValue
    {
        public HandRank Rank { get; set; }

        // 비교 우선순위가 높은 숫자부터 넣는 리스트
        public List<int> Tiebreakers { get; set; } = new List<int>();
        
        //랭크 이름 
        public string RankName { get; set; }

        //왜 이겼는지 
        public string Detail { get; set; }
    }

    //족보의 비교 클래스
    public class CheckHandRanking
    {
        //Ace를 14로 취급해서 비교하기 위한 함수
        private int AceValue(Rank rank)
        {
            return rank == Rank.Ace ? 14 : (int)rank;
        }

        //카드의 수트를 비교하는 함수 스페이드 > 다이아 > 하트 > 클로버
        private int SuitValue(Suit suit)
        {
            switch (suit)
            {
                case Suit.Spades:
                    return 4;

                case Suit.Diamond:
                    return 3;

                case Suit.Heart:
                    return 2;

                case Suit.Clover:
                    return 1;

                default:
                    return 0;

            }
        }

        //각 카드의 힘
        private int CardPower(Card card)
        {
            int rank = AceValue(card.Rank); //Ace = 14
            int suit = SuitValue(card.Suit); // 스다하클 순서

            return rank * 10 + suit; //숫자가 우선, 숫자가 같으면 수트(무늬)로 비교
        }

        //파이브 포커 비교 - 어떤카드, 족보로 이겼는지 출력
        public HandValue Evaluate5(List<Card> hand)
        {
           List<int> powers = new List<int>(); //CardPower담는 리스트

            //5장 카드 각각 power계산해서 넣기
            for (int i = 0; i < hand.Count; i++)
            {
                int power = CardPower(hand[i]);
                powers.Add(power);
            }

            //powers 내림차순 정렬(가장 큰것이 맨앞으로)
            powers.Sort();
            powers.Reverse();
            HandValue handValue = new HandValue();

            //플러시 변환
            if(IsFlush(hand))
            {
                handValue.Rank = HandRank.Flush;
                handValue.Tiebreakers = powers;
                handValue.RankName = "플러시";
                handValue.Detail = $"{PowerToText(powers[0])} 플러시";

                return handValue;
            }

            //하이카드 변환
            handValue.Rank = HandRank.HighCard;
            handValue.Tiebreakers = powers;
            handValue.RankName = "하이카드";
            handValue.Detail = $"{PowerToText(powers[0])} 하이";

            return handValue;
        }

        //족보 비교
        public int CompareRanking(HandValue handA, HandValue handB)
        {
            if(handA.Rank > handB.Rank)
            {
                return 1;
            }
            if(handA.Rank < handB.Rank)
            {
                return -1;
            }

            //같은 족보면 Tiebreaker 앞에서부터 비교
            int count = handA.Tiebreakers.Count;

            if (handB.Tiebreakers.Count < count)
            {
                count = handB.Tiebreakers.Count;
            }

            for(int i = 0; i < count;i++)
            {
                if (handA.Tiebreakers[i] > handB.Tiebreakers[i])
                {
                    return 1;
                }

                if (handA.Tiebreakers[i] < handB.Tiebreakers[i])
                {
                    return -1;
                }
            }
                return 0;
        }

        //플러시 판정
        private bool IsFlush(List<Card> hand)
        {
            Suit firstSuit = hand[0].Suit;

            for (int i = 0; i < hand.Count; i++)
            {
                if(hand[i].Suit != firstSuit)
                {
                    return false;
                }
            }

            return true;
        }

        //스트레이트 판정
        private bool IsStraight(List<Card> hand,int highRank)
        {
            List<int> ranks = new List<int>();
            
            for (int i = 0; i < hand.Count; i++)
            {
                ranks.Add(AceValue(hand[i].Rank));
            }

            ranks.Sort();
            ranks.Reverse();

            for(int i = 0; i< ranks.Count -1; i++)
            {
                if (ranks[i] == ranks[i+1])
                {
                    highRank = 0;
                    return false;
                }
            }
            return true;
        }

        //텍스트
        private string PowerToText(int power)
        {
            int rankValue = power / 10;  // 14~2
            int suitValue = power % 10;  // 4~1

            string rankText;
            if (rankValue == 14) rankText = "A";
            else if (rankValue == 13) rankText = "K";
            else if (rankValue == 12) rankText = "Q";
            else if (rankValue == 11) rankText = "J";
            else rankText = rankValue.ToString();

            string suitText;
            if (suitValue == 4) suitText = "♠";
            else if (suitValue == 3) suitText = "◆";
            else if (suitValue == 2) suitText = "♥";
            else if (suitValue == 1) suitText = "♣";
            else suitText = "?";

            return suitText + rankText; // 예: ♠A, ◆10
        }

    }
    /// <summary>
    /// 클래스를 통해 객체를 만들고 매서드 호출
    /// </summary>
    public class Program
    {
        static void Main()
        {
            GameManager gameManager = new GameManager();
            int cardsPerPlayer;
            int input;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("포커입니다.");
                Console.WriteLine("플레이 인원 지정 (2~6)");

                if (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("숫자를 입력하세요.");
                    Console.ReadKey();
                    continue;
                }

                if (input < 2 || input > 6)
                {
                    Console.WriteLine("적절한 인원수를 지정해주십시오.");
                    Console.ReadKey();
                    continue;
                }

                // 플레이어 생성
                gameManager.CreatePlayers(input);

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("게임 모드 선택");
                    Console.WriteLine("1. 파이브 포커 (5장)");
                    Console.WriteLine("2. 세븐 포커 (7장)");

                    int mode = int.Parse(Console.ReadLine());

                    if (mode == 1)
                    {
                        cardsPerPlayer = 5;
                        Console.WriteLine("파이브포커를 선택하셨습니다.");
                        break;
                    }
                    else if (mode == 2)
                    {
                        cardsPerPlayer = 7;
                        Console.WriteLine("세븐 포커를 선택하셨습니다.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                    }
                }

                gameManager.StartGame(cardsPerPlayer);

                Console.ReadKey();

            }


        }

    }


}

/*
 포커 만들기
세븐 포커 - 각 패 7장
파이브 포커 - 각 패 5장
조커 x

1. 카드 패
스페이드
다이아
하트
클로버

각 A, 2~10, J,Q,K 
13장 * 4 = 52장이 섞여있어야 함.

2. 나와 다른 플레이어에게 골고루 나누어주어야 함.

3. 족보 - 나중에
 */