using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkHash
{
    class Program
    {
        static bool MainFirstModeMenu(int size, int choiceCollisionMethod, int choiceHashFunc)
        {
            int choice;
            //Объявление хеш-таблицы
            IHashTable hashTable;
            //Объявление хеш-функции
            IHashFunc hashFunc;
            //Определение типа хеш-таблиц и её хеш-функции исходя из choiceCollisionMethod и choiceHashFunc
            switch (choiceHashFunc)
            {
                case (int)HashFuncMethod.division:
                    {
                        hashFunc = new DivisionHashFunc();
                        break;
                    }
                case (int)HashFuncMethod.multiplicative:
                    {
                        hashFunc = new MultiplicativeHashFunc();
                        break;
                    }
                default:
                    {
                        hashFunc = new MidSquareHashFunc();
                        break;
                    }
            }

            switch (choiceCollisionMethod)
            {
                case (int)CollisionMethod.Chains:
                    {
                        hashTable = new ListHashTable(size, hashFunc);
                        break;
                    }
                case (int)CollisionMethod.BinaryTree:
                    {
                        hashTable = new TreeHashTable(size, hashFunc);
                        break;
                    }
                case (int)CollisionMethod.LinearProbing:
                    {
                        hashTable = new LinearOpenHashTable(size, hashFunc);
                        break;
                    }
                case (int)CollisionMethod.QuadraticProbing:
                    {
                        hashTable = new QuadraticOpenHashTable(size, hashFunc);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Выберите вторую хеш-функцию для данного метода");
                        Console.WriteLine("[1]: Метод деления");
                        Console.WriteLine("[2]: Мультипликатевный метод");
                        Console.WriteLine("[3]: Метод середины квадрата");
                        int choiceSecondHashFuncMethod = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите целочисленное значение, отвечающее за смещение результата второй хеш-функции");
                        int shift = Convert.ToInt32(Console.ReadLine());
                        IHashFunc secondHashFunc;
                        switch (choiceSecondHashFuncMethod)
                        {
                            case (int)HashFuncMethod.division:
                                {
                                    secondHashFunc = new DivisionHashFunc();
                                    break;
                                }
                            case (int)HashFuncMethod.multiplicative:
                                {
                                    secondHashFunc = new MultiplicativeHashFunc();
                                    break;
                                }
                            default:
                                {
                                    secondHashFunc = new MidSquareHashFunc();
                                    break;
                                }
                        }

                        hashTable = new DoubleOpenHashTable(size, hashFunc, secondHashFunc, shift);
                        break;
                    }
            }
          
            while (true)
            {
                Console.Clear();

                Console.WriteLine("[Меню проведения работы с хеш-таблицей]:");
                //Генерируется несколько значений, сколько указал пользователь, в диапазоне, который указал пользователь и они все добавляются в таблицу. На экран выводится какой элемент куда записался и успешно или нет.
                Console.WriteLine("[1]: Генерация значений для хеш-таблицы");
                //Добавляется элемент, который ввел пользователь. На экран выводится лишь результат. Типа число "6" успешно добавилось в хеш-таблицу, в ячейку №3
                Console.WriteLine("[2]: Добавление нового элемента в хеш-таблицу");
                //Пользователь должен ввести номер ячейки в хеш-таблице, а потом ввести новое значение данного элемента. Выводится информация о том, в какую ячейку было перенесено значение
                //Console.WriteLine("[3]: Изменение элемента в хеш-таблице");
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

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        {
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                if (choice == 7)
                {
                    return false;
                }
                else if (choice == 0)
                {
                    return true;
                }
            }
        }

        static void SettingsFirstModeMenu(int length)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("[Меню выбора способа разрешения коллизий, а также метода хеширования для хеш-таблицы]:");
                Console.WriteLine("");
                Console.WriteLine("Выберите способ разрешения коллзий");
                Console.WriteLine("[1]: Метод цепочек");
                Console.WriteLine("[2]: Бинарные деревья");
                Console.WriteLine("[3]: Открытая адресация (линейные пробы)");
                Console.WriteLine("[4]: Открытая адресация (квадратиченые пробы)");
                Console.WriteLine("[5]: Открытая адресация (двойное хеширование)");

                int choiceCollisionMethod = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("");
                Console.WriteLine("Выберите метод хеширования");
                Console.WriteLine("[1]: Метод деления");
                Console.WriteLine("[2]: Мультипликативный метод");
                Console.WriteLine("[3]: Метод середины квадрата");

                int choiceHashFunc = Convert.ToInt32(Console.ReadLine());

                bool exit = MainFirstModeMenu(length, choiceCollisionMethod, choiceHashFunc);

                if (exit)
                {
                    break;
                }
            }
           
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

            int size = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    {
                        SettingsFirstModeMenu(size);
                        break;
                    }
                default:
                    {
                        SecondModeMenu1(size);
                        break;
                    }  
            }
        }

        static void Main(string[] args)
        {
            Console.Title = "Hashometer";

            Menu0();
        }
    }
}
