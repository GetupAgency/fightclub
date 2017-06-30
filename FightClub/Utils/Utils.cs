using System;
using System.Diagnostics;
using System.Linq;
using Troschuetz.Random;

namespace FightClub
{
    public static class Utils
    {
        // Dans cette classe on met toutes les fonctions "utiles"

		private static readonly Random random = new Random();
		private static readonly object syncLock = new object();

		public static int RandomNumber(int min, int max)
		{
			lock (syncLock)
			{ // synchronize
				return random.Next(min, max);
			}
		}
		// Methode qui renvoie true ou false, selon la critical chance
		public static bool IsSuccess(int cc)
		{
            var randomNumber = RandomNumber(0, 100);
			return randomNumber < cc;
		}
		
    }
}
