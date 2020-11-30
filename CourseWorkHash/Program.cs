using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkHash
{
    class Program
    {
        static void FirstModeMenu3()
        {
            Console.Clear();

            Console.WriteLine("[Меню проведения работы с хеш-таблицей]:");
            //Генерируется несколько значений, сколько указал пользователь, в диапазоне, который указал пользователь и они все добавляются в таблицу. На экран выводится какой элемент куда записался и успешно или нет.
            Console.WriteLine("[1]: Генерация значений для хеш-таблицы");
            //Добавляется элемент, который ввел пользователь. На экран выводится лишь результат. Типа число "6" успешно добавилось в хеш-таблицу, в ячейку №3
            Console.WriteLine("[1]: Добавление нового элемента в хеш-таблицу");
            //Пользователь должен ввести номер ячейки в хеш-таблице, а потом ввести новое значение данного элемента. Выводится информация о том, в какую ячейку было перенесено значение
            Console.WriteLine("[2]: Изменение элемента в хеш-таблице");
            //Пользователь вводит номер ячейки в котором записан элемент, элемент удаляется. На экран вывести успешно удалился элемент или нет
            Console.WriteLine("[3]: Удаление элемента из хеш-таблицы");
            //Просто очищается таблица.
            Console.WriteLine("[4]: Очистка хеш-таблицы");
            //Выводится на экран и возможно в файл. Через функцию GetItems()
            Console.WriteLine("[5]: Вывод элементов хеш-таблицы");
            //Пользователь выбирает найти все элементы по их значению или по их индексу в хеш-таблице. В результате программа выводит либо то сколько раз она нашла это значение либо просто, что элемента с таким значением не сущесивует
            Console.WriteLine("[6]: Поиск элемента в хеш-таблице");
            //Можно выйти и выбрать другой способ разрешения хеширования и метод хеширования (при этом очистится хеш-таблица и создастся новая)
            Console.WriteLine("[7]: Выход в меню выбора способа разрешения коллизий и метода хеширования для хеш-таблицы");
            //Выход из программы
            Console.WriteLine("[0]: Выход из программы");

            int choice = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            Console.WriteLine("[Меню]: ...");

            int choice1 = Convert.ToInt32(Console.ReadLine());
        }

        static void FirstModeMenu1(int length)
        {
            Console.Clear();

            Console.WriteLine("[Меню выбора способа разрешения коллизий, а также метода хеширования для хеш-таблицы]:");
            Console.WriteLine("");
            Console.WriteLine("Выберите способ разрешения коллзий");
            Console.WriteLine("[1]: Метод цепочек");
            Console.WriteLine("[2]: Бинарные деревья");
            Console.WriteLine("[3]: Открытая адресация");

            int choiceCollisionMethod = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("");
            Console.WriteLine("Выберите метод хеширования");
            Console.WriteLine("[1]: Метод деления");
            Console.WriteLine("[2]: Мультипликативный метод");
            Console.WriteLine("[3]: Аддитивный метод");

            int choiceHashFunc = Convert.ToInt32(Console.ReadLine());

            FirstModeMenu2(length, choiceCollisionMethod, choiceHashFunc);
        }

        static void FirstModeMenu2(int length, int choiceCollisionMethod, int choiceHashFunc)
        {
            Console.Clear();

            int choice = Convert.ToInt32(Console.ReadLine());
            choice.GetHashCode();

            FirstModeMenu3();
        }

        static void SecondModeMenu1(int length)
        {
            Console.Clear();

            //Пользователь, попадав во второй режим сбора данных, сначала вводит 
            Console.WriteLine("Введите диапазон и шаг изменения количества входных значений");
            int minLength = Convert.ToInt32(Console.ReadLine());
            int maxLength = Convert.ToInt32(Console.ReadLine());
            int step = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("");
            Console.WriteLine("Введите диапазон значений");
            int minValue = Convert.ToInt32(Console.ReadLine());
            int maxValue = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("");
            int choice = Convert.ToInt32(Console.ReadLine());
        }

        static void Menu0()
        {
            Console.Clear();

            Console.WriteLine("[Меню выбора режима работы с программой]:");
            Console.WriteLine("[1]: Режим прямого взаимодействия с хеш-таблицей");
            Console.WriteLine("[2]: Режим сбора данных");

            int choice = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("");
            Console.WriteLine("Введите количество ячеек в хеш-таблице");

            int length = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    {
                        FirstModeMenu1(length);
                        break;
                    }
                default:
                    {
                        SecondModeMenu1(length);
                        break;
                    }  
            }
        }

        static void Main(string[] args)
        {
            Console.Title = "Hashometer";

            FirstModeMenu3();
        }
    }
}
