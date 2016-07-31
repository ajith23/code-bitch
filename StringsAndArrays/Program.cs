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
			//var sorted = MergeSortedArrays(new int[] { 1,3, 3, 3, 5,6,7}, new int[] {2, 3, 4,6,8 });
			//foreach (var i in sorted)
			//	Console.WriteLine(i);
			//Console.WriteLine(LengthOfLongestSubstring("abcabcbbabcd"));
			Console.WriteLine(AddBinary("100","1"));
			Console.Read();
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

				sortedArray[k] = (j >= 0 && i >= 0) ? (a[i] >= b[j] ? a[i--] : b[j--]) : ((i >= 0) ? a[i--] : b[j--]);

				/*if (j >= 0 && i >= 0)
					sortedArray[k] = a[i] >=  b[j] ? a[i--] : b[j--];
				else
				{
					if (i >= 0)
						sortedArray[k] = a[i--];
					else
						sortedArray[k] = b[j--];
				}*/
				k--;
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
