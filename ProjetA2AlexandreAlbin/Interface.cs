using Projet_A2_S1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetA2AlexandreAlbin
{
    internal class Interface
    {
        #region Helpers pour l'esthétique
        // Petite méthode pour dessiner des lignes de séparation
        private static void DessinerLigne() => Console.WriteLine("╔" + new string('═', 66) + "╗");
        private static void DessinerFin() => Console.WriteLine("╚" + new string('═', 66) + "╝");
        #endregion

        #region Interface du jeu :

        public static void PageAcceuil()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            DessinerLigne();
            Console.WriteLine("║                BIENVENUE DANS LE JEU DES MOTS !!                 ║");
            DessinerFin();
            Console.ResetColor();

            Console.WriteLine("\n\n    [ RÉGLLES DU JEU ]");
            Console.WriteLine("    - Trouvez un maximum de mots dans la grille.");
            Console.WriteLine("    - Les mots doivent être présents dans le dictionnaire.");
            Console.WriteLine("    - Chaque lettre a une valeur en points.");

            Console.WriteLine("\n\n\n    Appuyez sur une touche pour commencer l'aventure...");
            Console.ReadKey();
            DeuxiemePage();
        }

        private static void DeuxiemePage()
        {
            Console.Clear();
            Plateau map;
            string path;

            Console.ForegroundColor = ConsoleColor.Yellow;
            DessinerLigne();
            Console.WriteLine("║                CONFIGURATION DE LA GRILLE DE JEU                ║");
            DessinerFin();
            Console.ResetColor();

            Console.WriteLine("\n    1.  Charger une grille enregistrée");
            Console.WriteLine("    2.  Charger via un fichier externe (.txt)");
            Console.WriteLine("    3.  Créer une grille aléatoire");
            Console.Write("\n    Votre choix : ");

            int var = Methode.ChiffreValide();

            switch (var)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine(" > Saisissez le nom du dossier : ");
                    string mot = Methode.MotValide();
                    path = Path.Combine("externalFiles", mot);
                    map = new Plateau(path);
                    AfficherPlateau(map);
                    InitialisationDesJoueurs(map);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine(" > Quel plateau voulez-vous charger ? (de 1 à 7)");
                    int nbr = Methode.ChiffreValide();
                    path = Path.Combine("externalFiles", "PlateauTest","Pregenerer","Plateau" + nbr + ".txt");
                    map = new Plateau(path);
                    AfficherPlateau(map);
                    InitialisationDesJoueurs(map);
                    break;
                case 3:
                    Console.WriteLine("\n > Nombre de lignes ?");
                    int ligne = Methode.ChiffreValide();
                    Console.WriteLine(" > Nombre de colonnes ?");
                    int colonne = Methode.ChiffreValide();
                    map = new Plateau(ligne, colonne);
                    AfficherPlateau(map);
                    InitialisationDesJoueurs(map);
                    break;
                default:
                    DeuxiemePage();
                    break;
            }
        }

        private static void AfficherPlateau(Plateau map)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--- GRILLE GÉNÉRÉE ---\n");
            Console.ResetColor();
            Console.WriteLine(map);
            Console.WriteLine("\nAppuyez sur une touche pour configurer les joueurs...");
            Console.ReadKey();
        }

        private static void InitialisationDesJoueurs(Plateau map)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            DessinerLigne();
            Console.WriteLine("║                   INSCRIPTION DES JOUEURS                        ║");
            DessinerFin();
            Console.ResetColor();

            Console.Write("\n     Nom du Joueur 1 : ");
            string nom1 = Methode.MotValide();
            Console.Write("     Nom du Joueur 2 : ");
            string nom2 = Methode.MotValide();

            Joueur j1 = new Joueur(nom1);
            Joueur j2 = new Joueur(nom2);

            Console.Clear();
            Console.WriteLine("\n     Joueurs enregistrés avec succès !");
            Console.WriteLine($"    ------------------------------------------");
            Console.WriteLine($"    P1: {j1.toString()} | P2: {j2.toString()}");
            Console.WriteLine($"    ------------------------------------------");

            Console.WriteLine("\n    Appuyer sur [ENTRÉE] pour choisir la langue...");
            Console.ReadKey();
            InitialisationDeLaLangue(j1, j2, map);
        }

        private static void InitialisationDeLaLangue(Joueur j1, Joueur j2, Plateau map)
        {
            Console.Clear();
            Dictionnaire dico;
            Jeu Game;

            Console.ForegroundColor = ConsoleColor.Yellow;
            DessinerLigne();
            Console.WriteLine("║                    CHOIX DE LA LANGUE                            ║");
            DessinerFin();
            Console.ResetColor();

            Console.WriteLine("\n    1.  Français");
            Console.WriteLine("    2.  Anglais (Beta)");
            Console.Write("\n    Votre choix : ");

            int var = Methode.ChiffreValide();

            // Logique inchangée pour le dictionnaire
            dico = new Dictionnaire();
            Game = new Jeu(dico, j1, j2, map);

            
            Partie(Game);
        }

        private static void Partie(Jeu game)
        {
            while (game.EtatJeu())
            {
                game.Round();
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            DessinerLigne();
            Console.WriteLine("║                   PARTIE TERMINÉE !                              ║");
            DessinerFin();
            Console.ResetColor();

            Console.WriteLine("\n    --- TABLEAU DES SCORES ---");
            if (game.J1.Score > game.J2.Score)
            {
                Console.WriteLine($"     VICTOIRE DE : {game.J1.Nom.ToUpper()} ({game.J1.Score} pts)");
                Console.WriteLine($"     DEUXIÈME : {game.J2.Nom} ({game.J2.Score} pts)");
            }
            else if (game.J2.Score > game.J1.Score)
            {
                Console.WriteLine($"     VICTOIRE DE : {game.J2.Nom.ToUpper()} ({game.J2.Score} pts)");
                Console.WriteLine($"     DEUXIÈME : {game.J1.Nom} ({game.J1.Score} pts)");
            }
            else
            {
                Console.WriteLine("     ÉGALITÉ PARFAITE !");
            }

            game.SaveEtatPartie();
            Console.WriteLine("\n    Partie sauvegardée. Merci d'avoir joué !");
            Console.ReadKey();
        }

        public static void AfficherMenuPause()
        {
            // On sauvegarde l'ancienne vue ou on prévient l'utilisateur
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("=== PAUSE ===");
            Console.ResetColor();
            Console.WriteLine("1. Reprendre la partie");
            Console.WriteLine("2. Relancer une partie");
            Console.WriteLine("3. Sauvegarder la grille générer (si la partie a été initialisé");
            Console.WriteLine("4. Quitter le jeu");

            string choix = Console.ReadLine();

            if (choix == "3")
            {
                Environment.Exit(0); // Quitte proprement le programme
            }
            if(choix == "2")
            {
                Program.Test4();
            }

            //Rajouter grille


            // Si l'utilisateur choisit 1 ou autre, la fonction se termine.
            // Le programme "retombe" dans la boucle de saisie de MotValide().
            Console.Clear();
            Console.WriteLine("Reprise de la saisie...");
        }

        #endregion
    }
}