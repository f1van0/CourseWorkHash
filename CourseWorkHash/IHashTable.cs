using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    //Интерфейс хеш-таблицы со всеми основными операциями над ней
    public interface IHashTable
    {
        //Полное название хеш-таблицы с методом хеширования и способом разрешения коллизии
        string Name { get; }
        //Короткое название хеш-таблицы
        string ShortName { get; }
        //Функция позволяет добавить значение в хеш-таблицу
        bool Add(string item);
        //Функция позволяет удалить значение из хеш-таблицы
        bool Delete(string item);
        //Функция позволяет найти значение в хеш-таблице
        bool Find(string item);
        //Функция позволяет найти значение в хеш-таблице и вычислить длительность поиска и количество итераций
        bool Find(string item, out TimeSpan timeEllapsed, out int iter);
        //Функция позволяет вывести элементы хеш-таблицы на экран
        void Print();
        //Функция позволяет очистить хеш-таблицу от значений
        void Clear();
        //Функция позволяет представить элементы хеш-таблицы в виде строки
        string TableToString();
    }
}