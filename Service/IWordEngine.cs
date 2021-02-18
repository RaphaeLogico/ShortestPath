using System;
using System.Collections.Generic;
using System.Text;

namespace WordLadder.Service
{
    public interface IWordEngine<IWord>
    {
        /// <summary>
        /// Searches the Shortest path from Start Word to the target Word using a dictionary as words list
        /// </summary>
        /// <param name="startWord">Start Word</param>
        /// <param name="targetWord">Target Word</param>      
        /// <returns></returns>
        public IEnumerable<IWord> FindPath(IWord startWord, IWord targetWord, List<Word> originalWords);

        /// <summary>
        /// Iterate through each step identifying the possibilities and separating the lists to be calculated
        /// </summary>
        /// <param name="targetWord">End word to finish the iteration when it's reached</param>
        /// <param name="wordPath"></param>
        /// <param name="stopWhile">Field used to identify when the list is not being filled anymore to leave the loop While. It does not need to be manually setted</param>
        /// <returns>Returs the List of Words found</returns>
        public IEnumerable<IWord> IterateWordSteps(IWord targetWord, IEnumerable<IWord> wordPath, out bool stopWhile);
    }
}
