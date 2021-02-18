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
                IEnumerable<string> output = sPath.Select(x=> x.Value);

                fileHandler.SaveOutputFile(startWord.Value + targetWord.Value + ".txt", output);

                Console.WriteLine($"Output file is {startWord.Value + targetWord.Value}.txt");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
