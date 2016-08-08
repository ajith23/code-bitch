using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
	public static class Utility
	{

		public static long[,] GetSampleMatrix()
		{
			return new long[,] {
				{131, 673,234, 103,18 },
				{ 201, 96, 342,965, 150},
				{630,830,746,422,111 },
				{ 537, 699,497, 121, 956},
				{ 805,732,524,37,331} };
		}
		public static long[,] GetMatrix()
		{
			var matrix = new long[80, 80];
			var lines = System.IO.File.ReadAllLines(@"C:\Users\ajithv\git\code-bitch\ProjectEuler\data\p083_matrix.txt");

			var index = 0;
			foreach (var line in lines)
			{
				var temp = line.Split(',');
				for (var j = 0; j < temp.Length; j++)
				{
					matrix[index, j] = Convert.ToInt64(temp[j]);
				}
				index++;
			}

			return matrix;
		}
	}


	public class Node
	{
		public Node(long value, Point point, int matrixSize )
		{
			Value = value;
			MatrixPosition = point;
			AdjacencyList = new List<Node>();
			this.matrixSize = matrixSize;
			PathCost = long.MaxValue;
		}
		private int matrixSize;
		public int Key { get { return ((MatrixPosition.X * matrixSize) + MatrixPosition.Y); } }
		public Point MatrixPosition { get; set; }
		public long Value { get; set; }
		public List<Node> AdjacencyList { get; set; }
		public long PathCost { get; set; }
		public string Path { get; set; }
		public static List<Node> GetNodes(long[,] matrix)
		{
			var nodeList = new List<Node>();
			for (var i = 0; i < matrix.GetLength(0); i++)
			{
				for (var j = 0; j < matrix.GetLength(1); j++)
				{
					nodeList.Add(new Node(matrix[i, j], new Point(i, j), matrix.GetLength(0)));
				}
			}

			return nodeList;
		}
	}

	public class Point
	{
		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}
		public int X { get; set; }
		public int Y { get; set; }

	}

}
