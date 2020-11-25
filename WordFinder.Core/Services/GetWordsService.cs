using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordFinder.Core.Infrastructure;

namespace WordFinder.Core.Services
{
    public class GetWordsService : IGetWordsService
    {
        private readonly IDictionaryReader dictionaryReader;
        private readonly GetWordsSettings settings;

        public GetWordsService(IDictionaryReader dictionaryReader, GetWordsSettings settings)
        {
            this.dictionaryReader = dictionaryReader;
            this.settings = settings;
        }

        public IEnumerable<IGrouping<int, string>> Get(string letters)
        {
            var dictionaryWords = dictionaryReader.Read();

            return dictionaryWords
                .AsParallel()
                .Where(dictionaryWord => 
                    dictionaryWord.Length >= settings.MinimumCharacters && 
                    dictionaryWord.CanBeMadeFrom(letters))
                .OrderBy(x => x)
                .GroupBy(x => x.Length)
                .OrderBy(x => x.Key);
        }
    }

    public static class Extensions
    {
        public static bool CanBeMadeFrom(this string dictionaryWord, string letters)
        {
            List<char> dictionaryLettersList = dictionaryWord.Select(l => l).ToList();
            List<char> lettersList = letters.Select(l => l).ToList();

            foreach (var dictionaryLetter in dictionaryWord)
            {
                if (lettersList.Any() && lettersList.Contains(dictionaryLetter))
                {
                    lettersList.Remove(lettersList.First(c => c == dictionaryLetter));
                    dictionaryLettersList.Remove(dictionaryLettersList.First(c => c == dictionaryLetter));
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
