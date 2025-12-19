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
        #region Attributs de la classe
        //Atributs
        private Dictionnaire dico;
        private Joueur j1;
        private Joueur j2;
        private Plateau map;
        private List<string> etatPartie;
        private DateTime debutPartie;
        private DateTime finTour;


        //Propriété
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

        //Constructeurs
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



        #endregion


        /// <summary>
        /// Définits si la partie est terminer en fonction du temps
        /// </summary>
        /// <returns></returns>
        public bool EtatJeu()  //return true si la partie n'est pas terminer
        {
            bool verif = true;
            if(DateTime.Now > finTour)
            {
                verif = false;
            }
            return verif;
        }




        /// <summary>
        /// Utiliser pour l'historique de la partie, return en string la personne que doit jouer
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public string EtatActuellePartie(bool player)
        {
            if (player)
            {
                return map + "\n\n" + j1.Nom + " doit jouer";
            }
            else return map + "\n\n" + j2.Nom + " doit jouer";

        }


        /// <summary>
        /// Utilisation pour sauvegarder la grille présente de la partie dans externalFiles/PlateauTest/Save
        /// </summary>
        public void ToFile()
        {
            int nbr = 1;
            string dir = Path.Combine("externalFiles", "PlateauTest", "Save");
            Directory.CreateDirectory(dir);

            string path = Path.Combine(dir, "Plateau" + nbr + ".txt");
            while (File.Exists(path))
            {
                nbr++;
                path = Path.Combine(dir, "Plateau" + nbr + ".txt");
            }

            var sb = new StringBuilder();
            int ligne = map.Matrice.GetLength(0);
            int colonne = map.Matrice.GetLength(1);
            for (int i = 0; i < ligne; i++)
            {
                for (int j = 0; j < colonne; j++)
                {
                    sb.Append(map.Matrice[i, j]);
                    if (j < colonne - 1)
                    {
                        sb.Append(',');
                    }
                }
                if (i < ligne - 1)
                {
                    sb.AppendLine();
                }
            }

            File.WriteAllText(path, sb.ToString());
        }


        /// <summary>
        /// Save la partie entiere en forme d'historique 
        /// </summary>
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


        /// <summary>
        /// Définis les actions du joueurs 1 a faire dans la partie, il dois ecrire le mot, puis update dans le plateau de ce mot
        /// </summary>
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


        /// <summary>
        /// Définis les actions du joueurs 2 a faire dans la partie, il dois ecrire le mot, puis update dans le plateau de ce mot, Actions juste Aprés celle du joueurs 1
        /// </summary>
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
