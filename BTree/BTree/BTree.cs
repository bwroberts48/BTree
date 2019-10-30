using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{
    public class BTree<T> where T: IComparable<T>
    {
        Node<T> m_root;
        int m_height;
        public BTree()
        {
            m_root = null;
            m_height = 0;
        }
        public void Insert(T data, int heightOfInsertion)
        {
            //Disallow the client to insert two levels of height with one node
            if(heightOfInsertion > m_height + 1)
            {
                throw new Exception();
            }
            //Disallow the client to insert at 0 or negative heights
            if(heightOfInsertion <= 0)
            {
                throw new Exception();
            }
            //Disallow the client to insert multiple nodes at height 1
            if(heightOfInsertion == 1 && m_root != null)
            {
                throw new Exception();
            }

            Insert(data, heightOfInsertion, m_root);
        }
        private void Insert(T data, int heightOfInsertion, Node<T> root)
        {
            if(heightOfInsertion == 0)
            {
                root = new Node<T>(data);
            }
            else if(heightOfInsertion > 1)
            {
                //Find which way to go and call insert with -1 to heightOfInsertion
                int index = FindInsertionIndex(data, root.Nodes);
                Insert(data, heightOfInsertion - 1, root.Nodes[index]);
            }

            
        }

        //Finds where an object sorts within the nodes list 
        private int FindInsertionIndex(T data, List<Node<T>> nodes)
        {
            int index = -1;
            for (int i = 0; i < nodes.Count && index == -1; ++i)
            {
                //If the data is smaller than nodes[i].Data declare that as the correct position
                if(data.CompareTo(nodes[i].Data) <= 0)
                    index = i;
            }
                return index;
        }
    }
}
