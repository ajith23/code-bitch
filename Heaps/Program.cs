using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heaps
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var inputLines = System.IO.File.ReadAllLines(@"C:\Users\avimalch\code-bitch\Heaps\input.txt"); 
            var expectedOutputLines = System.IO.File.ReadAllLines(@"C:\Users\avimalch\code-bitch\Heaps\expectedOutput.txt"); 

            var count = Convert.ToInt32(inputLines[0]);
            for (var i = 1; i <= count; i++)
            {
                ProcessItem(Convert.ToInt32(inputLines[i]));

                var expectedMedian = Convert.ToDouble(expectedOutputLines[i - 1]);
                var median = GetMedian();
                if (median != expectedMedian)
                    Console.Write("Error --- ({0})--- ", expectedMedian);
                Console.WriteLine(median.ToString("0.0"));
            }*/
            JessieAndCookies();
            Console.ReadLine();
        }

        static void JessieAndCookies()
        {
            var input = Console.ReadLine().Split(' ');
            var n = Convert.ToInt32(input[0]);
            var k = Convert.ToInt32(input[1]);
            var count = 0;
            var dataInput = Console.ReadLine().Split(' ');
            for (var i = 0; i < n; i++)
            {
                AddToMinHeap(Convert.ToInt32(dataInput[i]));
            }
            while (MinHeap[0] < k && MinHeap.Count > 0)
            {
                var newValue = PopMin() + (2 * PopMin());
                AddToMinHeap(newValue);
                count++;
            }
            Console.WriteLine(MinHeap[0] < k ? count: -1);
        }

        static int PopMin()
        {
            var temp = MinHeap[0];
            MinHeap[0] = MinHeap[MinHeap.Count - 1];
            MinHeap.RemoveAt(MinHeap.Count - 1);
            MinHeapify(0);
            return temp;
        }

        static List<int> MinHeap = new List<int>();
        static List<int> MaxHeap = new List<int>();
        static void ProcessItem(int item)
        {
            if (MaxHeap.Count > 0 && MaxHeap[0] > item)
            {
                AddToMaxHeap(item);
                if ((MaxHeap.Count - MinHeap.Count) > 1)
                {
                    AddToMinHeap(MaxDelete());
                }
            }
            else
            {
                AddToMinHeap(item);
                if ((MinHeap.Count - MaxHeap.Count) > 1)
                {
                    AddToMaxHeap(MinDelete());
                }
            }
        }
        static int MinDelete()
        {
            var temp = MinHeap[0];
            MinHeap[0] = MinHeap[MinHeap.Count - 1];
            MinHeap.RemoveAt(MinHeap.Count - 1);
            MinHeapify(0);
            return temp;
        }

        static int MaxDelete()
        {
            var temp = MaxHeap[0];
            MaxHeap[0] = MaxHeap[MaxHeap.Count - 1];
            MaxHeap.RemoveAt(MaxHeap.Count - 1);
            MaxHeapify(0);
            return temp;
        }
        static void AddToMinHeap(int item)
        {
            MinHeap.Add(item);
            var index = MinHeap.Count - 1;
            while (index > 0)
            {
                var parentIndex = (int)(Math.Ceiling((decimal)index / 2) - 1);
                if (MinHeap[parentIndex] > MinHeap[index])
                {
                    Swap(MinHeap, index, parentIndex);
                    index = parentIndex;
                }
                else
                    break;
            }
        }
        static void AddToMaxHeap(int item)
        {
            MaxHeap.Add(item);
            var index = MaxHeap.Count - 1;
            while (index > 0)
            {
                var parentIndex = (int)(Math.Ceiling((decimal)index / 2) - 1);
                if (MaxHeap[parentIndex] < MaxHeap[index])
                {
                    Swap(MaxHeap, index, parentIndex);
                    index = parentIndex;
                }
                else
                    break;
            }
        }
        static double GetMedian()
        {
            if (MinHeap.Count == MaxHeap.Count)
                return ((double)MinHeap[0] + MaxHeap[0]) / 2;
            else
                return MinHeap.Count > MaxHeap.Count ? MinHeap[0] : MaxHeap[0];
        }

        static void MinHeapify(int index)
        {
            var minIndex = index;
            var leftChildIndex = 2 * (index + 1) - 1;
            var rightChildIndex = 2 * (index + 1);
            if (leftChildIndex < MinHeap.Count && MinHeap[leftChildIndex] < MinHeap[minIndex]) 
                minIndex = leftChildIndex;
            if(rightChildIndex < MinHeap.Count && MinHeap[rightChildIndex] < MinHeap[minIndex])
                minIndex = rightChildIndex;

            if (minIndex != index)
            {
                Swap(MinHeap, index, minIndex);
                MinHeapify(minIndex);
            }
        }

        static void MaxHeapify(int index)
        {
            var maxIndex = index;
            var leftChildIndex = 2 * (index + 1) - 1;
            var rightChildIndex = 2 * (index + 1);
            maxIndex = leftChildIndex < MaxHeap.Count && MaxHeap[leftChildIndex] > MaxHeap[index] ? leftChildIndex : index;
            maxIndex = rightChildIndex < MaxHeap.Count && MaxHeap[rightChildIndex] > MaxHeap[maxIndex] ? rightChildIndex : maxIndex;

            if (maxIndex != index)
            {
                Swap(MaxHeap, index, maxIndex);
                MaxHeapify(maxIndex);
            }
        }
        static void Swap(List<int> heap, int i, int j)
        {
            var temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
    }
}
