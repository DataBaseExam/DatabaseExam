namespace TexasHoldem.AI.RaiseTwoSevenTestPlayer
{
    using System;
    using TexasHoldem.Logic.Players;
    using TexasHoldem.Logic.Cards;
    using TexasHoldem.Logic;
    using Statistics;
    using Helpers;
    using Helpers.HandStrengthValuation;
    using Evhand;
    using System.Collections.Generic;

    public class RaiseTwoSevenPlayer : BasePlayer
    {
        public override string Name { get; } = "RaiseTwoSeven" + Guid.NewGuid();

        private int startMoney;

        private CardValuationType cardPreflopStrength;

        private PokerHands ourCompleteHand;

        private OpponentEvaluationType opponentType;

        private Card firstCard;

        private Card secondCard;

        private int ourSmallBlindsLeft;

        private int ourMoney;

        private int smallBlind;

        private int currentPot;

        private Statistic stats;
        //TODO : implement statistic
        public override void StartGame(StartGameContext context)
        {
            var opponentName = "";
            //this.opponentType = OpponentEvaluationType.Tornado;
            foreach (var name in context.PlayerNames)
            {
                if (name != this.Name)
                {
                    opponentName = name.Substring(0, 7);
                }
            }

            this.stats = new Statistic(opponentName);

            this.startMoney = context.StartMoney;
        }

        public override void StartHand(StartHandContext context)
        {
            // this.ourMoney = context.MoneyLeft;
            this.smallBlind = context.SmallBlind;
            this.ourSmallBlindsLeft = this.ourMoney / this.smallBlind;
            this.firstCard = context.FirstCard;
            this.secondCard = context.SecondCard;
            this.opponentType = stats.OpponentType();
            this.ourMoney = (int)context.MoneyLeft;
            stats.oppHandsCount++;
            if (ourSmallBlindsLeft >= 50)
            {
                if (stats.EnougthInfo())
                {
                    if (opponentType == OpponentEvaluationType.Solid)
                    {
                        this.cardPreflopStrength = FiftyBigBlindsHandStrengthValuation.PreflopAgainstSolidOpponent(this.firstCard, this.secondCard);
                    }
                    else if (opponentType == OpponentEvaluationType.Tight)
                    {
                        this.cardPreflopStrength = FiftyBigBlindsHandStrengthValuation.PreflopAgainstTightOpponent(this.firstCard, this.secondCard);
                    }
                    else if (opponentType == OpponentEvaluationType.Wild)
                    {
                        this.cardPreflopStrength = FiftyBigBlindsHandStrengthValuation.PreflopAgainstWildOpponent(this.firstCard, this.secondCard);
                    }
                    else if (opponentType == OpponentEvaluationType.Tornado)
                    {
                        this.cardPreflopStrength = FiftyBigBlindsHandStrengthValuation.PreflopAgainstTornado(this.firstCard, this.secondCard);
                    }
                }
                else
                {
                    this.cardPreflopStrength = HandStrengthValuation.PreFlop(this.firstCard, this.secondCard);
                }
            }
        }

        public override void StartRound(StartRoundContext context)
        {

            this.currentPot = context.CurrentPot;
            //TODO: inspect current pot
            List<PlayingCard> playingCards = new List<PlayingCard>();

            if (context.CommunityCards.Count > 2)
            {
                foreach (var card in context.CommunityCards)
                {
                    playingCards.Add(new PlayingCard(card.Suit, card.Type));
                }

                playingCards.Add(new PlayingCard(this.firstCard.Suit, this.firstCard.Type));
                playingCards.Add(new PlayingCard(this.secondCard.Suit, this.secondCard.Type));

                this.ourCompleteHand = EvaluateHandRaiseTwo(playingCards);

                playingCards.Clear();
            }

        }

        public override void EndRound(EndRoundContext context)
        {
            this.ourCompleteHand = PokerHands.Nothing;
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
                    this.cardPreflopStrength = TwentyFiveBlindsHandStrengthValuation.PreFlopOnButton(this.firstCard, this.secondCard);
                }
                else if (context.PreviousRoundActions.Count == 3)
                {
                    this.cardPreflopStrength = TwentyFiveBlindsHandStrengthValuation.PreFlopOnBigBlind(this.firstCard, this.secondCard);
                    //we are on big blind
                }

            }

            else if (ourSmallBlindsLeft < 17)
            {
                if (context.PreviousRoundActions.Count == 2)
                {
                    // we are on btn
                    this.cardPreflopStrength = TenBigBlindsHandStrengthValuation.PreflopShove(this.firstCard, this.secondCard);
                }
                else if (context.PreviousRoundActions.Count == 3)
                {
                    this.cardPreflopStrength = TenBigBlindsHandStrengthValuation.PreflopCallAllIn(this.firstCard, this.secondCard);
                    //we are on big blind
                }
            }
            #endregion

            if (context.RoundType == GameRoundType.PreFlop)
            {

                var amountOfMoneyToRaisePreflop = 1;

                #region how much to raise preflop
                if (this.smallBlind > 100)
                {
                    //TODO change value
                    amountOfMoneyToRaisePreflop = this.smallBlind * 10;
                }
                else if (this.smallBlind > 17)
                {
                    //TODO change value
                    amountOfMoneyToRaisePreflop = this.smallBlind * 5;
                }
                else if (this.smallBlind <= 17)
                {
                    amountOfMoneyToRaisePreflop = ourMoney;
                }
                #endregion

                #region preflopaction
                if (context.PreviousRoundActions.Count == 2)//we are on button
                {
                    if (stats.EnougthInfo())
                    {
                        if (this.opponentType == OpponentEvaluationType.Tornado)
                        {
                            if (this.cardPreflopStrength == CardValuationType.CallTornadoAllIn)
                            {
                                return PlayerAction.Raise(amountOfMoneyToRaisePreflop);
                            }

                        }
                    }

                    else
                    {

                        if (
                            cardPreflopStrength == CardValuationType.Raise
                           || this.cardPreflopStrength == CardValuationType.ThreeBet
                           || this.cardPreflopStrength == CardValuationType.AllIn)
                        {
                            return PlayerAction.Raise(amountOfMoneyToRaisePreflop);
                        }

                        return PlayerAction.Fold();

                    }

                }
                else if (context.PreviousRoundActions.Count == 3)
                {
                    stats.OppPreflopRaise();
                    if (this.opponentType == OpponentEvaluationType.Tornado)
                    {
                        if (this.cardPreflopStrength == CardValuationType.CallTornadoAllIn)
                        {
                            if (context.IsAllIn || context.MoneyToCall > ourMoney / 20)
                            {
                                return PlayerAction.Raise(ourMoney * 2);
                            }
                            else
                            {
                                return PlayerAction.Raise(amountOfMoneyToRaisePreflop);
                            }
                        }
                        if (context.CanCheck)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        return PlayerAction.Fold();

                    }
                    else
                    {
                        if (
                            this.cardPreflopStrength == CardValuationType.Raise
                            || this.cardPreflopStrength == CardValuationType.ThreeBet
                            || this.cardPreflopStrength == CardValuationType.AllIn)
                        {
                            if (context.IsAllIn || context.MoneyToCall > ourMoney / 20)
                            {
                                return PlayerAction.Raise(ourMoney * 2);
                            }
                            else
                            {
                                return PlayerAction.Raise(amountOfMoneyToRaisePreflop);
                            }
                        }
                        if (context.CanCheck)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        return PlayerAction.Fold();

                    }
                }
                else if (context.PreviousRoundActions.Count == 4)//sb and opp has 3betet us
                {
                    stats.OppPreflopRaise();
                    if (this.opponentType == OpponentEvaluationType.Tornado)
                    {
                        if (this.cardPreflopStrength == CardValuationType.CallTornadoAllIn)
                        {
                            if (context.MoneyToCall * 3 > ourMoney / 10)
                            {
                                return PlayerAction.Raise(ourMoney * 2);
                            }
                            return PlayerAction.Raise(context.MoneyToCall * 3);
                        }

                    }

                    else
                    {

                        if (
                            this.cardPreflopStrength == CardValuationType.AllIn)
                        {
                            if (context.MoneyToCall * 3 > ourMoney / 10)
                            {
                                return PlayerAction.Raise(ourMoney * 2);
                            }
                            return PlayerAction.Raise(context.MoneyToCall * 3);
                        }
                        return PlayerAction.Fold();

                    }
                }
                else if (context.PreviousRoundActions.Count == 5 || context.PreviousRoundActions.Count == 6) //opponent has 4-betet us
                {
                    stats.OppPreflopRaise();
                    if (this.opponentType == OpponentEvaluationType.Tornado)
                    {
                        if (this.cardPreflopStrength == CardValuationType.CallTornadoAllIn)
                        {
                            if (context.MoneyToCall * 3 > ourMoney / 10)
                            {
                                return PlayerAction.Raise(ourMoney * 2);
                            }
                            return PlayerAction.Raise(context.MoneyToCall * 3);
                        }

                    }

                    else
                    {
                        if (this.cardPreflopStrength == CardValuationType.AllIn)
                        {
                            return PlayerAction.Raise(ourMoney * 2);
                        }
                        return PlayerAction.Fold();

                    }
                }
            }
            #endregion

            else if (context.RoundType == GameRoundType.Flop)
            {
                var moneyToRaise = 1;
                stats.OpponentPlaysOnFlop();
                #region amount of money to raise
                //amount of money to raise onf lop
                if (context.MoneyLeft / 5 > context.CurrentPot)
                {
                    if (context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        moneyToRaise = context.CurrentPot / 3;
                    }
                    else
                    {
                        if (context.MoneyLeft / 8 > context.MoneyToCall * 3)
                        {
                            moneyToRaise = context.MoneyLeft / 8;
                        }
                        else
                        {
                            moneyToRaise = ourMoney * 2;
                        }
                    }
                }
                else
                {
                    moneyToRaise = ourMoney * 2;
                }

                if (moneyToRaise < ourMoney / 50)
                {
                    moneyToRaise = ourMoney / 45;
                }
                #endregion
                if (context.CanCheck || context.MoneyToCall <= ourMoney / 30)
                {

                    if ((int)ourCompleteHand >= 1)//enougth value, value bet
                    {
                        return PlayerAction.Raise(moneyToRaise);
                    }
                    else if (!stats.EnougthInfo())
                    {
                        return PlayerAction.Raise(moneyToRaise);
                    }
                    else if (stats.EnougthInfo() && (stats.GetOpponentCallCBetStats() < 0.3))
                    {
                        return PlayerAction.Raise(moneyToRaise);
                    }
                }
                else//enought value to 
                {
                    if ((int)ourCompleteHand > 1)
                    {
                        return PlayerAction.Raise(moneyToRaise);
                    }
                }
                return PlayerAction.Fold();
            }
            else if (context.RoundType == GameRoundType.Turn)
            {
                var moneyToRaise = 1;

                if (context.MoneyLeft / 5 > context.CurrentPot)
                {
                    if (context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        moneyToRaise = context.CurrentPot / 3;
                    }
                    else
                    {
                        if (context.MoneyLeft / 8 > context.MoneyToCall * 3)
                        {
                            moneyToRaise = context.MoneyLeft / 8;
                        }
                        else
                        {
                            moneyToRaise = ourMoney * 2;
                        }
                    }
                }
                else
                {
                    moneyToRaise = ourMoney * 2;
                }
                if (moneyToRaise < ourMoney / 40)
                {
                    moneyToRaise = ourMoney / 35;
                }

                if (context.CanCheck || context.MoneyToCall <= ourMoney / 40)
                {
                    if ((int)ourCompleteHand >= 1)//enougth value, value bet
                    {
                        return PlayerAction.Raise(moneyToRaise);
                    }
                }
                else
                {
                    if ((int)ourCompleteHand >= 2)//enougth value, value bet
                    {
                        return PlayerAction.Raise(moneyToRaise);
                    }
                }
                return PlayerAction.Fold();
            }
            else
            {
                stats.OpponentCallsCBet();
                var moneyToRaise = 1;
                if (moneyToRaise < ourMoney / 30)
                {
                    moneyToRaise = ourMoney / 26;
                }
                if (context.MoneyLeft / 5 > context.CurrentPot)
                {
                    if (context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        moneyToRaise = context.CurrentPot / 3;
                    }
                    else
                    {
                        if (context.MoneyLeft / 8 > context.MoneyToCall * 3)
                        {
                            moneyToRaise = context.MoneyLeft / 8;
                        }
                        else
                        {
                            moneyToRaise = ourMoney * 2;
                        }
                    }
                }
                else
                {
                    moneyToRaise = ourMoney * 2;
                }

                if (context.CanCheck || context.MoneyToCall <= ourMoney / 40)
                {
                    if ((int)ourCompleteHand <= 2)//enougth value, value bet
                    {
                        return PlayerAction.CheckOrCall();
                    }
                    else
                    {
                        return PlayerAction.Raise(moneyToRaise);
                    }
                }
                else
                {
                    if ((int)ourCompleteHand <= 2)//enougth value, value bet
                    {
                        return PlayerAction.CheckOrCall();
                    }
                    else
                    {
                        return PlayerAction.Raise(moneyToRaise);
                    }
                }
            }
            return PlayerAction.Fold();
        }

        internal static PokerHands EvaluateHandRaiseTwo(List<PlayingCard> playingCards)
        {
            HandEvaluateRaiseTwo pk = new HandEvaluateRaiseTwo();

            playingCards.Sort();

            if (pk.Rules[PokerHands.RoyalFlush](playingCards))
            {
                return PokerHands.RoyalFlush;
            }
            else if (pk.Rules[PokerHands.StraightFlush](playingCards))
            {
                return PokerHands.StraightFlush;
            }
            else if (pk.Rules[PokerHands.FourOfKind](playingCards))
            {
                return PokerHands.FourOfKind;
            }
            else if (pk.Rules[PokerHands.FullHouse](playingCards))
            {
                return PokerHands.FullHouse;
            }
            else if (pk.Rules[PokerHands.Flush](playingCards))
            {
                return PokerHands.Flush;
            }
            else if (pk.Rules[PokerHands.Straight](playingCards))
            {
                return PokerHands.Straight;
            }

            if (pk.Rules[PokerHands.ThreeOfKind](playingCards))
            {
                return PokerHands.ThreeOfKind;
            }
            else if (pk.Rules[PokerHands.TwoPair](playingCards))
            {
                return PokerHands.TwoPair;
            }
            else if (pk.Rules[PokerHands.Pair](playingCards))
            {
                return PokerHands.Pair;
            }
            else if (pk.Rules[PokerHands.FlushDraw](playingCards))
            {
                return PokerHands.FlushDraw;
            }
            else
            {
                return PokerHands.Nothing;
            }
        }

    }
}
