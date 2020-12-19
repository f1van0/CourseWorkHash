using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    public class Leaf
    {
        public static bool operator >(Leaf leaf, string value2)
        {
            int i = 0;
            string leafValue = leaf.value;
            int length1 = leafValue.Length;
            int length2 = value2.Length;
            int minLength = Math.Min(length1, length2);
            while (leafValue[i] == value2[i] && i < minLength)
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
                if (leafValue[i] > value2[i])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool operator <(Leaf leaf, string value2)
        {
            int i = 0;
            string leafValue = leaf.value;
            int length1 = leafValue.Length;
            int length2 = value2.Length;
            int minLength = Math.Min(length1, length2);
            while (leafValue[i] == value2[i] && i < minLength)
            {
                i++;
            }

            if (i == minLength - 1)
            {
                if (length1 < length2)
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
                if (leafValue[i] < value2[i])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool operator ==(Leaf leaf, string value2)
        {
            return (leaf.value == value2);
        }

        public static bool operator !=(Leaf leaf, string value2)
        {
            return (leaf.value != value2);
        }

        public Leaf()
        {
            left = null;
            right = null;
            value = "";
        }

        public Leaf(string _value)
        {
            value = _value;
            left = null;
            right = null;
        }

        public Leaf(string _value, Leaf _left, Leaf _right)
        {
            value = _value;
            left = _left;
            right = _right;
        }

        public Leaf left;
        public Leaf right;
        public string value;
    }

    public class BinaryTree
    {
        public BinaryTree()
        {
            root = null;
            leafsCount = 0;
        }

        public bool Add(string value)
        {
            if (!Find(value))
            {
                Leaf newLeaf = new Leaf(value);

                if (root == null)
                {
                    root = newLeaf;
                }
                else
                {
                    Leaf currentLeaf = root;
                    Leaf previousLeaf = null;

                    while (currentLeaf != null)
                    {
                        if (currentLeaf < value)
                        {
                            currentLeaf = currentLeaf.right;
                        }
                        else
                        {
                            currentLeaf = currentLeaf.left;
                        }

                        previousLeaf = currentLeaf;
                    }

                    if (previousLeaf < value)
                    {
                        previousLeaf.right = newLeaf;
                    }
                    else
                    {
                        previousLeaf.left = newLeaf;
                    }
                }
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Find(string value)
        {
            Leaf currentLeaf = root;

            while (currentLeaf != null)
            {
                if (currentLeaf == value)
                {
                    return true;
                }
                else if (currentLeaf < value)
                {
                    currentLeaf = currentLeaf.right;
                }
                else
                {
                    currentLeaf = currentLeaf.left;
                }
            }

            return false;
        }

        public bool Delete(string value)
        {
            Leaf currentLeaf = root;
            if (Find(value))
            {
                while (currentLeaf != value)
                {
                    if (currentLeaf < value)
                    {
                        currentLeaf = currentLeaf.right;
                    }
                    else
                    {
                        currentLeaf = currentLeaf.left;
                    }
                }

                currentLeaf = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Print()
        {

        }

        private Leaf root;
        private int leafsCount;
    }

    public class TreeHashTable : IHashTable
    {
        private BinaryTree[] elements;
        private IHashFunc hashFunc;
        private int size;

        public TreeHashTable(int _size, IHashFunc func)
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
            for (int i = 0; i < size; i++)
            {
                elements[i] = new BinaryTree();
            }
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