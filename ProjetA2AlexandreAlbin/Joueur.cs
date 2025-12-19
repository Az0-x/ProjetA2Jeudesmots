using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetA2AlexandreAlbin
{
    public class Joueur
    {
        #region Création de la classe hors métodes
        //Attributs
        string nom;
        List<string> motsTrouvees;
        int score;

        //Constructeur
        public Joueur(string Nom)
        {
            this.nom = Nom;
            this.motsTrouvees = new List<string>();
            this.score = 0;

        }

        //Propriétés
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

        #endregion


        
        /// <summary>
        /// Ajouts du mots dans la listes des mots jouée par un joueurs
        /// </summary>
        /// <param name="mot"></param>
        public void Add_Mot(string mot)
        {
            if (mot != null || Contient(mot) == false)// normalement on test dans un premier telmps si le repertoires est ok
            {
                Console.WriteLine("Le mot :" + mot + " est bien ajouté à la liste");
                motsTrouvees.Add(mot);// On ajoute le mot à la liste des mots trouvés 
            }
        }


        /// <summary>
        /// On regarde si le mots trouvés et est contenus dans la liste des mots trouvé par le joueur
        /// </summary>
        /// <param name="mot"></param>
        /// <returns></returns>
        public bool Contient(string mot)// on regarde si le mot est bien dans la liste des mots trouvés 
        {
            bool present = false;

            if (motsTrouvees.Contains(mot) == true)
            {
                present = true;
            }
            return present;
        }

        /// <summary>
        /// Affiche les informations du joueur
        /// </summary>
        /// <returns></returns>
        public string toString() 
        {
            string contener = "";
            contener += "\n\nLe joueur " + Nom + " a actuellement trouvé les mots : \n";
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

        /// <summary>
        /// Met le score associé au mots, en fonction du poid du mots ( voir Lettre.txt pour le poids des lettres  
        /// </summary>
        /// <param name="word"></param>
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