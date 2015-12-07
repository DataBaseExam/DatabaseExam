namespace TexasHoldem.AI.RaiseTwoSevenTestPlayer
{
    using System;
    using TexasHoldem.Logic.Players;
    using TexasHoldem.Logic.Cards;
    using TexasHoldem.Logic;
    using Statistic;
    using Helpers;
    using Helpers.HandStrengthValuation;
    
    public class RaiseTwoSevenPlayer : BasePlayer
    {
        public override string Name { get; } = "RaiseTwoSeven";

        private int startMoney;

        private CardValuationType cardStrength;

        private OpponentEvaluationType opponentType;

        private Card firstCard;

        private Card secondCard;

        private int ourSmallBlindsLeft;

        private int ourMoney;

        private int smallBlind;

        private bool enoughtOpponentInfo;

        private int currentPot;

        //TODO : implement statistic
        public override void StartGame(StartGameContext context)
        {
            var opponentName = "";
            this.opponentType = OpponentEvaluationType.Tornado;
            foreach (var name in context.PlayerNames)
            {
                if(name != this.Name)
                {
                    opponentName = name.Substring(0 , 7);
                }
            }

            Statistic.Statistic stats = new Statistic.Statistic(opponentName);

            this.startMoney = context.StartMoney;
        }

        public override void StartHand(StartHandContext context)
        {
            this.ourMoney = context.MoneyLeft;
            this.smallBlind = context.SmallBlind;
            this.ourSmallBlindsLeft = this.ourMoney / this.smallBlind;
            this.firstCard = context.FirstCard;
            this.secondCard = context.SecondCard;
            
            if (ourSmallBlindsLeft >= 50)
            {
                if (enoughtOpponentInfo)
                {
                    if (opponentType == OpponentEvaluationType.Solid)
                    {
                        this.cardStrength = FiftyBigBlindsHandStrengthValuation.PreflopAgainstSolidOpponent(this.firstCard, this.secondCard);
                    }
                    else if (opponentType == OpponentEvaluationType.Tight)
                    {
                        this.cardStrength = FiftyBigBlindsHandStrengthValuation.PreflopAgainstTightOpponent(this.firstCard, this.secondCard);
                    }
                    else if (opponentType == OpponentEvaluationType.Wild)
                    {
                        this.cardStrength = FiftyBigBlindsHandStrengthValuation.PreflopAgainstWildOpponent(this.firstCard, this.secondCard);
                    }
                    else if (opponentType == OpponentEvaluationType.Tornado)
                    {
                        this.cardStrength = FiftyBigBlindsHandStrengthValuation.PreflopAgainstTornado(this.firstCard, this.secondCard);
                    }
                }
                else
                {
                    this.cardStrength = FiftyBigBlindsHandStrengthValuation.PreflopAgainstSolidOpponent(this.firstCard, this.secondCard);
                }
            }
        }

        public override void StartRound(StartRoundContext context)
        {
            this.currentPot = context.CurrentPot;
            //TODO: inspect current pot
            if (context.CommunityCards.Count > 2)
            {
                foreach (var card in context.CommunityCards)
                {
                    //TODO: what we hit
                }
            }
            
        }

        public override void EndRound(EndRoundContext context)
        {
            //TODO: get round actions?
        }
        public override void EndGame(EndGameContext context)
        {
            //TODO : save statistic
        }
        public override PlayerAction GetTurn(GetTurnContext context)
        {
            //context.CanCheck;
            //context.CurrentMaxBet;
            //context.CurrentPot;
            //context.IsAllIn;
            //context.MoneyToCall;
            //context.MyMoneyInTheRound;

            #region evaluating hand
            if (50 > ourSmallBlindsLeft && ourSmallBlindsLeft >= 17)
            {
                if (context.PreviousRoundActions.Count == 2)
                {
                    // we are on btn
                    this.cardStrength = TwentyFiveBlindsHandStrengthValuation.PreFlopOnButton(this.firstCard, this.secondCard);
                }
                else if (context.PreviousRoundActions.Count == 3)
                {
                    this.cardStrength = TwentyFiveBlindsHandStrengthValuation.PreFlopOnBigBlind(this.firstCard, this.secondCard);
                    //we are on big blind
                }
                
            }
            else if (ourSmallBlindsLeft < 21)
            {
                if (context.PreviousRoundActions.Count == 2)
                {
                    // we are on btn
                    this.cardStrength = TenBigBlindsHandStrengthValuation.PreflopShove(this.firstCard, this.secondCard);
                }
                else if (context.PreviousRoundActions.Count == 3)
                {
                    this.cardStrength = TenBigBlindsHandStrengthValuation.PreflopCallAllIn(this.firstCard, this.secondCard);
                    //we are on big blind
                }
            }
            #endregion

            if (context.RoundType == GameRoundType.PreFlop)
            {
                var amountOfMoneyToRaisePreflop = 0;

                #region how much to raise preflop
                if (this.smallBlind > 100)
                {
                    //TODO change value
                    amountOfMoneyToRaisePreflop = this.smallBlind * 10;
                }
                else if (this.smallBlind > 50)
                {
                    //TODO change value
                    amountOfMoneyToRaisePreflop = this.smallBlind * 5;
                }
                else if (this.smallBlind < 17)
                {
                    amountOfMoneyToRaisePreflop = context.MoneyLeft;
                }
                #endregion

                if (context.PreviousRoundActions.Count == 2)//we are on button
                {
                    if (this.opponentType == OpponentEvaluationType.Tornado)
                    {
                        if (cardStrength == CardValuationType.Risky
                           || this.cardStrength == CardValuationType.CallTornadoAllIn)
                        {
                            Console.ReadLine();
                            return PlayerAction.Raise(amountOfMoneyToRaisePreflop);
                        }

                    }
                    
                    else if (cardStrength == CardValuationType.Risky
                           || this.cardStrength == CardValuationType.CallTornadoAllIn)
                        {
                            return PlayerAction.Raise(amountOfMoneyToRaisePreflop);
                        }
                    {
                        
                    }
                    
                }
                else if (context.PreviousRoundActions.Count == 3)
                {
                    if (this.opponentType == OpponentEvaluationType.Tornado)
                    {
                        if (context.IsAllIn || context.MoneyToCall > ourMoney / 20)
                        {
                            Console.ReadLine();
                            if (this.cardStrength == CardValuationType.CallTornadoAllIn)
                            {
                                return PlayerAction.Raise(amountOfMoneyToRaisePreflop);
                            }
                        }
                        else
                        {
                            if (
                                this.cardStrength == CardValuationType.CallTornadoAllIn 
                                || this.cardStrength == CardValuationType.Risky 
                                || this.cardStrength == CardValuationType.ThreeBet)
                            {
                                return PlayerAction.Raise(amountOfMoneyToRaisePreflop);
                            }

                        }
                        return PlayerAction.Fold();
                    }
                    //we are on big blind

                }
                else if (context.PreviousRoundActions.Count == 4)
                {
                    return PlayerAction.Raise(5);
                    //we are on btn and opponent has made 3 bet
                }
                else if (context.PreviousRoundActions.Count == 5 || context.PreviousRoundActions.Count == 6)
                {
                    //opponent has 4-betet us
                    return PlayerAction.Raise(5);
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
            return PlayerAction.CheckOrCall();
        }

    }
}
