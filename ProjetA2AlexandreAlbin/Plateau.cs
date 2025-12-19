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
    public class Plateau
    {
       
        //Attributs
        char[,] matrice;
        int lignes;
        int colones;


        //Propriétés
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


        //Constructeurs
        public Plateau(int ligne, int colone)
        {
            this.lignes = ligne;
            this.colones = colone;
            this.matrice = tableau(ligne, colone);
        }

        public Plateau(string path)
        {
            this.lignes = ChargerPlateau(path).ligne;
            this.colones = ChargerPlateau(path).colonne;
            this.matrice = ChargerPlateau(path).tab;
            
        }



        /// <summary>
        /// Création d'un tableau aléatoire en fonction du nombre de ligne, colonne, et l'occurence des lettres
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="colone"></param>
        /// <returns></returns>
        private char[,] tableau(int ligne, int colone)
        {
            char[,] mat = null;
            if (ligne != 0 && colone != 0)
            {
                Random r = new Random();
                mat = new char[ligne, colone];

                for (int i = 0; i < ligne; i++)
                {
                    for (int j = 0; j < colone; j++)
                    {
                        bool verif = false;
                        while (!verif)
                        {
                            
                            int a = r.Next(0, 26);

                            
                            if (LetterInformations.Lettres[a].Count < LetterInformations.Lettres[a].Occurence)
                            {
                                
                                mat[i, j] = LetterInformations.Lettres[a].Letter;

                                LetterInformations.Lettres[a].Count++;
                                verif = true;
                            }
                        }
                    }
                }
            }
            return mat;
        }

        /// <summary>
        /// Rehcherche si le mots entrée en paramétre est dans le tableau (il regarde dans un premeir temps le premier caractere, puis ensuite appellle une autre fonctions qui ll'as regarde apres toutes les lettre adjoints
        /// </summary>
        /// <param name="mot"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Regarde si l'indice entrée en parametre, est un indice correspondant au mot
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        /// <param name="val"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Charge un plateau en fonction du chemin d'accés
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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
        /// Mettre a jour le tableau (enleves les lettres, et les faits tombers
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




        /// <summary>
        /// Utilisation lors de précédents test, pour retourner un string correspondant a la matrice
        /// </summary>
        /// <returns></returns>
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


    

