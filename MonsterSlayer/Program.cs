using System.ComponentModel.Design;
using System.Threading;
using MonsterSlayer;

static void Game()
{
    Guerrier Joueur;
    Guerrier Ennemi = new Guerrier("Vergil", 100, 10);
    Elfe ElfeEnnemi = new Elfe("Gimli", 150, 2, 10);
    Nain NainEnnemi = new Nain("Legolas", 175, 2, 15);

    Console.WriteLine("--------------- Bienvenue dans Monster Slayer ! ---------------");
    Console.WriteLine("Choisissez votre type de personnage :");
    Console.WriteLine("1. Nain -> capable d'encaisser les coups");
    Console.WriteLine("2. Elfe -> ne rate jamais les attaques");
    Console.WriteLine("3. Guerrier -> délivre des attaques puissantes");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("4. Comment jouer ?");
    Console.ResetColor();
    Console.Write("Votre choix : ");
    string choix = Console.ReadLine()!;

    switch (choix)
    {
        case "1":
            Joueur = new Nain("Dante", 100, 2, 1);
            Ennemi = NainEnnemi;
            Console.WriteLine("Vous avez choisi : Nain");
            break;
        case "2":
            Joueur = new Elfe("Dante", 100, 1, 1);
            Ennemi = ElfeEnnemi;
            Console.WriteLine("Vous avez choisi : Elfe");
            break;
        case "3":
            Joueur = new Guerrier("Dante", 100, 1);
            Console.WriteLine("Vous avez choisi : Guerrier");
            break;
        case "4": 
            AidePourJouer();
            return;
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
        Guerrier.CalculAttaque(Joueur, Ennemi, out attaqueJoueur, out attaqueEnnemi);
        GestionPV(Joueur, Ennemi, attaqueJoueur, attaqueEnnemi);
        Guerrier.AfficherEtat(Joueur, Ennemi);
        Guerrier.GameOver(Joueur, Ennemi);

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
                    if (Guerrier.Fuite(Joueur)) return; 
                    break;
                default:
                    Console.WriteLine("Choix invalide. Vous continuez à combattre !");
                    break;
            }
        }
    }

}
static void UtiliserPotion(Guerrier Joueur)
{
    int scoreDe = new Random().Next(1, 21);
    Console.WriteLine("Lancement du dé de soin...");
    Thread.Sleep(750);
    if (scoreDe < 10)
    {
        Console.WriteLine("Vous ne pouvez pas vous soigner pour l'instant !");
        Thread.Sleep(750);
        Console.WriteLine("Lancement du dé d'attaque'...");
        Thread.Sleep(750);
        return;
    } else {
        Random random = new Random();
        int soin = random.Next(10, 21); // Soigne entre 10 et 20 PV
        Joueur.PV += soin;
        Console.WriteLine($"Vous utilisez une potion de soin et récupérez {soin} PV !");
        Thread.Sleep(1000);
    }
}


static void GestionPV(Guerrier Joueur, Guerrier Ennemi, int attaqueJoueur, int attaqueEnnemi)
{
    Joueur.PV -= attaqueEnnemi;
    Ennemi.PV -= attaqueJoueur;
}

static void AidePourJouer()
{   Console.Clear();
    Console.WriteLine("--------------- Comment jouer ? ---------------");
    Console.WriteLine("Le but du jeu est de vaincre votre adversaire en lui infligeant des dégâts.");
    Console.WriteLine("Vous avez le choix entre 3 classes de personnages : Nain, Elfe et Guerrier.");
    Console.WriteLine("Chaque personnage a ses propres caractéristiques et compétences. \n");
    Console.WriteLine("Le combat se déroule en tour par tour, où vous et votre adversaire vous infligez des dégâts.");
    Console.WriteLine("Vous pouvez choisir de continuer à combattre, utiliser une potion de soin ou fuir le combat.");
    Console.WriteLine("Attention, le soin ou la fuite n'est pas toujours possible ! \n");
    Console.WriteLine("Bonne chance !");
    Console.WriteLine("-----------------------------------------------------------------");
    Console.WriteLine("Jeu en cours de développement.");
    Console.WriteLine("Appuyez sur une touche pour revenir au jeu...");
    Console.ReadKey();
    Console.Clear();
    Game();
}

Game();
