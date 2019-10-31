using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{
    public class BTree<T> where T: IComparable<T>
    {
        public Node<T> Root { get; private set; }
        public int Height { get; private set; }
        public BTree()
        {
            Root = null;
            Height = -1;
        }
        public void Insert(T data, int heightOfInsertion)
        {
            //Disallow the client to insert two levels of height with one node
            if(heightOfInsertion > Height + 1)
            {
                throw new Exception("Insertion height cannot be more than 1 greater than overall tree height");
            }
            //Disallow the client to insert at 0 or negative heights
            if(heightOfInsertion < 0)
            {
                throw new Exception("Insertion height cannot be less 0");
            }
            //Disallow the client to insert multiple nodes at height 1
            if(heightOfInsertion == 0 && Root != null)
            {
                throw new Exception("There cannot be more than one Node at height 0");
            }

            if (heightOfInsertion == Height + 1)
                ++Height;

            if (Root == null)
            {
                Root = new Node<T>(data);
                Root.Nodes = new List<Node<T>>();
            }
            else
                Insert(data, heightOfInsertion - 1, Root);
        }
        private void Insert(T data, int heightBeforeInsertion, Node<T> root)
        {
            //Insert
            if(heightBeforeInsertion == 0)
            {
                root.Nodes.Add(new Node<T>(data));
                SortNodes(root.Nodes);
            }
            //Otherwise, search for the correct child of root to make the next root
            else
            {
                //Find where the data fits in the next list of nodes
                int index = FindInsertionIndex(data, root.Nodes);

                //Recall insert with 1 less height of insertion
                Insert(data, heightBeforeInsertion - 1, root.Nodes[index]);
            }
        }

        //Finds where an object sorts within the nodes list, returns -1
        private int FindInsertionIndex(T data, List<Node<T>> nodes)
        {
            //If the node is larger than all data (or its the first child of the root from Insert) place it in the largest index
            int index = nodes.Count - 1;
            for (int i = 0; i < nodes.Count - 1 && index == nodes.Count - 1; ++i)
            {
                //If the data is smaller than nodes[i].Data declare that as the correct position
                if(data.CompareTo(nodes[i].Data) <= 0)
                    index = i;
            }
                return index;
        }

        //Selection Sort
        private void SortNodes(List<Node<T>> nodes)
        {
            int smallest;
            T buffer;
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                smallest = i;
                for (int j = i + 1; j < nodes.Count; j++)
                {
                    if (nodes[j].Data.CompareTo(nodes[smallest].Data) < 0)
                        smallest = j;
                }
                buffer = nodes[i].Data;
                nodes[i].Data = nodes[smallest].Data;
                nodes[smallest].Data = buffer;
            }
        }
    }
}
