using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordFinder.Core.Infrastructure
{
    public class DictionaryReader : IDictionaryReader
    {
        private readonly string filepath;

        public DictionaryReader(string filepath)
        {
            this.filepath = filepath;
        }

        public IEnumerable<string> Read()
        {
            string[] words = File.ReadAllLines(filepath);

            return words.Select(w => w.ToLower());
        }
    }
}
