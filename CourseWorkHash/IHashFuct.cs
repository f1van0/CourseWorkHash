using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    //Интерфейс хеш-функции
    public interface IHashFunc
    {
        //Название хеш-функции
        string Name { get; }
        //Функция позволяет преобразовать входное значение item в хеш-ключ
        long GetHash(string item, int size);
    }
}