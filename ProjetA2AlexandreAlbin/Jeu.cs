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

        /*
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
        */
        
        
        

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
            Partie += "\n\n" + j1.toString() + "\n\n" + j2.toString;
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
    }
}
