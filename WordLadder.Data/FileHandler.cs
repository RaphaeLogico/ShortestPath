using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordLadder.Data
{
    public class FileHandler : IFileHandler
    {
        public IEnumerable<string> WordSet { get; set; }

        public IEnumerable<string> LoadDictionaryContent(string filePath)
        {
            try
            {
                WordSet = File.ReadAllLines(filePath).ToList();

                return WordSet;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading the list of words from file path: {filePath}. Error message: {ex.Message}");                
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
