using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Cards;

namespace TexasHoldem.AI.RaiseTwoSevenTestPlayer.Helpers.HandStrengthValuation
{
    public class TenBigBlindsHandStrengthValuation
    {
        private static readonly int[,] PreflopShoveSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 4, 4, 4, 4, 0, 4, 0, 4, 0, 4, 4, 0, 0 }, // A
            { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 }, // K
            { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 }, // Q
            { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0 }, // J
            { 0, 4, 4, 4, 4, 4, 0, 4, 0, 4, 4, 0, 0 }, // 10
            { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0 }, // 9
            { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0 }, // 8
            { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0 }, // 7
            { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0 }, // 6
            { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 0 }, // 5
            { 0, 4, 4, 4, 0, 0, 0, 4, 0, 4, 4, 0, 0 }, // 4
            { 0, 4, 4, 0, 0, 0, 0, 0, 0, 4, 0, 4, 0 }, // 3
            { 0, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4 } // 2
        };

        private static readonly int[,] PreflopShoveOffSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 4, 4, 4, 4, 4, 0, 4, 0, 0, 0, 0, 0, 0 }, // A
            { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 }, // K
            { 0, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0 }, // Q
            { 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0 }, // J
            { 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0 }, // 10
            { 0, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0 }, // 9
            { 4, 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0 }, // 8
            { 0, 4, 0, 0, 0, 0, 4, 4, 0, 0, 0, 0, 0 }, // 7
            { 0, 4, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0 }, // 6
            { 0, 4, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0 }, // 5
            { 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0 }, // 4
            { 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0 }, // 3
            { 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4 } // 2
        };

        private static readonly int[,] PreflopCallAllInSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 }, // A
            { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 }, // K
            { 4, 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0 }, // Q
            { 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0 }, // J
            { 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0 }, // 10
            { 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0 }, // 9
            { 4, 4, 4, 4, 0, 0, 4, 0, 0, 0, 0, 0, 0 }, // 8
            { 4, 4, 4, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0 }, // 7
            { 4, 4, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0 }, // 6
            { 4, 4, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0 }, // 5
            { 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0 }, // 4
            { 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0 }, // 3
            { 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4 } // 2
        };

        private static readonly int[,] PreflopCallAllInOffSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 }, // A
            { 4, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0 }, // K
            { 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0 }, // Q
            { 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0 }, // J
            { 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0 }, // 10
            { 4, 4, 4, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0 }, // 9
            { 4, 4, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0 }, // 8
            { 4, 4, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0 }, // 7
            { 4, 4, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0 }, // 6
            { 4, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0 }, // 5
            { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0 }, // 4
            { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0 }, // 3
            { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4 } // 2
        };

        public static RaiseTwoSevenCardValuationType PreflopShove(Card firstCard, Card secondCard)
        {
            var value = firstCard.Suit == secondCard.Suit
                            ? (firstCard.Type < secondCard.Type
                                   ? PreflopShoveSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopShoveSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2])
                            : (firstCard.Type < secondCard.Type
                                   ? PreflopShoveOffSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopShoveOffSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2]);

            switch (value)
            {
                case 0:
                    return RaiseTwoSevenCardValuationType.Fold;
                case 1:
                    return RaiseTwoSevenCardValuationType.Risky;
                case 2:
                    return RaiseTwoSevenCardValuationType.Raise;
                case 3:
                    return RaiseTwoSevenCardValuationType.ThreeBet;
                case 4:
                    return RaiseTwoSevenCardValuationType.AllIn;
                default:
                    return RaiseTwoSevenCardValuationType.Fold;
            }
        }

        public static RaiseTwoSevenCardValuationType PreflopCallAllIn(Card firstCard, Card secondCard)
        {
            var value = firstCard.Suit == secondCard.Suit
                            ? (firstCard.Type < secondCard.Type
                                   ? PreflopCallAllInSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopCallAllInSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2])
                            : (firstCard.Type < secondCard.Type
                                   ? PreflopCallAllInOffSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopCallAllInOffSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2]);

            switch (value)
            {
                case 0:
                    return RaiseTwoSevenCardValuationType.Fold;
                case 1:
                    return RaiseTwoSevenCardValuationType.Risky;
                case 2:
                    return RaiseTwoSevenCardValuationType.Raise;
                case 3:
                    return RaiseTwoSevenCardValuationType.ThreeBet;
                case 4:
                    return RaiseTwoSevenCardValuationType.AllIn;
                default:
                    return RaiseTwoSevenCardValuationType.Fold;
            }
        }
    }
}
