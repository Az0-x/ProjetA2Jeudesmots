using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetA2AlexandreAlbin
{
    internal class Plateau
    {
       
        
        char[,] matrice;
        int lignes;
        int colones;

        public char[,] Matrice
        {
            get { return matrice; }
        }
        public int Lignes
        {
            get { return lignes; }
        }
        public int Colones
        {
            get { return colones; }
        }

        public Plateau(int ligne, int colone)
        {
            this.lignes = ligne;
            this.colones = colone;
            this.matrice = tableau(ligne, colone);
            //On créer le tableau pour le mettre directement dans le constructeur

            
        }

        private char[,] tableau(int ligne, int colone)
        {
            char[,] mat = null;
            if (ligne != 0 && colone != 0)
            {
                Random r = new Random(32);
                int dim = ligne * colone;
                mat = new char[ligne, colone];
                bool verif;
                for (int i = 0; i < ligne; i++)
                {
                    for (int j = 0; j < colone; j++)
                    {
                        verif = false;
                        while (!verif)
                        {
                            int a = r.Next(1, 26);
                            
                            if (LetterInformations.Lettres[a].Count <= LetterInformations.Lettres[a].Occurence)
                            {
                                mat[i, j] = Convert.ToChar(a + 64);
                                LetterInformations.Lettres[a].Count++;
                                verif = true; ;
                            }
                            
                        }
                    }
                }
                
            }
            return mat;

        }


        // Je dois saisir les lignes et les colones !!!
        
        //public string toString()
        //{
        //    // Retourne le plateau en formant entier 
        //}
    }
}


    

