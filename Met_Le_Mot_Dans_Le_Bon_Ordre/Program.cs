using System;
using NKbibliotheque;
using Jeux;

namespace Met_Le_Mot_Dans_Le_Bon_Ordre
{
    public class Program
    {
        
        
        // Fonction permettant d'obtenir le mot dans le mauvais ordre
        public static string getBadWord(string correctWord)
        {
            var correctWordList = new List<char>();
            var badWordAtList = new List<char>();

            for (int i = 0; i < correctWord.Length; i++)
                correctWordList.Add(correctWord[i]);

            while (correctWordList.Count > 0)
            {
                var rand = new Random();
                int position = rand.Next(correctWordList.Count);
                badWordAtList.Add(correctWordList[position]);
                correctWordList.RemoveAt(position);
            }

            return String.Join("", badWordAtList);
        }

        // Fonction permettant de charger les mots a partir du fichier file
        public static string[] getWords(string file)
        {
            try
            {
                return File.ReadAllLines(file);
            }
            catch (Exception e) 
            {
                Console.WriteLine("Echec d'ouverture du fichier : "+e.Message);
            }
            return null;
        }
        
        // Fonction permettant de se rassurer que le nombre d'occurence d'un caractere ne deborde pas
        public static char CheckAvailability(char tmpcaracter, List<char> awaited, List<char> check)
        {
            check.Add(tmpcaracter);

            int nbNeed, nbEnter;

            nbNeed = awaited.Where(x => x == tmpcaracter).Count();
            nbEnter = check.Where(y => y == tmpcaracter).Count();

            if (nbEnter > nbNeed)
            {
                check.RemoveAt(check.Count - 1);
                tmpcaracter = NK.DemanderUneLettreDans($"Vous ne pouvez pas entrer plus de {nbNeed} lettre(s) '{tmpcaracter}' : ", awaited);
                return CheckAvailability(tmpcaracter, awaited, check);
            }

            return tmpcaracter;
        }

        // Fonction permettant de verifier que les mots sont identiques
        public static bool CheckWords(string word, char[] userWord)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] != userWord[i])
                    return false;
            }
            return true;
        }

        // Fonction pour reinitialiser les valeurs enrégistrées
        public static void Vider(char[] list1, List<char> list2)
        {
            for(int i = 0;i< list1.Length;i++)
            {
                list1[i] = '\0';
            }

            list2.Clear();
        }

        // Fonction permettant de ranger le mot dans le bon ordre
        public static void OrderTheWord(string correctWord, string badWord, int NB_VIE)
        {
            var listOfUserCaracter = new char[badWord.Length];
            var listForCheck = new List<char>();

            while (NB_VIE > 0)
            {
                for (int i = 0;i < correctWord.Length;i++)
                {
                    Console.WriteLine(Bonhomme.PENDU[3 - NB_VIE]);
                    Console.WriteLine("Trouvez le bon ordre du mot : " + badWord);
                    printWord(badWord, listOfUserCaracter);
                    Console.WriteLine("\n");
                    char tmpcaracter = NK.DemanderUneLettreDans("Entrez une lettre : ", string.Join(",", badWord).ToList());
                    char caracter = CheckAvailability(tmpcaracter, String.Join(",", correctWord).ToList(), listForCheck);
                    Console.Clear();
                    listOfUserCaracter[i] = caracter;
                }

                if(CheckWords(correctWord, listOfUserCaracter))
                {
                    Console.WriteLine(Bonhomme.PENDU[3 - NB_VIE]);
                    printWord(badWord, listOfUserCaracter);
                    Console.WriteLine();
                    Console.WriteLine("\nGAGNE !!!");
                    break;
                }

                Console.WriteLine("MAUVAIS ORDRE !");
                NB_VIE -= 1;
                Console.WriteLine("Vie(s) restante(s) : "+NB_VIE);
                Vider(listOfUserCaracter, listForCheck);                
            }

            if (NB_VIE == 0)
            {
                Console.WriteLine(Bonhomme.PENDU[3 - NB_VIE]);
                printWord(badWord, listOfUserCaracter);
                Console.WriteLine();
                Console.WriteLine("\nPERDU, le mot etaits : "+correctWord);
            }

        }

        // Fonction permettant d'afficher le mot
        public static void printWord(string word, char[] userCaracters)
        {
            for (int i = 0;i < word.Length;i++)
            {
                if (userCaracters[i] != 0)
                    Console.Write(userCaracters[i]+" ");
                else
                    Console.Write("_ ");
            }
        }

        // Programme principale
        public static void Main(string[] args)
        {
            var words = getWords("bd_words.txt");
            int NB_VIES = 3;

            if (words != null)
            {
                if (words.Length == 0)
                {
                    Console.WriteLine("Le fichier est vide !");
                }
                else
                {
                    while (true)
                    {
                        var rand = new Random();
                        string word = words[rand.Next(words.Length)].ToUpper().Trim();
                        string badWord = getBadWord(word);

                        OrderTheWord(word, badWord, NB_VIES);

                        Console.WriteLine();

                        char choix = NK.DemanderUneLettreDans("Vous les vous rejouer ? (o/n) ", new List<char> { 'O', 'N' });
                        if (choix.ToString().ToLower() == "n")
                        {
                            Console.WriteLine("A bientôt !");
                            break;
                        }
                        Console.Clear();

                    }
                }
            }
            
        }
    }
}