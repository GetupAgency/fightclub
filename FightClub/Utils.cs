using System;
namespace FightClub
{
    public class Utils
    {
        public Utils()
        {
        }
		public bool NextBool(this Random r, int truePercentage = 50)
		{
			return r.NextDouble() < truePercentage / 100.0;
		}
    }
}
