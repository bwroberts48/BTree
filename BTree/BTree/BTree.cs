using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{
    public class BTree<T> where T: IComparable<T>
    {
        private Node<T> m_root;
        private int m_height;
        public Node<T> Root 
        {
            get { return m_root; }
        }
        public int Height 
        {
            get { return m_height; }
        }
        public BTree()
        {
            m_root = null;
            m_height = -1;
        }

        //Client Insert
        public void Insert(T data, int heightBeforeInsertion)
        {
            //Disallow the client to insert two levels of height with one node
            if(heightBeforeInsertion > Height + 1)
            {
                throw new Exception("Insertion height cannot be more than 1 greater than overall tree height");
            }
            //Disallow the client to insert at 0 or negative heights
            if(heightBeforeInsertion < 0)
            {
                throw new Exception("Insertion height cannot be less 0");
            }
            //Disallow the client to insert multiple nodes at height 1
            if(heightBeforeInsertion == 0 && Root != null)
            {
                throw new Exception("There cannot be more than one Node at height 0");
            }

            if (heightBeforeInsertion == Height + 1)
                ++m_height;

            if (Root == null)
            {
                m_root = new Node<T>(data);
                Root.Nodes = new List<Node<T>>();
            }
            else
                Insert(data, heightBeforeInsertion - 1, Root);
        }
        
        //Recursive private insert
        //Started from client insert
        private void Insert(T data, int heightBeforeInsertion, Node<T> root)
        {
            //Insert
            if(heightBeforeInsertion == 0)
            {
                //Check for duplicate data and throw an exception if there is a duplicate
                if (FindDataIndex(data, root.Nodes) != -1)
                        throw new Exception("Cannot place duplicate data in child list");
                root.Nodes.Add(new Node<T>(data));
                SortNodes(root.Nodes);
            }
            //Otherwise, search for the correct child of root to make the next root
            else
            {
                //Find where the data fits in the next list of nodes
                int index = FindNextRootIndex(data, root.Nodes);

                //Recall insert with 1 less height of insertion
                Insert(data, heightBeforeInsertion - 1, root.Nodes[index]);
            }
        }

        public void Remove(T data, int heightBeforeDeletion)
        {
            //Disallow the client to attempt a deletion at negative
            if (heightBeforeDeletion < 0)
            {
                throw new Exception("Cannot delete from a height less than 0");
            }

            if (heightBeforeDeletion > m_height)
            {
                throw new Exception("Cannot delete from a height higher than the tree");
            }

            if(m_height == 0)
            {
                m_root = null;
                m_height = -1;
            }
            else
                Remove(data, heightBeforeDeletion, m_root);
        }

        private void Remove(T data, int heightOfDeletion, Node<T> root)
        {
            //If at the height for deletion delete the node if it exists
            if (heightOfDeletion == 1)
            {
                //Get the index of the data in the list
                int index = FindDataIndex(data, root.Nodes);

                //Disallow the user to delete data that doesn't exist
                if (index == -1)
                    throw new Exception("Data not found for deletion at the given height");
                
                //Remove the Node
                root.Nodes.RemoveAt(index);
                UpdateTreeHeight();
            }

            else
            {
                int index = FindNextRootIndex(data, root.Nodes);
                Remove(data, heightOfDeletion - 1, root.Nodes[index]);
            }
        }

        //Finds where an object sorts within the nodes list, returns -1
        private int FindNextRootIndex(T data, List<Node<T>> nodes)
        {
            //If the node is larger than all data (or its the first child of the root from Insert) place it in the largest index
            int index = nodes.Count - 1;
            for (int i = 0; i < nodes.Count - 1 && index == nodes.Count - 1; ++i)
            {
                //If the data is smaller than nodes[i].Data declare that as the correct position
                if (data.CompareTo(nodes[i].Data) <= 0)
                    index = i;
            }
            return index;
        }

        private int FindDataIndex(T data, List<Node<T>> nodes)
        {
            int index = -1;
            for(int i = 0; i < nodes.Count && index == -1; ++i)
            {
                if (data.CompareTo(nodes[i].Data) == 0)
                    index = i;
            }
            return index;
        }

        //Selection Sort passed through list
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

        public void UpdateTreeHeight()
        {
            if (m_root == null)
                m_height = -1;
            else
                m_height = CalcTreeHeight(m_root) - 1;
        }

        private int CalcTreeHeight(Node<T> root)
        {
            int farthestHeight = 0;
            int newHeight = 0;
            for(int i = 0; i < root.Nodes.Count; ++i)
            {
                newHeight = CalcTreeHeight(root.Nodes[i]);
                if (newHeight > farthestHeight)
                    farthestHeight = newHeight;
            }

            return farthestHeight + 1;
        }
    }
}
