namespace Applications.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Helper extension methods for enumerations
    /// </summary>
    public static class EnumeratorHelpers
    {
        /// <summary>
        /// Split a list of items into ranges of specified length
        /// </summary>
        /// <typeparam name="T">Type of list item</typeparam>
        /// <param name="values">List of items</param>
        /// <param name="rangeLimit">Lenght of each range</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> ToRanges<T>(this IEnumerable<T> values, int rangeLimit)
        {
            var valuesCount = values?.Count() ?? 0;
            if (valuesCount == 0)
            {
                return null;
            }

            var round = 0;
            var hasMore = true;
            var ranges = new List<IEnumerable<T>>();
            do
            {
                var count = rangeLimit;
                var startIndex = round * rangeLimit;
                if ((startIndex + 1 + count) > valuesCount)
                {
                    count = valuesCount - startIndex;
                    hasMore = false;
                }

                ranges.Add(values.Skip(startIndex).Take(count).ToArray());
                round += 1;
            }
            while (hasMore);

            return ranges;
        }

        /// <summary>
        /// Iterate a list and perform the action specified on each item
        /// </summary>
        /// <typeparam name="T">Type of list items</typeparam>
        /// <param name="items">List of items</param>
        /// <param name="action">Action to be taken on each item</param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if ((items?.Count() ?? 0) == 0)
            {
                return;
            }

            foreach (T item in items)
            {
                action(item);
            }
        }

        /// <summary>
        /// Find the index of an item in a list that matches the specified predicate
        /// </summary>
        /// <typeparam name="T">Type of list items</typeparam>
        /// <param name="entities">List of Items</param>
        /// <param name="predicate">Predicate to find the item in the list</param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> entities, Func<T, bool> predicate)
        {
            if ((entities?.Count() ?? 0) == 0 || predicate == null)
            {
                return -1;
            }

            var index = 0;
            foreach (var item in entities)
            {
                if (predicate(item))
                {
                    return index;
                }

                index += 1;
            }

            return -1;
        }
    }
}