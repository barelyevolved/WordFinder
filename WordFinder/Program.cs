using System;
using System.Collections.Generic;
using System.Linq;
using WordFinder.Core.Infrastructure;
using WordFinder.Core.Services;

namespace WordFinder
{
    class Program
    {
        const string DictionaryPath = @"Resources\ukenglish.txt";

        static void Main(string[] args)
        {
            int minimumWordLength = GetMinimumWordLength();

            IGetWordsService getWords = new GetWordsService(
                new CachingDictionaryReader(new DictionaryReader(DictionaryPath)),
                new GetWordsSettings(minimumWordLength)
                );

            do
            {
                Console.Clear();
                Console.Write("Enter letters: ");
                string letters = Console.ReadLine().ToLower();

                IEnumerable<IGrouping<int, string>> wordGroups = getWords.Get(letters);

                if (wordGroups.Any())
                {
                    DisplayWords(wordGroups);
                }
                else
                {
                    Console.WriteLine("No words were found.");
                }
            }
            while (RequestPlayAgain());
        }

        private static bool RequestPlayAgain()
        {
            Console.Write("Play again? (Y/N)");
            char response = Console.ReadKey().KeyChar;
            return response == 'Y' || response == 'y';
        }

        private static void DisplayWords(IEnumerable<IGrouping<int, string>> wordGroups)
        {
            int count = 0;
            foreach (var wordGroup in wordGroups)
            {
                Console.WriteLine($"Words of {wordGroup.Key} letters:");
                foreach (var word in wordGroup)
                {
                    count++;
                    Console.WriteLine($" {word}");
                }
            }
            Console.WriteLine($"Found {count} words.");
        }

        private static int GetMinimumWordLength()
        {
            string responseString;
            int minimumWordLength;
            do
            {
                Console.Write("Enter minimum word length (e.g. 3): ");
                responseString = Console.ReadLine();
            }
            while (!int.TryParse(responseString, out minimumWordLength) || minimumWordLength < 1);
            return minimumWordLength;
        }
    }
}
