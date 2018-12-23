 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBank.Models
{
    /*
      Class to handle text speak abbrevations.
      Handles the operations for the TSA file.
    */
    class TSA
    {
        public string FilePath { get; set; }

        // Constructor
        public TSA(string filePath)
        {
            FilePath = filePath;
        }

        // Parses the text speak abbreviations into key value pairs
        public static Dictionary<string, string> ParseTSAFile()
        {
            using(StreamReader reader = new StreamReader("_read\\TSA.csv"))
            {
                Dictionary<string, string> TSA = new Dictionary<string, string>();
                while(!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    TSA.Add(values[0], values[1]);
                }
                return TSA;
            }
        }

        // Replaces the abbreviations with the full word
        public static string[] ReplaceWords(string[] messageArray)
        {
            Dictionary<string, string>TSAList = ParseTSAFile();
            int index = 0;
            foreach (string word in messageArray)
            {
                string upperWord = word.ToUpper();

                if (TSAList.ContainsKey(upperWord))
                {
                    messageArray[index] = upperWord + " <" + TSAList[upperWord] + ">";
                }
                index++;
            }
            return messageArray;
        }
    }
}
