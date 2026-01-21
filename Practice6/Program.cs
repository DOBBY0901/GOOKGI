using System;
using System.Collections.Generic;
using System.Linq;
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

    //덱 관리
    public class Deck
    {
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
            cards.Clear();

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(suit, rank));
                }
            }
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
            return $"{Suit}, {Rank}";
        }

    }

    //플레이어 관리
    public class Player
    {

    }

    //게임 시스템 관리
    public class GameManager
    {
        
    }

    /// <summary>
    /// 클래스를 통해 객체를 만들고 매서드 호출
    /// </summary>
        public class Program
    {
        static void Main()
        {
            Deck deck = new Deck();

            Console.WriteLine($"카드 수 : {deck.Cards.Count}");
            Console.WriteLine();

            foreach (var card in deck.Cards)
            {
                Console.WriteLine(card);
            }

            Console.ReadLine();
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