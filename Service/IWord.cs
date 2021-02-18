using System.Collections.Generic;

namespace WordLadder.Service
{
    public interface IWord
    {
        public string Value { get; }

        /// <summary>
        /// Check if the Word contains only Letters
        /// </summary>        
        public bool IsValid();        
                
        /// <summary>
        /// Check if the provided word is on the words dictionary
        /// </summary>
        /// <param name="wordsList">Words Dictionary provided</param>
        public bool IsOnList(IEnumerable<IWord> wordsList);

        /// <summary>
        /// Check Length about two words
        /// </summary>
        /// <param name="word">Word to compare</param>
        public bool HasSameLength(IWord word);

        /// <summary>
        /// Check if two words has only one different letter
        /// </summary>
        /// <param name="word">word to compare</param>        
        public bool IsSimilar(IWord word);
    }
}