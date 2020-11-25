using System.Collections.Generic;
using System.Linq;

namespace WordFinder.Core.Services
{
    public interface IGetWordsService
    {
        IEnumerable<IGrouping<int, string>> Get(string letters);
    }
}
