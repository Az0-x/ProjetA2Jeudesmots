using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjetA2AlexandreAlbin
{
    internal class Jeu
    {
        static void Menu()
        {
            Jeu jeu = new Jeu();
            jeu.Init();
            
            // créer matrice init
            //jeu.Coups_Joueur();

        }
        public void Init()
        {
            //A voir si on fais une interface graphique ou autre, ou en tous cas pour la création de la partie et parametre, il y ai un choix avex les fleches plutot que ecrire tous les mots un a un

            
            PageAcceuil();
        }
        public void Coups_Joueur(char[,] tab)// Faire une alternace afin cahger de joueur entre le 1 et le 2 !!!
        {
            Console.WriteLine("Veuillez saisir le temps total de la partie en minutes");
            int delta_temps_game = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Veuillez saisir le temps intermédiaire pour chaque joueur en seconde");
            int delta_temps_player = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Récapitulatif des temps : ");
            Console.WriteLine("temps partie : " + delta_temps_game + " min // temps joueur : " + delta_temps_player + " sec");
            Console.WriteLine("Appuyer sur entrée pour aller à l'étape suivante");
            Console.ReadKey();
            DateTime date_a1_game = DateTime.UtcNow;
            DateTime date_a2_game = DateTime.UtcNow;
            DateTime ecart_a_game = date_a2_game.AddMinutes(delta_temps_game);
            TimeSpan tempsj1_game = ecart_a_game - date_a1_game;
            DateTime date_a1_player = DateTime.UtcNow;
            DateTime date_a2_player = DateTime.UtcNow;
            DateTime ecart_a_player = date_a2_player.AddSeconds(delta_temps_player);
            TimeSpan tempsj1_player = ecart_a_player - date_a1_player;
            bool etat = true;



            while (tempsj1_game.Minutes > 0 || tempsj1_game.Seconds > 0 || etat != false)
            {
                while (tempsj1_player.Seconds > 0)
                {
                    //ici on incrémente les secondes de la 1ere date d'autant de secondes qu'il s'est écoulé pendant le passage de la boucle
                    //Ensuite on fait la différence des 2 dates qui se décrémente donc en temps réel
                    DateTime increment_a_game = date_a1_game.AddSeconds((DateTime.UtcNow - date_a1_game).TotalSeconds);
                    tempsj1_game = ecart_a_game - increment_a_game;
                    Console.WriteLine(tempsj1_game);
                    DateTime increment_a_player = date_a1_player.AddSeconds((DateTime.UtcNow - date_a1_player).TotalSeconds);
                    tempsj1_player = ecart_a_player - increment_a_player;
                    Console.WriteLine(tempsj1_player);
                    TimeSpan tempsj1_game_new = tempsj1_game;

                    // ici on fait les coups, il faut :
                    // alternance joueur 1, joueur 2 (si possible pas duppliqué)
                    // def dans plateau : coups valides + drop des lettres

                    etat = EtatJeu(tab);
                    Console.Clear();
                }
            }
        }

        
        
        // Check si le tableau contient toujours des lettres ou pas
        static bool EtatJeu(char[,] tab)
        {
            bool test = false;
            if (tab.GetLength(0) != 0 && tab.GetLength(1) != 0)
            {
                test = true;
            }
            return test;
        }


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
                    break;
                case 3:
                    Console.WriteLine("Combien de ligne voulez vous ?");
                    int ligne = Methode.ChiffreValide();
                    Console.WriteLine("Combien de colonne voulez vous ?");
                    int colonne = Methode.ChiffreValide();

                    map = new Plateau(ligne, colonne);
                    Console.WriteLine(map);
                    break;
                default:
                    DeuxiemePage();
                    break;
            }

            InitialisationDesJoueurs();

        }


        private static void InitialisationDesJoueurs()
        {
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
            
        }



        #endregion
    }
}
