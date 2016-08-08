using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(PathSumFourWays());
			//Console.WriteLine(EvenFibonaciiNumbers());
			Console.ReadLine();
		}

		private static long PathSumFourWays()
		{
			var matrix = Utility.GetMatrix();
			var nodes = Node.GetNodes(matrix);
			var matrixSize = matrix.GetLength(0);
			//update adjacency list
			foreach(var node in nodes)
			{
				if ((node.MatrixPosition.X - 1) >= 0) node.AdjacencyList.Add(nodes.Where(n=> n.Key == (((node.MatrixPosition.X - 1)* matrixSize) + node.MatrixPosition.Y)).FirstOrDefault());
				if ((node.MatrixPosition.X + 1) <= (matrixSize-1)) node.AdjacencyList.Add(nodes.Where(n=> n.Key == (((node.MatrixPosition.X + 1)* matrixSize) + node.MatrixPosition.Y)).FirstOrDefault());
				if ((node.MatrixPosition.Y - 1) >= 0) node.AdjacencyList.Add(nodes.Where(n=> n.Key == (((node.MatrixPosition.X)* matrixSize) + (node.MatrixPosition.Y - 1))).FirstOrDefault());
				if ((node.MatrixPosition.Y + 1) <= (matrixSize-1)) node.AdjacencyList.Add(nodes.Where(n=> n.Key == (((node.MatrixPosition.X)* matrixSize) + (node.MatrixPosition.Y + 1))).FirstOrDefault());
				node.AdjacencyList.RemoveAll(n => n == null);
			}

			var sourceNode = nodes.Where(n => n.Key == 0).FirstOrDefault();
			var sinkNode = nodes.Where(n => n.Key == (((matrixSize-1) * matrixSize) +(matrixSize-1))).FirstOrDefault();
			var nodeStack = new Stack<Node>();
			var nodeVisited = new List<Node>();
			sourceNode.PathCost = sourceNode.Value;
			sourceNode.Path = sourceNode.Value.ToString();
			nodeStack.Push(sourceNode);
			while(nodeStack.Count != 0)
			{
				var currentNode = nodeStack.Pop();
				if (nodeVisited.Contains(currentNode)) continue;
				nodeVisited.Add(currentNode);
				if (currentNode == sinkNode) break;

				foreach (var node in currentNode.AdjacencyList)
				{
					node.PathCost = Math.Min(node.Value + currentNode.PathCost, node.PathCost);
				}

				var sorted = currentNode.AdjacencyList.OrderByDescending(n => n.PathCost);
				foreach (var node in sorted)
				{
					if (!nodeVisited.Contains(node))
					{
						//node.PathCost = currentNode.PathCost + node.Value;
						node.Path = currentNode.Path + " -> " + node.Value.ToString();
						nodeStack.Push(node);
					}
				}
			}
			Console.WriteLine(sinkNode.Path);
			return sinkNode.PathCost;
		}

		private static long PathSumFourWays1()
		{
			var matrix = Utility.GetMatrix();
			long sum = matrix[0,0];
			var i = 0;
			var j = 0;
			var visitedPoints = new List<Point>();
			visitedPoints.Add(new Point(0, 0));
			while(i <= 79 || j <=79)
			{
				var current = matrix[i, j];
				var possibleSteps = new Dictionary<Point, long>();

				var top = new Point(i - 1, j);
				var bottom = new Point(i + 1, j);
				var left = new Point(i, j - 1);
				var right = new Point(i, j + 1);

				if ((i - 1) >= 0 && !IsVisitedPoint(top, visitedPoints)) possibleSteps.Add(top, matrix[i - 1, j]);
				if ((i + 1) <= 79 && !IsVisitedPoint(bottom, visitedPoints)) possibleSteps.Add(bottom, matrix[i + 1, j]);
				if ((j - 1) >= 0 && !IsVisitedPoint(left, visitedPoints)) possibleSteps.Add(left, matrix[i, j - 1]);
				if ((j + 1) <= 79 && !IsVisitedPoint(right, visitedPoints)) possibleSteps.Add(right, matrix[i, j + 1]);

				var selectedStep = possibleSteps.OrderBy(p => p.Value).FirstOrDefault();
				sum += selectedStep.Value;
				visitedPoints.Add(selectedStep.Key);
				i = selectedStep.Key.X;
				j = selectedStep.Key.Y;

				Console.WriteLine("matrix[{0},{1}] = {2}", i, j, sum);
			}

			return sum;
		}

		private static bool IsVisitedPoint(Point point, List<Point> points)
		{
			foreach(var p in points)
			{
				if (p.X == point.X && p.Y == point.Y)
					return true;
			}
			return false;
		}

		private static long EvenFibonaciiNumbers()
		{
			var f = 0;
			var s = 1;
			var sum = 0;
			
			while (f < 4000000)
			{
				var t = f + s;
				if (t % 2 == 0)
					sum = sum + t;
				f = s;
				s = t;
			}
			return sum;
		}
	}
}
