using Microsoft.VisualStudio.TestTools.UnitTesting;
using IncoherentMeshChecker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncoherentMeshChecker.ViewModel.Tests
{
    [TestClass()]
    public class TableValidationTests
    {
        [TestMethod()]
        public void Validate_ValidateTableWithAppropriateHeaders_Passed()
        {
            string[] firstRow = { "A", "Node", "X", "W", "Y", "K", "Z" };
            TableValidation tw = new TableValidation();

            Assert.IsTrue(tw.ValidateNodeHeader(firstRow));
        }
        [TestMethod()]
        public void Validate_ValidateTableWithoutNodeHeader_Failed()
        {
            string[] firstRow = { "A", "X", "W", "Y", "K", "Z" };
            TableValidation tw = new TableValidation();

            Assert.IsFalse(tw.ValidateNodeHeader(firstRow));
        }
        [TestMethod()]
        public void Validate_ValidateTableWithoutXHeader_Failed()
        {
            string[] firstRow = { "A", "U", "W", "Y", "K", "Z" };
            TableValidation tw = new TableValidation();

            Assert.IsFalse(tw.ValidateNodeHeader(firstRow));
        }
        [TestMethod()]
        public void Validate_ValidateTableWithoutYHeader_Failed()
        {
            string[] firstRow = { "A", "X", "W", "I", "K", "Z" };
            TableValidation tw = new TableValidation();

            Assert.IsFalse(tw.ValidateNodeHeader(firstRow));
        }
        [TestMethod()]
        public void Validate_ValidateTableWithoutZHeader_Failed()
        {
            string[] firstRow = { "A", "X", "W", "Y", "K", "1" };
            TableValidation tw = new TableValidation();

            Assert.IsFalse(tw.ValidateNodeHeader(firstRow));
        }

        [TestMethod()]
        public void Validate_ValidateElementTable_Passed()
        {
            string[] firstRow = { "Element", "Node1", "Node2", "Node3", "Node4" };
            TableValidation tw = new TableValidation();
            Assert.IsTrue(tw.ValidateElementHeader(firstRow));
        }

        [TestMethod()]
        public void Validate_ValidateElementTableWithoutElement_Failed()
        {
            string[] firstRow = { "K", "Node1", "Node2", "Node3", "Node4" };
            TableValidation tw = new TableValidation();
            Assert.IsFalse(tw.ValidateElementHeader(firstRow));
        }
        [TestMethod()]
        public void Validate_ValidateElementTableWithoutNode1_Failed()
        {
            string[] firstRow = { "Element", "k", "Node2", "Node3", "Node4","5" };
            TableValidation tw = new TableValidation();
            Assert.IsFalse(tw.ValidateElementHeader(firstRow));
        }
        [TestMethod()]
        public void Validate_ValidateElementTableWithoutNode2_Failed()
        {
            string[] firstRow = { "K", "Node1", "Node", "Node3", "Node4" };
            TableValidation tw = new TableValidation();
            Assert.IsFalse(tw.ValidateElementHeader(firstRow));
        }
        [TestMethod()]
        public void Validate_ValidateElementTableWithoutNode3_Failed()
        {
            string[] firstRow = { "K", "Node1", "Node2", "Nod", "Node4" };
            TableValidation tw = new TableValidation();
            Assert.IsFalse(tw.ValidateElementHeader(firstRow));
        }
        [TestMethod()]
        public void Validate_ValidateElementTableWithoutNode4_Failed()
        {
            string[] firstRow = { "K", "Node1", "Node2", "Node3", "Noe4" };
            TableValidation tw = new TableValidation();
            Assert.IsFalse(tw.ValidateElementHeader(firstRow));
        }
    }
}