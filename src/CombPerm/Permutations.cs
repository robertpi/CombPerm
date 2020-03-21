using System;
using System.Collections.Generic;
using System.Linq;

namespace CombPerm
{
    public static class Permutations
    {
        /// <summary>
        /// Get the next permutation in lexicographical ordering, the permutation
        /// will be made in place.
        /// </summary>
        /// <typeparam name="T">type of item to order</typeparam>
        /// <param name="items">an array containing the items to permutate</param>
        /// <returns>true if there is another permutation available, false otherwise</returns>
        public static bool Next<T>(T[] items) where T : IComparable
        {
            // find the longest suffix that is non-increasing 
            int pivotIndex = items.Length - 1;
            for (int i = items.Length - 1; i > 0; i--)
            {
                if (items[i - 1].CompareTo(items[i]) >= 0) // pervious items is bigger
                {
                    pivotIndex--;
                }
                else
                {
                    break;
                }
            }

            if (pivotIndex == 0)
            {
                return false;
            }

            int frontSwapItemIndex = pivotIndex - 1;
            T frontSwapItem = items[frontSwapItemIndex];
            int swapIndex = -1;

            // find the first item in the suffix bigger than the frontSwapItem
            for (int i = items.Length - 1; i >= pivotIndex; i--)
            {
                if (items[i].CompareTo(frontSwapItem) > 0)
                {
                    swapIndex = i;
                    break;
                }
            }

            if (swapIndex == -1)
            {
                return false;
            }

            // swap the items,
            T backSwapItem = items[swapIndex];
            items[swapIndex] = frontSwapItem;
            items[frontSwapItemIndex] = backSwapItem;

            // reverse the suffix, if necessary
            int countToRev = items.Length - pivotIndex;
            if (countToRev > 0)
            {
                Array.Reverse(items, pivotIndex, countToRev);
            }

            return true;
        }

        /// <summary>
        /// Compute all permutations for in a pre-allocated array
        /// </summary>
        /// <typeparam name="T">type of item to order</typeparam>
        /// <param name="items">Items to permutate</param>
        /// <param name="sorted">If the list is sorted (to avoid performing the sort)</param>
        /// <returns>An array containing all possible permutations</returns>
        public static T[][] AllPermutation<T>(T[] items, bool sorted = false) where T : IComparable
        {
            T[][] result = new T[Math.Factorial(items.Length)][];
            result[0] = new T[items.Length];
            items.CopyTo(result[0], 0);
            if (!sorted)
            {
                Array.Sort(result[0]);
            }

            for (int i = 1; i < result.Length; i++)
            {
                result[i] = new T[items.Length];
                result[i - 1].CopyTo(result[i], 0);
                Next(result[i]);
            }
            return result;
        }

        /// <summary>
        /// Compute all permutations in a lazy, on demand manner
        /// </summary>
        /// <typeparam name="T">type of item to order</typeparam>
        /// <param name="items">Items to permutate</param>
        /// <param name="sorted">If the list is sorted (to avoid performing the sort)</param>
        /// <returns>An IEnumerable that will generate the permutations when enumerated</returns>
        public static IEnumerable<T[]> AllPermutationLazy<T>(T[] items, bool sorted = false) where T : IComparable
        {
            int results = Math.Factorial(items.Length);
            var result = new T[items.Length];
            items.CopyTo(result, 0);
            if (!sorted)
            {
                Array.Sort(result);
            }
            yield return result;

            for (int i = 1; i < results; i++)
            {
                var nextResult = new T[items.Length];
                result.CopyTo(nextResult, 0);
                Next(nextResult);
                yield return nextResult;
                result = nextResult;
            }
        }

        static IEnumerable<IEnumerable<T>> AllPermutationLazyInternal<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return AllPermutationLazyInternal(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        /// <summary>
        /// Compute all permutations in a lazy, on demand manner, from an IEnumerable,
        /// because of the constrains of IEnumerable, generation is less efficient
        /// </summary>
        /// <typeparam name="T">type of item to order</typeparam>
        /// <param name="items">Items to permutate</param>
        /// <returns>An IEnumerable that will generate the permutations when enumerated</returns>
        public static IEnumerable<IEnumerable<T>> AllPermutationLazy<T>(IEnumerable<T> list)
        {
            return AllPermutationLazyInternal(list, list.Count());
        }
    }
}
