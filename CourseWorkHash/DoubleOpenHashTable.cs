using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    public class DoubleOpenHashTable : IHashTable
    {
        public bool Add(string item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            for (int i = 0; i < size; i++)
            {
                elements[i] = "";
                elementsState[i] = ElementStatement.empty;
            }
        }

        public bool Delete(string item)
        {
            throw new NotImplementedException();
        }

        public bool Find(string item)
        {
            throw new NotImplementedException();
        }

        public bool Find(string item, out TimeSpan timeEllapsed)
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

        public void Print()
        {
            throw new NotImplementedException();
        }

        string[] IHashTable.GetItems()
        {
            throw new NotImplementedException();
        }
    }
}