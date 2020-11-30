using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    public interface IHashTable
    {
        bool Add(int item);
        bool Delete(int item);
        bool HasItem(int item);
        bool HasItem(int item, out TimeSpan timeEllapsed);
        int[] GetItems();
        void Clear();

    }
}