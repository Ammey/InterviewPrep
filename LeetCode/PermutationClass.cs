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
            var obj = new PermutationClass();
            var res = obj.NextPermutation(new int[] { 2, 4, 3, 1 });
            var kth = obj.KthPermutation(4, 8);
            int[] x = new int[] { 1, 2, 3, 4};
            var ans = Permute(x);
            Console.ReadLine();
        }

        public string KthPermutation(int n, int k)
        {
            int[] fac = new int[n + 1];
            fac[0] = 1;
            for (int i = 1; i <= n; i++)
            {
                fac[i] = fac[i - 1] * i;
            }
            k--;
            var nums = new List<int>();
            for (int i = 1; i <= n; i++)
            {
                nums.Add(i);
            }
            string res = "";
            while (k != 0)
            {
                int index = k / fac[n - 1];
                res += nums[index];
                nums.RemoveAt(index);
                k = k - index * fac[n - 1];
                n--;
            }
            foreach (int i in nums)
            {
                res += i;
            }
            return res;
        }

        public int[] NextPermutation(int[] nums)
        {
            var length = nums.Length;

            int i = length - 1;
            while(i > 0 && nums[i] <= nums[i - 1])
            {
                i--;
            }

            i--;

            if(i >=0)
            {
                int j = length - 1;

                while (j >= i && nums[j] <= nums[i])
                {
                    j--;
                }

                Swap(nums, i, j);
            }

            Reverse(nums, i + 1);
            return nums;
        }

        private void Reverse(int[] nums, int start)
        {
            int i = start;
            int j = nums.Length - 1;
            while (i < j)
            {
                Swap(nums, i, j);
                i++;
                j--;
            }
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
