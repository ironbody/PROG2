using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

//API key: A8NQFOCN

namespace Hangman
{
    class Program
    {
        static Random random = new Random();

        static HttpClient client = new HttpClient();

        // static HangmanWord hangmanWord = new HangmanWord();




        static void Main(string[] args)
        {
            /* Exempel på förbättringar:
             * - Använd char istället för string.
             * - Bygg om metoderna så att man inte kan gissa på en bokstav man redan gissat på.
             * - Felsäkra metoderna. Utgå från att andra människor kommer att mata in nonsens som parametrar och 
             *   se till att koden inte krashar.
             * - Bygg en klass att spara ordet i. Klassen innehåller både det rätta ordet och hur långt spelaren kommit.
             * - Hämta ordlistan från en textfil eller en databas.
             * - Definitivt överkurs: Hitta ett sätt att hämta ett slumpat ord från en hemsida.
             */


            HangmanGame();


        }

        static void HangmanGame()
        {
            // FÖRBEREDELSER

            string word = RandomWord();

            string[] visibleWord = GenerateUnderscores(word);

            List<char> erroneousGuesses = new List<char>();

            int maxErroneousGuesses = 7;

            // SPELET
            while (!IsComplete(visibleWord) && erroneousGuesses.Count < maxErroneousGuesses)
            {
                DrawHangedMan(erroneousGuesses.Count);
                PrettyPrint(visibleWord);

                char guess = GetGuess();

                if (IsIn(guess, word))
                {
                    visibleWord = ReplaceFilter(guess, word, visibleWord);
                    System.Console.WriteLine("Correct!");

                }
                else
                {
                    erroneousGuesses.Add(guess);
                    System.Console.WriteLine("Wrong guess");
                }
            }

            if (IsComplete(visibleWord))
            {
                DisplayWin(word);
            }
            else
            {
                DisplayLoss(word);
            }
        }

        static string[] GenerateUnderscores(string word)
        {
            // Skapa en array med lika många _ som det finns bokstäver/tecken i word
            var underscoreArray = new string[word.Length];

            for (int i = 0; i < word.Length; i++)
            {
                underscoreArray[i] = "_";
            }

            return underscoreArray;
        }

        static string RandomWord()
        {
            // Slumpa ett ord från en lista, bara små bokstäver

            var words = new List<string> { "hey", "green", "yeah" };
            var randomIndex = random.Next(words.Count);
            var word = words[randomIndex].ToLower();

            return word;
        }

        static void PrettyPrint(string[] visibleWord)
        {
            // Returnera en snygg string som genererats utifrån visibleWord.
            // T.ex. om visibleWord är ["a", "p", "a"] så kan man returnera "a p a".
            var output = "";

            foreach (var letter in visibleWord)
            {
                output += letter + " ";
            }

            output.TrimEnd();

            System.Console.WriteLine(output);
        }

        static bool IsComplete(string[] visibleWord)
        {
            // Returnera true ifall alla _ har bytts ut mot tecken, annars false

            return !visibleWord.Contains("_");
        }

        static char GetGuess()
        {
            // Returnera en 1 bokstavs gissning. Returnera alltid en liten bokstav (a istället för A t.ex.)
            char guess;
            var input = Console.ReadLine().ToLower();

            while (!char.TryParse(input, out guess))
            {
                System.Console.WriteLine("Only write a single character, try again");
                input = Console.ReadLine();
            }


            return guess;
        }

        static bool IsIn(char s, string word)
        {
            // Returnera true om s finns i word, annars false.
            return word.Contains(s);
        }

        static string[] ReplaceFilter(char s, string word, string[] visibleWord)
        {
            // Skapa och returnera en kopia av visibleWord där alla positioner där 
            // s finns i word bytts ut mot s.
            // T.ex. om s är "m", word är ["m", "a", "m", "m", "a"] och visibleWord är
            // ["_", "_", "_", "_", "_"] så ska metoden returnera ["m", "_", "m", "m", "_"]

            for (int i = 0; i < word.Length; i++)
            {
                if (s == word[i])
                {
                    visibleWord[i] = s.ToString();
                }
            }

            return visibleWord;
        }

        static string DrawHangedMan(int step)
        {
            // Print the hanged man's current status, where step = 0 equals nothing being shown,
            // step = 1 equals the hill being drawn, etc.

            // TODO

            return "";
        }

        static void DisplayWin(string word)
        {
            // Visa någon form av vinst-meddelande

            System.Console.WriteLine("The word was: {0}", word);
            System.Console.WriteLine("YOU WIN!! YOU'RE A REAL GAMER");
        }

        static void DisplayLoss(string word)
        {
            // Visa någon form av förlust-meddelande

            System.Console.WriteLine("The correct word was: {0}", word);
            System.Console.WriteLine("Bruh moment");
        }

    }
}
