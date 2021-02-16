using System;
using System.Collections.Generic;
using System.Text;

namespace WordLadder.Data
{
    interface IFileHandler
    {
        /// <summary>
        /// List of Words
        /// </summary>
        public IEnumerable<string> WordSet { get; set; }

        /// <summary>
        /// Return a List of words generated before the calculation of shortest path between Start Word and End Word provided
        /// </summary>
        /// <param name="filePath">Name of the file to Load</param>
        /// <param name="wordsLength">Size of the words to be loaded</param>        
        public IEnumerable<string> LoadDictionaryContent(string filePath);        

        /// <summary>
        /// Save the result of the words after shortest path calculated
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        /// <param name="resultList">List of the words to be saved</param>
        public void SaveOutputFile(string filePath, IEnumerable<string> resultList);

    }
}
