using System;

namespace FightClub
{
    public class Fighter
    {
        public string Name { get; set; }
		public double Strength { get; set; }
		public double Health { get; set; }
		public double Armor { get; set; }
        public double Exp { get; set; }
        public int CC { get; set; }
        public int CD { get; set; }
        public double CriticalStrength { get{
                return this.Strength * (this.CD / 10);
            } }

		public Fighter()
        {
        }
    }
}
