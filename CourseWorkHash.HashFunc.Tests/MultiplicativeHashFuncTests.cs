using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseWorkHash.HashFunc.Tests
{
    [TestClass]
    public class MultiplicativeHashFuncTests
    {
        [TestMethod]
        public void GetHash_HashValue2And4_HashValuesAreSame()
        {
            // arrange
            MultiplicativeHashFunc multiplicativeHashFunc = new MultiplicativeHashFunc();

            // act
            long resultFromValue2 = multiplicativeHashFunc.GetHash("2", 2);
            long resultFromValue4 = multiplicativeHashFunc.GetHash("4", 2);

            // assert
            Assert.AreEqual(resultFromValue2, resultFromValue4);
        }

        [TestMethod]
        public void GetHash_HashValue2And4_HashValuesAreDifferent()
        {
            // arrange
            MultiplicativeHashFunc multiplicativeHashFunc = new MultiplicativeHashFunc();

            // act
            long resultFromValue2 = multiplicativeHashFunc.GetHash("13", 2);
            long resultFromValue4 = multiplicativeHashFunc.GetHash("2", 2);

            // assert
            Assert.AreEqual(resultFromValue2, resultFromValue4);
        }
    }
}
