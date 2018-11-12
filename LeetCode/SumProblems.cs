
using System;
using System.Collections.Generic;

namespace LeetCode
{
    class SumProblems
    {
        public static void Main()
        {
            var nums = new int[] { -2, 0, 1, 3 };
            var closest = ThreeSumSmaller(nums, 2);
            var result = ThreeSum(nums);
            Random rand = new Random();
            rand.Next();
        }

        // Sum of 3 numbers smaller than target
        public static int ThreeSumSmaller(int[] nums, int target)
        {
            int result = 0;
            Array.Sort(nums);

            var max = nums.Length;
            for (int i = 0; i < max - 2; i++)
            {
                if(nums[i] * 3 >= target)
                {
                    return result;
                }
                int sum = target - nums[i];

                int low = i + 1;
                int high = max - 1;

                while (low < high)
                {
                    var curSum = nums[low] + nums[high];

                    if (curSum < sum)
                    {
                        result += high - low;
                        low++;
                    }
                    else
                    {
                        high--;
                    }
                }
            }
            return result;
        }

        // Sum of 3 numbers closest to the target
        public static int ThreeSumClosest(int[] nums, int target)
        {
            var distance = int.MaxValue;
            int result = 0;
            Array.Sort(nums);

            var max = nums.Length;
            for (int i = 0; i < max - 2; i++)
            {
                if (i != 0 && nums[i] == nums[i - 1])
                {
                    continue;
                }

                int low = i + 1;
                int high = max - 1;

                while (low < high)
                {
                    var curSum = nums[low] + nums[high] + nums[i];
                    if(curSum == target)
                    {
                        return target;
                    }

                    var curDistance = Math.Abs(curSum - target);
                    if (curDistance < distance)
                    {
                        distance = curDistance;
                        result = curSum;
                    }

                    if (curSum < target)
                    {
                        low++;
                    }
                    else
                    {
                        high--;
                    }
                }
            }
            return result;
        }

        // Sum of 3 integers is 0
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            var result = new List<IList<int>>();
            Array.Sort(nums);

            var max = nums.Length;
            for(int i = 0; i < max - 2; i++)
            {
                if(i != 0 && nums[i] == nums[i - 1])
                {
                    continue;
                }

                int sum = -nums[i];

                int low = i + 1;
                int high = max - 1;

                while(low < high)
                {
                    if (nums[low] + nums[high] == sum)
                    {
                        result.Add(new List<int> { nums[i], nums[low], nums[high] });
                        while (low < high && nums[low] == nums[low + 1]) low++;
                        while (low < high && nums[high] == nums[high - 1]) high--;
                        low++;
                        high--;
                    }
                    else if(nums[low] + nums[high] < sum)
                    {
                        low++;
                    }
                    else
                    {
                        high--;
                    }
                }
            }
            return result;
        }
    }
}
