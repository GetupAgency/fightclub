using System;
using System.Collections.Generic;
using System.Linq;
using Troschuetz.Random;
using Troschuetz.Random.Distributions.Continuous;
using Troschuetz.Random.Generators;
namespace FightClub
{
    static class Program
    {

        static void Main(string[] args)
        {
            // coup raté                            fait --> esquive
            // plusieurs combats en meme temps
            // chance de taper 2 fois
            // Paralyser pour le tour d'après


            // On déclare et on initialise les combattants
            Fighter fighter1 = new Fighter
            // Pour voir les propriétés d'un fighter -> clic droit + Atteindre la déclaration
            {
                Armor = 45,
                Strength = 45,
                Exp = 0,
                Health = 10000,
                CriticalChance = 50,
                CriticalDamage = 110,
                MissedChance = 30,
                Name = "Ragnar"
            };
            Fighter fighter2 = new Fighter
            {
                Armor = 45,
                Strength = 45,
                Exp = 0,
                Health = 10000,
                CriticalChance = 50,
                CriticalDamage = 110,
                MissedChance = 50,
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
            // Console.WriteLine("And the winner is : " + GoFight(fighters));
            GenerateRandom();
            Console.ReadLine();
        }


        private static string GoFight(List<Fighter> fighters) {

            // Nombre de rounds
            var rounds = 10000;



            // On boucle pour chaque round
            for (int i = 0; i < rounds; i++) {
                // On attend une seconde avant de lancer le round suivant
                //Thread.Sleep(1000);
                Random r2 = new Random();
                Console.WriteLine(r2.NextDouble());


                // On définit si chaque joueur dispose d'un coup critique lors de ce round
                fighters[0].IsCritical = IsSuccess(fighters[0].CriticalChance);

                fighters[1].IsCritical = IsSuccess(fighters[1].CriticalChance);

                // On définit si chaque joueur esquive ou non ce round
                fighters[0].IsMissed = IsSuccess(fighters[0].MissedChance);

                fighters[1].IsMissed = IsSuccess(fighters[1].MissedChance);

                // Si le premier combattant fait un coup critique
                if (fighters[0].IsCritical) {
                    fighters[0].CurrentStrength = fighters[0].Strength * (1 + (fighters[0].CriticalDamage / 100));
                }
                else {
                    fighters[0].CurrentStrength = fighters[0].Strength;
                }

                // Si le second combattant fait un coup critique
                if (fighters[1].IsCritical)
                {
                    fighters[1].CurrentStrength = fighters[1].Strength * (1 + (fighters[1].CriticalDamage / 100));
                }
                else
                {
                    fighters[1].CurrentStrength = fighters[1].Strength;
                }


                // Si le premier combattant esquive
                if (fighters[0].IsMissed)
                {
                    fighters[1].CurrentStrength = 0;
                    Console.WriteLine(fighters[0].Name+" ESQUIVE LE COUP DE " +fighters[1].Name);
                }
              
                //Si le deuxième combattant esquive 
                if (fighters[1].IsMissed)
                {
                    fighters[0].CurrentStrength = 0;
                    Console.WriteLine(fighters[1].Name + " ESQUIVE LE COUP DE " +fighters[0].Name);
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
                if (fighters[0].Health <= 0 || fighters[1].Health <= 0) {

                    // Si c'est le premier qui a perdu
                    if (fighters[0].Health < fighters[1].Health) {
                        return fighters[1].Name+" "+i;
                    }
                    // Sinon c'est l'autre
                    else {
                        return fighters[0].Name + " " + i;
                    }

                }


            }

            return null;
        }

        // Methode qui renvoie true ou false, selon la critical chance
        private static bool IsSuccess(int cc) {
            Random r = new Random();
            return NextBool(r, cc);

        }
        // Methode qui tente de rendre le aléatoire plus performant
        static bool NextBool(this Random r, int truePercentage = 50)
        {
            return r.NextDouble() < truePercentage / 100.0;
        }
        private static void GenerateRandom()
        {
            var rounds = 101;

            for(var i = 1;i<rounds;i++)
            {
                var MonRandom = new TRandom();
                // Console.WriteLine("Chiffre aléatoire n°" + i + " :" + string.Join(", ", MonRandom.Integers().Take(1).Last()));
                Console.WriteLine(string.Join("", MonRandom.PoissonSamples(4).Take(200)));
            }
            /*
            Console.WriteLine("TRandom in action, used as an IGenerator");
            var trandom = new TRandom();
            Console.WriteLine(trandom.Next() - trandom.Next(5) + trandom.Next(3, 5));
            Console.WriteLine(trandom.NextDouble() * trandom.NextDouble(5.5) * trandom.NextDouble(10.1, 21.9));
            Console.WriteLine(trandom.NextBoolean());

            Console.WriteLine();

            // 2) Use TRandom to generate a few random numbers - via extension methods.
            Console.WriteLine("TRandom in action, used as an IGenerator augmented with extension methods");
            Console.WriteLine(string.Join(", ", trandom.Integers().Take(10)));
            Console.WriteLine(string.Join(", ", trandom.Doubles().Take(10)));
            Console.WriteLine(string.Join(", ", trandom.Booleans().Take(10)));

            Console.WriteLine();

            // 3) Use TRandom to generate a few distributed numbers.
            Console.WriteLine("TRandom in action, used as to get distributed numbers");
            Console.WriteLine(trandom.Normal(1.0, 0.1));
            Console.WriteLine(string.Join(", ", trandom.NormalSamples(1.0, 0.1).Take(20)));
            Console.WriteLine(trandom.Poisson(5));
            Console.WriteLine(string.Join(", ", trandom.PoissonSamples(5).Take(20)));

            Console.WriteLine();

            // 4) There are many generators available - XorShift128 is the default.
            var alf = new ALFGenerator(TMath.Seed());
            var nr3 = new NR3Generator();
            var std = new StandardGenerator(127);

            // 5) You can also use distribution directly, even with custom generators.
            Console.WriteLine("Showcase of some distributions");
            Console.WriteLine("Static sample for Normal: " + NormalDistribution.Sample(alf, 1.0, 0.1));
            Console.WriteLine("New instance for Normal: " + new NormalDistribution(1.0, 0.1).NextDouble());

            Console.WriteLine(); */

        }
    }
}
