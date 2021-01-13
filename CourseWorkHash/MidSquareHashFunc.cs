using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    //Класс реализует хеш-функцию метода середины квадрата
    public class MidSquareHashFunc : IHashFunc
    {
        public string Name => "Метод середины квадрата";

        public long GetHash(string item, int size)
        {
            long key = 0;
            for (int i = 0; i < item.Length; i++)
            {
                key += item[i];
            }

            //Возведение получившегося числа в квадрат
            key *= key;

            int m = size;
            //Сколько байт занимает количество ячеек size
            int nInBytes = 0;
            while (m != 0)
            {
                nInBytes++;
                m /= 2;
            }

            //Вычисление сдвига, чтобы потом сделать побитовый сдвиг
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