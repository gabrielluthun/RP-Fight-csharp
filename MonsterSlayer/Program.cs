/* 1. Lancer le dé pour la fuite -> si inférieur à 10, indiquer "Vous pouvez pas fuir"
   2. Lancer le dé pour se soigner -> si inférieur à 10, indiquer "Vous pouvez pas vous soigner pour l'instant"
   3. 
*/

using System;
using System.Threading;
using MonsterSlayer;

static void Game()
{
    Guerrier Joueur;
    Guerrier Ennemi = new Guerrier("Vergil", 100, 1);

    Console.WriteLine("--------------- Bienvenue dans Monster Slayer ! ---------------");
    Console.WriteLine("Choisissez votre type de personnage :");
    Console.WriteLine("1. Nain -> capable d'encaisser les coups");
    Console.WriteLine("2. Elfe -> ne rate jamais les attaques");
    Console.Write("Votre choix : ");
    string choix = Console.ReadLine()!;

    switch (choix)
    {
        case "1":
            Joueur = new Nain("Dante", 100, 1, new Random().Next(1, 11));
            Console.WriteLine("Vous avez choisi : Nain");
            break;
        case "2":
            Joueur = new Elfe("Dante", 100, 1, 5);
            Console.WriteLine("Vous avez choisi : Elfe");
            break;
        default:
            Console.WriteLine("Choix invalide. Par défaut, vous serez un Guerrier.");
            Joueur = new Guerrier("Dante", 100, 1);
            break;
    }

    Console.WriteLine("\nLe combat commence !");
    Thread.Sleep(1000);

    while (Joueur.PV > 0 && Ennemi.PV > 0) // Boucle jusqu'à ce que l'un des personnages n'ait plus de PV
    {
        int attaqueJoueur = 0;
        int attaqueEnnemi = 0;

        CalculAttaque(Joueur, Ennemi, out attaqueJoueur, out attaqueEnnemi);
        GestionPV(Joueur, Ennemi, attaqueJoueur, attaqueEnnemi);
        AfficherEtat(Joueur, Ennemi);
        GameOver(Joueur, Ennemi);

        if (Joueur.PV > 0 && Ennemi.PV > 0)
        {
            Console.WriteLine("\nQue voulez-vous faire ?");
            Console.WriteLine("1. Continuer à combattre");
            Console.WriteLine("2. Utiliser une potion de soin");
            Console.WriteLine("3. Fuir le combat");
            Console.Write("Votre choix : ");
            string action = Console.ReadLine()!;

            switch (action)
            {
                case "1":
                    Console.WriteLine("Vous continuez à combattre !");
                    break;
                case "2":
                    UtiliserPotion(Joueur);
                    break;
                case "3":
                    Console.WriteLine("Vous avez fui le combat !");
                    return;
                default:
                    Console.WriteLine("Choix invalide. Vous continuez à combattre !");
                    break;
            }
        }
    }
}

static void CalculAttaque(Guerrier Joueur, Guerrier Ennemi, out int attaqueJoueur, out int attaqueEnnemi)
{
    Random desAttaque = new Random();
    attaqueJoueur = 0;
    attaqueEnnemi = 0;

    // Jet pour toucher
    int jetPourToucherJoueur = desAttaque.Next(1, 21); // D20
    int jetPourToucherEnnemi = desAttaque.Next(1, 21); // D20

    // Modificateurs de classe
    if (Joueur is Elfe) jetPourToucherJoueur = Math.Max(jetPourToucherJoueur, 2); // Elfe ne rate jamais
    if (Joueur is Nain nain) jetPourToucherEnnemi -= nain.Bouclier; // Nain encaisse mieux

    // Calcul des dégâts si le jet pour toucher est réussi
    attaqueJoueur = (jetPourToucherJoueur >= 5) ? desAttaque.Next(1, 7) + (Joueur is Elfe elfe ? elfe.NbrePointsAttaque : 0) : 0;
    attaqueEnnemi = (jetPourToucherEnnemi >= 5) ? desAttaque.Next(1, 7) : 0;

    if (jetPourToucherJoueur < 5)
    {
        Console.WriteLine($"\nL'Attaque de {Joueur.Nom} n'a pas touché ! Dégâts infligés : {attaqueJoueur} !");
    } else if (jetPourToucherEnnemi < 5)
    {
        Console.WriteLine($"\nL'Attaque de {Ennemi.Nom} n'a pas touché ! Dégâts infligés : {attaqueEnnemi} !");
    }

    Console.WriteLine($"\nAttaque de {Joueur.Nom} : {attaqueJoueur} !");
    Thread.Sleep(750);
    Console.WriteLine($"Attaque de {Ennemi.Nom} : {attaqueEnnemi} !");
    Thread.Sleep(750);
}

static void UtiliserPotion(Guerrier Joueur)
{
    Random random = new Random();
    int soin = random.Next(10, 21); // Soigne entre 10 et 20 PV
    Joueur.PV += soin;
    Console.WriteLine($"Vous utilisez une potion de soin et récupérez {soin} PV !");
    Thread.Sleep(1000);
}

static void AfficherEtat(Guerrier Joueur, Guerrier Ennemi)
{
    Console.WriteLine("\n--- État des combattants ---");
    Console.WriteLine($"{Joueur.Nom} - PV: {Joueur.PV}");
    Console.WriteLine($"{Ennemi.Nom} - PV: {Ennemi.PV}");
    Console.WriteLine("----------------------------");
    Thread.Sleep(1000);
}

static void GameOver(Guerrier Joueur, Guerrier Ennemi)
{
    if (Joueur.PV <= 0)
    {
        Joueur.PV = 0;
        Console.WriteLine("\nGame Over, victoire de : " + Ennemi.Nom);
    }
    else if (Ennemi.PV <= 0)
    {
        Ennemi.PV = 0;
        Console.WriteLine("\nVictoire de " + Joueur.Nom);
    }
}

static void GestionPV(Guerrier Joueur, Guerrier Ennemi, int attaqueJoueur, int attaqueEnnemi)
{
    Joueur.PV -= attaqueEnnemi;
    Ennemi.PV -= attaqueJoueur;
}

Game();
