using ProjetA2AlexandreAlbin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Projet_A2_S1
{
    public class Dictionnaire
    {

        #region Initialisation de la classe
        // Chemins des fichiers + intitialisation variable (pathCombien pour linux et winfows)
        private static string path = "externalFiles";
        private static string pathmotsfrancais = Path.Combine(path, "Mots_Français.txt");
        private static string pathdicojson = Path.Combine(path, "Dictionary.json");

       
        private List<string>[] dict;


        //Propriétés
        public List<string>[] Dict
        {
            get { return dict; }
            private set { dict = value; }
        }


        /// <summary>
        /// La fonction présente regarde si un fichier json existe et est bon, et créer ainsi Dict un tab de 26 colonne de liste / Constructeur du Dictionnaire
        /// </summary>
        public Dictionnaire()
        {
            
            // Comparaison des dates pour savoir s'il faut charger le JSON ou reconstruire  ( Construit a partir du json ou refais le trie et recrée le dico de 0)
            if (File.Exists(pathdicojson) && IsJsonUpToDate())
            {
                LoadFromJson();
                Console.WriteLine("Fichier json déja existant");
            }
            else
            {
                BuildFromTxt();

                Console.WriteLine("Fichier json créer");
            }
        }



        #endregion


        #region json methode

        /// <summary>
        /// Vérifie si le JSON est plus récent que le fichier TXT.
        /// </summary>
        private bool IsJsonUpToDate()
        {
            DateTime dateTxt = File.GetLastWriteTime(pathmotsfrancais);
            DateTime dateJson = File.GetLastWriteTime(pathdicojson);
            return dateJson > dateTxt;
        }

        /// <summary>
        /// Charge depuis le JSON.
        /// </summary>
        private void LoadFromJson()
        {
            using (StreamReader sr = new StreamReader(pathdicojson))
            {
                string json = sr.ReadToEnd();
                Dict = JsonSerializer.Deserialize<List<string>[]>(json);      //Méthode qui récupérer le json et le met directement dans une tab de liste
            }
        }

        /// <summary>
        /// Sauvegarde en JSON. ( quand le dictionnaire a été trié )
        /// </summary>
        private void SerializeDictionary()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(Dict, options);
            using (StreamWriter sw = new StreamWriter(pathdicojson))
            {
                sw.Write(json);
            }
        }

        #endregion



        #region trie

        

        /// <summary>
        /// Construit le dictionnaire depuis le TXT, trie et sauvegarde.
        /// Il met tous les mots commencant par A dans la tab 0, par B dans 1 ...
        /// </summary>
        private void BuildFromTxt()
        {
            // Initialisation du tableau de 26 listes
            Dict = new List<string>[26];
            for (int i = 0; i < 26; i++)
            {
                Dict[i] = new List<string>();
            }

            if (File.Exists(pathmotsfrancais))
            {
                using (StreamReader sr = new StreamReader(pathmotsfrancais))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        foreach (var w in words)
                        {
                            if (string.IsNullOrWhiteSpace(w)) continue;

                            string cleanWord = w.Trim();

                            // On prend le premier caractère sans accent (méthode simple)
                            char firstChar = RemoveDiacritics(cleanWord[0]);
                            int index = GetIndexFromChar(firstChar);

                            // Si l'index est valide (entre 0 et 25)
                            if (index >= 0 && index < 26)
                            {
                                Dict[index].Add(cleanWord);
                            }
                        }
                    }
                }
            }

            // Tri de chaque liste (Merge Sort)
            for (int i = 0; i < Dict.Length; i++)
            {
                Dict[i] = MergeSort(Dict[i]);
            }

            SerializeDictionary();
        }




        #endregion




        //  Algorithmes 


        /// <summary>
        /// Effectue une recherche dichotomique (binaire) récursive pour vérifier si un mot existe dans une liste triée.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private bool DichotomicSearch(string word, List<string> list)
        {
            if (list == null || list.Count == 0) return false;

            int middle = list.Count / 2;
            int comparison = string.Compare(word, list[middle], StringComparison.OrdinalIgnoreCase);

            if (comparison == 0) return true;

            if (comparison < 0)
                return DichotomicSearch(word, list.GetRange(0, middle));
            else
                return DichotomicSearch(word, list.GetRange(middle + 1, list.Count - middle - 1));
        }


        /// <summary>
        /// Trie une liste de chaînes de caractères par ordre alphabétique en utilisant l'algorithme du Tri Fusion (Merge Sort).
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<string> MergeSort(List<string> list)
        {
            if (list.Count <= 1) return list;

            int middle = list.Count / 2;
            List<string> left = MergeSort(list.GetRange(0, middle));
            List<string> right = MergeSort(list.GetRange(middle, list.Count - middle));

            return Merge(left, right);
        }


        /// <summary>
        /// Fusionne deux listes triées en une seule liste triée de manière stable.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private List<string> Merge(List<string> left, List<string> right)
        {
            var result = new List<string>();
            int i = 0, j = 0;
            while (i < left.Count && j < right.Count)
            {
                if (string.Compare(left[i], right[j], StringComparison.OrdinalIgnoreCase) <= 0)
                    result.Add(left[i++]);
                else
                    result.Add(right[j++]);
            }
            while (i < left.Count) result.Add(left[i++]);
            while (j < right.Count) result.Add(right[j++]);
            return result;
        }










        #region Fonction hors trie
        
        
        /// <summary>
        /// Convertit une lettre en index (0-25). 
        /// Exemple: 'A' -> 0, 'B' -> 1.
        /// </summary>
        private int GetIndexFromChar(char c)
        {
            // On s'assure que c'est une majuscule
            c = char.ToUpper(c);
            // Calcul ASCII : 'A' vaut 65. Donc si c='A', 65-65=0.
            return c - 'A';
        }

        /// <summary>
        /// Convertit un index (0-25) en lettre.
        /// Exemple: 0 -> 'A'.
        /// </summary>
        private char GetCharFromIndex(int i)
        {
            return (char)('A' + i);
        }


        /// <summary>
        /// Recherche un mot via index tableau + dichotomie.
        /// </summary>
        public bool FindWord(string word)
        {
            bool verif = false;
            if (string.IsNullOrWhiteSpace(word)) verif = false;

            char firstChar = RemoveDiacritics(word[0]);
            int index = GetIndexFromChar(firstChar);

            // Vérification des bornes
            if (index < 0 || index >= 26)
            {
                Console.WriteLine($"Caractère '{word[0]}' non supporté.");
                verif = false;
            }


            verif = DichotomicSearch(word.Trim(), Dict[index]);
            // On lance la recherche dichotomique uniquement dans la bonne liste
            return verif;
        }

        /// <summary>
        /// Petit utilitaire pour remplacer les accents de la première lettre
        /// (Ex: 'É' -> 'E') afin de trouver le bon index.
        /// </summary>
        private char RemoveDiacritics(char c)
        {
            string s = c.ToString().Normalize(System.Text.NormalizationForm.FormD);
            // On prend le premier char qui est la lettre de base
            return s.Length > 0 ? s[0] : c;
        }


        /// <summary>
        /// Retourne en String le dictionnaire, utilisé pour des tests, il n'est pas utilisé ici
        /// </summary>
        /// <returns></returns>
        /// 
        /*
        public override string ToString()
        {
            string str = "--- Contenu du Dictionnaire (Tableau) ---\n";
            for (int i = 0; i < Dict.Length; i++)
            {
                foreach(string word in Dict[i])
                {
                    str += word + ' ';
                }
                str += "\n";
            }
            return str;
        }
        
        /// <summary>
        /// Affiche dans la console le dictionnaire, utilisé pour des tests, il n'est pas utile ici
        /// </summary>
        /// <returns></returns>
        public void AfficheDico()
        {
            Console.WriteLine("--- Contenu du Dictionnaire (Tableau) ---\n");
            for (int i = 0; i < Dict.Length; i++)
            {
                foreach (string word in Dict[i])
                {
                    Console.Write(word + ' ');
                }
                Console.WriteLine();
            }
        }
        */


        #endregion

    }
}