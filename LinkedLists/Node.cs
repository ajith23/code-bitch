using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedLists
{
    class Node
    {
        public Node(int value, Node next)
        {
            Value = value;
            Next = next;
        }

        public int Value { get; set; }
        public Node Next { get; set; }
    }
}
