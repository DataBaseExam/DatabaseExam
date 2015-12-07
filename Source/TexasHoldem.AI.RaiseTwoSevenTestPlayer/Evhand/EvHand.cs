namespace TexasHoldem.AI.RaiseTwoSevenTestPlayer.Evhand
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Logic.Cards;

    public enum PokerHands
    {
        Nothing = 0,
        FlushDraw = 1,
        Pair = 2,
        TwoPair = 3,
        ThreeOfKind = 4,
        Straight = 5,
        Flush = 6,
        FullHouse = 7,
        FourOfKind = 8,
        StraightFlush = 9,
        RoyalFlush = 10
    }

    public class PlayingCard : IComparable<PlayingCard>
    {
        public CardSuit Suit { get; private set; }
        public CardType NominalValue { get; private set; }

        public PlayingCard(CardSuit suit, CardType nominalValue)
        {
            Suit = suit;
            NominalValue = nominalValue;
        }

        public int CompareTo(PlayingCard other)
        {
            if ((int)this.NominalValue > (int)other.NominalValue)
            {
                return 1;
            }
            else if ((int)this.NominalValue < (int)other.NominalValue)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
    public class HandEvaluateRaiseTwo
    {
        private readonly IDictionary<PokerHands, Func<IEnumerable<PlayingCard>, bool>> _rules;
        public IDictionary<PokerHands, Func<IEnumerable<PlayingCard>, bool>> Rules { get { return _rules; } }

        public HandEvaluateRaiseTwo()
        {

            Func<IEnumerable<PlayingCard>, bool> hasPair =
                                  cards => cards.GroupBy(card => card.NominalValue)
                                                .Count(group => group.Count() == 2) == 1;

            Func<IEnumerable<PlayingCard>, bool> isPair =
                                  cards => cards.GroupBy(card => card.NominalValue)
                                                .Count(group => group.Count() == 3) == 0
                                           && hasPair(cards);

            Func<IEnumerable<PlayingCard>, bool> isTwoPair =
                                  cards => cards.GroupBy(card => card.NominalValue)
                                                .Count(group => group.Count() >= 2) == 2 || cards.GroupBy(card => card.NominalValue).Count(group => group.Count() >= 2) == 3;

            Func<IEnumerable<PlayingCard>, bool> isStraight =
                                  cards => cards.GroupBy(card => card.NominalValue)
                                                .Count() == cards.Count()
                                           && cards.Max(card => (int)card.NominalValue)
                                            - cards.Min(card => (int)card.NominalValue) == 4;

            Func<IEnumerable<PlayingCard>, bool> hasThreeOfKind =
                                  cards => cards.GroupBy(card => card.NominalValue)
                                                .Any(group => group.Count() == 3);

            Func<IEnumerable<PlayingCard>, bool> isThreeOfKind =
                                  cards => hasThreeOfKind(cards) && !hasPair(cards);

            Func<IEnumerable<PlayingCard>, bool> isFlush =
                                  cards => cards.GroupBy(card => card.Suit)
                                    .Count(group => group.Count() == 5) == 1;

            Func<IEnumerable<PlayingCard>, bool> isFlushDraw =
                                      cards => cards.GroupBy(card => card.Suit)
                                        .Count(group => group.Count() == 4) == 1;

            Func<IEnumerable<PlayingCard>, bool> isFourOfKind =
                                      cards => cards.GroupBy(card => card.NominalValue)
                                                    .Any(group => group.Count() == 4);

            Func<IEnumerable<PlayingCard>, bool> isFullHouse =
                                  cards => hasPair(cards) && hasThreeOfKind(cards);

            Func<IEnumerable<PlayingCard>, bool> hasStraightFlush =
                                  cards => isFlush(cards) && isStraight(cards);

            Func<IEnumerable<PlayingCard>, bool> isRoyalFlush =
                                  cards => cards.Min(card => (int)card.NominalValue) == (int)CardType.Ten
                                           && hasStraightFlush(cards);

            Func<IEnumerable<PlayingCard>, bool> isStraightFlush =
                                  cards => hasStraightFlush(cards) && !isRoyalFlush(cards);


            _rules = new Dictionary<PokerHands, Func<IEnumerable<PlayingCard>, bool>>
                     {
                         { PokerHands.Pair, isPair },
                         { PokerHands.TwoPair, isTwoPair },
                         { PokerHands.ThreeOfKind, isThreeOfKind },
                         { PokerHands.Straight, isStraight },
                         { PokerHands.Flush, isFlush },
                         { PokerHands.FullHouse, isFullHouse },
                         { PokerHands.FourOfKind, isFourOfKind },
                         { PokerHands.StraightFlush, isStraightFlush },
                         { PokerHands.RoyalFlush, isRoyalFlush },
                         { PokerHands.FlushDraw, isFlushDraw }
                     };
        }
    }
}
