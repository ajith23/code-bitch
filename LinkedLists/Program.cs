using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedLists
{
    class Program
    {
        static void Main(string[] args)
        {
            var start = DateTime.Now;
            var list1 = new Node(0, new Node(2, new Node(4, new Node(6, new Node(8, null)))));
            var list2 = new Node(1, new Node(3, new Node(5, new Node(7, new Node(9, null)))));

            //PrintLinkedList(ReverseList(list1));
            PrintLinkedList(MergeSortedList(list1, list2));
            Console.WriteLine("Execution completed. Time taken: {0}", DateTime.Now - start);
            Console.ReadLine();
        }

        private static Node MergeSortedList(Node list1, Node list2)
        {
            Node result = null;
            if (list1 == null)
                return list2;
            if (list2 == null)
                return list1;

            if(list1.Value <= list2.Value)
            {
                result = list1;
                result.Next = MergeSortedList(list1.Next, list2);
            }
            else
            {
                result = list2;
                result.Next = MergeSortedList(list1, list2.Next);
            }
            return result;
        }

        static Node ReverseList (Node head)
        {
            var current = head;
            Node previous = null;
            Node next = null;
            while (current != null)
            {
                next = current.Next;
                current.Next = previous;
                previous = current;
                current = next;
            }
            return previous;
        }

        static void PrintLinkedList(Node node)
        {
            while(node != null)
            {
                Console.WriteLine(node.Value);
                node = node.Next;
            }
                
        }
    }
}
