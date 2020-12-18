using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    public class QuadraticOpenHashTable : IHashTable
    {
        private string[] elements;
        private ElementStatement[] elementsState;
        //число элементов в таблице
        int n;
        //Заполненность таблицы
        int a;
        private IHashFunc hashFunc;

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

            n = 0;
            a = 0;
        }

        public QuadraticOpenHashTable(int _n, IHashFunc func)
        {
            elements = new string[_n];
            elementsState = new ElementStatement[_n];
            hashFunc = func;
            n = _n;
            a = 0;
        }

        //Функция позволяет добавить новый элемент в хеш-таблицу
        public bool Add(string item)
        {
            //Если элемента не найден, то работа по добавлению нового элемента продолжается
            if (!Find(item))
            {
                int hashKey = hashFunc.GetHash(item, n);

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
                    elementsState[hashKey] = ElementStatement.chained;

                    a++;
                    return true;
                }
                else
                {
                    //Разрешение коллизии

                    int i = 1;
                    int key = (hashKey + i * c1 + i * i * с2) % n;

                    List<int> chainedKeys = new List<int>();
                    //Пока новый ключ не совпадет с прошлым можно подбирать новый (совпадение нового ключа со старым означает то, что начинается проход "по тому же кругу" и это значит, что не нашлась ячейка для вставки нового элемента)
                    while (key != hashKey)
                    {
                        //Если элемент нового ключа пуст
                        if (elementsState[key] == ElementStatement.empty)
                        {
                            //Происходит вставка элемента
                            elements[key] = item;
                            elementsState[key] = ElementStatement.occupied;

                            foreach (var chainedKey in chainedKeys)
                            {
                                elementsState[chainedKey] = ElementStatement.chained;
                            }

                            a++;
                            return true;
                        }
                        //Если алгоритм попал в занятую ячейку, но не связанную с остальными, то такой ключ добавляется в список связанных
                        else if (elementsState[key] == ElementStatement.occupied)
                        {
                            chainedKeys.Add(key);
                        }

                        i++;
                        key = (hashKey + i * c1 + i * i * с2) % n;
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
            int key = hashFunc.GetHash(item, n);

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
                key = (hashKey + i * c1 + i * i * с2) % n;

                //Идет обход хеш-таблицы квадратичными пробами, если key повторится (т.е. совпадет с invalidKey), значит элемента с таким значением в хеш0-таблице не существует
                while (key != hashKey)
                {
                    if (elements[key] == item)
                    {
                        //?? нужно проверить следующий ключ, который может быть также связан с данным. Если следующего нет, то можно сделать этот empty
                        if (elementsState[key] == ElementStatement.chained)
                        {
                            elements[key] = "";
                            int checkKey = (key + (i + 1) * c1 + (i + 1) * (i + 1) * с2) % n;
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
                    key = (hashKey + i * c1 + i * i * с2) % n;
                }

                return false;
            }
        }

        /*
        //Функция позволяет найти заданный элемент в хеш-таблице единожды
        public int Find(string item)
        {
            int key = hashFunc.GetHash(item, n);

            if (elements[key] == item)
            {
                return key;
            }
            else
            {
                int hashKey = key;
                int i = 1;
                key = (hashKey + i * c1 + i * i * с2) % n;

                //Идет обход хеш-таблицы квадратичными пробами, если key повторится (т.е. совпадет с invalidKey), значит элемента с таким значением в хеш0-таблице не существует
                while (key != hashKey)
                {
                    if (elements[key] == item)
                    {
                        return key;
                    }

                    i++;
                    key = (hashKey + i * c1 + i * i * с2) % n;
                }

                return -1;
            }
        }
        */

        //Функция позволяет найти заданный элемент в хеш-таблице единожды
        public bool Find(string item)
        {
            int key = hashFunc.GetHash(item, n);

            if (elements[key] == item)
            {
                return true;
            }
            else
            {
                int hashKey = key;
                int i = 1;
                key = (hashKey + i * c1 + i * i * с2) % n;

                //Идет обход хеш-таблицы квадратичными пробами, если key повторится (т.е. совпадет с invalidKey), значит элемента с таким значением в хеш0-таблице не существует
                while (key != hashKey)
                {
                    if (elements[key] == item)
                    {
                        return true;
                    }

                    i++;
                    key = (hashKey + i * c1 + i * i * с2) % n;
                }

                return false;
            }
        }

        //Фцнкция позволяет вывести все ячейки и их содержимое на экран
        public void Print()
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"{i}. {elements[i]}");
            }
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
            throw new NotImplementedException();
        }

        public bool Delete(int item)
        {
            throw new NotImplementedException();
        }

        public string[] GetItems()
        {
            string[] items = new string[n];

            for (int i = 0; i < n; i++)
            {
                items[i] = elements[i];
            }

            return items;
        }

        public bool Find(string item, out TimeSpan timeEllapsed)
        {
            throw new NotImplementedException();
        }
    }
}