using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    //Класс хеш-функции мультипликативным методом
    public class MultiplicativeHashFunc : IHashFunc
    {
        public string Name => "Мультипликативный метод";

        public long GetHash(string item, int size)
        {
            double key = 0;

            //символы item суммируются в ключ key
            for (int i = 0; i < item.Length; i++)
            {
                key += item[i];
            }

            //например для size = 10, получившиеся randomValue будут 1 = 0.1, 2 = 0.2, 10 = 0.1, 11 = 0.1, 12 = 0,2 ...
            //Соответственно для size = 100, randomValue будут 1 = 0,1, 99 = 0,99 и т.д.
            double randomValue = key % size;
            while(randomValue >= 1)
            {
                randomValue /= 10;
            }

            key = key * randomValue;
            //Из полученного значения берется дробная часть
            key = key - (long)(key);

            //Прозведение key (<1) на количество ячеек даст значение в диапазоне от 0 до size
            return (long)(key * size);
        }
    }
}