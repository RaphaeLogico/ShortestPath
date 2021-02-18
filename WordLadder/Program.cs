using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using WordLadder.Data;
using WordLadder.Service;
using WordLadder.DI;
using System.Linq;

namespace WordLadder.App
{
    public class Program
    {
        public static IFileHandler fileHandler;
        public static IWordEngine<IWord> wordEngine;        
        public static List<Word> originalWords;

        static void Main(string[] args)
        {
            try
            {
                Bootstrapper.Initialize();
                fileHandler = InjectFactory.Resolve<IFileHandler>();
                wordEngine = InjectFactory.Resolve<IWordEngine<IWord>>();

                Word startWord;
                Word targetWord;
                int wordsLength = Convert.ToInt32(ConfigurationManager.AppSettings.Get("WordsLenght"));
                string filePath = ConfigurationManager.AppSettings.Get("InputFilePath");
                string resultfile;

                Console.WriteLine(string.Format("Enter start word: "));
                startWord = new Word(Console.ReadLine());               

                Console.WriteLine(string.Format("Enter target Word: "));
                targetWord = new Word(Console.ReadLine());
                Console.WriteLine(Environment.NewLine);

                if (!startWord.HasSameLength(targetWord))                
                    Console.WriteLine("Both words need to have same lenght.");
                
                if (!startWord.IsValid() && !targetWord.IsValid())
                    throw new Exception("One words is invalid.");

                if (startWord.Value.Length != wordsLength || targetWord.Value.Length != wordsLength)
                    throw new Exception($"Both words need to have {wordsLength} of length");

                resultfile = startWord.Value + targetWord.Value + ".txt";

                originalWords = fileHandler.LoadDictionaryContent(wordsLength).ToList()
                .Select(x => new Word(x.ToLower())).ToList();

                Console.WriteLine($"Word dictionary: {filePath} Loaded");

                WordEngine engine = new WordEngine();
                IEnumerable<IWord> sPath = engine.FindPath(startWord, targetWord, originalWords);

                sPath.ToList().ForEach(x => Console.WriteLine(x.Value));
                
                IEnumerable<string> output = sPath.Select(x=> x.Value);

                fileHandler.SaveOutputFile(resultfile, output);

                Console.WriteLine($"Output file is {resultfile}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
