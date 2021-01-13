﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CourseWorkHash
{
    //Класс реализует хеш-таблицу, разрешающую коллизии квадратичными пробами
    public class QuadraticOpenHashTable : IHashTable
    {
        public string Name => $"Квадратичные пробы. Хеш-функция - {hashFunc.Name}.";

        public string ShortName => $"Квадратичные пробы;{hashFunc.Name}";

        private string[] elements;
        private IHashFunc hashFunc;
        private ElementStatement[] elementsState;
        //число элементов в таблице
        int size;
        //Заполненность таблицы
        int fullness;

        //            (1)       (2)          (3)
        //Hi(key) = H0(key) + (i * c1) + (i * i * c2)
        //Константа c, характеризующая множитель сдвига у второго (2) слагаемого в разрешении коллизии
        int c1 = 0;
        //Константа c, характеризующая множитель сдвига у третьего (3) слагаемого в разрешении коллизии
        int с2 = 1;

        public QuadraticOpenHashTable()
        {
            elements = new string[0];
            elementsState = new ElementStatement[0];
            hashFunc = new MidSquareHashFunc();

            size = 0;
            fullness = 0;
        }

        public QuadraticOpenHashTable(int _n, IHashFunc func)
        {
            elements = new string[_n];
            elementsState = new ElementStatement[_n];
            hashFunc = func;
            size = _n;
            fullness = 0;
        }

        //Функция позволяет добавить новый элемент в хеш-таблицу
        public bool Add(string item)
        {
            //Если таблица не заполнена и элемента не найден, то работа по добавлению нового элемента продолжается
            if (fullness != size && !Find(item))
            {
                long hashKey = hashFunc.GetHash(item, size);

                //Если ячейка пуста
                if (elementsState[hashKey] == ElementStatement.empty)
                {
                    //Происходит вставка элемента
                    elements[hashKey] = item;
                    elementsState[hashKey] = ElementStatement.occupied;

                    fullness++;
                    return true;
                }
                else if (elementsState[hashKey] == ElementStatement.chained && elements[hashKey] == "")
                {
                    //Происходит вставка элемента
                    elements[hashKey] = item;
                    elementsState[hashKey] = ElementStatement.chained;

                    fullness++;
                    return true;
                }
                else
                {
                    //Разрешение коллизии

                    int i = 1;
                    long key = (hashKey + i * c1 + i * i * с2) % size;

                    List<long> chainedKeys = new List<long>();

                    if (elementsState[hashKey] == ElementStatement.occupied)
                        chainedKeys.Add(hashKey);

                    //Пока новый ключ не совпадет с прошлым можно подбирать новый (совпадение нового ключа со старым означает то, что начинается проход "по тому же кругу" и это значит, что не нашлась ячейка для вставки нового элемента)
                    while (key != hashKey)
                    {
                        //Если алгоритм попал в занятую ячейку, но не связанную с остальными, то такой ключ добавляется в список связанных
                        if (elementsState[key] == ElementStatement.occupied)
                        {
                            chainedKeys.Add(key);
                        }
                        //Иначе если ячейка была пустой изначально либо связанной, но без значения внутри, то добавляется новый элемент в хеш-таблицу
                        else if (elementsState[key] == ElementStatement.empty || (elementsState[key] == ElementStatement.chained && elements[key] == ""))
                        {
                            //Происходит вставка элемента
                            elements[key] = item;

                            if (elementsState[key] == ElementStatement.empty)
                                elementsState[key] = ElementStatement.occupied;


                            foreach (var chainedKey in chainedKeys)
                            {
                                elementsState[chainedKey] = ElementStatement.chained;
                            }

                            fullness++;
                            return true;
                        }

                        i++;
                        key = (hashKey + i * c1 + i * i * с2) % size;
                    }

                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //Функция позволяет удалить заданный элемент из хеш-таблицы
        public bool Delete(string item)
        {
            long key = hashFunc.GetHash(item, size);

            if (elements[key] == item)
            {
                if (elementsState[key] == ElementStatement.chained)
                {
                    elements[key] = "";
                }
                else
                {
                    elements[key] = "";
                    elementsState[key] = ElementStatement.empty;
                }

                fullness--;
                return true;
            }
            else
            {
                long hashKey = key;
                int i = 1;
                key = (hashKey + i * c1 + i * i * с2) % size;

                //Идет обход хеш-таблицы квадратичными пробами, если key повторится (т.е. совпадет с invalidKey), значит элемента с таким значением в хеш0-таблице не существует
                while (key != hashKey)
                {
                    if (elements[key] == item)
                    {
                        //?? нужно проверить следующий ключ, который может быть также связан с данным. Если следующего нет, то можно сделать этот empty
                        if (elementsState[key] == ElementStatement.chained)
                        {
                            elements[key] = "";
                        }
                        else
                        {
                            elements[key] = "";
                            elementsState[key] = ElementStatement.empty;
                        }

                        fullness--;
                        return true;
                    }

                    i++;
                    key = (hashKey + i * c1 + i * i * с2) % size;
                }

                return false;
            }
        }

        //Функция позволяет найти заданный элемент в хеш-таблице единожды
        public bool Find(string item)
        {
            long key = hashFunc.GetHash(item, size);

            if (elements[key] == item)
            {
                return true;
            }
            else
            {
                long hashKey = key;
                int i = 1;
                key = (hashKey + i * c1 + i * i * с2) % size;

                //Идет обход хеш-таблицы квадратичными пробами, если key повторится (т.е. совпадет с invalidKey), значит элемента с таким значением в хеш0-таблице не существует
                while (key != hashKey)
                {
                    if (elementsState[key] == ElementStatement.empty)
                    {
                        return false;
                    }
                    else
                    {
                        if (elements[key] == item)
                        {
                            return true;
                        }

                        i++;
                        key = (hashKey + i * c1 + i * i * с2) % size;
                    }
                }

                return false;
            }
        }

        //Функция позволяет вывести все ячейки и их содержимое на экран
        public void Print()
        {
            Console.WriteLine(TableToString());
        }

        //Функция позволяет преобразовать все записи хеш-таблицы в строку
        public string TableToString()
        {
            string table = "";
            for (int i = 0; i < size; i++)
            {
                table += $"{i}.";

                if (elements[i] != null)
                    table += $" {elements[i]} ";

                table += "\n";
            }

            return table;
        }

        //Функция позволяет считать из файла значения, которые добавятся в хеш-таблицу
        public void ReadFromFile(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName + ".txt"))
            {
                while (!sr.EndOfStream)
                {
                    string value = sr.ReadLine();
                    //Успешность операции добавления выводится на экран
                    if (Add(value))
                    {
                        Console.WriteLine($"Значение {value} было успешно добавлено в хеш-таблицу");
                    }
                    else
                    {
                        Console.WriteLine($"Значение {value} не удалось добавить в хеш-таблицу. Возможно не нашлось свободной ячейки либо элемент с таким значением уже существует.");
                    }
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < size; i++)
            {
                elements[i] = "";
                elementsState[i] = ElementStatement.empty;
            }

            fullness = 0;
        }

        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public bool Find(string item, out TimeSpan timeEllapsed, out int iter)
        {
            DateTime startTime, endTime;
            startTime = DateTime.Now;
            long key = hashFunc.GetHash(item, size);

            if (elements[key] == item)
            {
                iter = 1;
                endTime = DateTime.Now;
                timeEllapsed = endTime - startTime;
                return true;
            }
            else
            {
                iter = 1;
                long hashKey = key;
                int i = 1;
                key = (hashKey + i * c1 + i * i * с2) % size;

                //Идет обход хеш-таблицы квадратичными пробами, если key повторится (т.е. совпадет с invalidKey), значит элемента с таким значением в хеш0-таблице не существует
                while (key != hashKey)
                {
                    iter++;

                    if (elementsState[key] == ElementStatement.empty)
                    {
                        timeEllapsed = TimeSpan.Zero;
                        iter = -1;
                        return false;
                    }
                    else
                    {
                        if (elements[key] == item)
                        {
                            endTime = DateTime.Now;
                            timeEllapsed = endTime - startTime;
                            return true;
                        }

                        i++;
                        key = (hashKey + i * c1 + i * i * с2) % size;
                    }
                }

                timeEllapsed = TimeSpan.Zero;
                iter = -1;
                return false;
            }
        }
    }
}