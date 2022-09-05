using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseWorkHash.HashTable.Tests
{
    [TestClass]
    public class TreeHashTableTests
    {
        [TestMethod]
        public void Add_ExistentValue_FalseReturned()
        {
            // arrange
            ListHashTable hashTable = new ListHashTable(2, new DivisionHashFunc());
            hashTable.Add("Existent value");

            // act
            bool result = hashTable.Add("Existent value");

            // assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void Add_Add2ValuesWithSameHashInLinearOpenAddressingHashTable_TrueReturned()
        {
            // arrange
            TreeHashTable hashTable = new TreeHashTable(2, new DivisionHashFunc());

            // act
            hashTable.Add("2");
            hashTable.Add("4");

            // assert
            //Оба значения были найдены и, это значит, хранятся в хеш-таблице. 
            //При добавлении к уже помещенному в хеш-таблицу элементу "2" добавляется элемент со значением "4".
            //Элемент со значением "2" является корнем дерева, а элемент со значением "4" является листом.
            bool result = hashTable.Find("2") && hashTable.Find("4");
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void Find_ExistentValue_TrueReturned()
        {
            // arrange
            TreeHashTable hashTable = new TreeHashTable(2, new DivisionHashFunc());
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
            TreeHashTable hashTable = new TreeHashTable(2, new DivisionHashFunc());
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
            TreeHashTable hashTable = new TreeHashTable(2, new DivisionHashFunc());
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
            TreeHashTable hashTable = new TreeHashTable(2, new DivisionHashFunc());
            hashTable.Add("Existent value");

            // act
            bool result = hashTable.Delete("Non-existent value");

            // assert
            Assert.AreEqual(result, false);
        }
    }
}
