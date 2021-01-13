using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CourseWorkHash
{
    public class ChainElem
    {
        private string value;
        private ChainElem child;

        public ChainElem()
        {
            value = "";
            child = null;
        }

        public ChainElem(string _value)
        {
            value = _value;
            child = null;
        }

        public ChainElem(string _value, ChainElem _child)
        {
            value = _value;
            child = _child;
        }

        public string GetValue()
        {
            return value;
        }

        public ChainElem GetChild()
        {
            return child;
        }

        public bool Add(string _value)
        {
            if (!Find(_value))
            {
                if (value == "")
                {
                    value = _value;
                }
                else
                {
                    ChainElem _child = child;
                    ChainElem _parent = child;

                    while (_child != null)
                    {
                        _parent = _child;
                        _child = _child.child;
                    }

                    if (_parent == _child)
                        child = new ChainElem(_value);
                    else
                        _parent.child = new ChainElem(_value);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Find(string _value)
        {
            if (value == _value)
            {
                return true;
            }
            else
            {
                ChainElem _child = child;
                while (_child != null)
                {
                    if (_child.value == _value)
                    {
                        return true;
                    }

                    _child = _child.child;
                }

                return false;
            }
        }

        public bool Find(string _value, out int iter)
        {
            if (value == _value)
            {
                iter = 1;
                return true;
            }
            else
            {
                iter = 1;
                ChainElem _child = child;
                while (_child != null)
                {
                    iter++;
                    if (_child.value == _value)
                    {
                        return true;
                    }

                    _child = _child.child;
                }

                iter = -1;
                return false;
            }
        }

        public bool Delete(string _value)
        {
            if (Find(_value))
            {
                if (value == _value)
                {
                    value = null;
                    child = null;
                }
                else
                {
                    ChainElem _child = child;
                    ChainElem _parent = child;
                    while (_child.value != _value)
                    {
                        _parent = _child;
                        _child = _child.child;
                    }

                    if (_parent == _child)
                    {
                        child = child.child;
                    }
                    else
                    {
                        ref ChainElem _refParent = ref _parent;
                        _refParent.child = _child.child;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }

    //Класс реализует хеш-таблицу, разрешающую коллизии методом цепочек
    public class ListHashTable : IHashTable
    {
        public string Name => $"Метод цепочек. Хеш-функция - {hashFunc.Name}.";

        public string ShortName => $"Метод цепочек;{hashFunc.Name}";

        private ChainElem[] elements;
        private int size;
        private IHashFunc hashFunc;

        public ListHashTable(int _size, IHashFunc func)
        {
            size = _size;
            elements = new ChainElem[size];
            hashFunc = func;
        }

        //Функция позволяет добавить значение в хеш-таблицу
        public bool Add(string item)
        {
            long hashKey = hashFunc.GetHash(item, size);
            if (elements[hashKey] == null)
            {
                elements[hashKey] = new ChainElem(item);
                return true;
            }
            else
                return elements[hashKey].Add(item);
        }

        //Функция позволяет очистить хеш-таблицу от значений
        public void Clear()
        {
            for (int i = 0; i < size; i++)
            {
                elements[i] = new ChainElem();
            }
        }

        //Функция позволяет удалить значение из хеш-таблицы
        public bool Delete(string item)
        {
            long hashKey = hashFunc.GetHash(item, size);
            if (elements[hashKey] == null)
                return false;
            else
                return elements[hashKey].Delete(item);
        }

        //Функция позволяет найти значение в хеш-таблице
        public bool Find(string item)
        {
            long hashKey = hashFunc.GetHash(item, size);
            if (elements[hashKey] == null)
            {
                return false;
            }
            else
                return elements[hashKey].Find(item);
        }

        //Функция позволяет найти значение в хеш-таблице и вычислить длительность поиска и количество итераций
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public bool Find(string item, out TimeSpan timeEllapsed, out int iter)
        {
            DateTime startTime, endTime;
            startTime = DateTime.Now;
            
            long hashKey = hashFunc.GetHash(item, size);
            if (elements[hashKey] == null)
            {
                timeEllapsed = TimeSpan.Zero;
                iter = -1;
                return false;
            }
            else
            {
                bool result = elements[hashKey].Find(item, out iter);
                endTime = DateTime.Now;
                timeEllapsed = endTime - startTime;
                return result;
            }
        }

        //Функция позволяет вывести элементы хеш-таблицы на экран
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
                table += $"{i}. ";
                if (elements[i] != null)
                {
                    table += elements[i].GetValue();
                    ChainElem _child = elements[i].GetChild();
                    while (_child != null)
                    {
                        table += $" -> {_child.GetValue()}";
                        _child = _child.GetChild();
                    }
                }

                table += "\n";
            }

            return table;
        }
    }
}