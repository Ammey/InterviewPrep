using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class PermutationClass
    {
        static IList<IList<int>> results;

        public static void Main()
        {
            int[] x = new int[] { 1, 2, 3 };
            var ans = Permute(x);
            Console.ReadLine();
        }

        static void Swap(int[] array, int position1, int position2)
        {
            int temp = array[position1];
            array[position1] = array[position2];
            array[position2] = temp;
        }

        static void Permutations(int[] arr, int k)
        {
            for (int i = k; i < arr.Length; i++)
            {
                Swap(arr, i, k);
                Permutations(arr, k + 1);
                Swap(arr, k, i);
            }
            if (k == arr.Length - 1)
            {
                results.Add(arr.ToList());
            }
        }

        public static IList<IList<int>> Permute(int[] nums)
        {
            results = new List<IList<int>>();
            Permutations(nums, 0);
            return results;
        }
    }
}
