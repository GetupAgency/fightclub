using System;
using System.Collections.Generic;

namespace FightClub
{
    public class Datafights 
    {
        public List<FighterStats> Fighters { get; set; }
        public Datafights()
        {
            Fighters = new List<FighterStats>();
        }
    }
    public class FighterStats
    {
        public string Name { get; set; }
        public int BestCritical { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
        public List<FightStats> Stats { get; set; }

        public FighterStats() {
            Stats = new List<FightStats>();
        }
    }

    public class FightStats
    {
        public int RoundsNumber { get; set; }
        public bool Victory { get; set; }
        public int CriticalNumber { get; set; }
        public int MissedNumber { get; set; }
        public DateTime FightDate { get; set; }
        public double RemainingHealth { get; set; }
    }
}
