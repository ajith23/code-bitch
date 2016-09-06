using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var start = DateTime.Now;
            Console.WriteLine();
            //PrintPermutations("abc", "de", "");
            //Console.WriteLine("--------------");
            //Console.WriteLine(MinimumPathCost(new int[3, 3] { { 4,2,0}, {1,3,0 }, {4,1,2 } }));
            //var node = GetNotCancelledNodes(new Node(6, new Node(-6, new Node(8, new Node(4, new Node(-12, new Node(9, new Node(8, new Node(-8, null)))))))));
            //while (node != null)
            //{
            //    Console.WriteLine(node.Value);
            //    node = node.Next;
            //}
            for(var i =0; i< input.Length; i++) {
                GetCombinationsForTargetSum("", i, 0);
            }
            foreach(var item in combinations) { Console.WriteLine(item); }
            Console.WriteLine("Execution completed. Time taken: {0}", DateTime.Now - start);
            Console.ReadLine();
        }

        static int[] input = new int[] { 1, 2, 3, 5 };
        static int target = 10;
        static HashSet<string> combinations = new HashSet<string>();
        static private void GetCombinationsForTargetSum(string built, int next, int sum)
        {
            if (next >= input.Length || sum > target) return;
            if (sum == target)
            {
                combinations.Add(built);
                return;
            }
            GetCombinationsForTargetSum(built + " " + input[next], next, sum + input[next]);
            GetCombinationsForTargetSum(built + " " + input[next], next + 1, sum + input[next]);
        }
        private static int MinimumPathCost(int[,] matrix)
        {
            var dp = new int[matrix.GetLength(0), matrix.GetLength(1)];
            dp[0, 0] = matrix[0, 0];

            //set first row
            for (var j = 1; j < dp.GetLength(1); j++)
                dp[0, j] = matrix[0, j] + dp[0, j - 1];

            //set first column
            for (var i = 1; i < dp.GetLength(0); i++)
                dp[i, 0] = matrix[i, 0] + dp[i - 1, 0];

            for (var i = 1; i < dp.GetLength(0); i++)
            {
                for (var j = 1; j < dp.GetLength(1); j++)
                {
                    dp[i, j] = matrix[i, j] + Math.Min(dp[i - 1, j - 1], Math.Min(dp[i - 1, j], dp[i, j - 1]));
                }
            }

            return dp[dp.GetLength(0)-1, dp.GetLength(1)-1];
        }

        private static void PrintPermutations(string s1, string s2, string built)
        {
            if (s1 == string.Empty && s2 == string.Empty)
            {
                Console.WriteLine(built);
            }
            else { 
                if (s1.Length > 0)
                {
                    PrintPermutations(s1.Substring(1), s2, built + s1[0]);
                }
                if (s2.Length > 0)
                {
                    PrintPermutations(s1, s2.Substring(1), built + s2[0]);
                }
            }
        } 

        private static Node GetNotCancelledNodes(Node head)
        {
            if (head == null) return null;

            var start = head;
            var end = head;
            var sum = 0;
            while(start!= null && end != null)
            {
                sum += end.Value;
                if (sum == 0)
                {
                    start = end.Next;
                    end = start;
                }
                else
                {
                    end = end.Next;
                }
            }
            return start;

            //var nodeStack = new Stack<Node>();
            //Node node = head;
            //while(node != null)
            //{
            //    if(node.Value < 0)
            //    {
            //        var sum = node.Value;
            //        while(nodeStack.Count != 0)
            //        {
            //            sum += nodeStack.Pop().Value;
            //            if(sum == 0)
            //            {
            //                node = nodeStack.Peek();
            //                break;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        nodeStack.Push(node);
            //    }
            //    node = node.Next;
            //}
            
            //return node;
        }
    }

    public class Node
    {
        public Node(int value, Node next)
        {
            Value = value;
            Next = next;
        }
        public int Value { get; private set; }
        public Node Next { get; private set; }
    }
}
