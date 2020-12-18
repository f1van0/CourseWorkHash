using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    /*
    public class ChainList
    {
        private string value;
        private ChainList* child;

        public string GetValue()
        {
            return value;
        }

        public ChainList* GetChild()
        {
            return child;
        }
    }
    */

    public class ListHashTable : IHashTable
    {
        private List<string>[] Values;
        private int size;
        private IHashFunc function;

        public ListHashTable(int capacity, IHashFunc func)
        {
            n = capacity;
            Values = new List<string>[capacity];
            function = func;
        }

        public bool Add(string item)
        {
            int index = function.GetHash(item, n);
            Values[index].Add(item);
            return true;
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int item)
        {
            throw new NotImplementedException();
        }

        public string[] GetItems()
        {
            throw new NotImplementedException();
        }

        public bool HasItem(int item)
        {
            throw new NotImplementedException();
        }

        public bool HasItem(int item, out TimeSpan timeEllapsed)
        {
            throw new NotImplementedException();
        }
    }
}