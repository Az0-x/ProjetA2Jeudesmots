namespace ProjetA2AlexandreAlbin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to continue!");
            Console.ReadKey();
        }

        static void StartGame()
        {
            //Init du tab
            Plateau map = new Plateau(5,5);
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
            char[,] mat = new char[10,10];
            Methode.AfficherMatrice(mat);
        }

        static void Interface()
        {

        }
    }
}
