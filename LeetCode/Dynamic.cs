using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    class Dynamic
    {
        public static void Main()
        {
            var maxRob = Rob(new int[] { 1, 2, 3, 1 });
            var x = new int[] { 1, 100, 1, 1, 1, 100, 1, 1, 100, 1 };
            Fib(3);
            Console.WriteLine(MinCostClimbingStairs(x));
        }

        public static int Rob(int[] nums)
        {
            if (nums == null || nums.Count() == 0)
            {
                return 0;
            }
            if (nums.Count() == 1)
            {
                return nums[0];
            }

            var count = nums.Count();
            int[] nums0 = nums.Skip(1).ToArray();
            int[] nums1 = nums.ToList().GetRange(0, count - 1).ToArray();

            return Math.Max(RobHelper(nums0), RobHelper(nums1));
        }

        public static int RobHelper(int[] nums)
        {
            int prevMax = 0;
            int currMax = 0;
            foreach (int x in nums)
            {
                int temp = currMax;
                currMax = Math.Max(prevMax + x, currMax);
                prevMax = temp;
            }
            return currMax;
        }

        public static int MinCostClimbingStairs(int[] cost)
        {
            for (int i = 2; i < cost.Length; i++)
            {
                cost[i] += Math.Min(cost[i - 1], cost[i - 2]);
            }
            return Math.Min(cost[cost.Length - 1], cost[cost.Length - 2]);
        }

        public static int Fib(int N)
        {
            if(N == 0)
            {
                return 0;
            }

            int[] fibo = new int[N + 1];
            fibo[0] = 0;
            fibo[1] = 1;

            for (int i = 2; i <= N; i++)
            {
                fibo[i] = fibo[i - 1] + fibo[i - 2];
            }

            return fibo[N];
        }

        public static int ClimbStairs(int n)
        {
            if(n == 1)
            {
                return 1;
            }

            int[] fibo = new int[n + 1];
            fibo[1] = 1;
            fibo[2] = 2;

            for (int i = 3; i <= n; i++)
            {
                fibo[i] = fibo[i - 1] + fibo[i - 2];
            }

            return fibo[n];
        }

    }
}
