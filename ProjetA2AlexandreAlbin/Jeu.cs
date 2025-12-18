using Projet_A2_S1;
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
        private Dictionnaire dico;
        private Joueur j1;
        private Joueur j2;
        private Plateau map;
        private List<string> etatPartie;
        private DateTime debutPartie;
        private DateTime finTour;



        public Plateau Map
        {
            get { return map; }
            set { map = value; }
        }

        public Dictionnaire Dico
        {
            get { return dico; }
            set { dico = value; }
        }

        public List<string> EtatPartie
        {
            get { return etatPartie; }
            set { etatPartie = value; }
        }

        public Joueur J1
        {
            get { return j1; }
            set { j1 = value; }
        }

        public Joueur J2
        {
            get { return j2; }
            set { j2 = value; }
        }


        public Jeu(Dictionnaire dico, Joueur joueur1, Joueur joueur2, Plateau map) //Rajouter l'indice pour changer la valeur de la fin de la partie
        {
            this.dico = dico;
            this.j1 = joueur1;
            this.j2 = joueur2;
            this.map = map;
            this.etatPartie = new List<string>();
            this.debutPartie = DateTime.Now;
            this.finTour = debutPartie.AddMinutes(2);

            etatPartie.Add(EtatActuellePartie(true));
        }

        public bool EtatJeu()  //return true si la partie n'est pas terminer
        {
            bool verif = true;
            if(DateTime.Now > finTour)
            {
                verif = false;
            }
            return verif;
        }





        public string EtatActuellePartie(bool player)
        {
            if (player)
            {
                return map + "\n\n" + j1.Nom + " doit jouer";
            }
            else return map + "\n\n" + j2.Nom + " doit jouer";

        }

        public void EtatGrille(bool player)
        {
            

            int nbr = 1;
            string path = Path.Combine("externalFiles", "PlateauTest", "Save", "Plateau" + nbr + ".txt");
            while (File.Exists(path))
            {
                nbr++;
                path = Path.Combine("externalFiles", "PlateauTest", "Save", "Plateau" + nbr + ".txt");
            }
            string Grille = "";
            int ligne = map.Matrice.GetLength(0);
            int colonne = map.Matrice.GetLength(1);
            for (int i = 0; i < ligne; i++)
            {
                for (int j = 0; j < colonne; j++)
                {
                    Grille += map.Matrice[i, j] + ',';
                }
                Grille += "\n";
            }
            File.WriteAllText(path, Grille);
        }


        public void SaveEtatPartie()
        {
            int nbr = 1;
            string path = Path.Combine("externalFiles", "SavePartie", "SavePartie" + nbr + ".txt");
            while (File.Exists(path))
            {
                nbr++;
                path = Path.Combine("externalFiles", "SavePartie", "SavePartie" + nbr + ".txt");
            }
            string Partie="";
            foreach(string round in etatPartie)
            {
                Partie += round;
            }
            Partie += j1.toString();
            Partie += j2.toString();
            File.WriteAllText(path, Partie);
        }
        public void Round()
        {
            Console.Clear();
            string word;
            DateTime debutRound = DateTime.Now;
            DateTime finRound = debutRound.AddSeconds(10);
            bool verif = false;

            Methode.AfficherMatrice(Map.Matrice);
            Console.WriteLine("\n\n");

            while (DateTime.Now < finRound && !verif)
            {
                
                Console.WriteLine("Joueur 1 quelle mot choisis tu ?\n\n\nScore Actuelle : Joueur1 "+J1.Score +"  Joueur2 "+J2.Score);
                word = Methode.MotValide();

                if (Dico.FindWord(word))
                {
                    verif = Map.Recherche_Mot(word).verif;
                    if (verif)
                    {
                        Map.Maj_Plateau(Map, word, Dico);
                        J1.Add_Mot(word);
                        J1.UpdateScore(word);
                        Console.WriteLine(Map);
                    }
                    else
                    {
                        Console.WriteLine("Le mot n'est pas dans la grille");
                    }

                }
                else Console.WriteLine("Le mot n'est pas dans le dictionnaire");
            }
            etatPartie.Add(EtatActuellePartie(true));


            RoundJ2();
        }

        private void RoundJ2()
        {
            Console.Clear();
            string word;
            DateTime debutRound = DateTime.Now;
            DateTime finRound = debutRound.AddSeconds(10);
            bool verif = false;

            Methode.AfficherMatrice(Map.Matrice);
            Console.WriteLine("\n\n");

            while (DateTime.Now < finRound && !verif)
            {

                Console.WriteLine("Joueur 2 quelle mot choisis tu ?\n\n\nScore Actuelle : Joueur1 "+J1.Score +"  Joueur2 "+J2.Score);
                word = Methode.MotValide();

                if (Dico.FindWord(word))
                {
                    verif = Map.Recherche_Mot(word).verif;
                    if (verif)
                    {
                        Map.Maj_Plateau(Map, word, Dico);
                        J2.Add_Mot(word);
                        J2.UpdateScore(word);
                        Console.WriteLine(Map);
                    }
                    else
                    {
                        Console.WriteLine("Le mot n'est pas dans la grille");
                    }

                }
                else Console.WriteLine("Le mt n'est pas dans le dictionnaire");

            }
            etatPartie.Add(EtatActuellePartie(false));
        }

        public static void AfficherMenuPause()
        {
            // On sauvegarde l'ancienne vue ou on prévient l'utilisateur
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("=== PAUSE ===");
            Console.ResetColor();
            Console.WriteLine("1. Reprendre la partie");
            Console.WriteLine("2. Quitter le jeu");

            string choix = Console.ReadLine();

            if (choix == "2")
            {
                Environment.Exit(0); // Quitte proprement le programme
            }

            // Si l'utilisateur choisit 1 ou autre, la fonction se termine.
            // Le programme "retombe" dans la boucle de saisie de MotValide().
            Console.Clear();
            Console.WriteLine("Reprise de la saisie...");
        }
    }
}
