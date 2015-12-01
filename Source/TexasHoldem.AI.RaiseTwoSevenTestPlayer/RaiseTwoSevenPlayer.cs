namespace TexasHoldem.AI.RaiseTwoSevenTestPlayer
{
    using System;
    using TexasHoldem.Logic.Players;
    using TexasHoldem.Logic.Cards;
    using Logic;
    
    public class RaiseTwoSevenPlayer : BasePlayer
    {
        public override string Name { get; } = "RaiseTwoSeven";



        public override PlayerAction GetTurn(GetTurnContext context)
        {
            //var playHand = HandStrengthValuation.PreFlop(this.FirstCard, this.SecondCard);

            if (context.RoundType == GameRoundType.PreFlop)
            {
                //if (context.MoneyLeft / context.SmallBlind > 100)
                //{
                //    if ()
                //    if (context.CanCheck)
                //    {
                //        return PlayerAction.Raise(context.SmallBlind * 3);
                //    }
                //}
            }
            else if (context.RoundType == GameRoundType.Flop)
            {

            }
            else if (context.RoundType == GameRoundType.Turn)
            {

            }
            else
            {

            }



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
