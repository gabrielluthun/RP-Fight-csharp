using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class Guerrier
{
    public string Nom { get; set; }
    public int PV { get; set; }
    public int NbDesAttaque { get; set; }

    public Guerrier(string nom, int pv, int nbDesAttaque)
    {
        Nom = nom;
        PV = pv;
        NbDesAttaque = nbDesAttaque;
    }

    public virtual void CalculAttaque(Guerrier ennemi, out int attaqueJoueur, out int attaqueEnnemi)
    {
        Random desAttaque = new Random();
        attaqueJoueur = desAttaque.Next(1, 7) * NbDesAttaque;
        attaqueEnnemi = desAttaque.Next(1, 7) * ennemi.NbDesAttaque;
    }
}


