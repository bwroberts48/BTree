using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{
    internal class Node<T>
    {
        public Node(T data)
        {
            Data = data;
        }
        public T Data
        {
            set { Data = value; }
            get { return Data; }
        }
        public List<Node<T>> Nodes
        {
            set { Nodes = value; }
            get { return Nodes; }
        }
    }
}
