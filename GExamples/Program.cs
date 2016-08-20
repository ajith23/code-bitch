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
            //Console.WriteLine(LongestSubstringWithKUniqueCharacters("abcbbbbcccbdddadacb", 3));
            //Console.WriteLine(Fibonacii(6, new int[7]));
            //Console.WriteLine(HouseRobber(new int[] { 50, 1, 1, 50 }));

            var list = GetPossibleInterpretations("@@@@@@");
            foreach (var item in list)
                Console.WriteLine(item);
            Console.ReadLine();
        }

        public static List<string> GetPossibleInterpretations(string input)
        {
            var interpretations = new List<string>();
            var interpretationStack = new Stack<KeyValuePair<string, int>>();
            var lettersDecoded = 0;

            interpretationStack.Push(new KeyValuePair<string, int>(input, lettersDecoded));

            while (interpretationStack.Count() > 0)
            {
                var currentString = interpretationStack.Pop();
                if(currentString.Key.Length == currentString.Value)
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
        

        private static List<string> GetDecodedList(string input, int lettersDecoded)
        {
            var decoded = input.Substring(0, lettersDecoded);
            var toDecode = input.Substring(lettersDecoded);
            var decodedList = new List<string>();
            var decodeData = GetDefinitions(); 
            for(var i = 1; i<=toDecode.Length; i++)
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

            dictionary.Add("@", "a");
            dictionary.Add("@@", "b");
            dictionary.Add("@@@", "c");
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
