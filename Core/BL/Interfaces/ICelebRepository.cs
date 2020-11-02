using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BL
{
    public interface ICelebRepository
    {
        bool DeleteCelebById(string Id);

        List<Celebrity> GetTop100Celebrities();

        List<Celebrity> ClearTop100Celebrities();
    }
}
