using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    public interface IHashFuct
    {
        string Name { get; }
        int GetHash(int key);
    }
}