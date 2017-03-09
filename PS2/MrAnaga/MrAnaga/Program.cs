using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MrAnaga
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> solutions = new HashSet<string>();
            HashSet<string> rejections = new HashSet<string>();

            string line;
            string sortedLine;
            bool firstInts = false;

            while ((line = Console.ReadLine()) != null && line != "")
            {
                // The first line will always be two integers e.g. '122 6'
                // which denote 'number of words' and 'number of letters per word'
                // so let's take those, split the string, and store them as ints
                // in case we need them later on. 

                if (!firstInts)
                {
                    firstInts = true;
                }
                else
                {
                    // Sort the word alphabetically
                    sortedLine = sortWord(line);

                    // Now check the hashset. If it exists in the solution set, remove it 
                    // and add it to the reject list. If it doesn't exist in the reject set, 
                    // add it to the solution set.
                    if (solutions.Contains(sortedLine))
                    {
                        solutions.Remove(sortedLine);
                        rejections.Add(sortedLine);
                    }
                    else if(!rejections.Contains(sortedLine))
                    {
                        solutions.Add(sortedLine);
                    }
                    
                }
            }
            Console.Out.WriteLine(solutions.Count);
            //Console.ReadLine();
        }

        /// <summary>
        /// Helper method that takes a string and sorts it alphabetically
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string sortWord (string s) 
        {
            char[] unsorted = s.ToArray();
            Array.Sort(unsorted);
            string sortedWord = new string(unsorted);
            return sortedWord;
        }
    }
}
