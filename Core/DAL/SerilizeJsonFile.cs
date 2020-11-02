using Core.BL.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BL
{
    public class SerilizeJsonFile : ISerilizeJsonFile
    {
        private const string JSONPATH = @"C:\temp\";
        private const string JSONFILE = @"CelebriteJson.json";
        public SerilizeJsonFile()
        {
            if (!Directory.Exists(JSONPATH))
                Directory.CreateDirectory(JSONPATH);
        }

        /// check if json file exist
        /// </summary>
        /// <returns></returns>
        public bool CheckIfJsonFilesExist()
        {
            return File.Exists(JSONPATH + JSONFILE);
        }

        /// <summary>
        /// delete celeb by id from json file
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteCelebFromJsonFile(string Id)
        {
            try
            {
                string json = File.ReadAllText(JSONPATH + JSONFILE);

                List<Celebrity> celebrities = JsonConvert.DeserializeObject<List<Celebrity>>(json);
                var deleteCeleb = celebrities.Where(c => c.Id == Id).FirstOrDefault();
                if (deleteCeleb != null)
                {
                    celebrities.Remove(deleteCeleb);
                    SaveCelebsToJsonFile(celebrities);
                    return true;
                }
                   
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return false;
        }

        /// <summary>
        /// delete json file
        /// </summary>
        /// <returns></returns>
        public bool DeleteJsonFile()
        {
            if (CheckIfJsonFilesExist())
            {
                File.Delete(JSONPATH + JSONFILE);
                return true;
            }
            return false;
        }

        /// <summary>
        /// get all celebs from json file
        /// </summary>
        /// <returns></returns>
        public List<Celebrity> GetAllCelebsFromJsonFile()
        {
            string json = File.ReadAllText(JSONPATH + JSONFILE);

             List<Celebrity> celebrities = JsonConvert.DeserializeObject<List<Celebrity>>(json);
            return celebrities;
          
        }
       
        /// <summary>
        /// save list of celebs into json file
        /// </summary>
        /// <param name="celebrities"></param>
        /// <returns></returns>
        public bool SaveCelebsToJsonFile(List<Celebrity> celebrities)
        {
            try
            {
                DeleteJsonFile();

                using (StreamWriter file = File.CreateText(JSONPATH + JSONFILE))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, celebrities);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }
    }
}
