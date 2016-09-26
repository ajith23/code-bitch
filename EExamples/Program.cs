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
            Console.WriteLine();
            var list = new List<string>();
            GetCombinations(4, 0, "", list);
            PrintStringList(list);
            Console.WriteLine("Execution completed. Time taken: {0}", DateTime.Now - start);
            Console.ReadLine();
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

        public static void PrintStringList(List<string> list)
        {
            foreach (var item in list)
                Console.WriteLine(item.ToString());
        }

    }
}
