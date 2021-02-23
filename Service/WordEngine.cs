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
        private List<IWord> intermediateWords;
        private List<Word> originalWords;
        public IEnumerable<IWord> FindPath(IWord startWord, IWord targetWord, List<Word> wordsFile)
        {;
            var wordsReturn = new List<IWord>() { startWord };
            allWordPaths = new List<List<IWord>>() { wordsReturn };            
            intermediateWords = new List<IWord>();
            originalWords = wordsFile.ToList();

            bool stopWhile = false;            

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
            List<IWord> returnWords = new List<IWord>();

            List<List<IWord>> allWordPathCopy = new List<List<IWord>>(allWordPaths.Select(x => x)).ToList();
            stopWhile = false;
            int i = 0;
            bool isCompleted = false;

            try
            {
                allWordPaths.Clear();

                if (words.Count() == 0 && allWordPathCopy.Count() == 0)
                    stopWhile = true;

                Parallel.ForEach(allWordPathCopy, (wordSteps, state) =>
                {
                    i++;
                    var similarList = originalWords.Where(x => x.IsSimilar(wordSteps.Last()) && !wordSteps.Any(y => y.Value.Equals(x.Value)));

                    if (similarList.Any(x => x.Value.Equals(endWord.Value)))
                    {
                        wordSteps.Add(endWord);
                        returnWords = wordSteps;
                        isCompleted = true;
                        state.Break();
                    }

                    if (!isCompleted)
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
                
            }
            catch (Exception ex)
            {
                stopWhile = true;
                throw new Exception($"Error during Shortest Path process. Error Message: {ex.Message}");
            }

            return returnWords;
        }
    }
}
