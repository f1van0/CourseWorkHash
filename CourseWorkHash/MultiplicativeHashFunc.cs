using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    public class MultiplicativeHashFunc : IHashFunc
    {
        public string Name => "Мультипликативный метод";

        public int GetHash(string item, int size)
        {
            float key = 0;

            for (int i = 0; i < item.Length; i++)
            {
                key += item[i];
            }

            //1 = 0.1, 2 = 0.2, 10 = 0.1, 15 = 0.15...
            float randomValue = key % size;
            while(randomValue > 0)
            {
                randomValue /= 10;
            }

            key = key * randomValue;
            key = key - (int)(key);

            return (int)key * size;
        }
    }
}