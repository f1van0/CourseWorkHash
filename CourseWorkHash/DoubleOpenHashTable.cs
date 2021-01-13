﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CourseWorkHash
{
    //Класс реализует хеш-функцию, разрешающую возникающие коллизии двойным хешированием
    public class DoubleOpenHashTable : IHashTable
    {
        public string Name => $"Двойное хеширование. Первая хеш-функция - {hashFunc.Name}. Вторая хеш-функция - {secondHashFunc.Name}. Коэфициент отклонения у второй хеш-функции - {shift}";

        public string ShortName => $"Двойное хеширование;{hashFunc.Name}+{secondHashFunc.Name}={shift}";

        private string[] elements;
        private IHashFunc hashFunc;
        private IHashFunc secondHashFunc;
        private ElementStatement[] elementsState;
        //число элементов в таблице
        int size;
        //Смещение от числа элементов size для второй хеш-функции
        int shift = 0;
        //Заполненность таблицы
        int fullness;

        public DoubleOpenHashTable()
        {
            elements = new string[0];
            elementsState = new ElementStatement[0];
            hashFunc = new MidSquareHashFunc();

            size = 0;
            fullness = 0;
        }

        public DoubleOpenHashTable(int _n, IHashFunc func, IHashFunc secondFunc)
        {
            elements = new string[_n];
            elementsState = new ElementStatement[_n];
            hashFunc = func;
            secondHashFunc = secondFunc;
            size = _n;
            fullness = 0;
        }

        public DoubleOpenHashTable(int _n, IHashFunc func, IHashFunc secondFunc, int _shift)
        {
            elements = new string[_n];
            elementsState = new ElementStatement[_n];
            hashFunc = func;
            secondHashFunc = secondFunc;
            size = _n;
            shift = _shift;
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

                    fullness++;
                    return true;
                }
                else
                {
                    //Разрешение коллизии

                    int i = 1;
                    long key = (hashKey + secondHashFunc.GetHash(item, size) + shift) % size;

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
                        key = (hashKey + (secondHashFunc.GetHash(item, size) + shift) * i) % size;
                    }

                    return false;
                }
            }
            else
            {
                return false;
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
                key = (hashKey + secondHashFunc.GetHash(item, size) + shift) % size;

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
                    key = (hashKey + (secondHashFunc.GetHash(item, size) + shift) * i) % size;
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
                long i = 1;
                key = (hashKey + secondHashFunc.GetHash(item, size) + shift) % size;

                //Идет обход хеш-таблицы двойным хешированием, если key повторится (т.е. совпадет с invalidKey), значит элемента с таким значением в хеш0-таблице не существует
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
                        key = (hashKey + (secondHashFunc.GetHash(item, size) + shift) * i) % size;
                    }
                }

                return false;
            }
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
                long i = 1;
                key = (hashKey + secondHashFunc.GetHash(item, size) + shift) % size;

                //Идет обход хеш-таблицы двойным хешированием, если key повторится (т.е. совпадет с invalidKey), значит элемента с таким значением в хеш0-таблице не существует
                while (key != hashKey)
                {
                    iter++;

                    if (elementsState[key] == ElementStatement.empty)
                    {
                        iter = -1;
                        timeEllapsed = TimeSpan.Zero;
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
                        key = (hashKey + (secondHashFunc.GetHash(item, size) + shift) * i) % size;
                    }
                }

                iter = -1;
                timeEllapsed = TimeSpan.Zero;
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
    }
}