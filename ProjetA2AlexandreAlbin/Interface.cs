using Projet_A2_S1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjetA2AlexandreAlbin
{
    internal class Interface
    {
        private static Jeu currentGame;

        #region Helpers pour l'esthétique

        // Méthode simple pour les titres avec des tirets
        private static void AfficherTitre(string titre)
        {
            string ligne = new string('-', 50);
            Console.WriteLine(ligne);
            Console.WriteLine("   " + titre); 
            Console.WriteLine(ligne);
        }

        #endregion

        #region Interface du jeu :

        public static void PageAcceuil()
        {
            Console.Clear();
            AfficherTitre("BIENVENUE DANS LE JEU DES MOTS !!");

            Console.WriteLine("\n\n    [ RÈGLES DU JEU ]");
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

            AfficherTitre("CONFIGURATION DE LA GRILLE DE JEU");

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
                    path = Path.Combine("externalFiles", "PlateauTest", "Pregenerer", "Plateau" + nbr + ".txt");
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
            Console.WriteLine("\n--- GRILLE GÉNÉRÉE ---\n");
            Console.WriteLine(map);
            Console.WriteLine("\nAppuyez sur une touche pour configurer les joueurs...");
            Console.ReadKey();
        }

        private static void InitialisationDesJoueurs(Plateau map)
        {
            Console.Clear();
            AfficherTitre("INSCRIPTION DES JOUEURS");

            Console.Write("\n     Nom du Joueur 1 : ");
            string nom1 = Methode.MotValide();
            Console.Write("     Nom du Joueur 2 : ");
            string nom2 = Methode.MotValide();

            Joueur j1 = new Joueur(nom1);
            Joueur j2 = new Joueur(nom2);

            Console.Clear();
            Console.WriteLine("\n     Joueurs enregistrés avec succès !");
            Console.WriteLine("    ------------------------------------------");
            Console.WriteLine("    P1: " + j1.toString() + " | P2: " + j2.toString());
            Console.WriteLine("    ------------------------------------------");

            Console.WriteLine("\n    Appuyer sur [ENTRÉE] pour choisir la langue...");
            Console.ReadKey();
            

            Dictionnaire dico = new Dictionnaire();
            Jeu Game = new Jeu(dico, j1, j2, map);

            currentGame = Game; // Ajout de cette ligne pour définir la partie en cours

            Partie(Game);
        }

        

        private static void Partie(Jeu game)
        {
            while (game.EtatJeu())
            {
                game.Round();
            }

            Console.Clear();
            AfficherTitre("PARTIE TERMINÉE !");

            Console.WriteLine("\n    --- TABLEAU DES SCORES ---");

            // Remplacement des $ et {} par des +
            if (game.J1.Score > game.J2.Score)
            {
                Console.WriteLine("     VICTOIRE DE : " + game.J1.Nom.ToUpper() + " (" + game.J1.Score + " pts)");
                Console.WriteLine("     DEUXIÈME : " + game.J2.Nom + " (" + game.J2.Score + " pts)");
            }
            else if (game.J2.Score > game.J1.Score)
            {
                Console.WriteLine("     VICTOIRE DE : " + game.J2.Nom.ToUpper() + " (" + game.J2.Score + " pts)");
                Console.WriteLine("     DEUXIÈME : " + game.J1.Nom + " (" + game.J1.Score + " pts)");
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
            Console.Clear();
            Console.WriteLine("=== PAUSE ===");
            Console.WriteLine("1. Reprendre la partie");
            Console.WriteLine("2. Relancer une partie");
            Console.WriteLine("3. Sauvegarder");
            Console.WriteLine("4. Quitter");

            // On utilise ReadKey pour éviter de bloquer l'input stream principal
            var choix = Console.ReadKey(true).KeyChar;

            if (choix == '4') Environment.Exit(0);
            else if (choix == '2') Interface.PageAcceuil();
            else if (choix == '3')
            {
                if (currentGame != null)
                {
                    currentGame.ToFile();
                    Console.WriteLine("\nSauvegarde OK !");
                    System.Threading.Thread.Sleep(1000); // Petite pause visuelle
                    AfficherMenuPause(); // On relance le menu
                }
            }
            else // (Choix 1 ou autre touche : on reprend)
            {
                // C'EST ICI QUE LA MAGIE OPÈRE
                Console.Clear(); // On efface le menu pause

                if (currentGame != null)
                {
                    // 1. On redessine la grille
                    Methode.AfficherMatrice(currentGame.Map.Matrice);

                    // 2. On redessine les infos (Scores, tours...)
                    Console.WriteLine("\n\nScore Actuel : J1 " + currentGame.J1.Score + " | J2 " + currentGame.J2.Score);

                    // 3. On indique qui doit jouer pour redonner le contexte
                    string joueurActuel = (currentGame.EtatPartie.Count % 2 == 0) ? currentGame.J2.Nom : currentGame.J1.Nom;
                    Console.WriteLine("C'est au tour de : " + joueurActuel);
                }

                // Quand on sort d'ici, on retourne dans MotValide
                // Et MotValide affichera juste la ligne "> Reprise : ..." en dessous de tout ça.
            }
        }

        #endregion
    }
}