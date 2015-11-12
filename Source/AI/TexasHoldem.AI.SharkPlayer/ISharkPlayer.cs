namespace TexasHoldem.AI.SharkPlayer
{
    using System;
    using TexasHoldem.AI.SmartPlayer.Helpers;
    using TexasHoldem.Logic;
    using TexasHoldem.Logic.Extensions;
    using TexasHoldem.Logic.Players;

    public interface ISharkPlayer
    {
        PlayerAction GetTurn(GetTurnContext context);
    }
}
