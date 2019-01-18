using System.Linq;

namespace LeetCode
{
    public class BinarySearch
    {
        public static int Search(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Count() - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (nums[mid] == target)
                {
                    return mid;
                }
                else if (nums[mid] > target)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            return -1;
        }

        public static int Sqrt(int x)
        {
            if (x == 0)
                return 0;
            int left = 1;
            int right = x;
            int ans = 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (mid <= x / mid)
                {
                    left = mid + 1;
                    ans = mid;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return ans;
        }

        public static void Main()
        {
            var nums = new int[] { -1, 0, 3, 5, 9, 12, 14 };
            int target = -2;
            var result = Search(nums, target);
        }
    }
}
