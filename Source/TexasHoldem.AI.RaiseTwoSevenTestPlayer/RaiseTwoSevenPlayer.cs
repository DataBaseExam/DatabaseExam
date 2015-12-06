namespace TexasHoldem.AI.RaiseTwoSevenTestPlayer
{
    using System;
    using TexasHoldem.Logic.Players;
    using TexasHoldem.Logic.Cards;
    using Logic;
    //using Helpers;

    public class RaiseTwoSevenPlayer : BasePlayer
    {
        public override string Name { get; } = "RaiseTwoSeven";

        public bool IsOnButton { get; private set; }

        public bool HaveBeenRaisedPreflop { get; private set; }

        public bool HaveBeenRaisedOnFlop { get; private set; }

        public bool HaveBeenRaisedOnTurn { get; private set; }

        public bool HaveBeenRaisedOnRiver { get; private set; }

        public bool OpponentHasMadeValueBet { get; private set; }

        public bool OpponentHasMadeCBet { get; private set; }


        public override PlayerAction GetTurn(GetTurnContext context)
        {
            if (context.RoundType == GameRoundType.PreFlop)
            {
                UpdateRoundStatistics();
                

                if (context.PreviousRoundActions.Count == 2)
                {
                    IsOnButton = true;
                    //var playHand = FiftyBigBlindsHandStrengthValuation.PreFlopOnButton(this.FirstCard, this.SecondCard);
                }

                if (IsOnButton)
                {
                    if (context.MoneyLeft / context.SmallBlind > 100)
                    {
                        if (context.CanCheck)
                        {
                            return PlayerAction.Raise(context.SmallBlind * 3);
                        }
                        
                    }
                }

            
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

        public void UpdateRoundStatistics()
        {
            IsOnButton = false;
            HaveBeenRaisedPreflop = false;
            HaveBeenRaisedOnFlop = false;
            HaveBeenRaisedOnTurn = false;
            HaveBeenRaisedOnRiver = false;
            OpponentHasMadeValueBet = false;
            OpponentHasMadeCBet = false;
        }
    }
}
