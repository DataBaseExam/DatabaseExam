using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldem.AI.RaiseTwoSevenTestPlayer.Helpers
{
    public enum CardValuationType
    {
        Fold = 0,
        Risky = 1,
        Raise = 2,
        ThreeBet = 3,
        AllIn = 4,
        CallTornadoAllIn = 5
    }
}
