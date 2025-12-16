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
        // Chemins des fichiers
        private static string path = "externalFiles";
        private static string pathmotsfrancais = Path.Combine(path, "Mots_Français.txt");
        private static string pathdicojson = Path.Combine(path, "Dictionary.json");

       
        private List<string>[] dict;

        public List<string>[] Dict
        {
            get { return dict; }
            private set { dict = value; }
        }


        /// <summary>
        /// La fonction présente regarde si un fichier json existe et est bon, et créer ainsi Dict un tab de 26 colonne de liste
        /// </summary>
        public Dictionnaire()
        {
            
            // Comparaison des dates pour savoir s'il faut charger le JSON ou reconstruire
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
                // Désérialisation directe en tableau de listes (tente de convertir tes données, et elle protège ton programme si ça échoue.)
                Dict = JsonSerializer.Deserialize<List<string>[]>(json)
                       ?? throw new Exception("Erreur désérialisation");
            }
        }


        

        /// <summary>
        /// Sauvegarde en JSON.
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

        private List<string> MergeSort(List<string> list)
        {
            if (list.Count <= 1) return list;

            int middle = list.Count / 2;
            List<string> left = MergeSort(list.GetRange(0, middle));
            List<string> right = MergeSort(list.GetRange(middle, list.Count - middle));

            return Merge(left, right);
        }

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
            if (string.IsNullOrWhiteSpace(word)) return false;

            char firstChar = RemoveDiacritics(word[0]);
            int index = GetIndexFromChar(firstChar);

            // Vérification des bornes
            if (index < 0 || index >= 26)
            {
                Console.WriteLine($"Caractère '{word[0]}' non supporté.");
                return false;
            }

            // On lance la recherche dichotomique uniquement dans la bonne liste
            return DichotomicSearch(word.Trim(), Dict[index]);
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



        #endregion

    }
}