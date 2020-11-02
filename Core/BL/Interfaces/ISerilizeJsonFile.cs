using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BL.Interfaces
{
    interface ISerilizeJsonFile
    {
        bool CheckIfJsonFilesExist();

        List<Celebrity> GetAllCelebsFromJsonFile();

        bool DeleteCelebFromJsonFile(string Id);

        bool SaveCelebsToJsonFile(List<Celebrity> celebrities);

        bool DeleteJsonFile();
    }
}
