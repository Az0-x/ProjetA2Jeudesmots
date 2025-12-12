using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetA2AlexandreAlbin
{
    internal class Methode
    {



        public static void AfficherMatrice(char[,] matrice)  //on affiche la matrice
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
    }
}
