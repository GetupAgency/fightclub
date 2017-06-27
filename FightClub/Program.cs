using System;
using System.Collections.Generic;

namespace FightClub
{
    static class Program
    {
        
        static void Main(string[] args)
        {
            Fighter fighter1 = new Fighter
            {
                Armor = 45,
                Strength = 45,
                Exp = 0,
                Health = 500,
                CC = 50,
                CD = 120,
                Name = "Ragnaru"
            };

			Fighter fighter2 = new Fighter
			{
				Armor = 45,
				Strength = 55,
				Exp = 0,
				Health = 500,
                CC = 50,
                CD = 150,
				Name = "Hercule"
			};

            List<Fighter> fighters = new List<Fighter>();
			fighters.Add(fighter1);
			fighters.Add(fighter2);

            Console.WriteLine("And the winner is : "+GoFight(fighters));
		}


        private static string GoFight(List<Fighter> fighters){            
            var rounds = 2000;
			
			for (int i = 0; i < rounds; i++){

				var critical1 = IsCritical(fighters[0].CC);
				var critical2 = IsCritical(fighters[1].CC);
				if(critical1){
                    fighters[0].Strength = fighters[0].Strength * (fighters[0].CD / 10);
                    Console.WriteLine(fighters[0].Strength.ToString());
                }
               

                fighters[0].Health = fighters[0].Health - (fighters[1].Strength - (fighters[1].Strength * (fighters[0].Armor / 100)));
                fighters[1].Health = fighters[1].Health - (fighters[0].Strength - (fighters[0].Strength * (fighters[1].Armor / 100)));

                Console.WriteLine("****** ROUND "+i+" *******");

                Console.WriteLine("coup critique : "+critical1.ToString());
				
                Console.WriteLine("health fighter 1 : " + fighters[0].Health.ToString());
				
                Console.WriteLine("health fighter 2 : " + fighters[1].Health.ToString());

                if (fighters[0].Health <= 0){
					return fighters[1].Name;

                }
                else if (fighters[1].Health <= 0){
                    return fighters[0].Name;
				}
 
			}

            return null;
        }

        private static bool IsCritical(int cc){
			Random r = new Random();
			return NextBool(r,cc);

		}
		 static bool NextBool(this Random r, int truePercentage = 50)
		{
			return r.NextDouble() < truePercentage / 100.0;
		}
    }
}
