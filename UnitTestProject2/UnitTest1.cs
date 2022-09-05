using System;
using CourseWorkHash;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Find_NonExistentValue_FalseReturned()
        {
            // arrange
            LinearOpenHashTable hashTable = new LinearOpenHashTable(2, new DivisionHashFunc());
            hashTable.Add("Test");

            // act
            bool result = hashTable.Find("Non-existent value");

            // assert
            Assert.AreEqual(result, false);
        }
    }
}
