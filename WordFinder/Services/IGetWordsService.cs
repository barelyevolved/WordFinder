using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordFinder.Services
{
    public interface IGetWordsService
    {
        Task<IEnumerable<IGrouping<int, string>>> GetAsync(string letters);
    }
}
