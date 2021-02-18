using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using WordLadder.Data;
using WordLadder.Service;


namespace WordLadder.App
{
    public class Program
    {
        public string filePath = ConfigurationManager.AppSettings.Get("InputFilePath");
        
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            
            Word startWord;
            Word targetWord;

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

            stopWatch.Start();

            WordEngine engine = new WordEngine();
            IEnumerable<IWord> sPath = engine.FindPath(startWord, targetWord);
            
            foreach (var item in sPath)
            {
                Console.WriteLine(item.Value);
            }
            Console.WriteLine(sPath);

        }

    }
}
