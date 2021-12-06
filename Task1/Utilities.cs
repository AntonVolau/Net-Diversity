using System;

namespace Task1
{
    public static class Utilities
    {
        /// <summary>
        /// Sorts an array in ascending order using bubble sort.
        /// </summary>
        /// <param name="numbers">Numbers to sort.</param>
        public static void Sort(int[] numbers)
        {
            try
            {
                int temp;
                for (int i = 0; i < numbers.Length; i++)
                {
                    for (int j = 0; j < numbers.Length - 1; j++)
                    {
                        if (numbers[j] > numbers[j + 1])
                        {
                            temp = numbers[j + 1];
                            numbers[j + 1] = numbers[j];
                            numbers[j] = temp;
                        }
                    }
                }
            }
            catch
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Searches for the index of a product in an <paramref name="products"/> 
        /// based on a <paramref name="predicate"/>.
        /// </summary>
        /// <param name="products">Products used for searching.</param>
        /// <param name="predicate">Product predicate.</param>
        /// <returns>If match found then returns index of product in <paramref name="products"/>
        /// otherwise -1.</returns>
        public static int IndexOf(Product[] products, Predicate<Product> predicate)
        {
            try
            {
                for (int i = 0; i < products.Length; i++)
                {
                    var product = products[i];
                    if (predicate(product))
                    {
                        return i++;
                    }
                }

                return -1;
            }
            catch
            {
                throw new ArgumentNullException();
            }
        }
    }
}
