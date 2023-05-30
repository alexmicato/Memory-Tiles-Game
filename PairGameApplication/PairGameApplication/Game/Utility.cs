﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairGameApplication
{
    internal class Utility
    {
        public static void ShuffleArray<T>(ref T[] array)
        {
            Random random = new Random();

            if (array is null)
            {

                throw new ArgumentNullException(nameof(array));
            }

            for (int i = 0; i < array.Length - 1; ++i)
            {
                int r = random.Next(i, array.Length);
                (array[r], array[i]) = (array[i], array[r]);
            }

        }

        public static void ShuffleList<T>(IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
