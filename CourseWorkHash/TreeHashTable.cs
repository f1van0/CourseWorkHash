using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    

    public class Leaf
    {
        public static bool operator>(string value1, string value2)
        {
            int i = 0;
            int length1 = value1.Length;
            int length2 = value2.Length;
            int minLength = Math.Min(length1, length2);
            while (value1[i] == value2[i] && i < minLength)
            {
                i++;
            }

            if (i == minLength - 1)
            {
                if (length1 > length2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (value1[i] > value2[i])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Leaf()
        {
            left = null;
            right = null;
            value = "";
        }

        public Leaf left;
        public Leaf right;
        public string value;
    }

    public class BinaryTree
    {
        public bool Add(string value)
        {
            if (value > root.value)
            {

            }
        }

        public bool Find(string value)
        {
            return false;
        }

        public bool Delete(string value)
        {
            return false;
        }

        public void Print()
        {

        }

        private Leaf root;
        private int leafsCount;
    }

    public class TreeHashTable<T> : IHashTable
    {
        private BinaryTree[] elements;
        private IHashFunc hashFunc;
        private int size;

        TreeHashTable(int _size, IHashFunc func)
        {
            size = _size;
            elements = new BinaryTree[_size];
            hashFunc = func;
        }

        public bool Add(string item)
        {
            if (!Find(item))
            {
                int hashKey = hashFunc.GetHash(item, size);
                elements[hashKey].Add(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Delete(string item)
        {
            int hashKey = hashFunc.GetHash(item, size);
            if (elements[hashKey].Delete(item))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Find(string item)
        {
            int hashKey = hashFunc.GetHash(item, size);
            if (elements[hashKey].Find(item))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Find(string item, out TimeSpan timeEllapsed)
        {
            throw new NotImplementedException();
        }

        public int[] GetItems()
        {
            throw new NotImplementedException();
        }

        public void Print()
        {
            for (int i = 0; i < size; i++)
            {
                elements[i].Print();
            }
        }

        string[] IHashTable.GetItems()
        {
            throw new NotImplementedException();
        }
    }
}