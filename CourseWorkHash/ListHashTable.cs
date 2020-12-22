using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool Find(string item, out TimeSpan timeEllapsed)
        {
            throw new NotImplementedException();
        }

        public void Print()
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write($"{i}. ");
                if (elements[i] != null)
                {
                    Console.Write(elements[i].GetValue());
                    ChainElem _child = elements[i].GetChild();
                    while (_child != null)
                    {
                        Console.Write($" -> {_child.GetValue()}");
                        _child = _child.GetChild();
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}