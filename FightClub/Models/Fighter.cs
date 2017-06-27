﻿using System;

namespace FightClub
{
    public class Fighter
    {
		/** 
		 * Cette classe définie les caractéristiques d'un combattant
		 * Chaque propriété est déclaré comme suit :
		 * public (accessible à d'autres classes) string (type de la variable) get (on peut obtenir la valeur) set (on peut mettre à jour la valeur)
        **/

		//Propriétés de base
		public string Name { get; set; }
		public double Strength { get; set; }
		public double Health { get; set; }
		public double Armor { get; set; }
        public double Exp { get; set; }

        public int CriticalChance { get; set; }
		public int CriticalDamage { get; set; }

        // Propriétés qui changent à chaque round
		public bool IsCritical { get; set; }
		public bool IsMissed { get; set; }
		public double CurrentStrength { get; set; }

		// On calcule la force critique en augmentant la force par rapport aux dommages critiques
		public double CriticalStrength { get{
                return this.Strength * (this.CriticalDamage / 10);
            } }

        // Le constructeur de notre classe, qui permet d'initialiser des variables, de faire des traitements etc...
		public Fighter()
        {
            
        }
    }
}
