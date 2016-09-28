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
            //for(var i =0; i< input.Length; i++) {
            //    GetCombinationsForTargetSum("", i, 0);
            //}
            //foreach(var item in combinations) { Console.WriteLine(item); }
            //Console.WriteLine(Get2CountBruteForce(10000));

            //recurringTask("30/05/1995", 2, new string[] { "Monday", "Thursday" }, 4);
            //var s = backupTimeEstimator(new int[] { 461620201, 461620202, 461620203 }, new int[] { 2, 2, 2 }, 3);
            //foreach (var i in s)
            //    Console.WriteLine(i);
            //Console.WriteLine(SpiderMove(2,-2));
            //var s = MoveZerosToEnd(new int[] { 2,3,4});

            /*var left = new TreeNode(1, new TreeNode(2, new TreeNode(4, null, null), new TreeNode(5, null, null)), new TreeNode(3, new TreeNode(6, null, null), new TreeNode(7, null, null)));
            var t = new TreeNode(1, null, null);
            var s = GetTreeNodesInLevel(left, 2);
            foreach (var i in s)
                Console.WriteLine(i);*/
            //Rotate();
            SquareCount();
            //Console.WriteLine(GetGeneration("s->p,p->n,s->k,m->b,f->m,m->s,b->j,p"));
            Console.WriteLine("Execution completed. Time taken: {0}", DateTime.Now - start);
            Console.ReadLine();
        }

        static void SquareCount()
        {
            var count = 2;
            var queryArray = new string[2] { "3 9", "17 24" };
            var output = new List<int>();

            for (var i = 0; i < count; i++)
            {
                var input = queryArray[i];
                var a = Convert.ToInt32(input.Split(' ')[0]);
                var b = Convert.ToInt32(input.Split(' ')[1]);
                var squareCount = 0;

                int start = (int)Math.Sqrt(a);
                if (start * start == a) squareCount++;
                for (var j = start+1; j*j <= b; j++)
                {
                    squareCount++;
                }
                output.Add(squareCount);
            }

            foreach (var i in output)
                Console.WriteLine(i);
        }
        static void Rotate()
        {
            var input = "8 4";
            var n = Convert.ToInt32(input.Split(' ')[0]);
            var m = Convert.ToInt32(input.Split(' ')[1]);
            var inputString = "1 2 3 4 5 6 7 8";
            var queryArray = new string[4] { "1 2 4", "2 3 5", "1 4 7", "2 1 4" };
            var inputArray = inputString.Split(' ');
            var output = new List<string>();
            output.AddRange(inputArray);
            Console.WriteLine(string.Join(" ", output));
            for (var i = 0; i < m; i++)
            {
                var query = queryArray[i];
                var type = Convert.ToInt32(query.Split(' ')[0]);
                var start = Convert.ToInt32(query.Split(' ')[1]) - 1;
                var end = Convert.ToInt32(query.Split(' ')[2]);
                
                if (type == 2)
                {
                    var temp = output.GetRange(start, end - start);
                    output.RemoveRange(start, end - start );
                    output.AddRange(temp);
                }
                else
                {
                    var temp1 = output.GetRange(start, end-start  );
                    output.RemoveRange(start, end - start );
                    output.InsertRange(0, temp1);
                }
                
                Console.WriteLine(string.Join(" ", output));
            }

            Console.WriteLine(Math.Abs(Convert.ToInt32(output[0]) - Convert.ToInt32(output[output.Count - 1])));
            Console.WriteLine(string.Join(" ", output));
        }

        static string GetGeneration(string input)
        {
            var output = new List<string>();
            var clauses = input.Split(',');
            var parentMap = new Dictionary<string, string>();
            var childMap = new Dictionary<string, List<string>>();
            var query = clauses[clauses.Length - 1];
            var parent = string.Empty;
            foreach (var clause in clauses)
            {
                if (clause.Contains("->"))
                {
                    var f = clause.Replace("->", "|").Split('|');
                    parentMap.Add(f[1], f[0]);
                    if (f[1] == query) parent = parentMap[f[1]];
                    if (childMap.ContainsKey(f[0]))
                        childMap[f[0]].Add(f[1]);
                    else
                        childMap.Add(f[0], new List<string>() { f[1] });
                }
            }
            if (parent == string.Empty) return query;
            else
            {
                if (parentMap.ContainsKey(parent))
                {
                    var grandParent = parentMap[parent];
                    foreach (var p in childMap[grandParent])
                    {
                        output.AddRange(childMap[p]);
                    }
                }
                else
                    output.AddRange(childMap[parent]);

            }
            return string.Join(",", output.OrderBy(s => s).ToList());
        }

        static int[] MoveZerosToEnd(int[] inputArray)
        {
            var j = 0;
            for (var i = 0; i < inputArray.Length; i++)
            {
                if (inputArray[i] != 0)
                {
                    inputArray[j] = inputArray[i];
                    j++;
                }
            }
            for (var i = j; i < inputArray.Length; i++)
            {
                inputArray[i] = 0;
            }
            return inputArray;
        }

        static List<int> GetTreeNodesInLevel(TreeNode root, int level)
        {
            var result = new Queue<TreeNode>();
            //ParseTree(root, 0, level, result);

            result.Enqueue(root);
            result.Enqueue(null);
            var l = 0;
            while (result.Count() > 0)
            {
                var current = result.Dequeue();
                if (current == null)
                {
                    l++;
                    if (l == level)
                        return result.ToList().Select(s => s.Value).ToList();
                    else
                        result.Enqueue(null);
                }
                else
                {
                    if (current.Left != null)
                    {
                        result.Enqueue(current.Left);
                    }
                    if (current.Right != null)
                    {
                        result.Enqueue(current.Right);
                    }
                }
            }
            return result.ToList().Select(s => s.Value).ToList();
        }
        static void ParseTree(TreeNode node, int nodeLevel, int level, Queue<TreeNode> result)
        {
            if (nodeLevel == level)
                result.Enqueue(node);
            if (node.Left != null)
                ParseTree(node.Left, nodeLevel + 1, level, result);
            if (node.Right != null)
                ParseTree(node.Right, nodeLevel + 1, level, result);
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

            return dp[dp.GetLength(0) - 1, dp.GetLength(1) - 1];
        }

        private static void PrintPermutations(string s1, string s2, string built)
        {
            if (s1 == string.Empty && s2 == string.Empty)
            {
                Console.WriteLine(built);
            }
            else
            {
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
            while (start != null && end != null)
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

        private static int Get2CountBruteForce(int input)
        {
            var count = 0;
            for (var i = 0; i <= input; i++)
            {
                var temp = i.ToString().ToCharArray();
                foreach (var c in temp)
                    if (c == '2')
                        count++;
            }
            return count;
        }
        private static int Get2Count(int input)
        {
            var count = 0;
            var temp = input.ToString().ToCharArray();

            for (var i = temp.Count() - 1; i <= 0; i++)
            {

            }


            return count;
        }

        static string[] recurringTask(string firstDate, int k, string[] daysOfTheWeek, int n)
        {

            var temp = firstDate.Split('/');
            var firstDate1 = new DateTime(Convert.ToInt32(temp[2]), Convert.ToInt32(temp[1]), Convert.ToInt32(temp[0]));

            //var firstDate1 = DateTime.Parse(firstDate);
            //var currentDate1 = DateTime.Parse(firstDate);
            var output = new List<string>();
            var weekCount = 0;
            var dayCounter = 0;
            while (output.Count < n)
            {
                if (weekCount % k == 0)
                {
                    if (daysOfTheWeek.Contains(firstDate1.ToString("dddd")))
                        output.Add(firstDate1.ToString("dd/MM/yyyy"));
                }
                firstDate1 = firstDate1.AddDays(1);
                dayCounter++;
                if (dayCounter % 7 == 0)
                {
                    weekCount++;
                }

            }
            return output.ToArray();
        }

        static int[] incrementalBackups(int lastBackupTime, int[,] changes)
        {
            var set = new HashSet<int>();
            for (var i = 0; i < changes.GetLength(0); i++)
            {
                if (changes[i, 0] > lastBackupTime)
                    set.Add(changes[i, 1]);
            }
            var array = new int[set.Count];
            var i1 = 0;
            foreach (int s in set)
                array[i1++] = s;

            return set.OrderBy(s => s).ToArray();
        }

        static double[] backupTimeEstimator(int[] startTimes, int[] backupDuration, int maxThreads)
        {
            var output = new double[startTimes.Length];
            return output;
        }

        static int SpiderMove(int x, int y)
        {
            x = Math.Abs(x) + 1;
            y = Math.Abs(y) + 1;
            if (x == 0 || y == 0) return 0;
            if (x == 1 || y == 1) return 1;
            var dp = new int[x, y];
            for (var j = 1; j < y; j++)
                dp[0, j] = 1;
            for (var i = 1; i < x; i++)
            {
                dp[i, 0] = 1;
                for (var j = 1; j < y; j++)
                    dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
            }
            return dp[x - 1, y - 1];
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

    public class TreeNode
    {
        public TreeNode(int value, TreeNode left, TreeNode right)
        {
            Value = value;
            Left = left;
            Right = right;
        }
        public int Value { get; private set; }
        public TreeNode Left { get; private set; }
        public TreeNode Right { get; private set; }
    }
}
