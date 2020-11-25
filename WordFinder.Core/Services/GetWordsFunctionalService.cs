using System.Collections.Generic;
using System.Linq;
using WordFinder.Core.Infrastructure;

namespace WordFinder.Core.Services
{
    public class GetWordsFunctionalService : IGetWordsService
    {
        private readonly IDictionaryReader dictionaryReader;
        private readonly GetWordsSettings settings;

        public GetWordsFunctionalService(IDictionaryReader dictionaryReader, GetWordsSettings settings)
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
                    dictionaryWord.CanBeMadeFrom2(letters))
                .OrderBy(x => x)
                .GroupBy(x => x.Length)
                .OrderBy(x => x.Key);
        }
    }

    public static class Extensions2
    {
        public static bool CanBeMadeFrom2(this string dictionaryWord, string letters)
        {
            var wordAndLetters = new WordAndLetters(
                dictionaryWord.Select(l => l).OrderBy(l => l),
                letters.Select(l => l).OrderBy(l => l)
                );

            return RemoveMatchingLetters(wordAndLetters).IsWordEmpty();
        }

        public static WordAndLetters RemoveMatchingLetters(WordAndLetters wordAndLetters)
        {
            if (!wordAndLetters.DictionaryWord.Any() || !wordAndLetters.Letters.Any())
                return wordAndLetters;

            if (wordAndLetters.Letters.First() < wordAndLetters.DictionaryWord.First())
            {
                return RemoveMatchingLetters(
                    new WordAndLetters(
                       wordAndLetters.DictionaryWord,
                       wordAndLetters.Letters.Skip(1)
                       )
                );
            }

            if (wordAndLetters.Letters.First() == wordAndLetters.DictionaryWord.First())
            {
                return RemoveMatchingLetters(
                    new WordAndLetters(
                       wordAndLetters.DictionaryWord.Skip(1),
                       wordAndLetters.Letters.Skip(1)
                       )
                );
            }
            else
            {
                return wordAndLetters;
            }
        }

        public static IEnumerable<char> RemoveFirstInstanceOf(this IEnumerable<char> letters, char toRemove)
        {
            bool isRemoved = false;
            foreach (var letter in letters)
            {
                if (!isRemoved && letter == toRemove)
                {
                    isRemoved = true;
                }
                else
                {
                    yield return letter;
                }
            }
        }

        public class WordAndLetters
        {
            public IEnumerable<char> DictionaryWord { get; }
            public IEnumerable<char> Letters { get; }

            public bool IsWordEmpty() => !DictionaryWord.Any();

            public WordAndLetters(IEnumerable<char> dictionaryWord, IEnumerable<char> letters)
            {
                this.DictionaryWord = dictionaryWord;
                this.Letters = letters;
            }
        }
    }
}
