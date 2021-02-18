using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using WordLadder.Data;
using Xunit;

namespace WordLadder.Tests
{
    public class Data_Tests : IDisposable
    {
        private FileHandler _fileHandler;
        
        public Data_Tests()
        {
            _fileHandler = new FileHandler();
        }

        [Fact(DisplayName = "TestWordsDictionaryLoadSuccess")]
        public void File_WordsDictionaryLoadSuccess()
        {
            string filePath = "words-english.txt";
            var wordsLength = 4;
            var wordsDictionary = _fileHandler.LoadDictionaryContent(wordsLength, filePath);

            Assert.True(wordsDictionary != null);
        }

        [Fact(DisplayName = "TestWordsDictionaryLoadFailure")]
        public void File_WordsDictionaryLoadFailure()
        {            
            var wordsLength = 4;

            Assert.Throws<Exception>(() => _fileHandler.LoadDictionaryContent( wordsLength));
        }
        

        [Fact(DisplayName = "TestSaveResultFileSuccess")]
        public void File_SaveResultFileSuccess()
        {
            var filePath = "test.txt";
            var wordsDictionary = new List<string>();

            wordsDictionary.Add("same");
            wordsDictionary.Add("came");
            wordsDictionary.Add("case");
            wordsDictionary.Add("cast");
            wordsDictionary.Add("cost");

            _fileHandler.SaveOutputFile(filePath, wordsDictionary);

            var fileSavedSuccessfully = File.Exists(filePath);

            Assert.True(fileSavedSuccessfully);
        }

        [Fact(DisplayName = "TestSaveResultFileFailure")]
        public void File_SaveResultFileFailure()
        {
            var filePath = "../../xpto\\abs//test.txt";
            var wordsDictionary = new List<string>();

            wordsDictionary.Add("same");
            wordsDictionary.Add("came");
            wordsDictionary.Add("case");
            wordsDictionary.Add("cast");
            wordsDictionary.Add("cost");

            Assert.Throws<Exception>(() => _fileHandler.SaveOutputFile(filePath, wordsDictionary));
        }

        public void Dispose()
        {
            File.Delete("test.txt");
            File.Delete("notexists.txt");
            File.Delete("words.txt");
        }
    }
}
