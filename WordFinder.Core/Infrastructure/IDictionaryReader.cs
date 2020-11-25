using System.Collections.Generic;

namespace WordFinder.Core.Infrastructure
{
    public interface IDictionaryReader
    {
        IEnumerable<string> Read();
    }
}
