using Core.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BL
{
    public class CelebRepository : ICelebRepository
    {
        private readonly IHtmlScraper _htmlRepository;
        private readonly ISerilizeJsonFile _serilizeJsonFile;

        public CelebRepository()
        {
            _htmlRepository = new HtmlScraper();
            _serilizeJsonFile = new SerilizeJsonFile();
        }

        public List<Celebrity> ClearTop100Celebrities()
        {
            List<Celebrity> celebrities = null;
            if (_serilizeJsonFile.DeleteJsonFile())
            {
                celebrities = _htmlRepository.GetAllCelebrities();
                _serilizeJsonFile.SaveCelebsToJsonFile(celebrities);
            }
            return celebrities;
        }

        public bool DeleteCelebById(string Id)
        {
            return _serilizeJsonFile.DeleteCelebFromJsonFile(Id);
        }

        public List<Celebrity> GetTop100Celebrities()
        {
            if (_serilizeJsonFile.CheckIfJsonFilesExist())
                return _serilizeJsonFile.GetAllCelebsFromJsonFile();
            else
            {
              List<Celebrity> celebrities = _htmlRepository.GetAllCelebrities();
                _serilizeJsonFile.SaveCelebsToJsonFile(celebrities);
                return celebrities;
            }
            
        }


    }
}
