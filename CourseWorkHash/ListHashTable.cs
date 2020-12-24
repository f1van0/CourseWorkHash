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

    public class ListHashTable : IHashTable
    {
        public string Name => $"Хеш-таблица. Способ разрешения коллизий - метод цепочек. Хеш-функция - {hashFunc.Name}.";

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

        public bool Add(string item)
        {
            int hashKey = hashFunc.GetHash(item, size);
            if (elements[hashKey] == null)
            {
                elements[hashKey] = new ChainElem(item);
                return true;
            }
            else
                return elements[hashKey].Add(item);
        }

        public void Clear()
        {
            for (int i = 0; i < size; i++)
            {
                elements[i] = new ChainElem();
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