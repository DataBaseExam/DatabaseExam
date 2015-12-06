using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Cards;

namespace TexasHoldem.AI.RaiseTwoSevenTestPlayer.Helpers.HandStrengthValuation
{
    public static class AboveFiftyBigBlindsHandStrengthValuation
    {
        //http://www.anskypoker.com/2008/11/heads-up-nl-preflop-hand-ranges/
        private static readonly int[,] PreflopOnButtonSuited =
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

        //http://www.anskypoker.com/2008/11/heads-up-nl-preflop-hand-ranges/
        private static readonly int[,] PreflopOnButtonOffSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // A
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0 }, // K
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0 }, // Q
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0 }, // J
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0 }, // 10
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0 }, // 9
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0 }, // 8
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0 }, // 7
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0 }, // 6
            { 2, 2, 0, 0, 0, 0, 0, 2, 2, 2, 2, 0, 0 }, // 5
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0 }, // 4
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 }, // 3
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 } // 2
        };

        //http://www.anskypoker.com/2008/11/heads-up-nl-preflop-hand-ranges/
        private static readonly int[,] PreflopOnBigBlindSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, // A
            { 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0 }, // K
            { 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0 }, // Q
            { 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0 }, // J
            { 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0 }, // 10
            { 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0 }, // 9
            { 2, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0 }, // 8
            { 2, 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0 }, // 7
            { 2, 0, 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0 }, // 6
            { 2, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0 }, // 5
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0 }, // 4
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 }, // 3
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 } // 2
        };

        //http://www.anskypoker.com/2008/11/heads-up-nl-preflop-hand-ranges/
        private static readonly int[,] PreflopOnBigBlindOffSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0 }, // A
            { 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0 }, // K
            { 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0 }, // Q
            { 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0 }, // J
            { 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0 }, // 10
            { 3, 0, 0, 0, 3, 3, 0, 0, 0, 0, 0, 0, 0 }, // 9
            { 3, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0 }, // 8
            { 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 }, // 7
            { 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0 }, // 6
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0 }, // 5
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0 }, // 4
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 }, // 3
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 } // 2
        };

        public static RaiseTwoSevenCardValuationType PreFlopOnButton(Card firstCard, Card secondCard)
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
                    return RaiseTwoSevenCardValuationType.Fold;
                case 1:
                    return RaiseTwoSevenCardValuationType.Risky;
                case 2:
                    return RaiseTwoSevenCardValuationType.Raise;
                default:
                    return RaiseTwoSevenCardValuationType.Fold;
            }
        }

        public static RaiseTwoSevenCardValuationType PreFlopOnBigBlind(Card firstCard, Card secondCard)
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
                    return RaiseTwoSevenCardValuationType.Fold;
                case 1:
                    return RaiseTwoSevenCardValuationType.Risky;
                case 2:
                    return RaiseTwoSevenCardValuationType.Raise;
                default:
                    return RaiseTwoSevenCardValuationType.Fold;
            }
        }
    }
}
