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
            if (players.Count == 0) return;

            // 각 플레이어 평가 결과 저장
            List<HandValue> values = new List<HandValue>();

            for (int i = 0; i < players.Count; i++)
            {
                HandValue hv = checkHandRanking.Evaluate5(players[i].Hand);
                values.Add(hv);
            }

            // 승자 찾기
            int winnerIndex = 0;
            for (int i = 1; i < players.Count; i++)
            {
                int result = checkHandRanking.CompareRanking(values[i], values[winnerIndex]);
                
                if (result > 0)
                {
                    winnerIndex = i;
                }
                    
            }

            for (int i = 0; i < players.Count; i++)
            {
                bool needSuit = false;

                // 내 (족보, 대표랭크)와 같은 사람이 있는지 검사
                int myRank = (int)values[i].Rank;
                int myKeyRank = values[i].Tiebreakers.Count > 0 ? (values[i].Tiebreakers[0] / 10) : 0;

                for (int j = 0; j < players.Count; j++)
                {
                    if (i == j) continue;

                    int otherRank = (int)values[j].Rank;
                    int otherKeyRank = values[j].Tiebreakers.Count > 0 ? (values[j].Tiebreakers[0] / 10) : 0;

                    if (myRank == otherRank && myKeyRank == otherKeyRank)
                    {
                        needSuit = true;
                        break;
                    }
                }

                string detail = values[i].Detail;

                // 수트가 필요하면 Detail 뒤에 (대표카드 수트포함) 추가 표시
                if (needSuit && values[i].Tiebreakers.Count > 0)
                {
                    detail += $" [{checkHandRanking.PowerToText(values[i].Tiebreakers[0])}]";
                    // PowerToText가 private이라면 public wrapper를 하나 만들면 됨(아래 참고)
                }

                string mark = (i == winnerIndex) ? " <== 승자" : "";
                Console.WriteLine($"{players[i].Name} : {values[i].RankName} ({detail}){mark}");
            }

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
        //그룹핑 - 딕셔너리
        private Dictionary<int, List<Card>> GroupByRank(List<Card> hand)
        {
            Dictionary<int, List<Card>> groups = new Dictionary<int, List<Card>>();

            for (int i = 0; i < hand.Count; i++)
            {
                int rv = AceValue(hand[i].Rank); // A=14

                if (!groups.ContainsKey(rv))
                    groups[rv] = new List<Card>();

                groups[rv].Add(hand[i]);
            }

            return groups;
        }

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

            //스트레이트 플러시 변환 (Flush && Straight)
            int straightFlushHighPower;
            if (IsFlush(hand) && IsStraight(hand, out straightFlushHighPower))
            {
                handValue.Rank = HandRank.StraightFlush;
                handValue.Tiebreakers = new List<int>();
                handValue.Tiebreakers.Add(straightFlushHighPower);

                handValue.RankName = "스트레이트 플러시";
                handValue.Detail = $"{RankToText(PowerToRankValue(straightFlushHighPower))} 스트레이트 플러시";

                return handValue;
            }

            //포카드 변환
            int fourPower, kickerPower;
            if (TryGetFourOfAKind(hand, out fourPower, out kickerPower))
            {
                handValue.Rank = HandRank.FourOfAKind;
                handValue.Tiebreakers = new List<int>();
                handValue.Tiebreakers.Add(fourPower);
                handValue.Tiebreakers.Add(kickerPower);

                handValue.RankName = "포카드";
                handValue.Detail = $"{RankToText(fourPower / 10)} 포카드";
                return handValue;
            }

            //풀하우스 변환
            int fullTriplePower, fullPairPower;
            if (TryGetFullHouse(hand, out fullTriplePower, out fullPairPower))
            {
                handValue.Rank = HandRank.FullHouse;
                handValue.Tiebreakers = new List<int>();
                handValue.Tiebreakers.Add(fullTriplePower);
                handValue.Tiebreakers.Add(fullPairPower);

                handValue.RankName = "풀하우스";
                handValue.Detail = $"{RankToText(fullTriplePower / 10)} 풀하우스";
                return handValue;
            }


            //플러시 변환
            if (IsFlush(hand))
            {
                handValue.Rank = HandRank.Flush;
                handValue.Tiebreakers = powers;
                handValue.RankName = "플러시";
                handValue.Detail = $"{RankToText(PowerToRankValue(powers[0]))} 플러시";

                return handValue;
            }

            //스트레이트 변환
            int straightHighPower;
            if (IsStraight(hand, out straightHighPower))
            {
                handValue.Rank = HandRank.Straight;
                handValue.Tiebreakers = new List<int>();
                handValue.Tiebreakers.Add(straightHighPower);

                handValue.RankName = "스트레이트";
                handValue.Detail = $"{RankToText(PowerToRankValue(straightHighPower))} 스트레이트";
                return handValue;
            }

            //트리플 변환
            int triplePower;
            List<int> tripleKickers;
            if (TryGetThreeOfAKind(hand, out triplePower, out tripleKickers))
            {
                handValue.Rank = HandRank.ThreeOfAKind;
                handValue.Tiebreakers = new List<int>();
                handValue.Tiebreakers.Add(triplePower);
                handValue.Tiebreakers.Add(tripleKickers[0]);
                handValue.Tiebreakers.Add(tripleKickers[1]);

                handValue.RankName = "트리플";
                handValue.Detail = $"{RankToText(triplePower / 10)} 트리플"; // 수트는 기본 출력에 숨김
                return handValue;
            }

            //투페어 변환
            int highPairPower, lowPairPower;
            if (TryGetTwoPair(hand, out highPairPower, out lowPairPower, out kickerPower))
            {
                handValue.Rank = HandRank.TwoPair;
                handValue.Tiebreakers = new List<int>();
                handValue.Tiebreakers.Add(highPairPower);
                handValue.Tiebreakers.Add(lowPairPower);
                handValue.Tiebreakers.Add(kickerPower);

                handValue.RankName = "투페어";

                // 출력은 랭크만(원하면)
                handValue.Detail = $"{RankToText(highPairPower / 10)} & {RankToText(lowPairPower / 10)} 투페어";
                return handValue;
            }

            //원페어 변환
            int pairPower;
            List<int> kickerPowers;
            if (TryGetOnePair(hand, out pairPower, out kickerPowers))
            {
                handValue.Rank = HandRank.OnePair;
                handValue.Tiebreakers = new List<int>();

                handValue.Tiebreakers.Add(pairPower);        // 페어(랭크 우선, 같으면 수트)
                for (int i = 0; i < kickerPowers.Count; i++) // 키커 3장
                    handValue.Tiebreakers.Add(kickerPowers[i]);

                handValue.RankName = "원페어";
                handValue.Detail = $"{RankToText(PowerToRankValue(pairPower))} 페어";

                return handValue;
            }

            //하이카드 변환
            handValue.Rank = HandRank.HighCard;
            handValue.Tiebreakers = powers;
            handValue.RankName = "하이카드";
            handValue.Detail = $"{RankToText(PowerToRankValue(powers[0]))} 하이";

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
        private bool IsStraight(List<Card> hand, out int highCardPower)
        {
            // 랭크값(A=14) 리스트 만들기
            List<int> ranks = new List<int>();
            for (int i = 0; i < hand.Count; i++)
                ranks.Add(AceValue(hand[i].Rank));

            // 내림차순 정렬
            ranks.Sort();
            ranks.Reverse();

            // 중복 있으면 스트레이트 불가
            for (int i = 0; i < ranks.Count - 1; i++)
            {
                if (ranks[i] == ranks[i + 1])
                {
                    highCardPower = 0;
                    return false;
                }
            }

            // 일반 스트레이트 체크
            bool normalStraight = true;
            for (int i = 0; i < ranks.Count - 1; i++)
            {
                if (ranks[i] - 1 != ranks[i + 1])
                {
                    normalStraight = false;
                    break;
                }
            }

            int highRankValue;

            if (normalStraight)
            {
                highRankValue = ranks[0]; // 예: 9-high면 9
            }
            else
            {
                // A2345 예외: [14,5,4,3,2]
                if (ranks[0] == 14 && ranks[1] == 5 && ranks[2] == 4 && ranks[3] == 3 && ranks[4] == 2)
                {
                    highRankValue = 5; // 휠은 5-high
                }
                else
                {
                    highCardPower = 0;
                    return false;
                }
            }

            // 이제 highRankValue(예: 9 또는 휠이면 5)인 "그 카드"의 무늬까지 포함해서 power 계산
            // 같은 숫자면 suit가 더 높은 게 이기도록 max power를 뽑는다.
            int best = -1;
            for (int i = 0; i < hand.Count; i++)
            {
                int rv = AceValue(hand[i].Rank);
                if (rv == highRankValue) // 최고 숫자 카드 찾기(휠이면 5)
                {
                    int p = CardPower(hand[i]);
                    if (p > best) best = p;
                }
            }

            highCardPower = best;
            return true;
        }

        //원페어 판정
        private bool TryGetOnePair(List<Card> hand, out int pairPower, out List<int> kickerPowers)
        {
            pairPower = 0;
            kickerPowers = new List<int>();

            Dictionary<int, List<Card>> groups = GroupByRank(hand);

            int pairRank = 0;          // 페어 숫자(예: 11)
            List<Card> pairCards = null;

            // 1) 2장짜리 그룹이 정확히 1개인지 찾기
            foreach (var kv in groups)
            {
                if (kv.Value.Count == 2)
                {
                    if (pairRank != 0)
                    {
                        // 2장 그룹이 2개면 투페어라서 원페어 아님
                        return false;
                    }

                    pairRank = kv.Key;
                    pairCards = kv.Value;
                }
            }

            if (pairRank == 0)
                return false; // 페어 없음

            // 2) pairPower = 페어 중 가장 센 카드(수트까지 반영)
            int best = -1;
            for (int i = 0; i < pairCards.Count; i++)
            {
                int p = CardPower(pairCards[i]);
                if (p > best) best = p;
            }
            pairPower = best;

            // 3) 키커 3장 power 구해서 내림차순 정렬
            for (int i = 0; i < hand.Count; i++)
            {
                int rv = AceValue(hand[i].Rank);
                if (rv != pairRank)
                {
                    kickerPowers.Add(CardPower(hand[i]));
                }
            }

            kickerPowers.Sort();
            kickerPowers.Reverse();

            return true;
        }

        //투페어 판정
        private bool TryGetTwoPair(List<Card> hand, out int highPairPower, out int lowPairPower, out int kickerPower)
        {
            highPairPower = 0;
            lowPairPower = 0;
            kickerPower = 0;

            Dictionary<int, List<Card>> groups = GroupByRank(hand);

            // 2장짜리 그룹의 rank들을 수집
            List<int> pairRanks = new List<int>();
            foreach (var kv in groups)
            {
                if (kv.Value.Count == 2)
                    pairRanks.Add(kv.Key);
            }

            // 투페어 조건: 2장 그룹이 정확히 2개
            if (pairRanks.Count != 2)
                return false;

            // 높은 페어 rank / 낮은 페어 rank 결정
            int highPairRank = pairRanks[0];
            int lowPairRank = pairRanks[1];
            if (lowPairRank > highPairRank)
            {
                int tmp = highPairRank;
                highPairRank = lowPairRank;
                lowPairRank = tmp;
            }

            // highPairPower: highPairRank 그룹 중 최고 CardPower
            List<Card> highPairCards = groups[highPairRank];
            int bestHigh = -1;
            for (int i = 0; i < highPairCards.Count; i++)
            {
                int p = CardPower(highPairCards[i]);
                if (p > bestHigh) bestHigh = p;
            }
            highPairPower = bestHigh;

            // lowPairPower: lowPairRank 그룹 중 최고 CardPower
            List<Card> lowPairCards = groups[lowPairRank];
            int bestLow = -1;
            for (int i = 0; i < lowPairCards.Count; i++)
            {
                int p = CardPower(lowPairCards[i]);
                if (p > bestLow) bestLow = p;
            }
            lowPairPower = bestLow;

            // 키커: 남은 1장의 CardPower
            for (int i = 0; i < hand.Count; i++)
            {
                int rv = AceValue(hand[i].Rank);
                if (rv != highPairRank && rv != lowPairRank)
                {
                    kickerPower = CardPower(hand[i]);
                    break;
                }
            }

            return true;
        }

        //트리플 판정
        private bool TryGetThreeOfAKind(List<Card> hand, out int triplePower, out List<int> kickerPowers)
        {
            triplePower = 0;
            kickerPowers = new List<int>();

            Dictionary<int, List<Card>> groups = GroupByRank(hand);

            int tripleRank = 0;
            List<Card> tripleCards = null;

            // 1) 3장짜리 그룹 찾기
            foreach (var kv in groups)
            {
                if (kv.Value.Count == 3)
                {
                    // 트리플이 2개 나올 수는 없지만 안전 처리
                    if (tripleRank != 0) return false;

                    tripleRank = kv.Key;
                    tripleCards = kv.Value;
                }
            }

            if (tripleRank == 0)
                return false; // 트리플 없음

            // 2) 3+2면 풀하우스니까 트리플로 처리하면 안 됨
            foreach (var kv in groups)
            {
                if (kv.Value.Count == 2)
                {
                    return false; // 풀하우스 후보
                }
            }

            // 3) triplePower = 트리플 중 가장 센 카드(CardPower 최대)
            int best = -1;
            for (int i = 0; i < tripleCards.Count; i++)
            {
                int p = CardPower(tripleCards[i]);
                if (p > best) best = p;
            }
            triplePower = best;

            // 4) 키커 2장 뽑아서 내림차순 정렬
            for (int i = 0; i < hand.Count; i++)
            {
                int rv = AceValue(hand[i].Rank);
                if (rv != tripleRank)
                {
                    kickerPowers.Add(CardPower(hand[i]));
                }
            }

            kickerPowers.Sort();
            kickerPowers.Reverse();

            return true;
        }

        //포카드 판정
        private bool TryGetFourOfAKind(List<Card> hand, out int fourPower, out int kickerPower)
        {
            fourPower = 0;
            kickerPower = 0;

            Dictionary<int, List<Card>> groups = GroupByRank(hand);

            int fourRank = 0;
            List<Card> fourCards = null;

            // 1) 4장짜리 그룹 찾기
            foreach (var kv in groups)
            {
                if (kv.Value.Count == 4)
                {
                    fourRank = kv.Key;
                    fourCards = kv.Value;
                    break;
                }
            }

            if (fourRank == 0)
                return false;

            // 2) fourPower = 포카드 중 가장 센 카드(CardPower 최대)
            int best = -1;
            for (int i = 0; i < fourCards.Count; i++)
            {
                int p = CardPower(fourCards[i]);
                if (p > best) best = p;
            }
            fourPower = best;

            // 3) kickerPower = 남은 1장
            for (int i = 0; i < hand.Count; i++)
            {
                int rv = AceValue(hand[i].Rank);
                if (rv != fourRank)
                {
                    kickerPower = CardPower(hand[i]);
                    break;
                }
            }

            return true;
        }

        //풀하우스 판정
        private bool TryGetFullHouse(List<Card> hand, out int triplePower, out int pairPower)
        {
            triplePower = 0;
            pairPower = 0;

            Dictionary<int, List<Card>> groups = GroupByRank(hand);

            int tripleRank = 0;
            int pairRank = 0;
            List<Card> tripleCards = null;
            List<Card> pairCards = null;

            // 1) 3장 그룹 / 2장 그룹 찾기
            foreach (var kv in groups)
            {
                if (kv.Value.Count == 3)
                {
                    tripleRank = kv.Key;
                    tripleCards = kv.Value;
                }
                else if (kv.Value.Count == 2)
                {
                    pairRank = kv.Key;
                    pairCards = kv.Value;
                }
            }

            if (tripleRank == 0 || pairRank == 0)
                return false;

            // 2) triplePower = 트리플 중 max CardPower
            int bestTriple = -1;
            for (int i = 0; i < tripleCards.Count; i++)
            {
                int p = CardPower(tripleCards[i]);
                if (p > bestTriple) bestTriple = p;
            }
            triplePower = bestTriple;

            // 3) pairPower = 페어 중 max CardPower
            int bestPair = -1;
            for (int i = 0; i < pairCards.Count; i++)
            {
                int p = CardPower(pairCards[i]);
                if (p > bestPair) bestPair = p;
            }
            pairPower = bestPair;

            return true;
        }

        //텍스트
        public string PowerToText(int power)
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

        //랭크만 문자열
        private string RankToText(int rankValue)
        {
            if (rankValue == 14) return "A";
            if (rankValue == 13) return "K";
            if (rankValue == 12) return "Q";
            if (rankValue == 11) return "J";
            return rankValue.ToString();
        }

        private int PowerToRankValue(int power)
        {
            return power / 10; // CardPower = rank*10 + suit
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