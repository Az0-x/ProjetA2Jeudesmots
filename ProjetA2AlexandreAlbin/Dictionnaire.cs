using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetA2AlexandreAlbin
{
    internal class Dictionnaire
    {
        // Aller dans mots Francais.text, extraire et faire un tableau de List, tous trié pour la compraraison
        private static string path = Path.Combine("externalFiles", "Mots_Français.txt");
        // Path combine afin d'essayer régler les problèmes de \ et /

        private string dico;
        private StreamReader st = new StreamReader(path);
        string[] dicos = File.ReadAllLines(path);//le chemin
                                                 //du code est hardcodé


        // Pour le dicos voir s'il faur garder le stream reader !!!


        //La méthode de recherche de Dichotomie mais pour ca il va falloir un tableau déjà trié :

        public bool RechDichoRecursif(string mot, int index)// Rechercher si le mot est dans le dictionnaire !!!
        {
            if (index < 1)
            {
                return false;
            }

            if (dicos[index].Contains(" " + mot + " "))// espace pour eviter que des mots plus long passent le test
            {
                return true;
            }

            return RechDichoRecursif(mot, index - 1);
        }

        // Pas trop compris ce qu'on doit afficher doucoup : public string toString()



        // Peitit tri fusion pour la suite :
        // surement méthode non réccursive

        //public void Tri_XXX(string path, int gauche, int droite)// La méthode pour trier le chemin / directement le tableaux de mots
        //{
        //    for (int i = 0; i < 26; i++)
        //    {
        //        if (gauche >= droite)
        //        {
        //            return new string[] { path[gauche] };
        //        }
        //        int milieu = (gauche + droite) / 2;
        //        string[] partieGauche = Tri_XXX(path, gauche, milieu);
        //        string[] partieDroite = Tri_XXX(path, milieu + 1, droite);


        //        return Tri_XXX(partieGauche, partieDroite);
        //    }
        //}


        static void Fusion(int[] tab, int deb, int fin, int mil)
        {// ca à l'air ok cette histoire
            if (deb > fin)
            {
                int indicegauche = mil - deb + 1;
                int inddroite = fin - mil;
                int[] tabgauche = new int[indicegauche];
                int[] tabdroite = new int[indicegauche];
                for (int i = 0; i < indicegauche; i++)
                {
                    tabgauche[i] = tab[i + deb];
                }
                for (int i = 0; i < inddroite; i++)
                {
                    tabdroite[i] = tab[i + mil + 1];
                }
                int ind1 = 0, ind2 = 0;
                for (int i = deb; i < fin; i++)
                {
                    if (ind2 < inddroite && ind1 < indicegauche)
                    {
                        if (tabgauche[ind1] < tabdroite[ind2])
                        {
                            tab[i] = tabgauche[ind1]; i++;
                        }
                        else
                        {
                            tab[i] = tabdroite[ind1]; i++;
                        }
                    }
                    else
                    {
                        if (ind2 > inddroite)
                        {
                            tab[i] = tabgauche[ind1]; ind1++;
                        }
                        else
                        {
                            if (ind1 > indicegauche)
                            {
                                tab[i] = tabdroite[ind2]; ind2++;
                            }
                        }
                    }
                }
            }
        }
    }
}
