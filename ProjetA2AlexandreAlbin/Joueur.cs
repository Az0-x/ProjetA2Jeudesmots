using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetA2AlexandreAlbin
{
    public class Joueur
    {
        string nom;
        List<string> motsTrouvees;
        int score;
        public Joueur(string Nom)
        {
            this.nom = Nom;
            this.motsTrouvees = new List<string>();
            this.score = 0;

        }
        public string Nom
        {
            get { return nom; }
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
            string contener = "";
            contener += "\n\nLe joueur " + Nom + " a actuellement trouvé les mots : ";
            if (MotsTrouvees != null)
            {
                foreach (string mot in MotsTrouvees)
                {
                    contener += mot + ' ';
                }
            }
            contener +="\nEt " + Nom + " a actuellement " + score + " points!!";
            return contener;
        }


        public void UpdateScore(string word)
        {
            word = word.ToUpper();
            int count = 0;
            foreach(char lettre in word)
            {
                for (int i = 0; i < LetterInformations.Lettres.Length; i++)
                {
                    if (LetterInformations.Lettres[i].Letter == lettre)
                    {
                        count += LetterInformations.Lettres[i].Poids;
                        
                    }
                }
            }
            score += count;
            
        }
    }
}