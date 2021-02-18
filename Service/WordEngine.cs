using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordLadder.Data;

namespace WordLadder.Service
{
    public class WordEngine : IWordEngine<IWord>
    {
        private List<List<IWord>> allWordPaths;
        private List<Word> originalWords;
        private List<IWord> intermediateWords;

        public IEnumerable<IWord> FindPath(IWord startWord, IWord targetWord)
        {
            var wordsReturn = new List<IWord>() { startWord };
            allWordPaths = new List<List<IWord>>() { wordsReturn };            
            intermediateWords = new List<IWord>();

            bool stopWhile = false;
            int wordsLength;
            FileHandler fileIO = new FileHandler();

            if (!startWord.IsValid() && !targetWord.IsValid())
                throw new Exception("One words is invalid.");

            if (!startWord.HasSameLength(targetWord))
                throw new Exception("Both words need to have same lenght");
            else
                wordsLength = startWord.Value.Length;

            //List<string> lists = (List<string>)fileIO.LoadDictionaryContent(wordsLength);
            //allWords.AddRange(lists.ToList());

            originalWords = fileIO.LoadDictionaryContent(wordsLength)
                .ToList()
                .Select(x => new Word(x.ToLower()))
                .ToList();


            if (!startWord.IsOnList(originalWords))
                throw new Exception("Start word isn't on dictionary");

            if (!targetWord.IsOnList(originalWords))
                throw new Exception("Target word isn't on dictionary");

            do
            {
                wordsReturn = IterateWordSteps(targetWord, wordsReturn, out stopWhile).ToList();
            }
            while (wordsReturn.Count() == 0 && stopWhile == false);

            return wordsReturn;
        }

        public IEnumerable<IWord> IterateWordSteps(IWord endWord, IEnumerable<IWord> words, out bool stopWhile)
        {
            List<IWord> finalWords = new List<IWord>();

            List<List<IWord>> allWordStepsCopy = new List<List<IWord>>(allWordPaths.Select(x => x)).ToList();
            stopWhile = false;
            int i = 0;
            bool isCalculationNotCompleted = false;
            bool isCalculationFinished = false;

            try
            {
                allWordPaths.Clear();

                if (words.Count() == 0 && allWordStepsCopy.Count() == 0)
                    stopWhile = true;

                Parallel.ForEach(allWordStepsCopy, (wordSteps, state) =>
                {
                    i++;
                    var similarList = originalWords.Where(x => x.IsSimilar(wordSteps.Last()) && !wordSteps.Any(y => y.Value.Equals(x.Value)));

                    if (similarList.Any(x => x.Value.Equals(endWord.Value)))
                    {
                        wordSteps.Add(endWord);
                        finalWords = wordSteps;
                        isCalculationFinished = true;
                        state.Break();
                    }

                    if (!isCalculationFinished)
                    {
                        foreach (var word in similarList)
                        {
                            List<IWord> newWordStep = wordSteps.ToList();
                            newWordStep.Add(word);
                            allWordPaths.Add(newWordStep);
                        }
                    }

                    intermediateWords = wordSteps;
                });
                
                if (isCalculationNotCompleted)
                    stopWhile = isCalculationNotCompleted;
            }
            catch (Exception ex)
            {
                stopWhile = true;
                throw new Exception(string.Format("Exception Iterating the Words Steps during the Shortest Path Calculation using End Word: {0}. Exception Message: {1}", endWord, ex.Message));
            }

            return finalWords;
        }
    }
}
