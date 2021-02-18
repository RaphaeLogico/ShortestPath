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
        //public string filePath = ConfigurationManager.AppSettings.Get("InputFilePath");

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
                int wordsLength;

                Console.WriteLine(string.Format("Enter start word: "));
                startWord = new Word(Console.ReadLine());
                Console.WriteLine(Environment.NewLine);

                Console.WriteLine(string.Format("Enter target Word: "));
                targetWord = new Word(Console.ReadLine());
                Console.WriteLine(Environment.NewLine);

                if (!startWord.HasSameLength(targetWord))
                {
                    Console.WriteLine("Both words need to have same lenght.");
                }

                if (!startWord.IsValid() && !targetWord.IsValid())
                    throw new Exception("One words is invalid.");

                if (!startWord.HasSameLength(targetWord))
                    throw new Exception("Both words need to have same lenght");
                else
                    wordsLength = startWord.Value.Length;


                originalWords = fileHandler.LoadDictionaryContent(wordsLength).ToList()
                .Select(x => new Word(x.ToLower())).ToList();


                WordEngine engine = new WordEngine();
                IEnumerable<IWord> sPath = engine.FindPath(startWord, targetWord, originalWords);

                //result.ForEach(x => Console.WriteLine(x.Text));

                foreach (var item in sPath)
                {
                    Console.WriteLine(item.Value);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //static IEnumerable<Word> ExecuteCalculationProcess(Word startWord, Word targetWord, int wordsLength, string dictionaryFile, string resultFileName)
            //{
            //    List<Word> wordsList = new List<Word>();
            //    List<Word> result = new List<Word>();

            //    if (fileHandler.FileExists(dictionaryFile))
            //    {
            //        //Console.WriteLine(Environment.NewLine);
            //        //Console.WriteLine("Loading Words Dictionary File...");
            //        //// Load the Word List
            //        //wordsList = fileHandler.LoadDictionaryContent(wordsLength).ToList()
            //        //    .Select(x => new Word(x.ToLower()))
            //        //    .Where(y => y.IsValid())
            //        //    .Where(z => z.Value.Length(wordsLength)).ToList();

            //        //Console.WriteLine(Environment.NewLine);
            //        //Console.WriteLine("Loaded " + fileLoader.wordSet.Count() + " words");

            //        Console.WriteLine(Environment.NewLine);
            //        Console.WriteLine("Calculating Shortest Path...");

            //        // Calculate the Shortest Path from Start Word up to End Word
            //        var calculator = wordEngine.FindPath(startWord, targetWord);

            //        Console.WriteLine("Shortest Path Calculated Successfully.");

            //        Console.WriteLine(Environment.NewLine);
            //        Console.WriteLine("Saving Results to Output File...");

            //        if (!string.IsNullOrWhiteSpace(resultFileName))
            //        {
            //            // Save the Result to a file
            //            fileHadler.SaveResultFile(resultFileName, calculator);
            //            Console.WriteLine("Results Saved Successfully.");

            //            Console.WriteLine(Environment.NewLine);
            //            Console.WriteLine("Loading Results from Output File...");
            //            // It was not asked to be done but i added it anyway to show the results on screen
            //            result = fileLoader.LoadResultsDictionary(resultFileName).Select(x => new Word(x.ToLower())).ToList();
            //        }
            //        else
            //            Console.WriteLine(ValidationMessages.AnswerFileNameNotInformed);
            //    }
            //    else
            //    {
            //        Console.WriteLine(string.Format(ValidationMessages.FileDoesNotExists, dictionaryFile));
            //    }

            //    return result;
            //}
        }

    }
}
