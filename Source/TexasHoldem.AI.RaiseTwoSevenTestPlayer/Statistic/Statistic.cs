namespace TexasHoldem.AI.RaiseTwoSevenTestPlayer.Statistic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    //TODO: Implement statistic and memento instead
    public class Statistic
    {
        private string playerName;
        private Dictionary<string, int> oppStats;
        private Dictionary<string, int> oppHandsCount;
        //TODO return opponent type
        public Statistic(string name)
        {
            this.playerName = name;// TODO: what if name is shorter? Error?
            //read from file
            //if filename exists and if name == firstline read dictionaries from files
            //else.
            makeNewData();
        }

        internal int GetOppAggresionFactor()
        {
            return (this.oppStats["Bet"] + this.oppStats["Raise"]) / (this.oppStats["Check"] + this.oppStats["Call"] + this.oppStats["Fold"]);
        }
       
        internal void OppPreflopRaise()
        {
            oppStats["Raise"]++;
            oppStats["PreflopRaise"]++;

            oppHandsCount["All"]++;
            oppHandsCount["Preflop"]++;
        }

        internal void OppPreflop3bet()
        {
            oppStats["Raise"]++;
            oppStats["Preflop3Bet"]++;

            oppHandsCount["All"]++;
            oppHandsCount["Preflop"]++;
        }
        //TODO: use builder instead
        private void makeNewData()
        {
            oppStats = new Dictionary<string, int>();
            oppHandsCount = new Dictionary<string, int>();

            oppStats["Raise"] = 0;
            oppStats["Bet"] = 0;
            oppStats["Check"] = 0;
            oppStats["Fold"] = 0;
            oppStats["PreflopRaise"] = 0;
            oppStats["PreflopCall"] = 0;
            oppStats["PreflopCheck"] = 0;
            oppStats["PreflopFold"] = 0;
            oppStats["Preflop3Bet"] = 0;
            oppStats["FlopRaise"] = 0;
            oppStats["FlopBet"] = 0;
            oppStats["FlopCall"] = 0;
            oppStats["FlopCheck"] = 0;
            oppStats["FlopFold"] = 0;
            oppStats["FoldToCBet"] = 0;
            oppStats["TurnRaise"] = 0;
            oppStats["TurnBet"] = 0;
            oppStats["TurnCall"] = 0;
            oppStats["TurnCheck"] = 0;
            oppStats["TurnFold"] = 0;
            oppStats["RiverRaise"] = 0;
            oppStats["RiverBet"] = 0;
            oppStats["RiverCall"] = 0;
            oppStats["RiverCheck"] = 0;
            oppStats["RiverFold"] = 0;

            oppHandsCount["All"] = 0;
            oppHandsCount["Preflop"] = 0;
            oppHandsCount["Flop"] = 0;
            oppHandsCount["Turn"] = 0;
            oppHandsCount["River"] = 0;
        }
    }
}
