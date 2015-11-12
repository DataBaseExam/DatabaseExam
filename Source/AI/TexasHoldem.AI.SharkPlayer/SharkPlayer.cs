namespace TexasHoldem.AI.SharkPlayer
{
    using System;
    using TexasHoldem.AI.SmartPlayer.Helpers;
    using TexasHoldem.Logic;
    using TexasHoldem.Logic.Extensions;
    using TexasHoldem.Logic.Players;

    public class SharkPlayer : BasePlayer
    {
        public override string Name { get; } = "SmartPlayer_" + Guid.NewGuid();


        public override PlayerAction GetTurn(GetTurnContext context)
        {
            if (context.RoundType == GameRoundType.PreFlop)
            {
                //TODO: Implement raise when on position
                //TODO: Implement raise with medium hand not on position
                //TODO: Implement check/call with trash
                //TODO: Implement All-in when short stack under 10 BB
                //TODO: Implement All-in with medium hand when opponent raise
            }
            else if (context.RoundType == GameRoundType.Flop)
            {
                //TODO: Implement value bet
                //TODO: Implement c-bet
                //TODO: Implement min prob bet
                //TODO: Implement slow play with monster hand
                //TODO: Implement Raise with best hand
            }
            else if (context.RoundType == GameRoundType.Turn)
            {

            }
            else
            {

            }

            return new object() as PlayerAction; // Just to  Build
        }
    }
}
