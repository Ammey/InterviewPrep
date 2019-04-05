using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class Hard
    {
        private readonly string[] BelowTen = new string[] {"", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine"};
        private readonly string[] BelowTwenty = new string[] {"Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"};
        private readonly string[] BelowHundred = new string[] {"", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"};

        public static void Main()
        {
            var obj = new Hard();
            var stocks = obj.MaxProfit(2, new int[] { 3, 2, 6, 5, 0, 3 });
            var add = obj.AddOperators("105", 5);
            var ans = obj.NumberToWords(3423423);
            var lcs = "agbdba";
            var lcsSolution = obj.CalculateRecursive(lcs.ToCharArray(), 0, lcs.Length);
            var rainWater = new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 };
            var rainTrapped = Trap(rainWater);
            string s = "adobecodebanc";
            string t = "abc";
            var isPermutation = CheckInclusion("hello", "ooolleoooleh");
            ans = MinWindow(s, t);
            int[] x = new int[] { 2 };
            int[] y = new int[] { };

            Console.WriteLine(FindMedianSortedArrays(x, y));
            Console.ReadLine();
        }

        public int MaxProfit(int[] prices)
        {
            int sell1 = 0, sell2 = 0, buy1 = int.MinValue, buy2 = int.MinValue;

            for (int i = 0; i < prices.Length; i++)
            {
                buy1 = Math.Max(buy1, -prices[i]);
                sell1 = Math.Max(sell1, buy1 + prices[i]);
                buy2 = Math.Max(buy2, sell1 - prices[i]);
                sell2 = Math.Max(sell2, buy2 + prices[i]);
            }

            return sell2;
        }

        public int MaxProfit(int k, int[] prices)
        {
            if (k == 0 || prices.Length == 0)
            {
                return 0;
            }

            if (k >= prices.Length)
            {
                return AllTimeProfit(prices);
            }

            var T = new int[k + 1,prices.Length];

            for (int i = 1; i< T.GetLength(0); i++)
            {
                int maxDiff = -prices[0];
                for (int j = 1; j<T.GetLength(1); j++)
                {
                    T[i,j] = Math.Max(T[i, j - 1], prices[j] + maxDiff);
                    maxDiff = Math.Max(maxDiff, T[i - 1, j] - prices[j]);
                }
            }
            //printActualSolution(T, prices);
            return T[k, prices.Length - 1];
        }

        public int AllTimeProfit(int[] arr)
        {
            int profit = 0;
            int localMin = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i - 1] >= arr[i])
                {
                    localMin = arr[i];
                }
                else
                {
                    profit += arr[i] - localMin;
                    localMin = arr[i];
                }

            }
            return profit;
        }


        // https://leetcode.com/problems/expression-add-operators/
        public IList<string> AddOperators(string num, int target)
        {
            var result = new List<string>();

            if (string.IsNullOrWhiteSpace(num) || num.Length == 1)
            {
                return result;
            }

            OperatorHelper(num, target, result, string.Empty, 0, 0, 0);

            return result;
        }

        private void OperatorHelper(string num, int target, List<string> result, string path, int pos, long total, long mul)
        {
            if (pos == num.Length)
            {
                if (target == total)
                {
                    result.Add(path);
                }
                return;
            }
            for (int i = pos; i < num.Length; i++)
            {
                if (i != pos && num[pos] == '0')
                {
                    break;
                }

                long cur = long.Parse(num.Substring(pos, i + 1 - pos));
                if (pos == 0)
                {
                    OperatorHelper(num, target, result, path + cur, i + 1, cur, cur);
                }
                else
                {
                    OperatorHelper(num, target, result, path + "+" + cur, i + 1, total + cur, cur);

                    OperatorHelper(num, target, result, path + "-" + cur, i + 1, total - cur, -cur);

                    OperatorHelper(num, target, result, path + "*" + cur, i + 1, total - mul + mul * cur, mul * cur);
                }
            }
        }

        public class MinHeap
        {
            public readonly SortedDictionary<int, Queue<ListNode>> map = new SortedDictionary<int, Queue<ListNode>>();

            public void Add(int val, ListNode node)
            {
                if (!map.ContainsKey(val))
                {
                    map.Add(val, new Queue<ListNode>());
                }

                map[val].Enqueue(node);
            }

            public ListNode PopMin()
            {
                int minKey = map.First().Key;
                ListNode node = map[minKey].Dequeue();

                if (map[minKey].Count == 0)
                    map.Remove(minKey);

                return node;
            }
        }

        public ListNode MergeKLists(ListNode[] lists)
        {
            var pq = new MinHeap();
            foreach(var node in lists)
            {
                if(node != null)
                {
                    pq.Add(node.val, node);
                }
            }

            ListNode head = null;
            ListNode curr = null;

            while(pq.map.Any())
            {
                var node = pq.PopMin();

                if (node.next != null)
                {
                    pq.Add(node.next.val, node.next);
                }

                if (curr == null)
                {
                    head = node;
                    head = curr;
                }
                else
                {
                    curr.next = node;
                    curr = curr.next;
                }
            }

            return head;
        }

        public string NumberToWords(int num)
        {
            if (num == 0) return "Zero";
            return NumHelper(num);
        }

        private string NumHelper(int num)
        {
            var result = string.Empty;
            if (num < 10) result = BelowTen[num];
            else if (num < 20) result = BelowTwenty[num - 10];
            else if (num < 100) result = BelowHundred[num / 10] + " " + NumHelper(num % 10);
            else if (num < 1000) result = NumHelper(num / 100) + " Hundred " + NumHelper(num % 100);
            else if (num < 1000000) result = NumHelper(num / 1000) + " Thousand " + NumHelper(num % 1000);
            else if (num < 1000000000) result = NumHelper(num / 1000000) + " Million " + NumHelper(num % 1000000);
            else result = NumHelper(num / 1000000000) + " Billion " + NumHelper(num % 1000000000);
            return result.Trim();
        }

        public int CalculateRecursive(char[] str, int start, int len)
        {
            if (len == 1)
            {
                return 1;
            }
            if (len == 0)
            {
                return 0;
            }
            if (str[start] == str[start + len - 1])
            {
                return 2 + CalculateRecursive(str, start + 1, len - 2);
            }
            else
            {
                return Math.Max(CalculateRecursive(str, start + 1, len - 1), CalculateRecursive(str, start, len - 1));
            }
        }

        // Trapping Rain water
        // {2, 0, 2} = 2
        // {0,1,0,2,1,0,1,3,2,1,2,1} = 6
        public static int Trap(int[] height)
        {
            var result = 0;
            var curMax = 0;
            var rainStack = new Stack<int>();
            for(int i=0; i < height.Length; i++)
            {
                if(!rainStack.Any())
                {
                    if(height[i] != 0)
                    {
                        curMax = height[i];
                        rainStack.Push(height[i]);
                    }
                    continue;
                }

                if(curMax > height[i])
                {
                    rainStack.Push(height[i]);
                }
                else
                {
                    while(rainStack.Any())
                    {
                        result = result + (curMax - rainStack.Pop());
                    }
                    curMax = height[i];
                    rainStack.Push(curMax);
                }
            }

            if(rainStack.Count > 1)
            {
                var lastData = new List<int>();
                while(rainStack.Any())
                {
                    lastData.Add(rainStack.Pop());
                }

                result += Trap(lastData.ToArray());
                //var cur = rainStack.Pop();
                //while(rainStack.Any() && rainStack.Peek() <= cur)
                //{
                //    rainStack.Pop();
                //}

                //if(rainStack.Any())
                //{
                //    var newMax = rainStack.Pop();
                //    while (rainStack.Count > 1)
                //    {
                //        result += newMax - rainStack.Pop();
                //    }
                //}
            }

            return result;
        }

        // Median of 2 sorted arrays
        // O(log(min(x,y)))
        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if (nums1.Length > nums2.Length)
            {
                return FindMedianSortedArrays(nums2, nums1);
            }

            int len1 = nums1.Length;
            int len2 = nums2.Length;

            int low = 0;
            int high = len1;

            while (low <= high)
            {
                int p1 = (low + high) / 2;
                int p2 = (len1 + len2 + 1) / 2 - p1;

                int maxLeft1 = p1 == 0 ? int.MinValue : nums1[p1 - 1];
                int minRight1 = p1 == len1 ? int.MaxValue : nums1[p1];

                int maxLeft2 = p2 == 0 ? int.MinValue : nums2[p2 - 1];
                int minRight2 = p2 == len2 ? int.MaxValue : nums2[p2];

                if (maxLeft1 <= minRight2 && maxLeft2 <= minRight1)
                {
                    if ((len1 + len2) % 2 == 0)
                    {
                        return (double)(Math.Max(maxLeft1, maxLeft2) + Math.Min(minRight1, minRight2)) / 2;
                    }

                    return Math.Max(maxLeft1, maxLeft2);
                }
                else if (maxLeft1 > minRight2)
                {
                    high = p1 - 1;
                }
                else
                {
                    low = p1 + 1;
                }
            }

            return -1;
        }

        // Sliding Window
        public static string MinWindow(string s, string t)
        {
            if (s.Count() == 0 || t.Count() == 0 || t.Count() > s.Count())
            {
                return "";
            }

            if (string.Equals(s, t))
            {
                return t;
            }

            var goal = new Dictionary<char, int>();
            int minLen = int.MaxValue;
            string result = "";

            //target dictionary
            for (int k = 0; k < t.Length; k++)
            {
                if (goal.ContainsKey(t[k]))
                {
                    goal[t[k]] += 1;
                }
                else
                {
                    goal.Add(t[k], 1);
                }
            }

            int i = 0;
            int total = 0;
            var map = new Dictionary<char, int>();
            //string s = "ADOBECODEBANC";
            //string t = "ABC";
            for (int j = 0; j < s.Length; j++)
            {
                char c = s[j];
                if (!goal.ContainsKey(c))
                {
                    continue;
                }

                if (map.ContainsKey(c))
                {
                    map[c] = map[c] + 1;
                }
                else
                {
                    map.Add(c, 1);
                }

                if (goal[c] == map[c])
                {
                    total++;
                }

                while (i <= j && total == goal.Keys.Count())
                {
                    c = s[i];

                    // Save the smallest window until now.
                    if (minLen > j - i + 1)
                    {
                        minLen = j - i + 1;
                        result = s.Substring(i, minLen);
                    }

                    // The character at the position pointed by the
                    // `Left` pointer is no longer a part of the window.
                    if (map.ContainsKey(c))
                    {
                        map[c] = map[c] - 1;
                        if (map[c] < goal[c])
                        {
                            total--;
                        }
                    }

                    // Move the left pointer ahead, this would help to look for a new window.
                    i++;
                }
            }

            return result;
        }

        // Sliding window -- Permutation check
        public static bool CheckInclusion(string s1, string s2)
        {
            if (s1.Count() > s2.Count())
            {
                return false;
            }

            int[] s1map = new int[26];
            int[] s2map = new int[26];
            for (int i = 0; i < s1.Count(); i++)
            {
                s1map[s1[i] - 'a']++;
                s2map[s2[i] - 'a']++;
            }
            for (int i = 0; i < s2.Count() - s1.Count(); i++)
            {
                if (Match(s1map, s2map))
                {
                    return true;
                }
                s2map[s2[i + s1.Count()] - 'a']++;
                s2map[s2[i] - 'a']--;
            }

            return Match(s1map, s2map);
        }

        public static bool Match(int[] s1map, int[] s2map)
        {
            for (int i = 0; i < 26; i++)
            {
                if (s1map[i] != s2map[i])
                    return false;
            }
            return true;
        }
    }
}
