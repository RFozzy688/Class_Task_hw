using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Task_hw
{
    internal class WorkToText
    {
        string _text;

        public WorkToText()
        {
            using (FileStream fs = new FileStream("test.txt", FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    _text = sr.ReadToEnd();
                }
            }
        }
        public int NumberOfSentences()
        {
            string[] separator = { ".", "!", "?" };
            string[] sentences = null;

            sentences = _text.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            return sentences.Length;
        }
        public int NumbreOfSimbols()
        {
            return _text.Length;
        }
        public int NumbreOfWords()
        {
            char[] separator = { ' ', '.', '!', '?', '-', ',' };
            string[] words = _text.Split( separator, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }
        public int NumberOfInterrogativeSentences()
        {
            string[] sentences = _text.Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries);
            return sentences.Length;
        }
        public int NumberOfExclamationSentences()
        {
            string[] sentences = _text.Split(new[] { '!' }, StringSplitOptions.RemoveEmptyEntries);
            return sentences.Length;
        }
    }
}
