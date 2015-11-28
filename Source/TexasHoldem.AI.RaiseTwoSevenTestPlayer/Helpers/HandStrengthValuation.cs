using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Cards;

namespace TexasHoldem.AI.RaiseTwoSevenTestPlayer.Helpers
{
    public static class FiftyBigBlindsHandStrengthValuation
    {
        //http://www.anskypoker.com/2008/11/heads-up-nl-preflop-hand-ranges/
        private static readonly int[,] PreflopOnButtonHandsRecommendationsSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // A
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // K
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // Q
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // J
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // 10
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // 9
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // 8
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // 7
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // 6
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // 5
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // 4
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // 3
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 } // 2
        };

        //http://www.anskypoker.com/2008/11/heads-up-nl-preflop-hand-ranges/
        private static readonly int[,] PreflopOnButtonHandsRecommendationsOffSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // A
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0 }, // K
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0 }, // Q
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0 }, // J
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0 }, // 10
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0 }, // 9
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0 }, // 8
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0 }, // 7
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0 }, // 6
            { 3, 3, 0, 0, 0, 0, 0, 3, 3, 3, 3, 0, 0 }, // 5
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 0, 0 }, // 4
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 }, // 3
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 } // 2
        };

        //http://www.anskypoker.com/2008/11/heads-up-nl-preflop-hand-ranges/
        private static readonly int[,] PreflopOnBigBlindHandsRecommendationsSuited =
        {
            //A  K  Q  J 10  9  8  7  6  5  4  3  2
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // A
            { 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2 }, // K
            { 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2 }, // Q
            { 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2 }, // J
            { 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2 }, // 10
            { 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2 }, // 9
            { 3, 2, 2, 2, 2, 3, 3, 3, 2, 2, 2, 2, 2 }, // 8
            { 3, 2, 2, 2, 2, 2, 3, 3, 3, 2, 2, 2, 2 }, // 7
            { 3, 2, 2, 2, 2, 2, 2, 3, 3, 3, 2, 2, 2 }, // 6
            { 3, 2, 2, 2, 2, 2, 2, 2, 3, 3, 2, 2, 2 }, // 5
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2 }, // 4
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2 }, // 3
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3 } // 2
        };

        //http://www.anskypoker.com/2008/11/heads-up-nl-preflop-hand-ranges/
        private static readonly int[,] PrefloOnBigBlindHandsRecommendationsOffSuited =
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

        public static CardValuationType PreFlopOnButton(Card firstCard, Card secondCard)
        {
            var value = firstCard.Suit == secondCard.Suit
                            ? (firstCard.Type < secondCard.Type
                                   ? PreflopOnButtonHandsRecommendationsSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopOnButtonHandsRecommendationsSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2])
                            : (firstCard.Type < secondCard.Type
                                   ? PreflopOnButtonHandsRecommendationsOffSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopOnButtonHandsRecommendationsOffSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2]);

            switch (value)
            {
                case 0:
                    return CardValuationType.Unplayable;
                    break;
                case 1:
                    return CardValuationType.Risky;
                    break;
                case 2:
                    return CardValuationType.Recommended;
                    break;
                default:
                    return CardValuationType.Unplayable;
            }
        }

        public static CardValuationType PreFlopOnBigBlind(Card firstCard, Card secondCard)
        {
            var value = firstCard.Suit == secondCard.Suit
                            ? (firstCard.Type < secondCard.Type
                                   ? PreflopOnBigBlindHandsRecommendationsSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PreflopOnBigBlindHandsRecommendationsSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2])
                            : (firstCard.Type < secondCard.Type
                                   ? PrefloOnBigBlindHandsRecommendationsOffSuited[(int)firstCard.Type - 2, (int)secondCard.Type - 2]
                                   : PrefloOnBigBlindHandsRecommendationsOffSuited[(int)secondCard.Type - 2, (int)firstCard.Type - 2]);

            switch (value)
            {
                case 0:
                    return CardValuationType.Unplayable;
                    break;
                case 1:
                    return CardValuationType.Risky;
                    break;
                case 2:
                    return CardValuationType.Recommended;
                    break;
                default:
                    return CardValuationType.Unplayable;
            }
        }
    }
}
