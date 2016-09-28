using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var start = DateTime.Now;
            //Console.WriteLine();
            var list = new List<string>();
            GetCombinations(4, 0, "", list);
            //PrintStringList(list);

            //PrintStringList(GetPossibleWordsT9("123"));
            //Console.WriteLine(MajorityVote(new int[] { 1,2,1,4,1,5,6,7}));
            //Console.WriteLine(killKthBit(37, 3));
            HeapSort(new int[] { 1,2,3,4,5,4,3,2,1});
            Console.WriteLine("Execution completed. Time taken: {0}", DateTime.Now - start);
            Console.ReadLine();
        }

        static int killKthBit(int n, int k)
        {
            Console.WriteLine(1<< (k));
            return n & (~(1 << (k + 1)));
        }

        static void GetCombinations(int available, int open, string prefix, List<string> result)
        {
            if (available > 0)
                GetCombinations(available - 1, open + 1, prefix + "(", result);
            if (open > 0)
                GetCombinations(available, open - 1, prefix + ")", result);
            if (available == 0 && open == 0)
                result.Add(prefix);
        }

        static int MajorityVote(int[] input)
        {
            var count = 0;
            var result = 0;
            for(var i=0; i< input.Length; i++)
            {
                if (count == 0)
                {
                    result = input[i];
                    count = 1;
                }
                else if (result == input[i])
                    count++;
                else
                    count--;
            }
            return result;
        }

        public static void PrintStringList(List<string> list)
        {
            foreach (var item in list)
                Console.WriteLine(item.ToString());
        }

        public static List<string> GetPossibleWordsT9(string digits)
        {
            var keypad = GetT9KeyPad();
            var output = new Queue<string>();
            output.Enqueue("");
            for(var i =0; i< digits.Length; i++)
            {
                var currentDigit = digits[i];
                while(output.Peek().Length == i)
                {
                    var temp = output.Dequeue();
                    foreach (var c in keypad[currentDigit])
                        output.Enqueue(temp + c);
                }
            }
            return output.ToList();
        }

        public static Dictionary<char, string> GetT9KeyPad()
        {
            var dictionary = new Dictionary<char, string>();
            dictionary.Add('0', " ");
            dictionary.Add('1', ".");
            dictionary.Add('2', "abc");
            dictionary.Add('3', "def");
            dictionary.Add('4', "ghi");
            dictionary.Add('5', "jkl");
            dictionary.Add('6', "mno");
            dictionary.Add('7', "pqrs");
            dictionary.Add('8', "tuv");
            dictionary.Add('9', "wxyz");

            return dictionary;
        }

        static int heapSize = 0;
        public static void HeapSort(int[] array)
        {
            heapSize = array.Length - 1;
            BuildHeap(array);
            for (int i = array.Length - 1; i >= 0; i--)
            {
                SwapArrayElements(array, 0, i);
                heapSize--;
                Heapify(array, 0);
            }
            PrintStringList(Array.ConvertAll(array, a=> a.ToString()).ToList());
        }

        private static void BuildHeap(int[] array)
        {
            var heapSize = array.Length - 1;
            for (int i = heapSize / 2; i >= 0; i--)
            {
                Heapify(array, i);
            }
        }

        private static void Heapify(int[] array, int index)
        {
            var left = 2 * index;
            var right = 2 * index + 1;
            var largest = index;
            if(left <= largest && array[left] > array[index])
                largest = left;
            if (right <= heapSize  && array[right] > array[largest])
                largest = right;
            if(largest != index)
            {
                SwapArrayElements(array, index, largest);
                Heapify(array, largest);
            }
        }

        private static void SwapArrayElements(int[] array, int x, int y)
        {
            var temp = array[x];
            array[x] = array[y];
            array[y] = temp;
        }
    }
}
