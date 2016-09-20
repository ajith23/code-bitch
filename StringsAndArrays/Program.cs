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
            //Console.WriteLine(MinSubArrayLength(new int []{ 1, 2,3,4,5, 3 }, 9 ));
            Console.WriteLine(LongestPalindromeLength("abcbbccbb"));
            /*var sorted = MergeSortedArrays(new int[] { 1,3, 3, 3, 5,6,7}, new int[] {2, 3, 4,6,8 });
			foreach (var i in sorted)
				Console.WriteLine(i);*/
            //Console.WriteLine(LengthOfLongestSubstring("abcabcbbabcd"));
            //Console.WriteLine(AddBinary("100","100"));
            //Console.WriteLine(Utility.EditDistance("hotter","hittest"));
            //var path = "";
            //Console.WriteLine(WordLadder(new string[] { "hot", "dot", "dog", "lot", "log", "cog" }, "hit", "lot", out path));
            //Console.WriteLine(path);
            //Console.WriteLine(GetKthLargestElement(new int[] { 3, 2, 1, 5, 6, 4 }, 2));
            //Console.WriteLine(WildCardMatching("aabbbacdee", "a?bb*ee"));
            //TraverseDiagonally(new int[3, 3] { {1,2,3 }, {4,5,6 }, {7,8,9 } });
            Console.Read();
        }


        private static string LongestPalindromeHelper(string input, int i, int j)
        {
            var result = input[i].ToString();
            if (input[i] != input[j]) return result;
            
            else
            {
                while(i>=0 && j<input.Length)
                {
                    if (input[i] != input[j])
                        break;
                    else
                    {
                        i -= 1; j += 1;
                    }
                }
                return input.Substring(i+1, j-i-1);
            }

        }
        public static string LongestPalindromeLength(string input)
        {
            var result = string.Empty;
            for(var i = 0; i < input.Length - 1; i++)
            {
                var s = LongestPalindromeHelper(input, i, i);
                result = result.Length < s.Length ? s : result;
                s = LongestPalindromeHelper(input, i, i+1);
                result = result.Length < s.Length ? s : result;
            }

            return result;
        }
        public static bool WildCardMatching(string input, string query)
        {
            var inputIndex = 0;
            var queryIndex = 0;
            var starIndex = -1;
            var tempIndex = -1;
            while (inputIndex < input.Length)
            {
                if ((queryIndex < query.Length) && (input[inputIndex] == query[queryIndex] || query[queryIndex] == '?'))
                {
                    inputIndex++;
                    queryIndex++;
                }
                else if ((queryIndex < query.Length) && (query[queryIndex] == '*'))
                {
                    starIndex = queryIndex;
                    tempIndex = inputIndex;
                    queryIndex++;

                }
                else if (starIndex != -1)
                {
                    queryIndex = starIndex + 1;
                    inputIndex = ++tempIndex;
                }
                else
                {
                    return false;
                }
            }
            return queryIndex == query.Length;
        }

        public static int GetKthLargestElement(int[] array, int k)
        {
            if (k < 1 || array == null) return 0;
            return GetKth(array.Length - k + 1, array, 0, array.Length - 1);
        }

        private static int GetKth(int k, int[] array, int startIndex, int endIndex)
        {
            var pivot = array[endIndex];
            var leftIndex = startIndex;
            var rightIndex = endIndex;
            while (true)
            {
                while (pivot > array[leftIndex] && leftIndex < rightIndex)
                    leftIndex++;
                while (pivot <= array[rightIndex] && rightIndex > leftIndex)
                    rightIndex--;
                if (leftIndex == rightIndex)
                    break;
                Swap(ref array, leftIndex, rightIndex);
            }
            Swap(ref array, leftIndex, endIndex);

            if (k == leftIndex + 1)
                return pivot;
            else if (k < leftIndex + 1)
                return GetKth(k, array, startIndex, leftIndex - 1);
            else
                return GetKth(k, array, leftIndex + 1, endIndex);
        }

        private static void Swap(ref int[] array, int index1, int index2)
        {
            var temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
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
            while (start < charArray.Length)
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
                if (s <= sum)
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

        public static void TraverseDiagonally(int[,] matrix)
        {
            for (var k = 0; k <= 2 * (matrix.GetLength(0) - 1); k++)
            {
                var jStart = Math.Max(0, k - matrix.GetLength(0) +1);
                var jEnd = Math.Min(matrix.GetLength(0) - 1, k);
                for (var j = jStart; j <= jEnd; j++)
                {
                    var i = k - j;
                    Console.Write(matrix[j, i] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
