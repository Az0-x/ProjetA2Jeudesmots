using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetA2AlexandreAlbin
{
    internal class Joueur
    {
        string nom;
        List<string> motsTrouvees;
        int score;
        public Joueur(string Nom)
        {
            if (Nom == null)// Refaire la boucle
            {
                Console.WriteLine("veuillez saisir votre nom à nouveau !!!");
            }
            this.nom = Nom;
            this.motsTrouvees = null;
            this.score = 0;

        }
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        public List<string> MotsTrouvees
        {
            get { return motsTrouvees; }
            set { motsTrouvees = value; }
        }
        public int Compteur(int val)
        {
            int count = 0;
            if (val > 0)
            {
                count++;
            }
            return count;
        }
        public bool Contient(string mot, string[] tab)// faire une liste oxiliaire ...
        {
            bool present = false;
            // Regarder si le mot trouve est contenu dans la liste 
            return present;
        }

        public string toString() // Faire l'affichage de la fct !!!
        {
            return nom;
        }
    }
}