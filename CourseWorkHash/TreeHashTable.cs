﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    class Leaf
    {
        public Leaf()
        {
            left = null;
            right = null;

            value = 0;
        }

        public Leaf* left;
        public Leaf* right;
        public string value;
    }

    class BinaryTree
    {
        private Leaf* root;
        private int leafsCount;
    }

    public class TreeHashTable<T> : IHashTable
    {
        private Tree<T>[] Elements;

        TreeHashTable(int size)
        {
            Elements = new int[size];
        }

        public bool Add(int element)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int item)
        {
            throw new NotImplementedException();
        }

        public int[] GetItems()
        {
            throw new NotImplementedException();
        }

        public bool HasItem(int item)
        {
            throw new NotImplementedException();
        }

        public bool HasItem(int item, out TimeSpan timeEllapsed)
        {
            throw new NotImplementedException();
        }
    }
}