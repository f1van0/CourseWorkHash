using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseWorkHash.HashTable.Tests
{
    [TestClass]
    public class QuadraticOpenHashTableTests
    {
        [TestMethod]
        public void Add_ExistentValue_FalseReturned()
        {
            // arrange
            QuadraticOpenHashTable hashTable = new QuadraticOpenHashTable(2, new DivisionHashFunc());
            hashTable.Add("Existent value");

            // act
            bool result = hashTable.Add("Existent value");

            // assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void Add_NewValueInFullFilledOpenAddressingHashTable_FalseReturned()
        {
            // arrange
            QuadraticOpenHashTable hashTable = new QuadraticOpenHashTable(2, new DivisionHashFunc());
            hashTable.Add("Value 1");
            hashTable.Add("Value 2");

            // act
            bool result = hashTable.Add("Value 3");

            // assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void Add_Add2ValuesWithSameHashInLinearOpenAddressingHashTable_TrueReturned()
        {
            // arrange
            QuadraticOpenHashTable hashTable = new QuadraticOpenHashTable(2, new DivisionHashFunc());

            // act
            hashTable.Add("2");
            hashTable.Add("4");

            // assert
            //Оба значения были найдены и, это значит, хранятся в хеш-таблице. При добавлении была разрешена коллизия и элемент "4" был помещен в другую ячейку
            bool result = hashTable.Find("2") && hashTable.Find("4");
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void Find_ExistentValue_TrueReturned()
        {
            // arrange
            QuadraticOpenHashTable hashTable = new QuadraticOpenHashTable(2, new DivisionHashFunc());
            hashTable.Add("Existent value");

            // act
            bool result = hashTable.Find("Existent value");

            // assert
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void Find_NonExistentValue_FalseReturned()
        {
            // arrange
            QuadraticOpenHashTable hashTable = new QuadraticOpenHashTable(2, new DivisionHashFunc());
            hashTable.Add("Existent value");

            // act
            bool result = hashTable.Find("Non-existent value");

            // assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void Delete_ExistentValue_TrueReturned()
        {
            // arrange
            QuadraticOpenHashTable hashTable = new QuadraticOpenHashTable(2, new DivisionHashFunc());
            hashTable.Add("Existent value");

            // act
            bool result = hashTable.Delete("Existent value");

            // assert
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void Delete_NonExistentValue_FalseReturned()
        {
            // arrange
            QuadraticOpenHashTable hashTable = new QuadraticOpenHashTable(2, new DivisionHashFunc());
            hashTable.Add("Existent value");

            // act
            bool result = hashTable.Delete("Non-existent value");

            // assert
            Assert.AreEqual(result, false);
        }
    }
}
