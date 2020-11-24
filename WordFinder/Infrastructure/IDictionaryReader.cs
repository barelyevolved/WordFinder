using System.Collections.Generic;
using System.Threading.Tasks;

namespace WordFinder.Infrastructure
{
    public interface IDictionaryReader
    {
        Task<IEnumerable<string>> ReadAsync();
    }
}
