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
            string leafValue = leaf.val;
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
            string leafValue = leaf.val;
            int length1 = leafValue.Length;
            int length2 = value2.Length;
            int minLength = Math.Min(length1, length2);
            while (i < minLength && leafValue[i] == value2[i])
            {
                i++;
            }

            if (i == minLength)
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

        public Leaf()
        {
            left = null;
            right = null;
            val = "";
        }

        public Leaf(string _value)
        {
            val = _value;
            left = null;
            right = null;
        }

        public Leaf(string _value, Leaf _left, Leaf _right)
        {
            val = _value;
            left = _left;
            right = _right;
        }

        public Leaf GetLeft()
        {
            return left;
        }

        public void SetLeft(string _value)
        {
            left = new Leaf(_value);
        }

        public Leaf GetRight()
        {
            return right;
        }

        public void SetRight(string _value)
        {
            right = new Leaf(_value);
        }

        public Leaf Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
            }
        }

        public Leaf Right
        {
            get
            {
                return right;
            }
            set
            {
                right = value;
            }
        }

        public string Value
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
            }
        }

        public void Print(int _shift)
        {
            if (right != null)
            {
                right.Print(_shift + val.Length + 4);
            }

            string strShift = new string(' ', _shift);
            Console.WriteLine($"{strShift}- {val} -|");

            if (left != null)
            {
                left.Print(_shift + val.Length + 4);
            }
        }

        private Leaf left { get; set; }
        private Leaf right;
        private string val;
    }

    public class BinaryTree
    {
        public BinaryTree()
        {
            root = null;
            leafsCount = 0;
        }

        public BinaryTree(string value)
        {
            root = new Leaf(value);
            leafsCount = 1;
        }

        public bool Add(string value)
        {
            if (!Find(value))
            {
                Leaf newLeaf = new Leaf(value);

                Leaf currentLeaf = root;
                Leaf previousLeaf = null;

                while (currentLeaf != null)
                {
                    previousLeaf = currentLeaf;

                    if (currentLeaf < value)
                    {
                        currentLeaf = currentLeaf.Right;
                    }
                    else
                    {
                        currentLeaf = currentLeaf.Left;
                    }
                }

                if (previousLeaf < value)
                {
                    previousLeaf.Right = newLeaf;
                }
                else
                {
                    previousLeaf.Left = newLeaf;
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
                if (currentLeaf.Value == value)
                {
                    return true;
                }
                else if (currentLeaf < value)
                {
                    currentLeaf = currentLeaf.Right;
                }
                else
                {
                    currentLeaf = currentLeaf.Left;
                }
            }

            return false;
        }

        public bool Delete(string value)
        {
            Leaf currentLeaf = root;
            Leaf parentLeaf = root;
            if (Find(value))
            {
                while (currentLeaf.Value != value)
                {
                    parentLeaf = currentLeaf;

                    if (currentLeaf < value)
                    {
                        currentLeaf = currentLeaf.Right;
                    }
                    else
                    {
                        currentLeaf = currentLeaf.Left;
                    }
                }

                ref Leaf refParentLeaf = ref parentLeaf;
                ref Leaf refCurrentLeaf = ref currentLeaf;

                if (currentLeaf.Left != null && currentLeaf.Right == null)
                {
                    refParentLeaf.Left = refCurrentLeaf.Left;
                }
                else if (currentLeaf.Left == null && currentLeaf.Right != null)
                {
                    refParentLeaf.Right = refCurrentLeaf.Right;
                }
                else if (currentLeaf.Left != null && currentLeaf.Right != null)
                {
                    Leaf maxLeftLeaf = currentLeaf.Left;
                    Leaf parentMaxLeftLeaf = currentLeaf;
                    while (maxLeftLeaf != null)
                    {
                        parentMaxLeftLeaf = maxLeftLeaf;
                        maxLeftLeaf = maxLeftLeaf.Right;
                    }

                    if (parentMaxLeftLeaf != currentLeaf)
                    {
                        if (parentLeaf < currentLeaf.Value)
                        {
                            if (parentLeaf == currentLeaf)
                            {
                                refParentLeaf = new Leaf(parentMaxLeftLeaf.Value, currentLeaf.Left, currentLeaf.Right);
                            }
                            else
                            {
                                refParentLeaf.Right = new Leaf(parentMaxLeftLeaf.Value, currentLeaf.Left, currentLeaf.Right);
                            }
                        }
                        else
                        {
                            if (parentLeaf == currentLeaf)
                            {
                                refParentLeaf = new Leaf(parentMaxLeftLeaf.Value, currentLeaf.Left, currentLeaf.Right);
                            }
                            else
                            {
                                refParentLeaf.Left = new Leaf(parentMaxLeftLeaf.Value, currentLeaf.Left, currentLeaf.Right);
                            }
                        }
                    }
                    else
                    {
                        if (parentLeaf < currentLeaf.Value)
                        {
                            refParentLeaf.Right = new Leaf(parentMaxLeftLeaf.Value, null, currentLeaf.Right);
                        }
                        else
                        {
                            refParentLeaf.Left = new Leaf(parentMaxLeftLeaf.Value, null, currentLeaf.Right);
                        }
                    }
                }
                else
                {
                    if (parentLeaf < currentLeaf.Value)
                    {
                        refParentLeaf.Right = null;
                    }
                    else
                    {
                        refParentLeaf.Left = null;
                    }
                }
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Print()
        {
            int shift = 0;
            root.Print(shift);
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
                if (elements[hashKey] == null)
                {
                    elements[hashKey] = new BinaryTree(item);
                    return true;
                }
                else
                {
                    return elements[hashKey].Add(item);
                }
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
                elements[i] = null;
            }
        }

        public bool Delete(string item)
        {
            int hashKey = hashFunc.GetHash(item, size);
            if (elements[hashKey] == null)
                return false;
            else
                return elements[hashKey].Delete(item);
        }

        public bool Find(string item)
        {
            int hashKey = hashFunc.GetHash(item, size);
            if (elements[hashKey] == null)
                return false;
            else
                return elements[hashKey].Find(item);
        }

        public bool Find(string item, out TimeSpan timeEllapsed)
        {
            throw new NotImplementedException();
        }

        public void Print()
        {
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"{i}.");
                if (elements[i] != null)
                {
                    elements[i].Print();
                }
                Console.WriteLine();
            }
        }
    }
}