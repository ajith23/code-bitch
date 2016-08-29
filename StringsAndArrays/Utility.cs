using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringsAndArrays
{
	public class Utility
	{
		public static int Levenshtein(String a, String b)
		{

			if (string.IsNullOrEmpty(a))
			{
				if (!string.IsNullOrEmpty(b))
				{
					return b.Length;
				}
				return 0;
			}

			if (string.IsNullOrEmpty(b))
			{
				if (!string.IsNullOrEmpty(a))
				{
					return a.Length;
				}
				return 0;
			}

			Int32 cost;
			Int32[,] d = new int[a.Length + 1, b.Length + 1];
			Int32 min1;
			Int32 min2;
			Int32 min3;

			for (Int32 i = 0; i <= d.GetUpperBound(0); i += 1)
			{
				d[i, 0] = i;
			}

			for (Int32 i = 0; i <= d.GetUpperBound(1); i += 1)
			{
				d[0, i] = i;
			}

			Print2DArray(d);
			for (Int32 i = 1; i <= d.GetUpperBound(0); i += 1)
			{
				for (Int32 j = 1; j <= d.GetUpperBound(1); j += 1)
				{
					cost = Convert.ToInt32(!(a[i - 1] == b[j - 1]));

					min1 = d[i - 1, j] + 1;
					min2 = d[i, j - 1] + 1;
					min3 = d[i - 1, j - 1] + cost;
					d[i, j] = Math.Min(Math.Min(min1, min2), min3);
				}
			Print2DArray(d);
			}
			return d[d.GetUpperBound(0), d.GetUpperBound(1)];
		}

		private static void Print2DArray(int[,] d)
		{
			for (Int32 i = 0; i <= d.GetUpperBound(0); i += 1)
			{
				for (Int32 j = 0; j <= d.GetUpperBound(1); j += 1)
				{
					Console.Write(d[i, j].ToString() + "     ");
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}

		public static int EditDistance(string a, string b)
		{
			var matrix = new int [a.Length+1, b.Length+1];
			for (var i = 0; i <= a.Length; i++)
				matrix[i,0] = i;
			for (var j = 0; j <= b.Length; j++)
				matrix[0, j] = j;

			for(var i =1; i<= a.Length; i++)
			{
				for(var j=1; j<=b.Length; j++)
				{
					if (a.ToCharArray()[i-1] != b.ToCharArray()[j-1])
					{
						matrix[i,j] = (Math.Min(matrix[i-1,j-1], Math.Min(matrix[i,j-1], matrix[i-1,j]))) + 1;
					}
					else
						matrix[i, j] = matrix[i - 1, j - 1];
				}
			}
			return matrix[a.Length, b.Length];
		}

	}

	class Word
	{
		public Word(string wordString, int level, string path)
		{
			WordString = wordString;
			Level = level;
			Path = path;
		}
		public string WordString { get; set; }
		public int Level { get; set; }

		public string Path { get; set; }


	}
}
