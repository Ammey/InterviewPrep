using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    class Arrays
    {
        public static void Main()
        {
            var obj = new Arrays();
            var pow = obj.MyPow(2, 10);
            var coins = obj.CoinChange(new int[] { 2, 1, 5 }, 11);
            var c4 = new int[] { 1, 2, 3 };
            var c4Res = obj.CombinationSum4(c4, 4);
            var c42Res = obj.CombinationSum4_2(c4, 4);
            var numbers = new int[] { 1, 2, 3, 4 };
            obj.PermuteUnique(numbers);
            var candidates = new int[] { 10, 1, 2, 7, 6, 1, 5 };
            int target = 8;
            var c3 = CombinationSum3(3, 9);
            var c2 = CombinationSum2(candidates, target);
            var numsArr = new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
            var max = MaxSubArray(numsArr);
            var res = CombinationSum(candidates, target);
            var list1 = new [] { "Shogun", "Tapioca Express", "Burger King", "KFC" };
            var list2 = new [] { "KFC", "Shogun", "Burger King" };
            var result = FindRestaurant(list1, list2);
            var nums = new int[] { 1 , 1};
            MoveZeroes(nums);
            var ans = RemoveDuplicates(nums);
            var pascal = Generate(5);
            var matrix = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            var spiral = SpiralOrder(matrix);
            var diag = FindDiagonalOrder(matrix);

            var numss = new int[] { 1, 9, 9 };
            var target1 = PlusOne(numss);
            ans = PivotIndex(numss);
        }

        public bool IsPerfectSquare(int num)
        {
            if (num == 1)
                return true;

            long low = 1,
                high = num / 2,
                mid = 0;

            while (low <= high)
            {
                mid = low + (high - low) / 2;

                if ((mid * mid) == num)
                    return true;
                else if ((mid * mid) < num)
                    low = mid + 1;
                else
                    high = mid - 1;
            }

            return false;
        }

        public double MyPow(double x, int n)
        {
            if(n == 0)
            {
                return 1;
            }

            if (n < 0)
            {
                n = -n;
                x = 1 / x;
            }

            var result = 0.0;
            if (n % 2 == 0)
            {
               result = MyPow(x * x, n / 2);
            }
            else
            {
                result = x * MyPow(x * x, n / 2);
            }

            return double.IsInfinity(result) ? 0 : result;
        }

        //public int QuickSelect(int[] a, int lo, int hi, int k)
        //{
        //    int i = lo;
        //    int j = hi;
        //    int pivot = a[hi];

        //    while(i > j)
        //    {
        //        if(a[i++] > a[j])
        //        {
        //            Swap(a, --i, --j);
        //        }
        //    }

        //    Swap(a, i, j);
        //}

        private void Swap(int[] a, int i, int j)
        {
            var tmp = a[i];
            a[i] = a[j];
            a[j] = a[i];
        }

        // TODO : Must do DP
        public int CoinChange(int[] coins, int amount)
        {
            if (amount < 1)
            {
                return 0;
            }

            int[] dp = new int[amount + 1];
            int sum = 0;

            while (++sum <= amount)
            {
                int min = -1;
                foreach (int coin in coins)
                {
                    if (sum >= coin && dp[sum - coin] != -1)
                    {
                        int temp = dp[sum - coin] + 1;
                        min = min < 0 ? temp : Math.Min(temp, min);
                    }
                }
                dp[sum] = min;
            }
            return dp[amount];
        }

        public static int MaxSubArray(int[] nums)
        {
            if (nums == null)
            {
                return -1;
            }

            int maxSoFar = nums[0];
            int maxHere = nums[0];

            //-2, 1, -3, 4, -1, 2, 1, -5, 4
            for (int i = 1; i < nums.Length; i++)
            {
                maxHere = Math.Max(nums[i], maxHere + nums[i]);
                maxSoFar = Math.Max(maxSoFar, maxHere);
            }
            return maxSoFar;
        }

        public static IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            var res = new List<IList<int>>();
            if (candidates == null || candidates.Length == 0)
            {
                return res;
            }

            Traverse(candidates, 0, target, new List<int>(), res);
            return res;
        }

        public static IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            var res = new List<IList<int>>();
            if (candidates == null || candidates.Length == 0)
            {
                return res;
            }

            Array.Sort(candidates);

            Traverse(candidates, 0, target, new List<int>(), res);
            return res;
        }

        private static void Traverse(int[] can, int index, int target, List<int> cur, List<IList<int>> res)
        {
            if (target < 0)
            {
                return;
            }

            if (target == 0)
            {
                res.Add(new List<int>(cur));
                return;
            }

            for (int i = index; i < can.Length; i++)
            {
                cur.Add(can[i]);
                Traverse(can, i + 1, target - can[i], cur, res);
                cur.RemoveAt(cur.Count - 1);
            }
        }

        public static IList<IList<int>> CombinationSum3(int k, int n)
        {
            var res = new List<IList<int>>();
            int[] candidates = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9 };

            if (candidates == null || candidates.Length == 0)
            {
                return res;
            }

            Traverse3(candidates, 0, k, n, new List<int>(), res);
            return res;
        }

        private static void Traverse3(int[] can, int index, int count, int target, List<int> cur, List<IList<int>> res)
        {
            if (target < 0 || cur.Count > count)
            {
                return;
            }

            if (target == 0 && cur.Count == count)
            {
                res.Add(new List<int>(cur));
                return;
            }

            for (int i = index; i < can.Length; i++)
            {
                cur.Add(can[i]);
                Traverse3(can, i+1, count, target - can[i], cur, res);
                cur.RemoveAt(cur.Count - 1);
            }
        }

        public IList<IList<int>> CombinationSum4_2(int[] nums, int target)
        {
            var res = new List<IList<int>>();
            if (nums == null || nums.Length == 0)
            {
                return res;
            }

            Traverse4_2(nums, 0, target, new List<int>(), res);
            return res;
        }

        private static void Traverse4_2(int[] can, int index, int target, List<int> cur, List<IList<int>> res)
        {
            if (target < 0)
            {
                return;
            }

            if (target == 0)
            {
                res.Add(new List<int>(cur));
                return;
            }

            for (int i = 0; i < can.Length; i++)
            {
                cur.Add(can[i]);
                Traverse4_2(can, i, target - can[i], cur, res);
                cur.RemoveAt(cur.Count - 1);
            }
        }

        public int CombinationSum4(int[] nums, int target)
        {
            int[] comb = new int[target + 1];
            comb[0] = 1;
            for (int i = 1; i < comb.Length; i++)
            {
                for (int j = 0; j < nums.Length; j++)
                {
                    if (i - nums[j] >= 0)
                    {
                        comb[i] += comb[i - nums[j]];
                    }
                }
            }
            return comb[target];
        }

        private static void Traverse4(int[] can, int index, int target, List<int> cur, List<IList<int>> res)
        {
            if (target < 0)
            {
                return;
            }

            if (target == 0)
            {
                res.Add(new List<int>(cur));
                return;
            }

            for (int i = index; i < can.Length; i++)
            {
                cur.Add(can[i]);
                Traverse4(can, i, target - can[i], cur, res);
                cur.RemoveAt(cur.Count - 1);
            }
        }


        public IList<IList<int>> PermuteUnique(int[] nums)
        {
            var res = new List<IList<int>>();
            if (nums == null || nums.Length == 0)
            {
                return res;
            }
            var used = new bool[nums.Length];
            Array.Sort(nums);
            TraversePermute2(nums, used, new List<int>(), res);

            return res;
        }

        private void TraversePermute2(int[] can, bool[] used, List<int> cur, List<IList<int>> res)
        {
            if (cur.Count == can.Length)
            {
                res.Add(new List<int>(cur));
                return;
            }

            for (int i = 0; i < can.Length; i++)
            {
                if((i > 0 && can[i] == can[i-1] && !used[i-1]) || used[i])
                {
                    continue;
                }
                used[i] = true;
                cur.Add(can[i]);
                TraversePermute2(can, used, cur, res);
                cur.RemoveAt(cur.Count - 1);
                used[i] = false;
            }
        }

        public bool ValidSquare(int[] p1, int[] p2, int[] p3, int[] p4)
        {
            return CheckDistance(p1, p2, p3, p4) || CheckDistance(p1, p3, p2, p4) || CheckDistance(p1, p2, p4, p3);
        }

        public double GetDistance(int[] p1, int[] p2)
        {
            return (p2[1] - p1[1]) * (p2[1] - p1[1]) + (p2[0] - p1[0]) * (p2[0] - p1[0]);
        }
        public bool CheckDistance(int[] p1, int[] p2, int[] p3, int[] p4)
        {
            return GetDistance(p1, p2) > 0 && GetDistance(p1, p2) == GetDistance(p2, p3) &&
                GetDistance(p2, p3) == GetDistance(p3, p4) && GetDistance(p3, p4) == GetDistance(p4, p1) && 
                GetDistance(p1, p3) == GetDistance(p2, p4);
        }

        public static void MoveZeroes(int[] nums)
        {
            if (nums == null || nums.Length <= 1)
            {
                return;
            }

            int count = 0;
            int index = 0;

            while(nums[index] != 0)
            {
                index++;
                if(index == nums.Length)
                {
                    return;
                }
            }

            for (int i = index; i < nums.Length; i++)
            {
                if (nums[i] != 0 && i != index)
                {
                    var temp = nums[i];
                    nums[index] = nums[i];
                    index++;
                }
                else
                {
                    count++;
                }
            }

            while(count != 0)
            {
                nums[nums.Length - count] = 0;
                count--;
            }
        }

        public static int RemoveDuplicates(int[] nums)
        {
            if(nums == null || nums.Length == 0)
            {
                return 0;
            }

            int index = 1;
            int num = nums[0];

            for(int i = 1; i < nums.Length; i++)
            {
                if(nums[i] != num)
                {
                    num = nums[i];
                    nums[index] = nums[i];
                    index++;
                }
            }

            return index;
        }

        public static int[] FindDiagonalOrder(int[,] matrix)
        {
            if(matrix == null || matrix.GetLength(0) == 0)
            {
                return new int[0];
            }

            var rowMax = matrix.GetLength(0);
            int colMax = matrix.GetLength(1);
            var result = new int[rowMax * colMax];

            int row = 0, col = 0, d = 0;
            var dirs = new int [,] { { -1, 1 }, { 1, -1 } };

            for (int i = 0; i < rowMax * colMax; i++)
            {
                result[i] = matrix[row, col];
                row += dirs[d, 0];
                col += dirs[d, 1];

                if (row >= rowMax) { row = rowMax - 1; col += 2; d = 1 - d; }
                if (col >= colMax) { col = colMax - 1; row += 2; d = 1 - d; }
                if (row < 0) { row = 0; d = 1 - d; }
                if (col < 0) { col = 0; d = 1 - d; }
            }

            return result;
        }

        public static IList<int> SpiralOrder(int[,] matrix)
        {
            if (matrix == null || matrix.GetLength(0) == 0)
            {
                return new List<int>();
            }

            var rowMax = matrix.GetLength(0) - 1;
            int colMax = matrix.GetLength(1) - 1;
            var result = new List<int>(rowMax*colMax);

            int minRow = 0, minCol = 0;
            while (minRow <= rowMax && minCol <= colMax)
            {
                for (int j = minCol; j <= colMax; j++)
                {
                    result.Add(matrix[minRow,j]);
                }
                minRow++;

                // Traverse Down
                for (int j = minRow; j <= rowMax; j++)
                {
                    result.Add(matrix[j, colMax]);
                }
                colMax--;

                if (minRow <= rowMax)
                {
                    // Traverse Left
                    for (int j = colMax; j >= minCol; j--)
                    {
                        result.Add(matrix[rowMax, j]);
                    }
                }
                rowMax--;

                if (minCol <= colMax)
                {
                    // Traver Up
                    for (int j = rowMax; j >= minRow; j--)
                    {
                        result.Add(matrix[j, minCol]);
                    }
                }
                minCol++;
            }

            return result;
        }

        public static IList<IList<int>> Generate(int numRows)
        {
            var result = new int[numRows][];

            for (int i = 0; i < numRows; i++)
            {
                int[] current = new int[i + 1];
                for (int j = 0; j <= i; j++)
                {
                    if (j == 0 || j == i)
                        current[j] = 1;
                    else
                    {
                        current[j] = result[i - 1][j - 1] + result[i - 1][j];
                    }
                }
                result[i] = current;
            }

            return result;
        }

        public static int PivotIndex(int[] nums)
        {
            int total = 0, sum = 0;

            foreach (int num in nums)
            {
                total += num;
            }

            for (int i = 0; i < nums.Length; sum += nums[i++])
            {
                if (sum * 2 == total - nums[i]) return i;
            }

            return -1;
        }

        public int DominantIndex(int[] nums)
        {
            int largest = int.MinValue;
            int index = -1;

            for(int i = 0; i < nums.Length; i++)
            {
                if(nums[i] > largest)
                {
                    largest = nums[i];
                    index = i;
                }
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] * 2 > largest && index != i)
                {
                    return -1;
                }
            }

            return index;
        }

        public static int[] PlusOne(int[] digits)
        {
            int index = digits.Length - 1;
            int sum = digits[index] + 1;

            while(sum >= 10 && index > 0)
            {
                digits[index] = 0;
                index--;
                sum = digits[index] + 1;
            }

            digits[index] = sum % 10;

            if (sum < 10)
            {
                return digits;
            }

            int[] target = new int[digits.Length + 1];
            Array.Copy(digits, 0, target, 1, digits.Length);
            target[0] = 1;
            return target;
        }

        public int[] TwoSum(int[] nums, int target)
        {
            var set = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if(set.ContainsKey(target - nums[i]))
                {
                    return new int[] { set[target - nums[i]], i };
                }

                if(!set.ContainsKey(nums[i]))
                {
                    set.Add(nums[i], i);
                }
            }

            return null;
        }

        public static string[] FindRestaurant(string[] list1, string[] list2)
        {
            var map = new Dictionary<string, int>();

            for(int i = 0; i < list1.Length; i++)
            {
                map.Add(list1[i], i);
            }

            int min = int.MaxValue;
            var result = new List<string>();

            for(int i = 0; i < list2.Length; i++)
            {
                if(map.ContainsKey(list2[i]))
                {
                    var val = map[list2[i]] + i;
                    if (min > val)
                    {
                        min = val;
                        result = new List<string> { list2[i] };
                    }
                    else if(min == val)
                    {
                        result.Add(list2[i]);
                    }
                }
            }

            return result.ToArray();
        }
    }
}
