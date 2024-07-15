using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterSlayer;

//Surcharge

namespace MonsterSlayer
{
    internal class Elfe : Guerrier
    {
        private int _nbPointsAttaque;
        public int NbrePointsAttaque { get => _nbPointsAttaque; set => _nbPointsAttaque = value; }

        //Héritage de la classe Guerrier
        public Elfe(string nom, int pv, int nbDesAttaque, int PointsAttaque) : base(nom, pv, nbDesAttaque)
        {
            NbrePointsAttaque = PointsAttaque;
        }

        public void CalculAttaque(Guerrier ennemi, out int attaqueJoueur, out int attaqueEnnemi)
        {
            Random desAttaque = new Random();
            // Garantir que l'attaque de l'elfe ne rate jamais
            attaqueJoueur = desAttaque.Next(1, 7) * NbDesAttaque + NbrePointsAttaque;
            attaqueEnnemi = desAttaque.Next(1, 7) * ennemi.NbDesAttaque;
            // Assurer que le lancer de dé pour l'elfe est toujours considéré comme réussi
            attaqueJoueur = Math.Max(attaqueJoueur, 5);
        }

    }
}
