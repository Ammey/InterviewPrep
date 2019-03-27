
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
    public class BinarySearch
    {
        public static void Main()
        {
            var x = new int[] { 4,6,7,0,1,2 };
            var arr = new int[] { 1, 1, 2, 2, 2, 2, 2, 3, 3 };
            FindClosestElements(arr, 3, 2);
            Console.WriteLine(Search(x, 5));
            Console.ReadLine();
        }

        public int[] SearchRange(int[] nums, int target)
        {
            int[] ret = { -1, -1 };

            // the first binary search to find the left boundary
            int l = 0, r = nums.Length - 1;
            while (l < r)
            {
                int mid = (l + r) / 2;
                if (nums[mid] < target)
                    l = mid + 1;
                else
                    r = mid;
            }

            // if target can not be found, return {-1, -1}
            if (nums[l] != target)
                return ret;

            ret[0] = l;
            // second binary search to find the right boundary
            r = nums.Length - 1;
            while (l < r)
            {
                // mid is calculated differently
                int mid = (l + r + 1) / 2;
                if (nums[mid] > target)
                    r = mid - 1;
                else
                    l = mid;
            }
            ret[1] = l;
            return ret;
        }

        public static IList<int> FindClosestElements(int[] arr, int k, int x)
        {
            int left = 0, right = arr.Length - k;

            while (left < right)
            {
                int mid = (left + right) / 2;
                if (x - arr[mid] > arr[mid + k] - x)
                    left = mid + 1;
                else
                    right = mid;
            }

            var res = new List<int>();
            for (int i = 0; i < k; i++)
            {
                res.Add(arr[left + i]);
            }
            return res;
        }


        public static int Search(int[] nums, int target)
        {
            int pivot = FindPivotInRotatedArray(nums, 0, nums.Length - 1);

            if(pivot == -1)
            {
                return BSearch(nums, 0, nums.Length - 1, target);
            }

            if(nums[pivot] == target)
            {
                return pivot;
            }

            if(nums[0] <= target)
            {
                return BSearch(nums, 0, pivot - 1, target);
            }

            return BSearch(nums, pivot + 1, nums.Length - 1, target);
        }

        public static int FindPivotInRotatedArray(int[] nums, int low, int high)
        {
            if(low >= high)
            {
                return -1;
            }

            int mid = (low + high) / 2;

            if(mid < high && nums[mid] > nums[mid + 1])
            {
                return mid;
            }

            if(mid > low && nums[mid] < nums[mid - 1])
            {
                return mid - 1;
            }

            if(nums[low] >= nums[mid])
            {
                return FindPivotInRotatedArray(nums, low, mid - 1);
            }

            return FindPivotInRotatedArray(nums, mid + 1, high);

        }

        public static int BSearch(int[] nums, int low, int high, int target)
        {
            if(low > high)
            {
                return -1;
            }

            int mid = (low + high) / 2;

            if(nums[mid] == target)
            {
                return mid;
            }

            if(nums[mid] < target)
            {
                return BSearch(nums, mid + 1, high, target);
            }

            return BSearch(nums, low, mid - 1, target);
        }

        public int MySqrt(int x)
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
    }
}
