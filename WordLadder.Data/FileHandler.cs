using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace WordLadder.Data
{
    public class FileHandler : IFileHandler
    {
        public IEnumerable<string> WordList { get; set; }

        public string filePath = ConfigurationManager.AppSettings.Get("InputFilePath");
       
        public IEnumerable<string> LoadDictionaryContent(int wordLenght, string path = "")
        {
            try
            {
                filePath = (string.IsNullOrEmpty(path) ? filePath : path);
                WordList = File.ReadAllLines(filePath).Where(x => x.Length.Equals(wordLenght)).ToList();

                return WordList;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading the list of words from file path. Error message: {ex.Message}");                
            }
        }

        public void SaveOutputFile(string filePath, IEnumerable<string> resultList)
        {
            try
            {
                File.WriteAllLines(filePath, resultList);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving the Result list of words on: {filePath}. Error message: {ex.Message}");
            }
        }
    }
}
