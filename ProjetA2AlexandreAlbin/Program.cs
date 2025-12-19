using Projet_A2_S1;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;

namespace ProjetA2AlexandreAlbin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LetterInformations.creationTab();
            Dictionnaire dico = new Dictionnaire();
            Interface.PageAcceuil();
            Console.WriteLine("Press any key to continue!");
            Console.ReadKey();
        }
    }
}

