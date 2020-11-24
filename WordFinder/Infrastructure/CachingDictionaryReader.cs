using System.Collections.Generic;
using System.Threading.Tasks;

namespace WordFinder.Infrastructure
{
    public class CachingDictionaryReader : IDictionaryReader
    {
        private readonly IDictionaryReader innerReader;
        private IEnumerable<string> dictionary;

        public CachingDictionaryReader(IDictionaryReader innerReader)
        {
            this.innerReader = innerReader;
        }

        public async Task<IEnumerable<string>> ReadAsync()
        {
            this.dictionary = this.dictionary ?? await this.innerReader.ReadAsync();
            return this.dictionary;
        }
    }
}
