namespace TexasHoldem.AI.RaiseTwoSevenTestPlayer
{
    using System;
    using TexasHoldem.Logic.Players;
    using TexasHoldem.Logic.Cards;
    public class RaiseTwoSevenPlayer : BasePlayer
    {
        public override string Name { get; } = "RaiseTwoSeven";

        public override PlayerAction GetTurn(GetTurnContext context)
        {
            if (FirstCard.Type == CardType.Two && SecondCard.Type == CardType.Seven 
                || SecondCard.Type == CardType.Two && FirstCard.Type == CardType.Seven)
            {
                return PlayerAction.Raise(context.MoneyLeft);
            }
            else
            {
                return PlayerAction.Fold();
            }
        }
    }
}
