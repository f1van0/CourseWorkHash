using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CourseWorkHash
{
    //Класс листа в бинарном дереве
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

        //Функция позволяет данное поддерево представить в виде строки с добавлением отступов листьям поддерева
        public string LeafToString(int _shift)
        {
            string leaf = "";

            //Если есть правое поддерево, то вызывается эта же функцию, но уже для него
            if (right != null)
            {
                leaf += right.LeafToString(_shift + val.Length + 4);
            }

            //Выводится значение данного листа с соответствующим отступом в виде пробелом strShift
            string strShift = new string(' ', _shift);
            leaf += $"{strShift}- {val}";
            //Если у данного листа есть потомки, то следует добавить "-|" как символ того, что ветка продолжается
            if (left != null || right != null)
            {
                leaf += " -|";
            }

            leaf += "\n";

            //Если есть левое поддерево, то вызывается эта же функцию, но уже для него
            if (left != null)
            {
                leaf += left.LeafToString(_shift + val.Length + 4);
            }

            return leaf;
        }

        //левое поддерево
        private Leaf left { get; set; }
        //правое поддерево
        private Leaf right;
        //хранимое значение
        private string val;
    }

    //Класс реализующий работу бинарных деревьев
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

        //Функция позволяет добавить значение в бинарное дерево
        public bool Add(string value)
        {
            //создается лист с новым значением
            Leaf newLeaf = new Leaf(value);

            //Также определяются текущий лист и его предок
            Leaf currentLeaf = root;
            Leaf previousLeaf = null;

            //Пока текущий лист существует (т.е. пока в дереве не нашлось свободный лист)
            while (currentLeaf != null)
            {
                //предок становится равным текущему
                previousLeaf = currentLeaf;

                //а текущий заменяется левым либо правым поддеревом в зависимости от хранимого в нем значения
                if (currentLeaf < value)
                {
                    currentLeaf = currentLeaf.Right;
                }
                else
                {
                    currentLeaf = currentLeaf.Left;
                }
            }

            //В зависимости от значения предка, присваивается (в свободное место) новый лист правому либо левоу поддереву предка
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

        //Функция позволяет найти значение в бинарном дереве
        public bool Find(string value)
        {
            Leaf currentLeaf = root;

            //Пока в дереве есть очередной лист
            while (currentLeaf != null)
            {
                //Если значение было найдено, то можно возвращать значение true
                if (currentLeaf.Value == value)
                {
                    return true;
                }
                //Иначе выбирается поддерево текущего листа, в соответствии с его значением
                else if (currentLeaf < value)
                {
                    currentLeaf = currentLeaf.Right;
                }
                else
                {
                    currentLeaf = currentLeaf.Left;
                }
            }

            //Если значение так и не было найдено и цикл ушел в тупик, то происходит возвращение значения false
            return false;
        }

        //Функция позволяет найти значение в бинарном дереве и вычислить количество итераций
        public bool Find(string value, out int iter)
        {
            iter = 0;
            Leaf currentLeaf = root;

            while (currentLeaf != null)
            {
                iter++;
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

            iter = -1;
            return false;
        }

        //Функция позволяет удалить значение из бинарного дерева
        public bool Delete(string value)
        {
            Leaf deletedLeaf = root;
            Leaf parentDeletedLeaf = root;

            //Пока не нашелся удаляемый лист
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

            //Берутся значения по ссылке
            ref Leaf refParentDeletedLeaf = ref parentDeletedLeaf;
            ref Leaf refDeletedLeaf = ref deletedLeaf;

            //Если у удаляемого листа есть не пустое левое поддерево
            if (deletedLeaf.Left != null && deletedLeaf.Right == null)
            {
                //тогда у предка меняется левое поддерево на левое поддерево удаляемого листа
                if (deletedLeaf.Value == parentDeletedLeaf.Value)
                    root = refDeletedLeaf.Left;
                else
                    refParentDeletedLeaf.Left = refDeletedLeaf.Left;
            }
            //Если у удаляемого листа есть не пустое правое поддерево
            else if (deletedLeaf.Left == null && deletedLeaf.Right != null)
            {
                //тогда у предка меняется правое поддерево на правое поддерево удаляемого листа
                if (deletedLeaf.Value == parentDeletedLeaf.Value)
                    root = refDeletedLeaf.Right;
                else
                    refParentDeletedLeaf.Right = refDeletedLeaf.Right;
            }
            //Если у удаляемого листа и левое, и правое поддерево не пустые
            else if (deletedLeaf.Left != null && deletedLeaf.Right != null)
            {
                //Берется самый большой элемент (самый правый) в левом поддереве у удаляемого листа на замену
                Leaf maxLeftLeaf = deletedLeaf.Left;
                Leaf parentMaxLeftLeaf = deletedLeaf;

                //Пока есть правое поддерево у листа maxLeftLeaf
                while (maxLeftLeaf.Right != null)
                {
                    parentMaxLeftLeaf = maxLeftLeaf;
                    maxLeftLeaf = maxLeftLeaf.Right;
                }
                
                ref Leaf refParentMaxLeftLeaf = ref parentMaxLeftLeaf;

                //Если предок листа с самым большим числом в левом поддереве не совпадает с удаляемым листом
                if (parentMaxLeftLeaf != deletedLeaf)
                {
                    //Тогда у этого предка меняется правое поддеревео на левое поддерево листа maxLeftLeaf
                    refParentMaxLeftLeaf.Right = maxLeftLeaf.Left;

                    //В зависимости от значения предка и удаляемого листа выбирается левое либо правое поддерево, которое заменяется на maxLeftLeaf
                    //Но при этом остаются прежними его поддеревья 
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
                //Иначе, если предок самого  совпадает с удаляемым листом
                else
                {
                    //Происходит те же действия но теперь меняется еще и левое поддерево и заменяется на maxLeftLeaf.Left
                    refParentMaxLeftLeaf.Left = maxLeftLeaf.Left;
                    if (deletedLeaf == root)
                    {
                        root = new Leaf(maxLeftLeaf.Value, maxLeftLeaf.Left, root.Right);
                    }
                    else
                    {
                        if (refDeletedLeaf < deletedLeaf.Value)
                        {
                            refDeletedLeaf.Right = new Leaf(maxLeftLeaf.Value, maxLeftLeaf.Left, deletedLeaf.Right);
                        }
                        else
                        {
                            refDeletedLeaf.Left = new Leaf(maxLeftLeaf.Value, maxLeftLeaf.Left, deletedLeaf.Right);
                        }
                    }
                }
            }
            //Иначе, если у удаляемого листа нет поддеревьев
            else
            {
                //Если он совпал с предком (т.е. нашелся сразу же), то необходимо удалить узел
                if (parentDeletedLeaf.Value == deletedLeaf.Value)
                {
                    root = null;
                }
                //Иначе в зависимости от значения удаляемого узла у предка удаляется одно из поддеревьев
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

        //Функция позволяет представить дерево, начиная с корня root, в виде строки с добавлением отступов листьям поддерева
        public string LeafToString()
        {
            string leaf = "";
            leaf += root.LeafToString(0);
            return leaf;
        }

        //Функция позволяет получить корень дерева
        public Leaf GetRoot()
        {
            return root;
        }

        //Корень дерева
        private Leaf root;
        //Количество листьев в дереве
        private int leafsCount;
    }

    //Класс реализует хеш-таблицу с модифицированным методом цепочек, элементами которого являются бинарные деревья
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

        //Функция позволяет добавить значение в хеш-таблицу
        public bool Add(string item)
        {
            //Если значение item нет в хеш-таблице, то можно продолжить его добавление
            if (!Find(item))
            {
                long hashKey = hashFunc.GetHash(item, size);
                //Если элемент был пустым, то можно инициализировать
                //его в виде дерева с однил лишь только корнем
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

        //Функция позволяет очистить хеш-таблицу от значений
        public void Clear()
        {
            for (int i = 0; i < size; i++)
            {
                elements[i] = null;
            }
        }

        //Функция позволяет удалить значение из хеш-таблицы
        public bool Delete(string item)
        {
            //Если элемент в таблице был найден
            if (Find(item))
            {
                long hashKey = hashFunc.GetHash(item, size);
                bool result = elements[hashKey].Delete(item);

                //Если после удаления удалился корень дерева, то сам элемент станет равным null
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

        //Функция позволяет найти значение в хеш-таблице
        public bool Find(string item)
        {
            long hashKey = hashFunc.GetHash(item, size);
            if (elements[hashKey] == null)
                return false;
            else
                return elements[hashKey].Find(item);
        }

        //Функция позволяет найти значение в хеш-таблице и вычислить длительность поиска и количество итераций
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public bool Find(string item, out TimeSpan timeEllapsed, out int iter)
        {
            //Время старта и конца
            DateTime startTime, endTime;
            //Берется для старта текущее время
            startTime = DateTime.Now;
            
            long hashKey = hashFunc.GetHash(item, size);
            //Если элемента хеш-таблицы не существует
            if (elements[hashKey] == null)
            {
                timeEllapsed = TimeSpan.Zero;
                iter = -1;
                return false;
            }
            else
            {
                bool result = elements[hashKey].Find(item, out iter);
                //Берется для конца текущее время после поиска элемента
                endTime = DateTime.Now;
                //Разность времени конца и старта дает длительность поиска
                timeEllapsed = endTime - startTime;
                return result;
            }
        }

        //Функция позволяет вывести элементы хеш-таблицы на экран
        public void Print()
        {
            Console.WriteLine(TableToString());
        }

        //Функция позволяет представить элементы хеш-таблицы в виде строки
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