namespace TexasHoldem.AI.RaiseTwoSevenTestPlayer.Statistics
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
        //private Dictionary<string, int> oppHandsCount;
        internal int oppHandsCount;
        internal int opponendPlaysOnFlop;
        private int opponentCallsCBet;

        public int opponendPreflopRaise { get; private set; }
        public int opponentPreflopCheck { get; private set; }
        public int opponentPlaysOnFlop { get; private set; }

        public Statistic(string name)
        {
            this.playerName = name;// TODO: what if name is shorter? Error?
            //read from file
            //if filename exists and if name == firstline read dictionaries from files
            //else.
            makeNewData();
        }

        internal bool EnougthInfo()
        {
            if (oppHandsCount > 30)
            {
                return true;
            }
            return false;
        }
        //internal int GetOppAggresionFactor()
        //{
        //    return (this.oppStats["Bet"] + this.oppStats["Raise"]) / (this.oppStats["Check"] + this.oppStats["Call"] + this.oppStats["Fold"]);
        //}

        internal void OpponentPlaysOnFlop()
        {
            opponentPlaysOnFlop++;
        }
        internal void OpponentCallsCBet()
        {
            opponentCallsCBet++;
        }

        internal double GetOpponentCallCBetStats()
        {
            return opponentCallsCBet / opponentPlaysOnFlop;
        }

        internal OpponentEvaluationType OpponentType()
        {
            if (opponendPreflopRaise > 40)
            {
                return OpponentEvaluationType.Tornado;
            }
            else if (opponendPreflopRaise > 30)
            {
                return OpponentEvaluationType.Wild;
            }
            else if (opponendPreflopRaise > 20)
            {
                return OpponentEvaluationType.Solid;
            }
            else
            {
                return OpponentEvaluationType.Tight;
            }
        }

        internal void OpponentPreflopCheckFold()
        {
            opponentPreflopCheck++;
        }
        internal void OppPreflopRaise()
        {
            opponendPreflopRaise++;
        }
        
        //TODO: use builder instead
        private void makeNewData()
        {
            oppStats = new Dictionary<string, int>();
            opponendPlaysOnFlop = 0;
            opponendPreflopRaise = 0;
            opponentPreflopCheck = 0;
            opponentPlaysOnFlop = 0;
            opponentCallsCBet = 0;

            this.oppHandsCount = 0;
        }
    }
}
