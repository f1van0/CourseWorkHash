using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    public class MidSquareHashFunc : IHashFunc
    {
        public string Name => "Метод середины квадрата";

        public int GetHash(string item, int size)
        {
            //Метод середины квадрата
            int key = 0;
            for (int i = 0; i < item.Length; i++)
            {
                key += item[i];
            }

            //Возведение получившегося числа в квадрат
            key *= key;

            int m = size;
            int nInBytes = 0;
            while (m != 0)
            {
                nInBytes++;
                m /= 2;
            }

            int shift;
            if (nInBytes >= 32)
            {
                shift = 0;
            }
            else
            {
                shift = (32 - nInBytes) / 2;
            }


            //Извлечение nInBytes бит из середины strInBytes
            key >>= shift;
            return key % size;
        }
    }
}