using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    public interface IHashFunc
    {
        string Name { get; }
        int GetHash(string item, int size);
    }
}