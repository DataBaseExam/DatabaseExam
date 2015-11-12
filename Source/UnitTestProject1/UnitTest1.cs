namespace UnitTestProject1
{

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using TexasHoldem.Logic.Cards;

    [TestClass]
    public class UnitTest1
    {
        public void WhenCalledFromTwoDifferentInstancesGetNextCardShouldReturnDifferentCards()
        {
            IDeck deck1 = new Deck();
            IDeck deck2 = new Deck();

            var cards1 = new List<Card>();
            var cards2 = new List<Card>();

            for (var i = 0; i < 52; i++)
            {
                cards1.Add(deck1.GetNextCard());
                cards2.Add(deck2.GetNextCard());
            }

            CollectionAssert.AreNotEquivalent(cards1, cards2);
        }
    }
}
