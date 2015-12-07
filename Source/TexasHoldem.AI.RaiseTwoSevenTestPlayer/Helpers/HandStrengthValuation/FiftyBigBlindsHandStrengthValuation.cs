using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Cards;

namespace TexasHoldem.AI.RaiseTwoSevenTestPlayer.Helpers.HandStrengthValuation
{
    public class FiftyBigBlindsHandStrengthValuation
    {
        private static readonly int[,] PreflopAgainstTightOpponentSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // A
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // K
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // Q
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // J
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // 10
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // 9
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // 8
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // 7
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // 6
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // 5
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // 4
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // 3
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 } // 2
        };

        private static readonly int[,] PreflopAgainstTightOpponentOffSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // A
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // K
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // Q
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // J
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // 10
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0 }, // 9
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0 }, // 8
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0 }, // 7
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0 }, // 6
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0 }, // 5
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0 }, // 4
            { 2, 2, 2, 2, 2, 0, 0, 0, 0, 2, 2, 2, 0 }, // 3
            { 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 2 } // 2
        };

        private static readonly int[,] PreflopAgainstSolidOpponentSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // A
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // K
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // Q
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // J
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // 10
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // 9
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 }, // 8
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 }, // 7
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 }, // 6
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 }, // 5
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 }, // 4
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 }, // 3
            { 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2 } // 2
        };

        private static readonly int[,] PreflopAgainstSolidOpponentOffSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // A
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // K
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // Q
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // J
            { 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0 }, // 10
            { 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0 }, // 9
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0 }, // 8
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0 }, // 7
            { 2, 2, 2, 2, 0, 0, 2, 2, 2, 2, 0, 0, 0 }, // 6
            { 2, 2, 2, 2, 0, 0, 0, 2, 2, 2, 0, 0, 0 }, // 5
            { 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 2, 0, 0 }, // 4
            { 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 2, 0 }, // 3
            { 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2 } // 2
        };

        private static readonly int[,] PreflopAgainstWildOpponentSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // A
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // K
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // Q
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // J
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // 10
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1 }, // 9
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1 }, // 8
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1 }, // 7
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1 }, // 6
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 }, // 5
            { 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 1 }, // 4
            { 2, 2, 2, 2, 2, 1, 1, 1, 1, 2, 2, 2, 1 }, // 3
            { 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 2 } // 2
        };

        private static readonly int[,] PreflopAgainstWildOpponentOffSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // A
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // K
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // Q
            { 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0 }, // J
            { 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0 }, // 10
            { 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0 }, // 9
            { 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0 }, // 8
            { 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0 }, // 7
            { 2, 2, 2, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0 }, // 6
            { 2, 2, 2, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0 }, // 5
            { 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0 }, // 4
            { 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 }, // 3
            { 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 } // 2
        };

        private static readonly int[,] PreflopCallThreeBetSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }, // A
            { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }, // K
            { 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1 }, // Q
            { 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1 }, // J
            { 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1 }, // 10
            { 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1 }, // 9
            { 5, 5, 5, 5, 1, 1, 5, 1, 1, 1, 1, 1, 1 }, // 8
            { 5, 5, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 1 }, // 7
            { 5, 5, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1 }, // 6
            { 5, 5, 1, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1 }, // 5
            { 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 5, 1, 1 }, // 4
            { 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 1 }, // 3
            { 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5 } // 2
        };

        private static readonly int[,] PreflopCallThreeBetOffSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0 }, // A
            { 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0 }, // K
            { 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0 }, // Q
            { 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0 }, // J
            { 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0 }, // 10
            { 5, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0 }, // 9
            { 5, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0 }, // 8
            { 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0 }, // 7
            { 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0 }, // 6
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0 }, // 5
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0 }, // 4
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0 }, // 3
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 } // 2
        };

        //private static readonly int[,] PreflopCallWeakMinRaiserSuited =
        //{
        //    //A  K  Q  J 10  9  8  7  6  5  4  3  2
        //    { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }, // A
        //    { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }, // K
        //    { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }, // Q
        //    { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }, // J
        //    { 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1 }, // 10
        //    { 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1 }, // 9
        //    { 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1 }, // 8
        //    { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1 }, // 7
        //    { 5, 5, 5, 5, 1, 1, 5, 5, 5, 5, 5, 1, 1 }, // 6
        //    { 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 1 }, // 5
        //    { 5, 5, 5, 5, 1, 1, 1, 1, 5, 5, 5, 5, 1 }, // 4
        //    { 5, 5, 5, 5, 1, 1, 1, 1, 1, 5, 5, 5, 1 }, // 3
        //    { 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 5 } // 2
        //};

        //private static readonly int[,] PreflopCallWeakMinRaiserOffSuited =
        //{
        //    //A  K  Q  J 10  9  8  7  6  5  4  3  2
        //    { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }, // A
        //    { 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 }, // K
        //    { 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0 }, // Q
        //    { 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0 }, // J
        //    { 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0 }, // 10
        //    { 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 }, // 9
        //    { 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 }, // 8
        //    { 5, 5, 0, 0, 0, 5, 5, 5, 5, 0, 0, 0, 0 }, // 7
        //    { 5, 0, 0, 0, 0, 0, 0, 5, 5, 5, 0, 0, 0 }, // 6
        //    { 5, 0, 0, 0, 0, 0, 0, 0, 5, 5, 0, 0, 0 }, // 5
        //    { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0 }, // 4
        //    { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0 }, // 3
        //    { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 } // 2
        //};

        public static CardValuationType PreflopAgainstTightOpponent(Card firstCard, Card secondCard)
        {
            var value = firstCard.Suit == secondCard.Suit
                            ? (firstCard.Type < secondCard.Type
                                   ? PreflopAgainstTightOpponentSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopAgainstTightOpponentSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2])
                            : (firstCard.Type < secondCard.Type
                                   ? PreflopAgainstTightOpponentOffSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopAgainstTightOpponentOffSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2]);

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
                case 4:
                    return CardValuationType.AllIn;
                default:
                    return CardValuationType.Fold;
            }
        }

        public static CardValuationType PreflopAgainstSolidOpponent(Card firstCard, Card secondCard)
        {
            var value = firstCard.Suit == secondCard.Suit
                            ? (firstCard.Type < secondCard.Type
                                   ? PreflopAgainstSolidOpponentSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopAgainstSolidOpponentSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2])
                            : (firstCard.Type < secondCard.Type
                                   ? PreflopAgainstSolidOpponentOffSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopAgainstSolidOpponentOffSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2]);

            switch (value)
            {
                case 0:
                    return CardValuationType.Fold;
                case 1:
                    return CardValuationType.Risky;
                case 2:
                    return CardValuationType.Risky;
                case 3:
                    return CardValuationType.Raise;
                case 4:
                    return CardValuationType.ThreeBet;
                default:
                    return CardValuationType.Fold;
            }
        }

        public static CardValuationType PreflopAgainstWildOpponent(Card firstCard, Card secondCard)
        {
            var value = firstCard.Suit == secondCard.Suit
                            ? (firstCard.Type < secondCard.Type
                                   ? PreflopAgainstWildOpponentSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopAgainstWildOpponentSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2])
                            : (firstCard.Type < secondCard.Type
                                   ? PreflopAgainstWildOpponentOffSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopAgainstWildOpponentOffSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2]);

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
                case 4:
                    return CardValuationType.AllIn;
                default:
                    return CardValuationType.Fold;
            }
        }

        public static CardValuationType PreflopAgainstTornado(Card firstCard, Card secondCard)
        {
            var value = firstCard.Suit == secondCard.Suit
                            ? (firstCard.Type < secondCard.Type
                                   ? PreflopCallThreeBetSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopCallThreeBetSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2])
                            : (firstCard.Type < secondCard.Type
                                   ? PreflopCallThreeBetOffSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopCallThreeBetOffSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2]);

            switch (value)
            {
                case 0:
                    return CardValuationType.Fold;
                case 1:
                    return CardValuationType.Risky;
                case 5:
                    return CardValuationType.CallTornadoAllIn;
                default:
                    return CardValuationType.Fold;
            }
        }

        //public static RaiseTwoSevenCardValuationType PreflopCallWeakMinRaiser(Card firstCard, Card secondCard)
        //{
        //    var value = firstCard.Suit == secondCard.Suit
        //                    ? (firstCard.Type < secondCard.Type
        //                           ? PreflopCallWeakMinRaiserSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
        //                           : PreflopCallWeakMinRaiserSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2])
        //                    : (firstCard.Type < secondCard.Type
        //                           ? PreflopCallWeakMinRaiserOffSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
        //                           : PreflopCallWeakMinRaiserOffSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2]);

        //    switch (value)
        //    {
        //        case 0:
        //            return RaiseTwoSevenCardValuationType.Fold;
        //        case 1:
        //            return RaiseTwoSevenCardValuationType.Risky;
        //        case 5:
        //            return RaiseTwoSevenCardValuationType.Call;
        //        default:
        //            return RaiseTwoSevenCardValuationType.Fold;
        //    }
        //}
    }
}
