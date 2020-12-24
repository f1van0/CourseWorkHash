using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    public interface IHashTable
    {
        string Name { get; }
        string ShortName { get; }
        bool Add(string item);
        bool Delete(string item);
        bool Find(string item);
        bool Find(string item, out TimeSpan timeEllapsed);
        void Print();
        void Clear();
        string TableToString();
    }
}