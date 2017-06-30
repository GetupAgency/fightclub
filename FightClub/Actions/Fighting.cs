using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Troschuetz.Random;

namespace FightClub
{
    public class Fighting
    {
        private List<Fighter> fighters = new List<Fighter>();
        private Datafights stats;
        public Fighting()
        {
            // idées pour faire évoluer
			// chance de taper 2 fois
			// Paralyser pour le tour d'après


			// On déclare et on initialise les combattants
			Fighter fighter1 = new Fighter
			// Pour voir les propriétés d'un fighter -> clic droit + Atteindre la déclaration
			{
				Armor = 45,
				Strength = 28,
				Exp = 0,
				Health = 1000,
				CriticalChance = 20,
				CriticalDamage = 110,
				MissedChance = 50,
				Name = "Ragnar"
			};
			Fighter fighter2 = new Fighter
			{
				Armor = 45,
				Strength = 25,
				Exp = 0,
				Health = 1000,
				CriticalChance = 20,
				CriticalDamage = 120,
				MissedChance = 50,
				Name = "Hercule"
			};

			// On initialise une liste de combattants
			fighters = new List<Fighter>
			{
                // On ajoute les combattants à notre liste
                fighter1,
				fighter2
			};

            // GoFight(fighters) est la méthode qui réalise le combat, elle renvoie le nom du vainqueur
            if (File.Exists(@"datafights.txt")){
                var jsonText = System.IO.File.ReadAllText(@"datafights.txt");
				stats = JsonConvert.DeserializeObject<Datafights>(jsonText);
            }
            else {
				LaunchStats();
			}
				Console.WriteLine("And the winner is : " + GoFight());
				//GenerateRandom();
				string json = JsonConvert.SerializeObject(stats);

				//write string to file
				if (File.Exists(@"datafights.txt"))
				{
					File.Delete(@"datafights.txt");
				}
				System.IO.File.WriteAllText(@"datafights.txt", json);

			

			Console.ReadLine();
        }
        private void LaunchStats(){
            
            stats = new Datafights();
            foreach (var fighter in fighters){
                
                stats.Fighters.Add(new FighterStats{
                    Name = fighter.Name,
                });
            }

        }
		private string GoFight()
		{

			// Nombre de rounds
			var rounds = 10000;
			// On boucle pour chaque round
			for (int i = 0; i < rounds; i++)
			{
				
			
				// On définit si chaque joueur dispose d'un coup critique lors de ce round
				fighters[0].IsCritical = Utils.IsSuccess(fighters[0].CriticalChance);
				fighters[1].IsCritical = Utils.IsSuccess(fighters[1].CriticalChance);

				// On définit si chaque joueur esquive ou non ce round
				fighters[0].IsMissed = Utils.IsSuccess(fighters[0].MissedChance);
				fighters[1].IsMissed = Utils.IsSuccess(fighters[1].MissedChance);

				// Si le premier combattant fait un coup critique
				if (fighters[0].IsCritical)
				{
                    fighters[0].Stats.CriticalNumber = fighters[0].Stats.CriticalNumber + 1;
					fighters[0].CurrentStrength = fighters[0].Strength * (1 + (fighters[0].CriticalDamage / 100));
				}
				else
				{
					fighters[0].CurrentStrength = fighters[0].Strength;
				}

				// Si le second combattant fait un coup critique
				if (fighters[1].IsCritical)
				{
					fighters[1].Stats.CriticalNumber = fighters[1].Stats.CriticalNumber + 1;
					fighters[1].CurrentStrength = fighters[1].Strength * (1 + (fighters[1].CriticalDamage / 100));
				}
				else
				{
					fighters[1].CurrentStrength = fighters[1].Strength;
				}


				// Si le premier combattant esquive
				if (fighters[0].IsMissed)
				{
                    fighters[0].Stats.MissedNumber = fighters[0].Stats.MissedNumber + 1;
					fighters[1].CurrentStrength = 0;
					//Console.WriteLine(fighters[0].Name + " ESQUIVE LE COUP DE " + fighters[1].Name);
				}

				//Si le deuxième combattant esquive 
				if (fighters[1].IsMissed)
				{
					fighters[1].Stats.MissedNumber = fighters[1].Stats.MissedNumber + 1;
					fighters[0].CurrentStrength = 0;
					//Console.WriteLine(fighters[1].Name + " ESQUIVE LE COUP DE " + fighters[0].Name);
				}

				//Console.WriteLine("force premier : " + fighters[0].CurrentStrength.ToString());
				//Console.WriteLine("force second : " + fighters[1].CurrentStrength.ToString());
				// On calcule l'impact de la frappe sur la santé de l'adversaire :
				// Santé = Santé - (Force de l'adversaire - (Force de l'adversaire * (L'armure du receveur / 100))
				fighters[0].Health = fighters[0].Health - (fighters[1].CurrentStrength - (fighters[1].CurrentStrength * (fighters[0].Armor / 100)));
				fighters[1].Health = fighters[1].Health - (fighters[0].CurrentStrength - (fighters[0].CurrentStrength * (fighters[1].Armor / 100)));

				// Numéro du round
				/*  Console.WriteLine("****** ROUND " + i + " *******");

				  Console.WriteLine("health fighter 1 : " + fighters[0].Health.ToString());

				  Console.WriteLine("health fighter 2 : " + fighters[1].Health.ToString());*/

				// Si une des deux santé est à 0 ou en dessous
				if (fighters[0].Health <= 0 || fighters[1].Health <= 0)
				{
					fighters[0].Stats.FightDate = DateTime.Now;
					fighters[1].Stats.FightDate = DateTime.Now;
					fighters[0].Stats.RemainingHealth = fighters[0].Health;
					fighters[1].Stats.RemainingHealth = fighters[1].Health;
					fighters[0].Stats.RoundsNumber = i;
					fighters[1].Stats.RoundsNumber = i;

					// Si c'est le premier qui a perdu
					if (fighters[0].Health < fighters[1].Health)
					{
                        

						fighters[1].Stats.Victory = true;
						fighters[0].Stats.Victory = false;
						
						stats.Fighters[1].Victories = stats.Fighters[1].Victories + 1;
						stats.Fighters[0].Defeats = stats.Fighters[0].Defeats + 1;

						stats.Fighters[0].Stats.Add(fighters[0].Stats);
						stats.Fighters[1].Stats.Add(fighters[1].Stats);

						return fighters[1].Name + " " + i;
					}
					// Sinon c'est l'autre
					else
					{
						fighters[0].Stats.Victory = true;
                        fighters[1].Stats.Victory = false;

						stats.Fighters[0].Victories = stats.Fighters[0].Victories + 1;
                        stats.Fighters[1].Defeats = stats.Fighters[1].Defeats + 1;

						stats.Fighters[0].Stats.Add(fighters[0].Stats);
						stats.Fighters[1].Stats.Add(fighters[1].Stats);

						return fighters[0].Name + " " + i;
					}

				}


			}

			return null;
		}

		
    }
}
