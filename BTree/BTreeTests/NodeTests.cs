using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BTree;

namespace BTreeTests
{
    [TestClass]
    public class NodeTests
    {
        Node<int> testNode;
        [TestMethod]
        public void InitNode()
        {
            testNode = new Node<int>(5);
            Assert.AreEqual(5, testNode.Data);
        }
    }
}
