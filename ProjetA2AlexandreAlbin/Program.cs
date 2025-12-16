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





            //Test();
            //Test1(dico);
            //Test2(dico, 3);
            //Test3(dico, 7);
            Test4();//Test Interface 
            //Pas mal 
            //Interface();


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

        










        #region test
        static void Test()
        {

            //Plateau.Recherche_Mot("");

            //Console.WriteLine(dico.Dict);
            //dico.AfficheDico();

            Plateau map = new Plateau(8, 10);
            Methode.AfficherMatrice(map.Matrice);
            



            /*
            foreach (LetterInformations a in LetterInformations.Lettres)
            {
                Console.WriteLine(a);
            }
            */
        }

        static void Test1(Dictionnaire dico) //Test pour le mot et le chargement d'un fichier donnés
        {
            
            string path;
            string word = "chat";
            for (int i = 1; i < 7; i++)
            {
                Console.WriteLine("--------------- Test du tableau " + i + " ---------------\n");

                path = Path.Combine("externalFiles", "PlateauTest", "Plateau" + i +".txt");
                Plateau map2 = new Plateau(path);
                Methode.AfficherMatrice(map2.Matrice);

                if (dico.FindWord(word))
                {
                    Console.WriteLine(map2.Recherche_Mot(word).verif);
                    Methode.AfficherMatrice(map2.Recherche_Mot(word).Valeurs);
                }
                else Console.WriteLine("Le mot n'est pas dans le dictionnaire");
            }



            Console.WriteLine("--------------- Test du tableau 7 ---------------\n");

            path = Path.Combine("externalFiles","PlateauTest", "Plateau7.txt");
            Plateau map3 = new Plateau(path);
            Methode.AfficherMatrice(map3.Matrice);

            word = "dfgier";

            if (dico.FindWord(word))
            {
                Console.WriteLine(map3.Recherche_Mot(word).verif);
                Methode.AfficherMatrice(map3.Recherche_Mot(word).Valeurs);
            }
            else Console.WriteLine("Le mt n'est pas dans le dictionnaire");

        }

        static void Test2(Dictionnaire dico, int i)//test de enlever dans le tableau
        {
            string path;
            string word = "chat";
            path = Path.Combine("externalFiles", "PlateauTest", "Plateau" + i + ".txt");

            Console.WriteLine("--------------- Test du tableau "+i+" ---------------\n");

            
            Plateau map4 = new Plateau(path);
            Methode.AfficherMatrice(map4.Matrice);

        

            if (dico.FindWord(word))
            {
                Console.WriteLine(map4.Recherche_Mot(word).verif);
                Methode.AfficherMatrice(map4.Recherche_Mot(word).Valeurs);

                Console.WriteLine("Après actualisation");

                map4.Maj_Plateau(map4, word, dico );
                Methode.AfficherMatrice(map4.Matrice);
            }
            else Console.WriteLine("Le mt n'est pas dans le dictionnaire");


        }

        static void Test3(Dictionnaire dico, int i)  //+interface
        {
            string path;
            string word = "chat";
            
            for(int j = 1; j <= i; j++)
            {
                path = Path.Combine("externalFiles", "PlateauTest", "Plateau" + j + ".txt");
                Console.WriteLine("--------------- Test du tableau " + j + " ---------------\n");


                Plateau map4 = new Plateau(path);
                Methode.AfficherMatrice(map4.Matrice);


                if (dico.FindWord(word))
                {
                    Console.WriteLine(map4.Recherche_Mot(word).verif);
                    Methode.AfficherMatrice(map4.Recherche_Mot(word).Valeurs);

                    Console.WriteLine("Après actualisation");

                    map4.Maj_Plateau(map4, word, dico);

                    Methode.AfficherMatrice(map4.Matrice);
                }
                else Console.WriteLine("Le mt n'est pas dans le dictionnaire");
            }


            


        }

        public static void Test4()
        {
            Interface.PageAcceuil();
        }

        #endregion
    }
}
//possible implementation d'un historique 
