using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class Hard
    {
        public static void Main()
        {
            string s = "ADOBECODEBANC";
            string t = "ABC";
            var ans = MinWindow(s, t);
            int[] x = new int[] { 2 };
            int[] y = new int[] {};

            Console.WriteLine(FindMedianSortedArrays(x, y));
            Console.ReadLine();
        }

        // Median of 2 sorted arrays
        // O(log(min(x,y)))
        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if(nums1.Length > nums2.Length)
            {
                return FindMedianSortedArrays(nums2, nums1);
            }

            int len1 = nums1.Length;
            int len2 = nums2.Length;

            int low = 0;
            int high = len1;

            while(low <= high)
            {
                int p1 = (low + high) / 2;
                int p2 = (len1 + len2 + 1) / 2 - p1;

                int maxLeft1 = p1 == 0? int.MinValue : nums1[p1 - 1];
                int minRight1 = p1 == len1 ? int.MaxValue : nums1[p1];

                int maxLeft2 = p2 == 0 ? int.MinValue : nums2[p2 - 1];
                int minRight2 = p2 == len2 ? int.MaxValue : nums2[p2];

                if(maxLeft1 <= minRight2 && maxLeft2 <= minRight1)
                {
                    if((len1+len2) % 2 == 0)
                    {
                        return (double) (Math.Max(maxLeft1, maxLeft2) + Math.Min(minRight1, minRight2)) / 2;
                    }

                    return Math.Max(maxLeft1, maxLeft2);
                }
                else if(maxLeft1 > minRight2)
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

        public static string MinWindow(string s, string t)
        {
            var goal = new Dictionary<char, int>();
            int goalSize = t.Length;
            int minLen = int.MaxValue;
            string result = "";

            //target dictionary
            for (int k = 0; k < t.Length; k++)
            {
                goal.Add(t[k], goal.ContainsKey(t[k]) ? goal[t[k]] + 1 : 0);
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

                //if contains, increse
                int count = map.ContainsKey(c) ? map[c] : 0;
                if (count < goal[c])
                {
                    total++;
                }

                map.Add(c, count + 1);

                if (total == goalSize)
                {
                    while (!goal.ContainsKey(s[i]) || map[s[i]] > goal[s[i]])
                    {
                        char pc = s[i];
                        if (goal.ContainsKey(pc) && map[pc] > goal[pc])
                        {
                            map.Add(pc, map[pc] - 1);
                        }

                        i++;
                    }

                    if (minLen > j - i + 1)
                    {
                        minLen = j - i + 1;
                        result = s.Substring(i, j + 1);
                    }
                }
            }

            return result;
        }
    }
}
