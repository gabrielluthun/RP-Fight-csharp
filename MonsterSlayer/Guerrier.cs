using MonsterSlayer;
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

    public static void CalculAttaque(Guerrier Joueur, Guerrier Ennemi, out int attaqueJoueur, out int attaqueEnnemi)
    {
        Random desAttaque = new Random();
        attaqueJoueur = 0;
        attaqueEnnemi = 0;

        // Jet pour toucher
        int jetPourToucherJoueur = desAttaque.Next(1, 21); // D20
        int jetPourToucherEnnemi = desAttaque.Next(1, 21); // D20

        // Modificateurs de classe
        if (Joueur is Elfe) jetPourToucherJoueur = Math.Max(jetPourToucherJoueur, 2); // 2 est la valeur par défaut 
        if (Joueur is Nain nain) jetPourToucherEnnemi -= nain.Bouclier; // Nain encaisse mieux

        // Calcul des dégâts si le jet pour toucher est réussi
        attaqueJoueur = (jetPourToucherJoueur >= 5) ? desAttaque.Next(1, 7) + (Joueur is Elfe elfe ? elfe.NbrePointsAttaque : 0) : 0;
        attaqueEnnemi = (jetPourToucherEnnemi >= 5) ? desAttaque.Next(1, 7) : 0;

        if (jetPourToucherJoueur < 5)
        {
            Console.WriteLine($"\nL'Attaque de {Joueur.Nom} n'a pas touché ! Dégâts infligés : {attaqueJoueur} !");
        }
        else if (jetPourToucherEnnemi < 5)
        {
            Console.WriteLine($"\nL'Attaque de {Ennemi.Nom} n'a pas touché ! Dégâts infligés : {attaqueEnnemi} !");
        }

        Console.WriteLine($"\nAttaque de {Joueur.Nom} : {attaqueJoueur} !");
        Thread.Sleep(750);
        Console.WriteLine($"Attaque de {Ennemi.Nom} : {attaqueEnnemi} !");
        Thread.Sleep(750);
    }
}


