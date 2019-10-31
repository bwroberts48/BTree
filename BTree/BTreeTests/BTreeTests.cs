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
    }
}
