using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Foresight.Utilities
{
    public static class CollectionUtilities
    {
        #region GetRandomItem
        /// <summary>Get a random element of a given array.</summary>
        /// <typeparam name="T">The Type of the item to be returned.</typeparam>
        /// <param name="array">The array of objects to choose from.</param>
        /// <returns>Returns a random element from the entered array.</returns>
        public static T GetRandomItem<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }

        /// <summary>Get a random element of a given list.</summary>
        /// <typeparam name="T">The Type of the item to be returned.</typeparam>
        /// <param name="list">The list of objects to choose from.</param>
        /// <returns>Returns a random element from the entered list.</returns>
        public static T GetRandomItem<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        /// <summary>Get a random element of a given list.</summary>
        /// <typeparam name="T">The Type of the item to be returned.</typeparam>
        /// <param name="list">The list of objects to choose from.</param>
        /// <param name="excludedItems"> The list of items to ignore.</param>
        /// <returns>Returns a random element from the entered list, excluding elements in the execluded items list.</returns>
        public static T GetRandomItem<T>(this List<T> list, List<T> excludedItems)
        {
            List<T> chosenOnes = new List<T>();

            foreach(T t in list)
            {
                if (!excludedItems.Contains(t))
                    chosenOnes.Add(t);
            }

            return chosenOnes[Random.Range(0, chosenOnes.Count)];
        }

        /// <summary>Get a random element of a given array.</summary>
        /// <typeparam name="T">The Type of the item to be returned.</typeparam>
        /// <param name="array">The array of objects to choose from.</param>
        /// <param name="excludedItems"> The array of items to ignore.</param>
        /// <returns>Returns a random element from the entered array, excluding elements in the execluded items array.</returns>
        public static T GetRandomItem<T>(this T[] array, T[] excludedItems)
        {
            List<T> chosenOnes = new List<T>();

            foreach (T t in array)
            {
                if (!excludedItems.Contains(t))
                    chosenOnes.Add(t);
            }

            return chosenOnes[Random.Range(0, chosenOnes.Count)];
        }

        #endregion

        #region GetRandomItems
        /// <summary>Gets a list of random elements of a given list.</summary>
        /// <typeparam name="T">The Type of the item list to be returned.</typeparam>
        /// <param name="list">The list of objects to choose from.</param>
        /// <param name="amount">The number of items in the returned list.</param>
        /// <returns>Returns a randomized list of elements from the entered list.</returns>
        public static List<T> GetRandomItems<T>(this List<T> list, int amount)
        {
            List<T> origList = new List<T>();
            foreach (T item in list)
                origList.Add(item);

            List<T> tempList = new List<T>();

            int availableAmt = amount > list.Count ? list.Count : amount;

            for (int i = availableAmt; i > 0; i--)
            {
                T t = GetRandomItem(origList);
                tempList.Add(t);
                origList.Remove(t);

                if (origList.Count <= 0)
                {
                    foreach (T item in list)
                        origList.Add(item);
                }
            }

            return tempList;
        }

        /// <summary>Gets a list of random elements of a given array.</summary>
        /// <typeparam name="T">The Type of the item list to be returned.</typeparam>
        /// <param name="array">The array of objects to choose from.</param>
        /// <param name="amount">The number of items in the returned array.</param>
        /// <returns>Returns a randomized array of elements from the entered array.</returns>
        public static T[] GetRandomItems<T>(this T[] array, int amount)
        {
            List<T> origList = new List<T>();
            foreach (T item in array)
                origList.Add(item);

            List<T> tempList = new List<T>();

            int availableAmt = amount > array.Length ? array.Length : amount;

            for (int i = availableAmt; i > 0; i--)
            {
                T t = GetRandomItem(origList);
                tempList.Add(t);
                origList.Remove(t);

                if (origList.Count <= 0)
                {
                    foreach (T item in array)
                        origList.Add(item);
                }
            }

            return tempList.ToArray();
        }
        #endregion
            
        #region Shuffle
        private static System.Random rng = new System.Random();

        public static void Shuffle<T>(this IList<T> list)
        {
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
        #endregion
    }
}
