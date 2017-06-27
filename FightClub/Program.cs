using System;
using System.Collections.Generic;

namespace FightClub
{
    static class Program
    {
        
        static void Main(string[] args)
        {
            // On déclare et on initialise les combattants
            Fighter fighter1 = new Fighter
                // Pour voir les propriétés d'un fighter -> clic droit + Atteindre la déclaration
            {
                Armor = 45,
                Strength = 45,
                Exp = 0,
                Health = 500,
                CriticalChance = 50,
                CriticalDamage = 120,
                Name = "Ragnaru"
            };
			Fighter fighter2 = new Fighter
			{
				Armor = 45,
				Strength = 55,
				Exp = 0,
				Health = 500,
                CriticalChance = 50,
                CriticalDamage = 150,
				Name = "Hercule"
			};

            // On initialise une liste de combattants
            List<Fighter> fighters = new List<Fighter>
            {
                // On ajoute les combattants à notre liste
                fighter1,
                fighter2
            };

            // GoFight(fighters) est la méthode qui réalise le combat, elle renvoie le nom du vainqueur
            Console.WriteLine("And the winner is : "+GoFight(fighters));
		}


        private static string GoFight(List<Fighter> fighters){

            // Nombre de rounds
            var rounds = 2000;
			
            // On boucle pour chaque round
			for (int i = 0; i < rounds; i++){

                // On définit si chaque joueur dispose d'un coup critique lors de ce round
                fighters[0].IsCritical = IsCritical(fighters[0].CriticalChance);
				fighters[1].IsCritical = IsCritical(fighters[1].CriticalChance);

                // Si le premier combattant fait un coup critique
				if(fighters[0].IsCritical){
                    fighters[0].CurrentStrength = fighters[0].Strength * (fighters[0].CriticalDamage / 10);
                }
                else{
                    fighters[0].CurrentStrength = fighters[0].Strength;
                }

				// Si le second combattant fait un coup critique
				if (fighters[1].IsCritical)
				{
					fighters[1].CurrentStrength = fighters[1].Strength * (fighters[1].CriticalDamage / 10);
				}
				else
				{
					fighters[1].CurrentStrength = fighters[1].Strength;
				}

				// On calcule l'impact de la frappe sur la santé de l'adversaire :
                // Santé = Santé - (Force de l'adversaire - (Force de l'adversaire * (L'armure du receveur / 100))
				fighters[0].Health = fighters[0].Health - (fighters[1].CurrentStrength - (fighters[1].CurrentStrength * (fighters[0].Armor / 100)));
                fighters[1].Health = fighters[1].Health - (fighters[0].CurrentStrength - (fighters[0].CurrentStrength * (fighters[1].Armor / 100)));

                // Numéro du round
                Console.WriteLine("****** ROUND "+i+" *******");
                				
                Console.WriteLine("health fighter 1 : " + fighters[0].Health.ToString());
				
                Console.WriteLine("health fighter 2 : " + fighters[1].Health.ToString());

                // Si une des deux santé est à 0 ou en dessous
                if (fighters[0].Health <= 0 || fighters[1].Health <= 0){

                    // Si c'est le premier qui a perdu
                    if(fighters[0].Health < fighters[1].Health){
                        return fighters[1].Name;
                    }
                    // Sinon c'est l'autre
                    else{
						return fighters[0].Name;
					}

                }
              
 
			}

            return null;
        }

        // Methode qui renvoie true ou false, selon la critical chance
        private static bool IsCritical(int cc){
			Random r = new Random();
			return NextBool(r,cc);

		}
        // Methode qui tente de rendre le aléatoire plus performant
		 static bool NextBool(this Random r, int truePercentage = 50)
		{
			return r.NextDouble() < truePercentage / 100.0;
		}
    }
}
