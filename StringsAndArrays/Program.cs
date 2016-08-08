using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringsAndArrays
{
	class Program
	{
		static void Main(string[] args)
		{
			//Console.WriteLine(MinSubArrayLen(new int []{ 1, 2,3,4,5, 3 }, 9 ));
			/*var sorted = MergeSortedArrays(new int[] { 1,3, 3, 3, 5,6,7}, new int[] {2, 3, 4,6,8 });
			foreach (var i in sorted)
				Console.WriteLine(i);*/
			//Console.WriteLine(LengthOfLongestSubstring("abcabcbbabcd"));
			//Console.WriteLine(AddBinary("100","100"));
			//Console.WriteLine(Utility.EditDistance("hotter","hittest"));
			var path = "";
			Console.WriteLine(WordLadder(new string[] { "hot", "dot", "dog", "lot", "log", "cog" }, "hit", "lot", out path));
			Console.WriteLine(path);
			Console.Read();
		}

		public static int WordLadder(string[] dictionary, string start, string end, out string path)
		{
			var length = 0;

			var queue = new Queue<Word>();
			queue.Enqueue(new Word(start, 1, start));
			while (queue.Count > 0)
			{
				var currentWord = queue.Dequeue();
				foreach (var word in dictionary.ToList().Where(w => w.Length == start.Length))
				{
					if (currentWord.WordString == end)
					{
						path = currentWord.Path;
						return currentWord.Level;
					}
					if (Utility.EditDistance(currentWord.WordString, word) == 1)
						queue.Enqueue(new Word(word, currentWord.Level + 1, currentWord.Path + "-> " + word));
				}
			}
			path = "";
			return length;
		}

		public static int AddBinary(string b1, string b2)
		{
			return Convert.ToInt32(Convert.ToString((Convert.ToInt32(b1, 2) + Convert.ToInt32(b2, 2)), 2));
		}
		public static int LengthOfLongestSubstring(string input)
		{
			var result = 1;
			var dictionary = new Dictionary<char, int>();
			var charArray = input.ToCharArray();
			var index = 0;
			var start = 0;
			while(start < charArray.Length)
			{
				if (index >= charArray.Length)
					return Math.Max(result, dictionary.Count());
				if (dictionary.ContainsKey(charArray[index]))
				{
					result = Math.Max(result, dictionary.Count());
					dictionary.Clear();
					start++;
					index = start;
				}
				else
				{
					dictionary.Add(charArray[index], 1);
					index++;
				}
			}
			return result;
		}
		public static int[] MergeSortedArrays(int[] a, int[] b)
		{
			var sortedArray = new int[a.Length + b.Length];
			var i = a.Length - 1;
			var j = b.Length - 1;
			var k = sortedArray.Length - 1;

			while (k >= 0)
			{
				sortedArray[k--] = (j >= 0 && i >= 0) ? (a[i] >= b[j] ? a[i--] : b[j--]) : ((i >= 0) ? a[i--] : b[j--]);
			}
			return sortedArray;
		}
 
		public static int MinSubArrayLength(int[] array, int s)
		{
			var result = array.Length;
			var start = 0;
			var index = 0;
			var sum = 0;
			var exists = false;
			while (start < array.Length)
			{
				if(s <= sum)
				{
					exists = true;
					result = Math.Min(result, index - start);
					//sum = 0;
					//index = start + 1;
					sum -= array[start];
					start++;
				}
				else
				{
					if (index >= array.Length) break;
					sum = sum + array[index];
					index++;
				}
			}

			return exists ? result : 0;
		}
	}
}
