using System.Collections.Generic;

namespace WordFinder.Core.Infrastructure
{
    public class CachingDictionaryReader : IDictionaryReader
    {
        private readonly IDictionaryReader innerReader;
        private IEnumerable<string> dictionary;

        public CachingDictionaryReader(IDictionaryReader innerReader)
        {
            this.innerReader = innerReader;
        }

        public IEnumerable<string> Read()
        {
            this.dictionary = this.dictionary ?? this.innerReader.Read();
            return this.dictionary;
        }
    }
}
