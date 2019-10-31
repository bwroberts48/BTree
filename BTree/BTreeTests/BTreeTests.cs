using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BTree;

namespace BTreeTests
{
    [TestClass]
    public class BTreeTests
    {
        BTree<int> testTree;

        [TestInitialize]
        public void InitTree()
        {
            testTree = new BTree<int>();
        }

        [TestMethod]
        public void InsertToEmptyTree()
        {
            testTree.Insert(5, 0);
            Assert.IsNotNull(testTree.Root);
            Assert.AreEqual(5, testTree.Root.Data);
        }

        [TestMethod]
        public void InsertChild()
        {
            testTree.Insert(5, 0);
            testTree.Insert(4, 1);
            Assert.AreEqual(4, testTree.Root.Nodes[0].Data);   
        }

        [TestMethod]
        public void InsertChildOfChild()
        {
            testTree.Insert(5, 0);
            testTree.Insert(4, 1);
            testTree.Insert(3, 2);
            Assert.AreEqual(3, testTree.Root.Nodes[0].Nodes[0].Data);
        }

        [TestMethod]
        public void InsertSecondChild()
        {
            testTree.Insert(5, 0);
            testTree.Insert(4, 1);
            testTree.Insert(6, 1);
            Assert.AreEqual(4, testTree.Root.Nodes[0].Data);
            Assert.AreEqual(6, testTree.Root.Nodes[1].Data);
        }

        [TestMethod]
        public void InsertSecondUnorderedChild()
        {
            testTree.Insert(5, 0);
            testTree.Insert(6, 1);
            testTree.Insert(4, 1);
            Assert.AreEqual(4, testTree.Root.Nodes[0].Data);
            Assert.AreEqual(6, testTree.Root.Nodes[1].Data);
        }

        [TestMethod]
        public void InsertSecondUnorderedChildOfChild()
        {
            testTree.Insert(5, 0);
            testTree.Insert(4, 1);
            testTree.Insert(2, 2);
            testTree.Insert(0, 2);
            Assert.AreEqual(0, testTree.Root.Nodes[0].Nodes[0].Data);
            Assert.AreEqual(2, testTree.Root.Nodes[0].Nodes[1].Data);
        }

        [TestMethod]
        public void ComplexNodesSortInsertion()
        {
            testTree.Insert(5, 0);
            testTree.Insert(20, 1);
            testTree.Insert(15, 1);
            testTree.Insert(25, 1);
            testTree.Insert(1, 1);
            testTree.Insert(5, 1);
            testTree.Insert(-50, 1);
            testTree.Insert(0, 1);

            int last = Int32.MinValue;
            //Make sure the values are sorted smallest to largest
            foreach(Node<int> node in testTree.Root.Nodes)
            {
                if (last <= node.Data)
                {
                    last = node.Data;
                }
                else
                    Assert.Fail();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SecondRootException()
        {
            testTree.Insert(5, 0);
            testTree.Insert(2, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void NegativeNodeHeightException()
        {
            testTree.Insert(5, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void NodeHeightTooFarException()
        {
            testTree.Insert(5, 1);
        }
    }
}
