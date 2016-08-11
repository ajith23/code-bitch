using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(LongestSubstringWithKUniqueCharacters("abcbbbbcccbdddadacb", 3));
            Console.ReadLine();
        }

        private static int LongestSubstringWithKUniqueCharacters(string input, int k)
        {
            var inputCharArray = input.ToCharArray();
            var start = 0;
            var max = k;
            var dictionary = new Dictionary<char, int>();

            for(var i = 0; i < inputCharArray.Length; i++)
            {
                if (dictionary.ContainsKey(inputCharArray[i]))
                    dictionary[inputCharArray[i]]++;
                else
                    dictionary.Add(inputCharArray[i], 1);
                
                if(dictionary.Count > k)
                {
                    max = Math.Max(max, i - start);
                    while(dictionary.Count > k)
                    {
                        if (dictionary[inputCharArray[start]] > 1)
                            dictionary[inputCharArray[start]]--;
                        else
                            dictionary.Remove(inputCharArray[start]);
                        start++;
                    }
                }
            }

            return Math.Max(max, inputCharArray.Count() - start);
        }


    }
}
