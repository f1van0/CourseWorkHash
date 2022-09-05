using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseWorkHash.HashFunc.Tests
{
    [TestClass]
    public class DivisionHashFuncTests
    {
        [TestMethod]
        public void GetHash_HashValue2And4_HashValuesAreSame()
        {
            // arrange
            DivisionHashFunc divisionHashFunc = new DivisionHashFunc();

            // act
            long resultFromValue2 = divisionHashFunc.GetHash("2", 2);
            long resultFromValue4 = divisionHashFunc.GetHash("4", 2);

            // assert
            Assert.AreEqual(resultFromValue2, resultFromValue4);
        }

        [TestMethod]
        public void GetHash_HashValue13And2_HashValuesAreDifferent()
        {
            // arrange
            DivisionHashFunc divisionHashFunc = new DivisionHashFunc();

            // act
            long resultFromValue2 = divisionHashFunc.GetHash("13", 2);
            long resultFromValue4 = divisionHashFunc.GetHash("2", 2);

            // assert
            Assert.AreEqual(resultFromValue2, resultFromValue4);
        }
    }
}
