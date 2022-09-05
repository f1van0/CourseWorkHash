using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseWorkHash.HashFunc.Tests
{
    [TestClass]
    public class MidSquareHashFuncTests
    {
        [TestMethod]
        public void GetHash_HashValue2And4_HashValuesAreSame()
        {
            // arrange
            MidSquareHashFunc midSquareHashFunc = new MidSquareHashFunc();

            // act
            long resultFromValue2 = midSquareHashFunc.GetHash("2", 2);
            long resultFromValue4 = midSquareHashFunc.GetHash("4", 2);

            // assert
            Assert.AreEqual(resultFromValue2, resultFromValue4);
        }

        [TestMethod]
        public void GetHash_HashValue13And2_HashValuesAreDifferent()
        {
            // arrange
            MidSquareHashFunc midSquareHashFunc = new MidSquareHashFunc();

            // act
            long resultFromValue2 = midSquareHashFunc.GetHash("13", 2);
            long resultFromValue4 = midSquareHashFunc.GetHash("2", 2);

            // assert
            Assert.AreEqual(resultFromValue2, resultFromValue4);
        }
    }
}
