using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    //Класс реализует хеш-функцию метода деления
    public class DivisionHashFunc : IHashFunc
    {
        public string Name => "Метод деления";

        public long GetHash(string item, int size)
        {
            long value = 0;

            //Полученная строка преобразуется в value сложением её символов
            for (int i = 0; i < item.Length; i++)
            {
                value += item[i];
            }

            //Берется остаток от деления получившегося значения на число ячеек в таблице 
            return value % size;
        }
    }
}