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
            string[] separator = { ". ", "! ", "? " };
            string[] sentences = null;

            sentences = _text.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            return sentences.Length;
        }
    }
}
