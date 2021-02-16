using System;
using System.Collections.Generic;
using System.Diagnostics;
using WordLadder.Service;

namespace WordLadder.App
{
    class Program
    {
        static void Main(string[] args)
        {
            _ = new Stopwatch();

            //Variables
            Word startWord;
            Word targetWord;
            int wordsLength = 0;
            string dictionaryFile;
            string resultFileName;
            List<Word> result = new List<Word>();



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


            
        }
    }
}
