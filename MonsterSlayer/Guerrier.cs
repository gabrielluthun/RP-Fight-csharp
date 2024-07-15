using MonsterSlayer;
using System.Net;
using System.Threading.Tasks;

/* "Guerrier" is the 'main class' of the game. 
 * Any other class (Elfe & Nain) herits from it.
 * It contains all logical things that refers to warrior class (state, GameOver, escape)
 */

public class Guerrier
{
    public string Nom;
    public int PV;
    public int NbDesAttaque;

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

        //Simulation d'un dé de lancer en JDR
        int jetPourToucherJoueur = desAttaque.Next(1, 21);
        int jetPourToucherEnnemi = desAttaque.Next(1, 21); 

        // Modificateurs de classe
        if (Joueur is Elfe) jetPourToucherJoueur = Math.Max(jetPourToucherJoueur, 6); 
        if (Joueur is Nain nain) jetPourToucherEnnemi -= nain.Bouclier;

        // Calcul des dégâts si le jet pour toucher est réussi
        attaqueJoueur = (jetPourToucherJoueur >= 5) ? desAttaque.Next(1, 7) + (Joueur is Elfe elfe ? elfe.NbrePointsAttaque : 0) : 0;
        attaqueEnnemi = (jetPourToucherEnnemi >= 5) ? desAttaque.Next(1, 7) : 0;

        //Si le dé affiche moins de 5 ET si on est pas un elfe, on peut rater des attaques

        if (jetPourToucherJoueur < 5 && Joueur is Nain)
        {
            attaqueJoueur = 0;
            Console.WriteLine($"\nL'Attaque de {Joueur.Nom} n'a pas touché car le dé indique " + jetPourToucherJoueur + " ! Dégâts infligés : " + attaqueJoueur + " !");
        }
        else if (jetPourToucherEnnemi < 5 && Joueur is Nain )
        {
            attaqueEnnemi = 0;
            Console.WriteLine($"\nL'Attaque de {Ennemi.Nom} n'a pas touché car le dé indique " + jetPourToucherEnnemi + "! Dégâts infligés : " + attaqueEnnemi + " !");
        } 
        else if (jetPourToucherJoueur < 5 && jetPourToucherEnnemi < 5)
        {
            attaqueJoueur = 0;
            attaqueEnnemi = 0;
            Console.WriteLine($"\nLes deux attaques ont raté ! Dégâts infligés : {attaqueJoueur} pour {Joueur.Nom} et {attaqueEnnemi} pour {Ennemi.Nom} !");
        } 
        else if (Joueur is Elfe)
        {
            jetPourToucherEnnemi = 20;
        }
      
        Console.WriteLine($"\nAttaque de {Joueur.Nom} : {attaqueJoueur} !");
        Thread.Sleep(750);
        Console.WriteLine($"Attaque de {Ennemi.Nom} : {attaqueEnnemi} !");
        Thread.Sleep(750);
    }

    public static void AfficherEtat(Guerrier Joueur, Guerrier Ennemi)
    {
        Console.WriteLine("\n--- État des combattants ---");
        Console.WriteLine($"{Joueur.Nom} - PV: {Joueur.PV}");
        Console.WriteLine($"{Ennemi.Nom} - PV: {Ennemi.PV}");
        Console.WriteLine("----------------------------");
        Thread.Sleep(1000);
    }

    public static void GameOver(Guerrier Joueur, Guerrier Ennemi)
    {
        if (Joueur.PV <= 0)
        {
            Joueur.PV = 0;
            Console.WriteLine("\nDéfaite...");
        }
        else if (Ennemi.PV <= 0)
        {
            Ennemi.PV = 0;
            Console.WriteLine("\nVictoire !");
        }
    }

    public static bool Fuite(Guerrier Joueur)
    {
        // Vérification initiale des PV du joueur
        if (Joueur.PV > 50)
        {
            Console.WriteLine("Vos PV sont supérieurs à 50, vous ne pouvez donc pas fuir !");
            Thread.Sleep(750);
            return false;
        }

        Console.WriteLine("Lancement du dé de fuite...");
        Thread.Sleep(750);
        int scoreDe = new Random().Next(1, 21);

        if (scoreDe == 1)
        {
            Console.WriteLine("Échec critique, vous ne pouvez pas fuir car le dé indique 1 !");
            Thread.Sleep(750);
            return false;
        }
        else if (scoreDe > 1 && scoreDe <= 10)
        {
            Console.WriteLine("Vous ne pouvez pas fuir car le dé indique " + scoreDe + " !");
            Thread.Sleep(750);
            return false;
        }
        else if (scoreDe > 10)
        {
            Console.WriteLine("Vous avez fui !");
            return true;
        }

        // Retour par défaut, bien que normalement inatteignable
        return false;
    }


}


