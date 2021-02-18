using System;
using System.Collections.Generic;
using System.Linq;

namespace WordLadder.Service
{
    public class Word : IWord
    {
        public string Value { get; private set; }

        public Word(string value)
        {
            Value = value;
        }

        public bool IsValid()
        {
            return Value.All(Char.IsLetter);
        }

        public bool IsOnList(IEnumerable<IWord> wordsList)
        {
            return wordsList.Any(x => x.Value.ToLower().Equals(this.Value.ToLower()));
        }

        public bool HasSameLength(IWord word)
        {
            return this.Value.Length == word.Value.Length;
        }

        public bool IsSimilar(IWord word)
        {
            int differences = 0;
            if (this.HasSameLength(word))
            {
                for (int i = 0; i < this.Value.Length; i++)
                {
                    if (this.Value[i] != word.Value[i])
                    {
                        differences++;
                    }
                }

            }

            return differences == 1;
        }

        
    }
}

