using System;
using System.Collections.Generic;

using WordLadder.Service;
using Xunit;

namespace WordLadder.Tests
{
    public class Word_Engine_Tests : IDisposable
    {
        private readonly WordEngine wordEngine;
        Word startWord;
        Word targetWord;
        int wordsLength;

        public Word_Engine_Tests()
        {
            wordEngine = new WordEngine();
        }

        [Fact(DisplayName = "WordsHasTheSameLength")]
        public void WordEngine_WordsHasTheSameLength()
        {
            var wordsList = new List<Word>();
            startWord = new Word("same");
            targetWord = new Word("crown");

            Assert.Throws<Exception>(() => wordEngine.FindPath(startWord, targetWord, wordsList));
        }
        
        [Fact(DisplayName = "WordNotOnDictionary")]
        public void WordEngine_WordNotOnDictionary()
        {
            var wordsList = new List<Word>();
            startWord = new Word("sime");
            targetWord = new Word("cost");
            wordsLength = 4;

            wordsList.Add(new Word("same"));
            wordsList.Add(new Word("came"));
            wordsList.Add(new Word("case"));
            wordsList.Add(new Word("cast"));
            wordsList.Add(new Word("cost"));

            Assert.Throws<Exception>(() => wordEngine.FindPath(startWord, targetWord, wordsList));
        }
        
        public void Dispose()
        {

        }
    }
}
