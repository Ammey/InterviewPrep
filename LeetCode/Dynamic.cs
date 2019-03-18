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

            var obj = new Dynamic();
            var lis = obj.LengthOfLIS(new int[] { 10, 9, 2, 5, 3, 7, 101, 18 });

            var res = obj.FindTargetSumWays(new int[] { 1, 1, 1, 1, 1 }, 1);
            var maxRob = Rob(new int[] { 1, 2, 3, 1 });
            var x = new int[] { 1, 100, 1, 1, 1, 100, 1, 1, 100, 1 };
            Fib(3);
            Console.WriteLine(MinCostClimbingStairs(x));
        }

        // Longest Increasing Subsequence (NLogn)
        public int LengthOfLIS(int[] nums)
        {
            int[] dp = new int[nums.Length];
            int len = 0;
            foreach (int num in nums)
            {
                int i = Array.BinarySearch(dp, 0, len, num);
                if (i < 0)
                {
                    i = -(i + 1);
                }
                dp[i] = num;
                if (i == len)
                {
                    len++;
                }
            }
            return len;
        }

        // Longest Increasing Subsequence (N2)
        public int LengthOfLIS2(int[] nums)
        {
            if (nums.Length == 0)
            {
                return 0;
            }
            int[] dp = new int[nums.Length];
            dp[0] = 1;
            int maxans = 1;
            for (int i = 1; i < dp.Length; i++)
            {
                int maxval = 0;
                for (int j = 0; j < i; j++)
                {
                    if (nums[i] > nums[j])
                    {
                        maxval = Math.Max(maxval, dp[j]);
                    }
                }
                dp[i] = maxval + 1;
                maxans = Math.Max(maxans, dp[i]);
            }
            return maxans;
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

        // https://leetcode.com/problems/target-sum/
        public int FindTargetSumWays(int[] nums, int S)
        {
            // given nums = [1, 2, 3, 4, 5] and target = 3 then one possible solution is +1-2+3-4+5 = 3
            // Here positive subset is P = [1, 3, 5] and negative subset is N = [2, 4]
            // target = sum(P) +(-1)*sum(N)
            // target + sum(P) + sum(N) = sum(P) - sum(N) + sum(P) + sum(N)
            // target + sum(all items) = 2*sum(P)
            // so, we should find sum(positive) which is equal (target+sum(all items)/2)
            // since we have 2 in right part of equation, sum of target and all elemnts should be even!
            var sums = new Dictionary<int, int>();
            var total = 0;
            for (int i = 0; i < nums.Length; i++)
                total += nums[i];

            if (total < S || (total + S) % 2 == 1) return 0;
            return FindSumCombination(nums, (S + total) / 2);
        }

        private int FindSumCombination(int[] nums, int target)
        {

            var dp = new int[target + 1];
            dp[0] = 1;
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = target; j >= nums[i]; j--)
                    dp[j] += dp[j - nums[i]];
            }
            return dp[target];
        }


    }
}
