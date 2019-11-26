using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BTree;

namespace BTreeTests
{
    [TestClass]
    public class BTreeDeletionTests
    {
        BTree<int> testTree;
        [TestInitialize]
        public void InitTree()
        {
            testTree = new BTree<int>();
        }

        [TestMethod]
        public void DeleteOnlyNode()
        {
            testTree.Insert(5, 0);

            testTree.Remove(5, 0);
            Assert.IsNull(testTree.Root);
        }

        [TestMethod]
        public void DeleteOnlyChildLeaf()
        {
            testTree.Insert(5, 0);
            testTree.Insert(4, 1);
            testTree.Remove(4, 1);

            Assert.AreEqual(0, testTree.Root.Nodes.Count);
            Assert.AreEqual(0, testTree.Height);
        }
        [TestMethod]
        public void DeleteOneOfManyChildLeaves()
        {
            testTree.Insert(5, 0);
            testTree.Insert(4, 1);
            testTree.Insert(3, 1);
            testTree.Insert(2, 1);

            testTree.Remove(4, 1);
            Assert.AreEqual(2, testTree.Root.Nodes.Count);
            Assert.AreEqual(1, testTree.Height);
        }
        [TestMethod]
        public void DeleteMultipleChildLeaves()
        {
            testTree.Insert(5, 0);
            testTree.Insert(4, 1);
            testTree.Insert(3, 1);
            testTree.Insert(2, 1);

            testTree.Remove(4, 1);
            testTree.Remove(3, 1);
            testTree.Remove(2, 1);

            Assert.AreEqual(0, testTree.Root.Nodes.Count);
            Assert.AreEqual(0, testTree.Height);
        }
        [TestMethod]
        public void DeleteDeepChildLeaf()
        {
            testTree.Insert(5, 0);
            testTree.Insert(4, 1);
            testTree.Insert(3, 1);
            testTree.Insert(2, 1);
            testTree.Insert(1, 2);

            testTree.Remove(1, 2);
            Assert.AreEqual(0, testTree.Root.Nodes[0].Nodes.Count);
            Assert.AreEqual(1, testTree.Height);
        }
    }
}
