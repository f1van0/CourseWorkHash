using System;
using System.Collections.Generic;
using System.IO;
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
                            Console.WriteLine("Введите количество входных значений");
                            int valuesCount = Convert.ToInt32(Console.ReadLine());
                            Random random = new Random();
                            string newValue;
                            int length;

                            for (int i = 0; i <  valuesCount; i++)
                            {
                                length = random.Next(1, 10);
                                newValue = "";

                                for (int j = 0; j < length; j++)
                                {
                                    if (random.Next(0, 2) == 1)
                                        newValue += (char)random.Next(65, 90);
                                    else
                                        newValue += (char)random.Next(48, 57);
                                }

                                if (hashTable.Add(newValue))
                                {
                                    Console.WriteLine($"Значение {newValue} было успешно добавлено в хеш-таблицу");
                                }
                                else
                                {
                                    Console.WriteLine($"Значение {newValue} не удалось добавить в хеш-таблицу. Возможно такое значение уже существует или в таблице не нашлось свободной ячейки");
                                }
                            }

                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Введите значение");
                            string value = Console.ReadLine();
                            if (hashTable.Add(value))
                            {
                                Console.WriteLine($"Значение {value} было успешно добавлено в хеш-таблицу");
                            }
                            else
                            {
                                Console.WriteLine($"Значение {value} не удалось добавить в хеш-таблицу. Возможно такое значение уже существует или в таблице не нашлось свободной ячейки");
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Введите значение");
                            string value = Console.ReadLine();
                            if (hashTable.Delete(value))
                            {
                                Console.WriteLine($"Значение {value} было успешно удалено из хеш-таблицы");
                            }
                            else
                            {
                                Console.WriteLine($"Значение {value} не удалось удалить из хеш-таблицы");
                            }
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Вы точно хотите удалить все записи из хеш-таблицы? [1]: Да [2]: Нет");
                            int choice2 = Convert.ToInt32(Console.ReadLine());
                            if (choice2 == 1)
                            {
                                hashTable.Clear();
                                Console.WriteLine("Хеш-таблица была успешно очищена.");
                            }
                            break;
                        }
                    case 5:
                        {
                            hashTable.Print();
                            break;
                        }
                    case 6:
                        {
                            Console.WriteLine("Показать время поиска? [1]: Да [2]: Нет");
                            int choice2 = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Введите значение");
                            string value = Console.ReadLine();
                            TimeSpan time;
                            if (hashTable.Find(value, out time))
                            {
                                Console.Write($"Значение {value} было найдено. ");
                                Console.WriteLine($"Время поиска составило {time.TotalMilliseconds} мс.");
                            }
                            else
                            {
                                Console.WriteLine($"Значение {value} не удалось найти в хеш-таблице.");
                            }
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

                Console.WriteLine("Для продолжения работы нажмите enter");
                Console.ReadKey();
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

        static void SecondModeMenu1(int size)
        {
            IHashTable[] hashTables = new IHashTable[15];
            int hashTableNumber = 0;
            IHashFunc hashFunc;

            Console.Clear();

            //Пользователь, попадав во второй режим сбора данных, сначала вводит 
            Console.WriteLine("Введите количество входящих значений");
            int length = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите название файла в который будет выводится результат");
            string fileName = Console.ReadLine();

            //Массив сгенерированныых значений
            string[] values = new string[length];

            Random random = new Random();
            int randomLength;

            for (int t = 0; t < length; t++)
            {
                randomLength = random.Next(1, 10);
                for (int k = 0; k < randomLength; k++)
                {
                    if (random.Next(0, 2) == 1)
                        values[t] += (char)random.Next(65, 90);
                    else
                        values[t] += (char)random.Next(48, 57);
                }
            }
            //Производится перебор всех методов хеширования и разрещения коллизий, создаются хеш-таблицы с соответствующими методами
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    switch (j)
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

                    switch (i)
                    {
                        case (int)CollisionMethod.Chains:
                            {
                                hashTables[hashTableNumber] = new ListHashTable(size, hashFunc);
                                break;
                            }
                        case (int)CollisionMethod.BinaryTree:
                            {
                                hashTables[hashTableNumber] = new TreeHashTable(size, hashFunc);
                                break;
                            }
                        case (int)CollisionMethod.LinearProbing:
                            {
                                hashTables[hashTableNumber] = new LinearOpenHashTable(size, hashFunc);
                                break;
                            }
                        case (int)CollisionMethod.QuadraticProbing:
                            {
                                hashTables[hashTableNumber] = new QuadraticOpenHashTable(size, hashFunc);
                                break;
                            }
                        default:
                            {
                                if (j == 1)
                                {
                                    hashTables[hashTableNumber] = new DoubleOpenHashTable(size, hashFunc, new MultiplicativeHashFunc(), random.Next(0, size));
                                }
                                else if (j == 2)
                                {
                                    hashTables[hashTableNumber] = new DoubleOpenHashTable(size, hashFunc, new MidSquareHashFunc(), random.Next(0, size));
                                }
                                else
                                {
                                    hashTables[hashTableNumber] = new DoubleOpenHashTable(size, hashFunc, new DivisionHashFunc(), random.Next(0, size));
                                }

                                break;
                            }
                    }

                    if (!(i >= 5 && Math.Abs(i - j) % 3 == 2) && hashTableNumber < 15)
                    {
                        Console.WriteLine("-----------------------------------------------------------------------------");
                        Console.WriteLine($"Начало работы с хеш-таблицей - \"{hashTables[hashTableNumber].Name}\"");

                        int failedAddCount = 0;
                    
                        for (int io = 0; io < length; io++)
                        {
                            if (!hashTables[hashTableNumber].Add(values[io]))
                            {
                                failedAddCount++;
                            }
                        }

                        if (failedAddCount != 0)
                        {
                            Console.WriteLine($"Не удалось записать в таблицу {failedAddCount} значений");
                        }
                        else
                        {
                            Console.WriteLine("Все значения были успешно добавлены в таблицу");
                        }
                        hashTableNumber++;
                    }
                }
            }

            int findCount = 100;
            if (size < 100)
            {
                findCount = size;
            }

            bool notFound;
            int randomIndex = 0;
            TimeSpan tempTime;
            float[] maxTimeArray = new float[15];
            float[] avgTimeArray = new float[15];

            for (int k = 0; k < findCount; k++)
            {
                while (true)
                {
                    notFound = false;
                    randomIndex = random.Next(0, length);
                    for (int i = 0; i < 15; i++)
                    {
                        if (!hashTables[i].Find(values[randomIndex]))
                        {
                            notFound = true;
                            break;
                        }
                    }

                    if (!notFound)
                    {
                        break;
                    }
                }

                for (int i = 0; i < 15; i++)
                {
                    hashTables[i].Find(values[randomIndex], out tempTime);
                    avgTimeArray[i] += tempTime.Ticks / (float)size;
                    if (tempTime.Ticks > maxTimeArray[i])
                    {
                        maxTimeArray[i] = tempTime.Ticks;
                    }
                }
            }

            //Нахождение элемента, который есть во всех полученных хеш-таблицах
            //while (true)
            //{
            //    notFound = false;

            //    randomIndex = random.Next(0, length);
            //    for (int i = 0; i < 21; i++)
            //    {
            //        if (!hashTables[i].Find(values[randomIndex]))
            //        {
            //            notFound = true;
            //            break;
            //        }
            //    }

            //    if (!notFound)
            //    {
            //        break;
            //    }
            //}

            Console.WriteLine($"Поиск общего элемента со значением - {values[randomIndex]}");

            using (StreamWriter writer = new StreamWriter(fileName + ".csv", false, Encoding.UTF8))
            {
                TimeSpan time;
                for (int i = 0; i < 15; i++)
                {
                    //hashTables[i].Find(values[randomIndex], out time);
                    Console.WriteLine($"\n В хеш-таблице {hashTables[i].Name} \n оиск составил {avgTimeArray[i]} такт");
                    Console.WriteLine($"{hashTables[i].Name}\nВремя поиска: {maxTimeArray[i]} такт.");
                    //writer.WriteLine("=======================================================================");
                    writer.WriteLine($"{hashTables[i].ShortName};{avgTimeArray[i]};");
                
                    //writer.WriteLine($"Время поиска составило {time.TotalMilliseconds} мс.\n");
                }
            }
            Console.WriteLine("\n\n");
            Console.WriteLine("Для продолжения работы нажмите enter");
            Console.ReadKey();

            Menu0();
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

            //MainFirstModeMenu(2, (int)CollisionMethod.BinaryTree, (int)HashFuncMethod.division);
            Menu0();
        }
    }
}
