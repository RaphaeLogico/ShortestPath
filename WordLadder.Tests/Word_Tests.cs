using System;
using System.Collections.Generic;
using WordLadder.Service;
using Xunit;

namespace WordLadder.Tests
{
    public class Word_Tests : IDisposable
    {
        Word startWord;
        Word targetWord;

        [Fact(DisplayName = "IsValid")]
        public void Word_IsValid()
        {
            startWord = new Word("same");
            Assert.True(startWord.IsValid());
        }

        [Fact(DisplayName = "IsNtValid")]
        public void Word_IsNtValid()
        {
            startWord = new Word("sam4");

            Assert.False(startWord.IsValid());
        }

        [Fact(DisplayName = "OnWordsDictionary")]
        public void Word_OnWordsDictionary()
        {
            var wordsList = new List<Word>();
            startWord = new Word("same");

            wordsList.Add(new Word("same"));
            wordsList.Add(new Word("came"));
            wordsList.Add(new Word("case"));
            wordsList.Add(new Word("cast"));
            wordsList.Add(new Word("cost"));

            Assert.True(startWord.IsOnList(wordsList));
        }

        [Fact(DisplayName = "NotOnWordsDictionary")]
        public void Word_NotOnWordsDictionary()
        {
            var wordsList = new List<Word>();
            startWord = new Word("some");

            wordsList.Add(new Word("same"));
            wordsList.Add(new Word("came"));
            wordsList.Add(new Word("case"));
            wordsList.Add(new Word("cast"));
            wordsList.Add(new Word("cost"));

            Assert.False(startWord.IsOnList(wordsList));
        }


        [Fact(DisplayName = "SameLength")]
        public void Word_SameLength()
        {
            startWord = new Word("same");
            targetWord = new Word("came");

            Assert.True(startWord.HasSameLength(targetWord));
        }
                
        
        [Fact(DisplayName = "IsSimilar")]
        public void Word_IsSimilar()
        {
            startWord = new Word("same");
            targetWord = new Word("came");

            Assert.True(startWord.IsSimilar(targetWord));
        }

        [Fact(DisplayName = "IsNotSimilar")]
        public void Word_IsNotSimilar()
        {
            startWord = new Word("same");
            targetWord = new Word("come");

            Assert.False(startWord.IsSimilar(targetWord));
        }

        public void Dispose()
        {
        }
    }
}