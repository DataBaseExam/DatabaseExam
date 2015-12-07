using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Cards;

namespace TexasHoldem.AI.RaiseTwoSevenTestPlayer.Helpers.HandStrengthValuation
{
    public class TwentyFiveBlindsHandStrengthValuation
    {
        private static readonly int[,] PreflopOnButtonSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // A
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // K
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // Q
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // J
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // 10
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 }, // 9
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 }, // 8
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 }, // 7
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 }, // 6
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 }, // 5
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 }, // 4
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 }, // 3
            { 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 2 } // 2
        };

        private static readonly int[,] PreflopOnButtonOffSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // A
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // K
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // Q
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // J
            { 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0 }, // 10
            { 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0 }, // 9
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0 }, // 8
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0 }, // 7
            { 2, 2, 2, 2, 0, 0, 2, 2, 2, 2, 0, 0, 0 }, // 6
            { 2, 2, 2, 2, 0, 0, 0, 0, 2, 2, 2, 0, 0 }, // 5
            { 2, 2, 2, 2, 0, 0, 0, 0, 0, 2, 2, 0, 0 }, // 4
            { 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 2, 0 }, // 3
            { 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2 } // 2
        };

        private static readonly int[,] PreflopOnBigBlindSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // A
            { 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // K
            { 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // Q
            { 3, 2, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // J
            { 3, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 2 }, // 10
            { 3, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 1 }, // 9
            { 3, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 1 }, // 8
            { 3, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 1 }, // 7
            { 3, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 1 }, // 6
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 1 }, // 5
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 1 }, // 4
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 1 }, // 3
            { 3, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 3 } // 2
        };

        private static readonly int[,] PreflopOnBigBlindOffSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 2, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // A
            { 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // K
            { 2, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // Q
            { 2, 3, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0 }, // J
            { 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0 }, // 10
            { 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0 }, // 9
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0 }, // 8
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0 }, // 7
            { 2, 2, 2, 2, 0, 0, 2, 2, 2, 2, 0, 0, 0 }, // 6
            { 2, 2, 2, 0, 0, 0, 0, 0, 2, 2, 2, 0, 0 }, // 5
            { 2, 2, 2, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0 }, // 4
            { 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 }, // 3
            { 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 } // 2
        };

        public static CardValuationType PreFlopOnButton(Card firstCard, Card secondCard)
        {
            var value = firstCard.Suit == secondCard.Suit
                            ? (firstCard.Type < secondCard.Type
                                   ? PreflopOnButtonSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopOnButtonSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2])
                            : (firstCard.Type < secondCard.Type
                                   ? PreflopOnButtonOffSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopOnButtonOffSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2]);

            switch (value)
            {
                case 0:
                    return CardValuationType.Fold;
                case 1:
                    return CardValuationType.Risky;
                case 2:
                    return CardValuationType.Raise;
                case 3:
                    return CardValuationType.ThreeBet;
                default:
                    return CardValuationType.Fold;
            }
        }

        public static CardValuationType PreFlopOnBigBlind(Card firstCard, Card secondCard)
        {
            var value = firstCard.Suit == secondCard.Suit
                            ? (firstCard.Type < secondCard.Type
                                   ? PreflopOnBigBlindSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopOnBigBlindSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2])
                            : (firstCard.Type < secondCard.Type
                                   ? PreflopOnBigBlindOffSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopOnBigBlindOffSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2]);

            switch (value)
            {
                case 0:
                    return CardValuationType.Fold;
                case 1:
                    return CardValuationType.Risky;
                case 2:
                    return CardValuationType.Raise;
                case 3:
                    return CardValuationType.ThreeBet;
                default:
                    return CardValuationType.Fold;
            }
        }
    }
}