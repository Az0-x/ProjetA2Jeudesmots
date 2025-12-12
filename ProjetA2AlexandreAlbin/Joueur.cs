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
            if (Nom == null)// Refaire la boucle pour que la saisie soit plus sécurisée !!!
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

        // Faut incrémenter le score du joueur et aussi voir si le mot est contenu dans les guess du joueur !!!
        public void Add_Mot(string mot)
        {
            if (mot != null || Contient(mot) == false)// normalement on test dans un premier telmps si le repertoires est ok
            {
                Console.WriteLine("Le mot :" + mot + " est bien ajouté à la liste");
                motsTrouvees.Add(mot);// On ajoute le mot à la liste des mots trouvés 
            }
        }


        public bool Contient(string mot)// on regarde si le mot est bien dans la liste des mots trouvés 
        {
            bool present = false;

            if (motsTrouvees.Contains(mot) == true)
            {
                present = true;
            }
            return present;
        }

        public string toString() // Faire l'affichage de la fct !!!
        {
            return nom;
        }
    }
}