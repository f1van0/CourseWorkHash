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
        //Функция позволяет определить является ли строка числом
        static bool isNotInteger(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] < '0' || value[i] > '9')
                {
                    return true;
                }
            }

            return false;
        }

        //Функция позволяет корректно считать целое число из консоли,
        //предупреждает пользователя о некорректно введенном значении и запрашивает повторный ввод
        static int ReadInteger()
        {
            string strValue = Console.ReadLine();
            while (strValue == "" || isNotInteger(strValue))
            {
                Console.WriteLine("Введите значение корректно");
                strValue = Console.ReadLine();
            }

            //Конвертирует полученное значение в целое число
            return Convert.ToInt32(strValue);
        }

        //Функция позволяет корректно считать стрку из консоли,
        //предупреждает пользователя о некорректно введенном значении и запрашивает повторный ввод
        static string ReadString()
        {
            string strValue = Console.ReadLine();
            while (strValue == "")
            {
                Console.WriteLine("Введите значение корректно");
                strValue = Console.ReadLine();
            }

            return strValue;
        }

        //Функция основного меню непосредственного взаимодействия с хеш-функцией
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
                        //Для создания хеш-таблицы, разрешающей коллизии с помощью двойного хеширования,
                        //нужно также запросить у пользователя ввод второй хеш-функции
                        Console.WriteLine("Выберите вторую хеш-функцию для данного метода");
                        Console.WriteLine("[1]: Метод деления");
                        Console.WriteLine("[2]: Мультипликатевный метод");
                        Console.WriteLine("[3]: Метод середины квадрата");
                        int choiceSecondHashFuncMethod = ReadInteger();
                        Console.WriteLine("Введите целочисленное значение, отвечающее за смещение результата второй хеш-функции");
                        int shift = ReadInteger();
                        IHashFunc secondHashFunc;
                        //Определение метода второй хеш-функции, исходя из choiceSecondHashFuncMethod
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

                choice = ReadInteger();

                switch (choice)
                {
                    case 1:
                        {
                            Console.WriteLine("Введите количество входных значений");
                            int valuesCount = ReadInteger();
                            
                            Random random = new Random();
                            string newValue;
                            //Длина генерируемого значения. Выбриается случайно в диапазоне от 1 до 10
                            int length;

                            for (int i = 0; i <  valuesCount; i++)
                            {
                                length = random.Next(1, 10);
                                newValue = "";

                                //В цикле происходит добавления в пустую строку newValue новых символов, которые случайное генерируются
                                for (int j = 0; j < length; j++)
                                {
                                    //Здесь происходит выбор того, что будет добавлено. Число либо символ
                                    if (random.Next(0, 2) == 1)
                                        newValue += (char)random.Next(65, 90);
                                    else
                                        newValue += (char)random.Next(48, 57);
                                }

                                //Полученное значение добавляется в хеш-таблицу и выводится определенное сообщение в зависимости от успешности операции
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
                            string value = ReadString();

                            //Введенное значение добавляется в хеш-таблицу и выводится определенное сообщение в зависимости от успешности операции
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
                            string value = ReadString();

                            //Введенное значение удаляется из хеш-таблицы и выводится определенное сообщение в зависимости от успешности операции
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
                            int choice2 = ReadInteger();
                            
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
                            Console.WriteLine("Показать время поиска и количество итераций? [1]: Да [2]: Нет");
                            int choice2 = ReadInteger();

                            Console.WriteLine("Введите значение");
                            string value = ReadString();

                            //Время поиска
                            TimeSpan time;
                            //тво итераций поиска
                            int iter;
                            //Если значение нашлось, то
                            if (hashTable.Find(value, out time, out iter))
                            {
                                //Выводится успешность выполнения данной операции и если пользователь выбрал вывод 
                                //дополнительной информации, то следом выводится и она
                                Console.Write($"Значение {value} было найдено.\n");
                                if (choice2 == 1)
                                    Console.WriteLine($"- Время поиска составило {time.TotalMilliseconds} мс.\n- Произведено итераций: {iter}.");
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

                //Если пользователь выбрал пункт меню выхода в меню настройки хеш-таблицы,
                //то, для того, чтобы та функция продолжила работу, возвращается значение false
                if (choice == 7)
                {
                    return false;
                }
                //Иначе если пользователь решил выйти из программы возвращается значение true
                else if (choice == 0)
                {
                    return true;
                }

                Console.WriteLine("Для продолжения работы нажмите enter");
                Console.ReadKey();
            }
        }

        //Функция представляет собой меню, в котором пользователь может выбрать определенный способ разрешения коллизии и метод хеширования
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

                int choiceCollisionMethod = ReadInteger();

                Console.WriteLine("");
                Console.WriteLine("Выберите метод хеширования");
                Console.WriteLine("[1]: Метод деления");
                Console.WriteLine("[2]: Мультипликативный метод");
                Console.WriteLine("[3]: Метод середины квадрата");

                int choiceHashFunc = ReadInteger();

                //Если после работы функции MainFirstModeMenu вернулось значение true (т.е. пользователь выбрал пункт выхода из программы),
                bool exit = MainFirstModeMenu(length, choiceCollisionMethod, choiceHashFunc);

                //то прекращается работа с программой
                if (exit)
                {
                    break;
                }
            }
           
        }

        //Функция, реализующая работу второго режима (режима сбора данных)
        static void SecondModeMenu(int size)
        {
            IHashTable[] hashTables = new IHashTable[15];
            int hashTableNumber = 0;
            IHashFunc hashFunc;

            Console.Clear();

            //Пользователь, попадая во второй режим, сначала вводит количество генерируемых значений
            Console.WriteLine("Введите количество входящих значений");
            int inputValuesCount = ReadInteger();

            //Если введенное количество меньше нуля либо является слишком большим, то программа запросит у пользователя повторный ввод
            if (inputValuesCount <= 0 || inputValuesCount > 10000000)
            {
                Console.WriteLine("Введено недопустимое количество входящих значений. Попробуйте ввести его снова.\nДля продолжения работы нажмите enter");
                Console.ReadKey();
                SecondModeMenu(size);
            }
            else
            {
                Console.WriteLine("Введите название файла в который будет выводится результат");
                string fileName = ReadString();

                //Массив сгенерированныых значений
                string[] values = new string[inputValuesCount];
                
                Random random = new Random();
                //Случайная длина генерируемого значения (от 1 до 15)
                int randomLength;

                //Цикл в котором генерируется inputValuesCount значений и помещается в массив values
                for (int t = 0; t < inputValuesCount; t++)
                {
                    randomLength = random.Next(1, 15);
                    for (int k = 0; k < randomLength; k++)
                    {
                        if (random.Next(0, 2) == 1)
                            values[t] += (char)random.Next(65, 90);
                        else
                            values[t] += (char)random.Next(48, 57);
                    }
                }

                //В цикле производится перебор всех методов хеширования и разрещения коллизий, создаются хеш-таблицы
                for (int i = 1; i <= 4; i++)
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
                            default:
                                {
                                    hashTables[hashTableNumber] = new QuadraticOpenHashTable(size, hashFunc);
                                    break;
                                }
                        }

                        hashTableNumber++;
                    }
                }

                //Отдельно создается еще 3 хеш-таблицы, разрешающих коллизии двойным хешированием. (1. ; 2. Мультипликативный метод с )
                //                                             1 хеш-функция: Метод деления 2 хеш-функция: Мультипликативным метод
                hashTables[12] = new DoubleOpenHashTable(size, new DivisionHashFunc(), new MultiplicativeHashFunc(), random.Next(0, size));
                //                                             1 хеш-функция: Мультипликативный метод 2 хеш-функция: Метод середины квадрата
                hashTables[13] = new DoubleOpenHashTable(size, new MultiplicativeHashFunc(), new MidSquareHashFunc(), random.Next(0, size));
                //                                             1 хеш-функция: Метод середины квадрата 2 хеш-функция: Метод деления
                hashTables[14] = new DoubleOpenHashTable(size, new MidSquareHashFunc(), new DivisionHashFunc(), random.Next(0, size));

                //Начинается этап заполнения 15 хеш-функций случайными значениями из массива values
                for (int n = 0; n < 15; n++)
                {
                    Console.WriteLine("-----------------------------------------------------------------------------");
                    Console.WriteLine($"Начало работы с хеш-таблицей - \"{hashTables[n].Name}\"");

                    //Количество незаписанных значений
                    int failedAddCount = 0;

                    for (int io = 0; io < inputValuesCount; io++)
                    {
                        //Если значение values[io] не удалось добавить в хеш-таблицу hashTables[n], то происходит увеличение failedAddCount на 1
                        if (!hashTables[n].Add(values[io]))
                        {
                            failedAddCount++;
                        }
                    }

                    //Если после добавления всех значений были незаписанные значения, то происходит вывод их количества
                    if (failedAddCount != 0)
                    {
                        Console.WriteLine($"Не удалось записать в таблицу {failedAddCount} значений");
                    }
                    else
                    {
                        Console.WriteLine("Все значения были успешно добавлены в таблицу");
                    }
                }

                //Количество поисков различных случайных значений из массива values
                int findCount = 1000;

                //Если таблица состояла из менее чем 1000 ячеек
                if (size < 1000)
                {
                    if (inputValuesCount < size)
                    {
                        findCount = inputValuesCount;
                    }
                    else
                    {
                        findCount = size;
                    }
                }
                        
                //Случайный индекс значения в массиве
                int randomIndex = random.Next(0, inputValuesCount);
                //Очередное время поиска
                TimeSpan tempTime;
                //Очередное количества итераций поиска
                int tempIter;

                //Максимальное время поиска у каждой хеш-таблицы
                double[] maxTimeArray = new double[15];
                //Среднее время поиска у каждой хеш-таблицы
                double[] avgTimeArray = new double[15];
                //Максимальное количество итераций поиска у каждой хеш-таблицы
                int[] maxIterArray = new int[15];
                //Среднеарифметическое количество итераций поиска у каждой хеш-таблицы
                double[] avgIterArray = new double[15];

                //Начинается этап, в котором findCount раз производится поиск случайного значения из массива values с индексом randomIndex
                for (int k = 0; k < findCount; k++)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        randomIndex = random.Next(0, inputValuesCount);

                        //Пока не будет успешно найдено значение values[randomIndex] в хеш-таблице hashTables[i], 
                        //продолжать подбирать случайное значение randomIndex
                        while (!hashTables[i].Find(values[randomIndex]))
                        {
                            randomIndex = random.Next(0, inputValuesCount);
                        }

                        //Теперь производится поиск значения values[randomIndex], но с подсчетом времени и количества итераций
                        hashTables[i].Find(values[randomIndex], out tempTime, out tempIter);
                        //Полученное время и количество итераций записывается в среднеарифметические показатели и, если полученные значения больше максимальных
                        //, то и в максимальные значения
                        avgTimeArray[i] += tempTime.Ticks;
                        avgIterArray[i] += tempIter;
                        if (tempTime.Ticks > maxTimeArray[i])
                        {
                            maxTimeArray[i] = tempTime.Ticks;
                        }
                        if (tempIter > maxIterArray[i])
                        {
                            maxIterArray[i] = tempIter;
                        }
                    }
                }

                //Далее полученные значения выводятся на экран и в .csv файл с помощью StreamWriter
                using (StreamWriter writer = new StreamWriter(fileName + ".csv", false, Encoding.UTF8))
                {
                    for (int i = 0; i < 15; i++)
                    {
                        Console.WriteLine($"\n{hashTables[i].Name} \nПоиск в среднем составил {(avgTimeArray[i] / findCount)} тиков. \nНаибольшее время поиска: {maxTimeArray[i]} тиков.\nВ среднем производилось итераций: {avgIterArray[i] / findCount}\nМаксимальное количество итераций составило {maxIterArray[i]}");
                        writer.WriteLine($"{hashTables[i].ShortName};{avgIterArray[i] / findCount};");
                    }
                }
                Console.WriteLine("\n\n");
                Console.WriteLine("Для продолжения работы нажмите enter");
                Console.ReadKey();

                StartMenu();
            }
        }

        //Функция стартового меню предоставляет пользователю возможность выбрать режим работы с програмой и ввести количество ячеек в хеш-таблице
        static void StartMenu()
        {
            Console.Clear();

            Console.WriteLine("[Меню выбора режима работы с программой]:");
            Console.WriteLine("[1]: Режим прямого взаимодействия с хеш-таблицей");
            Console.WriteLine("[2]: Режим сбора данных");

            int choice = ReadInteger();

            Console.WriteLine("");
            Console.WriteLine("Введите количество ячеек в хеш-таблице");

            int size = ReadInteger();
            //Если введенное количество ячеек является отрицательным число либо слишком большим, то запросить повторный ввод
            if (size <= 0 || size > 10000000)
            {
                Console.WriteLine("Введено недопустимое количество ячеек в хеш-таблице. Попробуйте ввести его снова.\nДля продолжения работы нажмите enter");
                Console.ReadKey();
                StartMenu();
            }
            else
            {
                switch (choice)
                {
                    case 1:
                        {
                            SettingsFirstModeMenu(size);
                            break;
                        }
                    default:
                        {
                            SecondModeMenu(size);
                            break;
                        }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.Title = "Hashometer";

            StartMenu();
        }
    }
}
