using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    public class ListHashTable : IHashTable
    {
        private List<int>[] Values;
        private IHashFuct function;

        public ListHashTable(int capacity, IHashFuct func)
        {
            Values = new List<int>[capacity];
            function = func;
        }

        public bool Add(int item)
        {
            int index = function.GetHash(item);
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

        public int[] GetItems()
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