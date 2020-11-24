using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WordFinder.Infrastructure
{
    public class DictionaryReader : IDictionaryReader
    {
        private readonly string filepath;

        public DictionaryReader(string filepath)
        {
            this.filepath = filepath;
        }

        public async Task<IEnumerable<string>> ReadAsync()
        {
            string[] words = await File.ReadAllLinesAsync(filepath);

            return words.Select(w => w.ToLower());
        }
    }
}
