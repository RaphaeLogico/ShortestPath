using System;
using System.Collections.Generic;
using System.Linq;

namespace WordLadder.Service
{
    public class Word : IWord
    {
        public string Text { get; private set; }

        public Word(string value)
        {
            Text = value;
        }

        public bool IsValid()
        {
            return Text.All(Char.IsLetter);
        }

        public bool IsOnList(IEnumerable<IWord> wordsList)
        {
            return wordsList.Any(x => x.Text.ToLower().Equals(this.Text.ToLower()));
        }

        public bool HasSameLength(Word word)
        {
            return this.Text.Length == word.Text.Length;
        }

        public bool IsSimilar(IWord first, IWord second)
        {
            int differences = 0;
            if (first.Text.Length == second.Text.Length)
            {
                for (int i = 0; i < first.Text.Length; i++)
                {
                    if (first.Text[i] != second.Text[i])
                    {
                        differences++;
                    }
                }

            }

            return differences == 1;
        }

        
    }
}

