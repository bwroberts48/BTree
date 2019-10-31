using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{
    public class Node<T> where T : IComparable<T>
    {
        public Node(T data)
        {
            Data = data;
            Nodes = new List<Node<T>>();
        }

        public T Data { get; set; }

        public List<Node<T>> Nodes { get; set; }
    }
}
