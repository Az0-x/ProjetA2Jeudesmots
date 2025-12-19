using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjetA2AlexandreAlbin
{
    internal class Methode
    {


        /// <summary>
        /// Affichage de la matrice qu'importe le type en entrant tant que c'est une matrice avec l'utilisation de <>
        /// </summary>
        /// <typeparam name="Tableauquelquonque"></typeparam>
        /// <param name="matrice"></param>
        public static void AfficherMatrice<Tableauquelquonque>(Tableauquelquonque[,] matrice)  //on affiche la matrice
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
                    }
                }
            }
        }



        /// <summary>
        /// Demande un mots et vérifie si il est valide et complets, et si il est différent de ESC
        /// </summary>
        /// <returns></returns>
        public static string MotValide()
        {
            string mot = "";
            while (true)
            {
                // On lit la touche sans l'afficher immédiatement (.KeyChar fera l'affichage manuel)
                ConsoleKeyInfo info = Console.ReadKey(true);

                // CAS 1 : Menu de Pause
                if (info.Key == ConsoleKey.Escape)
                {
                    Interface.AfficherMenuPause();
                    Console.Write("\n> Reprise : " + mot);
                    continue;
                }

                // CAS 2 : Validation
                if (info.Key == ConsoleKey.Enter)
                {
                    if (!string.IsNullOrWhiteSpace(mot))
                    {
                        Console.WriteLine();
                        return mot;
                    }
                }

                // CAS 3 : Effacer (Backspace)
                if (info.Key == ConsoleKey.Backspace && mot.Length > 0)
                {
                    mot = mot.Substring(0, mot.Length - 1);
                    Console.Write("\b \b"); // Efface visuellement le caractère
                }
                // CAS 4 : Caractère classique
                else if (!char.IsControl(info.KeyChar))
                {
                    mot += info.KeyChar;
                    Console.Write(info.KeyChar);
                }
            }
        }


        /// <summary>
        /// Demande un mot ( sous forme de key ) et on regarde si c'est un nombre
        /// </summary>
        /// <returns></returns>
        public static int ChiffreValide()
        {
            string saisie = "";
            while (true)
            {
                ConsoleKeyInfo info = Console.ReadKey(true);

                if (info.Key == ConsoleKey.Escape)
                {
                    Interface.AfficherMenuPause();
                    Console.Clear();
                    Console.WriteLine("Reprise de la saisie...");
                    Console.Write("Entrez un chiffre : " + saisie);
                    continue;
                }

                if (info.Key == ConsoleKey.Enter)
                {
                    if (int.TryParse(saisie, out int nombre))
                    {
                        Console.WriteLine();
                        return nombre;
                    }
                }

                if (info.Key == ConsoleKey.Backspace && saisie.Length > 0)
                {
                    saisie = saisie.Substring(0, saisie.Length - 1);
                    Console.Write("\b \b");
                }
                // On n'accepte que les chiffres
                else if (char.IsDigit(info.KeyChar))
                {
                    saisie += info.KeyChar;
                    Console.Write(info.KeyChar);
                }
            }
        }

    }
}
