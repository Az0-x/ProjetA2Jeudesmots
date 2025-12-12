using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetA2AlexandreAlbin
{
    internal class Plateau
    {
       
        
            char[,] plateau;
            int lignes;
            int colones;
            //Dictionary<char, Lettre> lettresInfo; Revoir ca avec le txt

            public char[,] Plateau
            {
                get { return plateau; }
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
            }
            // Je dois saisir les lignes et les colones !!!
            public void AfficherMatrice(char[,] matrice)//on affiche la matrice
            {
                if (matrice == null) { Console.Write("(null)"); }
                else
                {
                    if (matrice.GetLength(0) == 0 || matrice.GetLength(1) == 0) { Console.Write("(vide)"); }
                    else
                    {
                        for (int index = 0; index < matrice.GetLength(0); index++)
                        {
                            for (int index2 = 0; index2 < matrice.GetLength(1); index2++)
                            {
                                Console.Write(matrice[index, index2] + " ");
                            }
                            Console.WriteLine();
                            Console.WriteLine();
                        }
                    }
                }
            }
            //public string toString()
            //{
            //    // Retourne le plateau en formant entier 
            //}
            public void ToFile(string nomfile)
            {
                int count = 0;
            private StreamReader st = new StreamReader(@$"C:\Users\albin\Documents\Esilv A1\C#\{nomfile}.txt"); //Lettre 

        }
        public void SeparerEnListes(string cheminFichier, int longeur, int largeur)//POur avoir plusieurs colones séparés
        {
            List<string> lettres = new List<string>();
            List<int> occurence = new List<int>();
            List<int> poids = new List<int>();

            using (StreamReader sr = new StreamReader(cheminFichier))
            {
                string ligne;
                while ((ligne = sr.ReadLine()) != null)
                {
                    string[] elements = ligne.Split(',');

                    if (elements.Length >= 3)
                    {
                        lettres.Add(elements[0]);
                        occurence.Add(int.Parse(elements[1]));
                        poids.Add(int.Parse(elements[2]));
                    }
                }
            }

            Random r = new Random();
            int count = 0;
            int dim = longeur * largeur;
            string[,] mat = new string[longeur, largeur];
            while (count != dim)
            {
                for (int i = 0; i <= dim; i++)
                {
                    for (int j = 0; j <= largeur; j++)
                    {
                        int pick = r.Next(1, 26);
                        if (occurence[pick] > 0)
                        {
                            mat[i, j] = lettres[pick];
                        }
                    }
                }
            }
        }


    }
}


    

