using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public string LeafToString(int _shift)
        {
            string leaf = "";

            if (right != null)
            {
                leaf += right.LeafToString(_shift + val.Length + 4);
            }

            string strShift = new string(' ', _shift);
            leaf += $"{strShift}- {val}";
            if (left != null || right != null)
            {
                leaf += " -|";
            }

            leaf += "\n";

            if (left != null)
            {
                leaf += left.LeafToString(_shift + val.Length + 4);
            }

            return leaf;
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
            Leaf deletedLeaf = root;
            Leaf parentDeletedLeaf = root;
            while (deletedLeaf.Value != value)
            {
                parentDeletedLeaf = deletedLeaf;

                if (deletedLeaf < value)
                {
                    deletedLeaf = deletedLeaf.Right;
                }
                else
                {
                    deletedLeaf = deletedLeaf.Left;
                }
            }

            ref Leaf refParentDeletedLeaf = ref parentDeletedLeaf;
            ref Leaf refDeletedLeaf = ref deletedLeaf;

            if (deletedLeaf.Left != null && deletedLeaf.Right == null)
            {
                if (deletedLeaf.Value == parentDeletedLeaf.Value)
                    root = refDeletedLeaf.Left;
                else
                    refParentDeletedLeaf.Left = refDeletedLeaf.Left;
            }
            else if (deletedLeaf.Left == null && deletedLeaf.Right != null)
            {
                if (deletedLeaf.Value == parentDeletedLeaf.Value)
                    root = refDeletedLeaf.Right;
                else
                    refParentDeletedLeaf.Right = refDeletedLeaf.Right;
            }
            else if (deletedLeaf.Left != null && deletedLeaf.Right != null)
            {
                Leaf maxLeftLeaf = deletedLeaf.Left;
                Leaf parentMaxLeftLeaf = deletedLeaf;
                while (true)
                {
                    parentMaxLeftLeaf = maxLeftLeaf;

                    if (maxLeftLeaf.Right == null)
                        break;

                    maxLeftLeaf = maxLeftLeaf.Right;
                }

                parentMaxLeftLeaf.Left = maxLeftLeaf.Left;

                if (parentMaxLeftLeaf != deletedLeaf)
                {
                    if (parentDeletedLeaf < deletedLeaf.Value)
                    {
                        if (parentDeletedLeaf == deletedLeaf)
                        {
                            root = new Leaf(maxLeftLeaf.Value, deletedLeaf.Left, deletedLeaf.Right);
                        }
                        else
                        {
                            refParentDeletedLeaf.Right = new Leaf(maxLeftLeaf.Value, deletedLeaf.Left, deletedLeaf.Right);
                        }
                    }
                    else
                    {
                        if (parentDeletedLeaf == deletedLeaf)
                        {
                            root = new Leaf(maxLeftLeaf.Value, deletedLeaf.Left, deletedLeaf.Right);
                        }
                        else
                        {
                            refParentDeletedLeaf.Left = new Leaf(maxLeftLeaf.Value, deletedLeaf.Left, deletedLeaf.Right);
                        }
                    }
                }
                else
                {

                }
            }
            else
            {
                if (parentDeletedLeaf.Value == deletedLeaf.Value)
                {
                    root = null;
                }
                else
                {
                    if (parentDeletedLeaf < deletedLeaf.Value)
                        refParentDeletedLeaf.Right = null;
                    else
                        refParentDeletedLeaf.Left = null;
                }
            }

            return true;
        }

        public string LeafToString()
        {
            string leaf = "";
            int shift = 0;
            leaf += root.LeafToString(0);
            return leaf;
        }

        public Leaf GetRoot()
        {
            return root;
        }

        private Leaf root;
        private int leafsCount;
    }

    public class TreeHashTable : IHashTable
    {
        public string Name => $"Бинарные деревья. Хеш-функция - {hashFunc.Name}.";

        public string ShortName => $"Бинарные деревья;{hashFunc.Name}";

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
            if (Find(item))
            {
                int hashKey = hashFunc.GetHash(item, size);
                bool result = elements[hashKey].Delete(item);

                if (elements[hashKey].GetRoot() == null)
                {
                    elements[hashKey] = null;
                }

                return result;
            }
            else
            {
                return false;
            }
        }

        public bool Find(string item)
        {
            int hashKey = hashFunc.GetHash(item, size);
            if (elements[hashKey] == null)
                return false;
            else
                return elements[hashKey].Find(item);
        }

        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public bool Find(string item, out TimeSpan timeEllapsed)
        {
            DateTime startTime, endTime;
            startTime = DateTime.Now;
            int hashKey = hashFunc.GetHash(item, size);
            if (elements[hashKey] == null)
            {
                timeEllapsed = TimeSpan.Zero;
                return false;
            }
            else
            {
                bool result = elements[hashKey].Find(item);
                endTime = DateTime.Now;
                timeEllapsed = endTime - startTime;
                return result;
            }
        }

        public void Print()
        {
            Console.WriteLine(TableToString());
        }

        //Функция позволяет преобразовать все записи хеш-таблицы в строку
        public string TableToString()
        {
            string table = "";

            for (int i = 0; i < size; i++)
            {
                table += $"{i}.\n";
                if (elements[i] != null)
                {
                    table += elements[i].LeafToString();
                }
                table += "\n";
            }

            return table;
        }
    }
}