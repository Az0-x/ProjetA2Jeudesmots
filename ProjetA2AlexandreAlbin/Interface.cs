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

        #region Interface du jeu :

        public static void PageAcceuil()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------------------------------\n");
            Console.WriteLine("            Bienvenue dans le jeu des Mots !!     ");
            Console.WriteLine("\n--------------------------------------------------------------------\n\n\n\n");
            Console.WriteLine("Mettre uin petit read me assez complet sur le jeu \n\n\n\n\n\n\n\n\n");

            Console.WriteLine("Pour continuer veuillez appuyer sur une touche ");
            Console.ReadKey();
            DeuxiemePage();
        }

        private static void DeuxiemePage()
        {
            Console.Clear();
            Plateau map;
            string path;
            Console.WriteLine("--------------------------------------------------------------------\n");
            Console.WriteLine("\n\n Voulez vous pour la grille :\n1.La chargé de notre base de grille enregistré ? \n2.La chargé via un de vos fichier ?\n3.La créer aléatoirement");

            int var = Methode.ChiffreValide();

            switch (var)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("--------------------------------------------------------------------\n");
                    Console.WriteLine("Mettez le nom de votre dossier :\n");
                    string mot = Methode.MotValide();
                    path = Path.Combine("externalFiles", mot);
                    map = new Plateau(path);
                    Console.WriteLine(map);


                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("--------------------------------------------------------------------\n");
                    Console.WriteLine("Quelle plateau voulez vous chargé ? ( de 1 à 7 )\n");
                    int nbr = Methode.ChiffreValide();
                    path = Path.Combine("externalFiles", "PlateauTest", "Plateau" + nbr + ".txt");
                    map = new Plateau(path);
                    Console.WriteLine(map);

                    InitialisationDesJoueurs(map);
                    break;
                case 3:
                    Console.WriteLine("Combien de ligne voulez vous ?");
                    int ligne = Methode.ChiffreValide();
                    Console.WriteLine("Combien de colonne voulez vous ?");
                    int colonne = Methode.ChiffreValide();

                    map = new Plateau(ligne, colonne);
                    Console.WriteLine(map);

                    InitialisationDesJoueurs(map);
                    break;
                default:
                    DeuxiemePage();
                    break;
            }
        }


        private static void InitialisationDesJoueurs(Plateau map)
        {
            Console.Clear();
            Console.WriteLine("Veuillez saisir le nom du premier joueur (joueur 1)");
            string nom1 = Methode.MotValide();
            Console.WriteLine("\n");
            Console.WriteLine("Veuillez saisir le nom du second joueur (joueur 2)");
            string nom2 = Methode.MotValide();
            Console.WriteLine("\n\n");

            Joueur j1 = new Joueur(nom1);
            Joueur j2 = new Joueur(nom2);


            Console.Clear();
            Console.WriteLine("Infos saisies : ");
            Console.WriteLine("j1 : " + j1.toString() + "     j2 : " + j2.toString());
            Console.WriteLine("Appuyer sur entrée pour aller à l'étape suivante");
            Console.ReadKey();
            InitialisationDeLaLangue(j1, j2, map);
        }

        private static void InitialisationDeLaLangue(Joueur j1, Joueur j2, Plateau map)
        {
            Console.Clear();
            Dictionnaire dico;
            string path;
            Jeu Game;
            Console.WriteLine("--------------------------------------------------------------------\n");
            Console.WriteLine("\n\n Voulez vous pour la grille :\n1.Langue Francaise  \n2.Langue Anglaise (pour l'instant la version anglaise ne fonctionne pas )");

            int var = Methode.ChiffreValide();

            switch (var)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("--------------------------------------------------------------------\n");
                    Console.WriteLine("Langue initialiser au francais :\n");
                    dico = new Dictionnaire();
                    Game = new Jeu(dico, j1, j2, map);
                    Partie(Game);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("--------------------------------------------------------------------\n");
                    Console.WriteLine("Langue initialiser au francais* :\n");
                    dico = new Dictionnaire();
                    Game = new Jeu(dico, j1, j2, map);
                    Partie(Game);
                    break;
                default:
                    InitialisationDeLaLangue(j1, j2, map);
                    break;
            }
            
        }

        private static void Partie(Jeu game)
        {
            while (game.EtatJeu())
            {
                //Definir le chrono pour un round
                game.Round();

            }

            Console.WriteLine("La partie est terminéee !!!");
            if (game.J1.Score > game.J2.Score)
            {
                Console.WriteLine("Victoire de " + game.J1.Nom + " sur " + game.J2.Nom);
            }
            else if (game.J2.Score > game.J1.Score)
            {
                Console.WriteLine("Victoire de " + game.J2.Nom + " sur " + game.J1.Nom);
            }
            else Console.WriteLine("Egalité parfaite entre " + game.J1.Nom + " et " + game.J2.Nom);

            game.SaveEtatPartie();

        }


        #endregion
    }
}
