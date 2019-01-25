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
            var candidates = new int[] { 10, 1, 2, 7, 6, 1, 5 };
            int target = 8;
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

            Array.Sort(candidates);
            Traverse(candidates, target, new List<int>(), res);
            return res;
        }

        private static void Traverse(int[] can, int target, List<int> cur, List<IList<int>> res)
        {
            for(int i = 0; i < can.Length; i++)
            {
                cur.Add(can[i]);
                int curSum = can[i];
                for(int j = i + 1; j < can.Length; j++)
                {
                    if(curSum + can[j] > target)
                    {
                        break;
                    }

                    if(curSum + can[j] + can[j] > target)
                    {
                        continue;
                    }

                    curSum += can[j];
                    cur.Add(can[j]);

                    if(curSum == target)
                    {
                        res.Add(cur);
                        break;
                    }
                }

            }
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
