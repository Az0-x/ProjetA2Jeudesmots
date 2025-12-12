namespace ProjetA2AlexandreAlbin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LetterInformations.creationTab();
            Test();


            Console.WriteLine("Press any key to continue!");
            Console.ReadKey();
        }

        static void StartGame()
        {
            //Init du tab
            
            //Modulle de chargement interface
            //Chargement des fichiers 
            //Trie du tableau
            //Lancement de la boucle jeu
            
            //appelle la fonctions
            //Demandé toutes les configurations
        }

        static void Game()
        {
            //Setup le start de la clock
            //Lancement du jeux et des éléments
        }

        static void Interface()
        {

        }

        static void Test()
        {

            Plateau map = new Plateau(8, 10);
            Methode.AfficherMatrice(map.Matrice);

            /*
            foreach (LetterInformations a in LetterInformations.Lettres)
            {
                Console.WriteLine(a);
            }
            */
        }
    }
}
//possible implementation d'un historique 
