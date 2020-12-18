using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    public class DivisionHashFunc : IHashFunc
    {
        public string Name => "Метод деления";

        public int GetHash(string item, int size)
        {
            int value = 0;

            for (int i = 0; i < item.Length; i++)
            {
                value += item[i];
            }

            return value % size;
        }
    }
}