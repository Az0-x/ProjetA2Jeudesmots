using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjetA2AlexandreAlbin
{
    public struct LetterInformations
    {
        #region def de la classe

        //Creation des éléments de la structures
        public char Letter { get; }
        public int Occurence { get; }
        public int Poids { get; }

        public int Count { get; set; }


        //definition d'un constructeur
        public LetterInformations(char letter, int occurence, int poids)
        {
            Letter = letter;
            Occurence = occurence;
            Poids = poids;
            Count = 0;
        }

        #endregion


        //On peut faire soit un tab ( il y a 26 lettres donc fixe ou une Liste, le choix dépend de la tournure du jeux si on veut ajouter des caractéres ou autres )
        
        
        private static string path = Path.Combine("externalFiles", "Lettre.txt"); // Path combine sert a créer le chemin d'acces parfais malgres Linux/ Windows ( Comme on peut avoir plusieur systeme d'exploitation )
        

        /// <summary>
        /// Utiliser pour la fonction test, et aussi lors de la création du jeu
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Letter + "," + Occurence + "," + Poids;
        }



        #region Création de la liste Lettre  ( optionnelle )
        /*
        public static List<LetterInformations> Lettres = new List<LetterInformations>() ;
        
        public static void creationTab()
        {
            if (File.Exists(path))
            {
                foreach(string subline in File.ReadLines(path))
                {
                    string[] part = subline.Split(',');
                    if (part.Length <= 3)
                    {
                        char letters = Convert.ToChar(part[0]);
                        int occurs = Convert.ToInt32(part[1]);
                        int wheigt = Convert.ToInt32(part[2]);
                        Lettres.Add(new LetterInformations(letters, occurs, wheigt));
                    }
                    else Console.WriteLine("Erreur dans le fichier Lettre.txt");
                } 
            }
            else Console.WriteLine("No path");
        }
        
        */
        #endregion



        #region Création de la tab Lettres

        /// <summary>
        /// Création d'un tablleau de valeurs Struct incluant la lettre, l'occurence, et le poid
        /// </summary>
        public static LetterInformations[] Lettres = new LetterInformations[26];
        public static void creationTab()
        {
            if (File.Exists(path))
            {
                int i = 0;
                foreach(string subline in File.ReadLines(path))
                {
                    string[] part = subline.Split(',');
                    if (part.Length <= 3)
                    {
                        char letters = Convert.ToChar(part[0]);
                        int occurs = Convert.ToInt32(part[1]);
                        int wheigt = Convert.ToInt32(part[2]);
                        Lettres[i] = new LetterInformations(letters, occurs, wheigt);
                        i++;
                    }
                    else Console.WriteLine("Erreur dans le fichier Lettre.txt");
                } 
            }
            else Console.WriteLine("No path");
        }
        #endregion
        
    }
}
