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
            var startTime = DateTime.Now;
            //Console.WriteLine(LongestSubstringWith2UniqueCharacters("abcbbbbcccbdddadacb"));
            //Console.WriteLine(Fibonacii(6, new int[7]));
            //Console.WriteLine(HouseRobber(new int[] { 50, 1, 1, 50 }));

            var list = GetPossibleInterpretations("herewego");
            foreach (var item in list)
                Console.WriteLine(item);
            //Console.WriteLine(MinimumCandies(new int[6] { 1,4,3,3,3,1}));
            //Console.WriteLine(MinimumPathSum(new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }}));
            //Console.WriteLine(GetUniquePathsCount(4,5));
            //Console.WriteLine(FractionToDecimal(53,83));

            var totalTime = DateTime.Now - startTime;
            Console.WriteLine("Computation completed. Took {0} time.", totalTime);
            Console.ReadLine();
        }


        static string delimiter = "|";  //&lt; &gt;   
        public static string ToString(List<string> strings)
        {
            var buffer = new StringBuilder();
            foreach (var item in strings)
            {
                buffer.Append(item.Replace("|", "\\|") + delimiter);
            }
            return buffer.ToString();
        }  //space: O(nm)

        static List<string> FromString(string str)
        {
            if (str == "|")
                return new List<string> { "" };
            var list = new List<string>();
            var temp = str;
            var index = 1;
            for (var i = 1; i < str.Length; i++)
            {
                index++;
                if (str[i - 1] != '\\' && str[i] == '|')
                {
                    var temp1 = temp.Substring(0, index);
                    list.Add(temp1.Replace("\\|", "|"));
                    temp = temp.Substring(index);
                    index = 0;
                }
            }
            return list;
        }


        public static List<string> GetPossibleInterpretations(string input)
        {
            var interpretations = new List<string>();
            var interpretationStack = new Stack<KeyValuePair<string, int>>();
            var decodedCharacterCount = 0;

            interpretationStack.Push(new KeyValuePair<string, int>(input, decodedCharacterCount));

            while (interpretationStack.Count() > 0)
            {
                var currentString = interpretationStack.Pop();
                if (currentString.Key.Length == currentString.Value)
                {
                    interpretations.Add(currentString.Key);
                    continue;
                }
                var decodedList = GetDecodedList(currentString.Key, currentString.Value);
                foreach (var item in decodedList)
                    interpretationStack.Push(new KeyValuePair<string, int>(item, currentString.Value + 1));
            }

            return interpretations;
        }

        private static List<string> GetDecodedList(string input, int decodedCharacterCount)
        {
            var decoded = input.Substring(0, decodedCharacterCount);
            var toDecode = input.Substring(decodedCharacterCount);
            var decodedList = new List<string>();
            var decodeData = GetDefinitions();
            for (var i = 1; i <= toDecode.Length; i++)
            {
                var key = toDecode.Substring(0, i);
                if (decodeData.ContainsKey(key))
                {
                    decodedList.Add(decoded + decodeData[key] + toDecode.Substring(i));
                }
            }
            return decodedList;
        }

        public static Dictionary<string, string> GetDefinitions()
        {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("here", " here ");
            dictionary.Add("we", " we ");
            dictionary.Add("go", " go ");
            dictionary.Add("he", " he ");
            //dictionary.Add("0", "+");
            //dictionary.Add("1", "a");
            //dictionary.Add("2", "b");
            //dictionary.Add("3", "c");
            //dictionary.Add("4", "d");
            //dictionary.Add("5", "e");
            //dictionary.Add("6", "f");
            //dictionary.Add("7", "g");
            //dictionary.Add("8", "h");
            //dictionary.Add("9", "i");
            //dictionary.Add("10", "j");
            //dictionary.Add("11", "k");
            //dictionary.Add("12", "l");
            //dictionary.Add("13", "m");
            //dictionary.Add("14", "n");
            //dictionary.Add("15", "o");
            //dictionary.Add("16", "p");
            //dictionary.Add("17", "q");
            //dictionary.Add("18", "r");
            //dictionary.Add("19", "s");
            //dictionary.Add("20", "t");
            //dictionary.Add("21", "u");
            //dictionary.Add("22", "v");
            //dictionary.Add("23", "w");
            //dictionary.Add("24", "x");
            //dictionary.Add("25", "y");
            //dictionary.Add("26", "z");
            //dictionary.Add("$", 'd');
            //dictionary.Add("%", 'e');
            //dictionary.Add("^", 'f');
            return dictionary;
        }

        private static int LongestSubstringWithKUniqueCharacters(string input, int k)
        {
            var inputCharArray = input.ToCharArray();
            var start = 0;
            var max = k;
            var dictionary = new Dictionary<char, int>();

            for (var i = 0; i < inputCharArray.Length; i++)
            {
                if (dictionary.ContainsKey(inputCharArray[i]))
                    dictionary[inputCharArray[i]]++;
                else
                    dictionary.Add(inputCharArray[i], 1);

                if (dictionary.Count > k)
                {
                    max = Math.Max(max, i - start);
                    while (dictionary.Count > k)
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

        private static int LongestSubstringWith2UniqueCharacters(string input, int k = 2)
        {
            var dictionary = new Dictionary<char, int[]>();
            var maximumLength = 0;
            for (var i = 0; i < input.Length; i++)
            {
                if (dictionary.ContainsKey(input[i]))
                {
                    dictionary[input[i]][1] = i;
                    maximumLength = Math.Max(maximumLength, ((i + 1) - (dictionary.OrderBy(d => d.Value[0]).First().Value[0])));
                }
                else
                {
                    if (dictionary.Count() < 2)
                        dictionary.Add(input[i], new int[] { i, i });
                    else
                    {
                        dictionary.Remove(dictionary.OrderBy(d => d.Value[1]).First().Key);
                        dictionary.OrderBy(d => d.Value[1]).First().Value[0] = dictionary.OrderBy(d => d.Value[1]).First().Value[1];
                        dictionary.Add(input[i], new int[] { i, i });
                    }
                }
            }
            return maximumLength;
        }

        private static int Fibonacii(int n, int[] memo)
        {
            if (n == 0 || n == 1)
            {
                memo[n] = n;
                return n;
            }
            if (memo[n] == 0)
                memo[n] = Fibonacii(n - 1, memo) + Fibonacii(n - 2, memo);
            foreach (var i in memo)
                Console.Write(i + " ");
            Console.WriteLine();
            return memo[n];
        }

        private static int HouseRobber(int[] houses)
        {
            var dp = new int[houses.Count()];
            dp[0] = houses[0];
            dp[1] = Math.Max(dp[0], houses[1]);

            for (var i = 2; i < houses.Count(); i++)
            {
                dp[i] = Math.Max(houses[i] + dp[i - 2], dp[i - 1]);
            }
            return dp[dp.Count() - 1];
        }

        private static int HouseRobber3(Node tree)
        {
            if (tree == null) return 0;
            if (tree.Left == null && tree.Right == null)
            {
                return tree.Value;
            }

            tree.Value = Math.Max(tree.Value, HouseRobber3(tree.Left) + HouseRobber3(tree.Right));
            //tree.Right.Value = Math.Max(tree.Value, HouseRobber3(tree.Right));

            return Math.Max(tree.Value, tree.Left.Value + tree.Right.Value);
            //return HouseRobber3(tree.Left);
        }

        private static int MinimumCandies(int[] rating)
        {
            foreach (var i in rating)
                Console.Write(i + " ");
            Console.WriteLine("");
            var candyCount = 0;
            var candies = new int[rating.Length];
            candies[0] = 1;
            for (var i = 1; i < rating.Length; i++)
            {
                if (rating[i - 1] < rating[i])
                    candies[i] = candies[i - 1] + 1;
                else
                    candies[i] = 1;
            }
            candyCount = candies[candies.Length - 1];
            for (var i = rating.Length - 2; i >= 0; i--)
            {
                var currentCandy = 1;
                if (rating[i] > rating[i + 1])
                {
                    currentCandy = candies[i + 1] + 1;
                }
                candies[i] = Math.Max(candies[i], currentCandy);
                candyCount += candies[i];
            }

            foreach (var i in candies)
                Console.Write(i + " ");
            Console.WriteLine("");
            return candyCount;
        }

        private static int MinimumPathSum(int[,] inputData)
        {
            var pathList = new List<string>();
            var dp = new int[inputData.GetLength(0), inputData.GetLength(1)];
            dp[0, 0] = inputData[0, 0];

            for (var i = 1; i < inputData.GetLength(1); i++)
                dp[0, i] = dp[0, i - 1] + inputData[0, i];

            for (var i = 1; i < inputData.GetLength(0); i++)
                dp[i, 0] = dp[i - 1, 0] + inputData[i, 0];

            for (var i = 1; i < inputData.GetLength(0); i++)
            {
                for (var j = 1; j < inputData.GetLength(1); j++)
                {
                    if (dp[i - 1, j] < dp[i, j - 1])
                        dp[i, j] = inputData[i, j] + dp[i - 1, j];
                    else
                        dp[i, j] = inputData[i, j] + dp[i, j - 1];
                }
            }

            pathList.Add((inputData.GetLength(0) - 1) + ", " + (inputData.GetLength(1) - 1));
            int row = inputData.GetLength(0) - 1, column = inputData.GetLength(1) - 1;
            while (row > 0 || column > 0)
            {
                if (inputData[row == 0 ? row : (row - 1), column] < inputData[row, column == 0 ? column : column - 1])
                {
                    pathList.Add((row - 1) + ", " + column);
                    row = row - 1;
                }
                else
                {
                    pathList.Add((row) + ", " + (column - 1));
                    column = column - 1;
                }

            }
            for (var i = 0; i < inputData.GetLength(0); i++)
            {
                for (var j = 0; j < inputData.GetLength(1); j++)
                {
                    Console.Write(dp[i, j] + " \t");
                }
                Console.WriteLine();
            }
            foreach (var path in pathList)
                Console.WriteLine(path);
            return dp[inputData.GetLength(0) - 1, inputData.GetLength(1) - 1];
        }

        private static int GetUniquePathsCount(int rowCount, int columnCount)
        {
            var dp = new int[rowCount, columnCount];
            for (var i = 0; i < rowCount; i++)
                dp[i, 0] = 1;
            for (var i = 0; i < columnCount; i++)
                dp[0, i] = 1;

            for (var i = 1; i < rowCount; i++)
            {
                for (var j = 1; j < columnCount; j++)
                {
                    dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
                }
            }

            for (var i = 0; i < rowCount; i++)
            {
                for (var j = 0; j < columnCount; j++)
                {
                    Console.Write(dp[i, j] + " \t");
                }
                Console.WriteLine();
            }
            return dp[rowCount - 1, columnCount - 1];
        }

        private static string FractionToDecimal(long numerator, long denominator)
        {
            var result = string.Empty;
            if (numerator < 0 ^ denominator < 0)
                result += "-";
            numerator = Math.Abs(numerator);
            denominator = Math.Abs(denominator);
            result += (numerator / denominator);
            var remainder = numerator % denominator;
            if (remainder == 0)
                return result;
            else
                remainder = remainder * 10;
            var decimalDictionary = new Dictionary<long, int>();
            result += ".";
            while (remainder != 0)
            {
                if (decimalDictionary.ContainsKey(remainder))
                {
                    return result.Substring(0, decimalDictionary[remainder]) + "(" + result.Substring(decimalDictionary[remainder]) + ")";
                }
                decimalDictionary.Add(remainder, result.Length);
                result += (remainder / denominator);
                remainder = (remainder % denominator) * 10;
            }
            return result;
        }
    }

    class Node
    {
        public Node(Node left, Node right, int value)
        {
            Left = left;
            Right = right;
            Value = value;
        }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Value { get; set; }
    }
}
