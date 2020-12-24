using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CourseWorkHash
{
    public class LinearOpenHashTable : IHashTable
    {
        public string Name => $"Линейные пробы. Хеш-функция - {hashFunc.Name}.";

        public string ShortName => $"Линейные пробы;{hashFunc.Name}";

        private string[] elements;
        private IHashFunc hashFunc;
        private ElementStatement[] elementsState;
        //число элементов в таблице
        int size;
        //Заполненность таблицы
        int a;

        int c = 1;

        public LinearOpenHashTable()
        {
            elements = new string[0];
            elementsState = new ElementStatement[0];
            hashFunc = new MidSquareHashFunc();

            size = 0;
            a = 0;
        }

        public LinearOpenHashTable(int _n, IHashFunc func)
        {
            elements = new string[_n];
            elementsState = new ElementStatement[_n];
            hashFunc = func;
            size = _n;
            a = 0;
        }

        //Функция позволяет добавить новый элемент в хеш-таблицу
        public bool Add(string item)
        {
            //Если элемента не найден, то работа по добавлению нового элемента продолжается
            if (!Find(item) && a != size)
            {
                int hashKey = hashFunc.GetHash(item, size);

                //Если ячейка пуста
                if (elementsState[hashKey] == ElementStatement.empty)
                {
                    //Происходит вставка элемента
                    elements[hashKey] = item;
                    elementsState[hashKey] = ElementStatement.occupied;

                    a++;
                    return true;
                }
                else if (elementsState[hashKey] == ElementStatement.chained && elements[hashKey] == "")
                {
                    //Происходит вставка элемента
                    elements[hashKey] = item;

                    a++;
                    return true;
                }
                //Разрешение коллизии
                else
                {
                    int i = 1;
                    int key = (hashKey + i * c) % size;

                    List<int> chainedKeys = new List<int>();

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

                            a++;
                            return true;
                        }

                        i++;
                        key = (hashKey + i * c) % size;
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
            int key = hashFunc.GetHash(item, size);

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

                a--;
                return true;
            }
            else
            {
                int hashKey = key;
                int i = 1;
                key = (hashKey + i * c) % size;

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

                        a--;
                        return true;
                    }

                    i++;
                    key = (hashKey + i * c) % size;
                }

                return false;
            }
        }

        //Функция позволяет найти заданный элемент в хеш-таблице единожды
        public bool Find(string item)
        {
            int key = hashFunc.GetHash(item, size);

            if (elements[key] == item)
            {
                return true;
            }
            else
            {
                int hashKey = key;
                int i = 1;
                key = (hashKey + i * c) % size;

                //Идет обход хеш-таблицы линейными пробами, если key повторится (т.е. совпадет с invalidKey), значит элемента с таким значением в хеш0-таблице не существует
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
                        key = key = (hashKey + i * c) % size;
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

        public void Clear()
        {
            for (int i = 0; i < size; i++)
            {
                elements[i] = "";
                elementsState[i] = ElementStatement.empty;
            }
        }

        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public bool Find(string item, out TimeSpan timeEllapsed)
        {
            DateTime startTime, endTime;
            startTime = DateTime.Now;
            int key = hashFunc.GetHash(item, size);

            if (elements[key] == item)
            {
                endTime = DateTime.Now;
                timeEllapsed = endTime - startTime;
                return true;
            }
            else
            {
                int hashKey = key;
                int i = 1;
                key = (hashKey + i) % size;

                //Идет обход хеш-таблицы линейными пробами, если key повторится (т.е. совпадет с invalidKey), значит элемента с таким значением в хеш0-таблице не существует
                while (key != hashKey)
                {
                    if (elementsState[key] == ElementStatement.empty)
                    {
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
                        key = (hashKey + i) % size;
                    }
                }

                timeEllapsed = TimeSpan.Zero;
                return false;
            }
        }
    }
}