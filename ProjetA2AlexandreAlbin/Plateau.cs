using Projet_A2_S1;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        public Plateau(string path)
        {
            this.lignes = ChargerPlateau(path).ligne;
            this.colones = ChargerPlateau(path).colonne;
            this.matrice = ChargerPlateau(path).tab;
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


        public (bool verif, int[,] Valeurs) Recherche_Mot(string mot)
        {
            if (string.IsNullOrWhiteSpace(mot) || matrice == null)
            {
                return (false, null);
            }

            mot = mot.ToUpper();
            int ligne = matrice.GetLength(0);
            int colonne = matrice.GetLength(1);
            if (ligne == 0 || colonne == 0)
            {
                return (false, null);
            }

            int lastligne = ligne - 1;

            bool found = false;
            int[,] finalVals = null;

            for (int i = 0; i < colonne && !found; i++)
            {
                if (matrice[lastligne, i] == mot[0])
                {
                    int[,] Valeurs = new int[mot.Length, 2];
                    Valeurs[0, 0] = lastligne;
                    Valeurs[0, 1] = i;

                    if (mot.Length == 1)
                    {
                        found = true;
                        finalVals = Valeurs;
                    }
                    else
                    {
                        var result = Veriflettretab(mot, 1, lastligne, i, Valeurs);
                        if (result.verif)
                        {
                            found = true;
                            finalVals = result.Valeurs;
                        }
                    }
                }
            }

            return (found, finalVals);
        }



        /// <summary>
        /// Fonction récursive pour trouver le mot dans la grille.
        /// </summary>
        
        public (bool verif, int[,] Valeurs) Veriflettretab(string mot, int indexmot, int indextab1, int indextab2, int[,] Valeurs)
        {
            
            // Si on a réussi à trouver toutes les lettres jusqu'à la fin du mot
            if (indexmot == mot.Length)
            {
                return (true, Valeurs);
            }

            // Récupération des dimensions de la grille (supposée être une variable globale 'matrice')
            int ligne = matrice.GetLength(0);
            int colonne = matrice.GetLength(1);

            //  délimitation des limites
            int imin = Math.Max(0, indextab1 - 1);
            int imax = Math.Min(ligne - 1, indextab1 + 1);
            int jmin = Math.Max(0, indextab2 - 1);
            int jmax = Math.Min(colonne - 1, indextab2 + 1);

            //  BOUCLE SUR LES VOISINS 
            for (int i = imin; i <= imax; i++)
            {
                for (int j = jmin; j <= jmax; j++)
                {
                    // On ne reste pas sur la case d'où l'on vient (surplace interdit)
                    if (i == indextab1 && j == indextab2) { 
                    }



                    else if(matrice[i, j] == mot[indexmot])
                    {
                        //Est ce que la lettre a deja été utilisé
                        if (!indicedanstab(i, j, Valeurs))
                        {
                            
                            // On enregistre la position actuelle dans le tableau de résultats
                            Valeurs[indexmot, 0] = i;
                            Valeurs[indexmot, 1] = j;

                            // On cherche la lettre suivante à partir d'ici
                            var res = Veriflettretab(mot, indexmot + 1, i, j, Valeurs);

                            // Verif du résultat
                            if (res.verif == true)
                            {
                                // On envoie les valeurs en haut de la recursivité
                                return (true, Valeurs);
                            }

                            
                        }
                    }
                }
            }

            // Si on a testé tous les voisins et qu'aucun ne marche, c'est que le mot n'est pas par là.
            return (false, Valeurs);
        }

        public bool indicedanstab(int i1, int i2, int[,] val)
        {
            if (val == null)
            {
                return false;
            }

            if (val.GetLength(1) < 2)
            {
                return false;
            }

            int ligne = val.GetLength(0);
            for (int r = 0; r < ligne; r++)
            {
                if (val[r, 0] == i1 && val[r, 1] == i2)
                {
                    return true;
                }
            }

            return false;
        }



        public static (int ligne, int colonne, char[,] tab) ChargerPlateau(string path)
        {
            (int ligne, int colonne, char[,] tab) final = (0, 0, null); 
            if (File.Exists(path))
            {
                int ligne = 0;
                int colonne = 0;
                foreach (string subline in File.ReadLines(path))
                {
                    string[] part = subline.Split(',');
                    for (int i = 0; i < part.Length; i++) {
                        colonne++;
                    }

                    ligne++;
                }

                char[,] tabl = new char[ligne, colonne];
                int indice = 0;
                foreach (string subline in File.ReadLines(path))
                {
                    string[] part = subline.Split(',');
                    for (int i = 0; i < part.Length; i++)
                    {
                        tabl[indice,i] = Convert.ToChar(part[i]);
                    }
                    indice++;
                }
                final = (ligne, colonne, tabl);
            }
            else Console.WriteLine("No path");
            return final;
        }






        /// <summary>
        /// Mettre a jour le tableau
        /// </summary>
        public void Maj_Plateau(Plateau map, string mot, Dictionnaire dico)
        {
            if(mot != null && mot.Length != 0)
            {
                if (dico.FindWord(mot))
                {
                    var val = map.Recherche_Mot(mot);
                    if (val.verif)
                    {
                        int[,] coords = val.Valeurs;

                        for (int k = 0; k < coords.GetLength(0); k++)
                        {
                            int ligne = coords[k, 0];
                            int colonne = coords[k, 1];
                            Matrice[ligne, colonne] = ' ';
                        }
                        
                        Methode.AfficherMatrice(Matrice);
                        Console.WriteLine("\n\n");


                        for(int i = 0; i < matrice.GetLength(0); i++)
                        {
                            for(int j = 0; j < matrice.GetLength(1); j++)
                            {
                                if (matrice[i,j] == ' ')
                                {
                                    for (int r = i; r > 0; r--)
                                    {
                                        matrice[r, j] = matrice[r - 1, j];
                                        matrice[r-1, j] = ' ';

                                        /*
                                        Console.Clear();
                                        Methode.AfficherMatrice(Matrice);
                                        System.Threading.Thread.Sleep(100); //Pour l'aspect graphique
                                        */
                                    }
                                }
                            }
                        }



                    }
                }
                else Console.WriteLine("Mot introuvable");
            }
        }


        public override string ToString()  //on affiche la matrice
        {
            string mat ="";
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
                            mat += matrice[index, index2] + " ";
                        }
                        mat += "\n";
                    }
                }
            }
            return mat;
        }
    }
}


    

