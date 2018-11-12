using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class HashMapCode
    {
        public static void Main()
        {
            var nums = new int[] {-3, -2, -1, 0, 0, 1, 2, 3 };
            var ans1 = FourSum(nums, 0);
            var A = new int[] { 1, 2 };
            var B = new int[] { -2, -1 };
            var C = new int[] {-1, 2 };
            var D = new int[] {0, 2 };
            Console.WriteLine(FourSumCount(A, B, C, D));
            var a = new string[] { "abc", "bcd", "acef", "xyz", "az", "ba", "a", "z" };
            var res = GroupStrings(a);
            var x = new string[] {"eat", "tea", "tan", "ate", "nat", "bat"};
            var sol = GroupAnagrams(x);
            var nums1 = new int[] { 1, 2, 3, 1, 2, 3 };
            var result = ContainsNearbyDuplicate(nums1, 2);
            var ans = FirstUniqChar("loveleetcode");
            
        }

        /// <summary>
        /// See this
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IList<IList<int>> FourSum(int[] nums, int target)
        {
            var result = new List<IList<int>>();
            if (nums == null || nums.Length == 0) return result;
            var tempList = new List<int>();
            Array.Sort(nums);
            Get4Sum(nums, 0, target, result, tempList, 0);
            return result;
        }

        private static void Get4Sum(int[] nums, int sum, int target, List<IList<int>> ans, List<int> tempList, int pos)
        {
            if (tempList.Count == 4 && sum == target && !ans.Any(l => l.SequenceEqual(tempList)))
            {
                ans.Add(new List<int>(tempList)); return;
            }
            else if (tempList.Count == 4) return;
            for (int i = pos; i < nums.Length; i++)
            {
                if (nums[i] + nums[nums.Length - 1] * (3 - tempList.Count) + sum < target) continue;
                if (nums[i] * (4 - tempList.Count) + sum > target) return;
                tempList.Add(nums[i]);
                Get4Sum(nums, sum + nums[i], target, ans, tempList, i + 1);
                tempList.RemoveAt(tempList.Count - 1);
            }
        }

        public static int FourSumCount(int[] A, int[] B, int[] C, int[] D)
        {
            var sumMap = new Dictionary<int, int>();

            for (int i = 0; i < A.Length; i++)
            {
                for (int j = 0; j < B.Length; j++)
                {
                    int sum = A[i] + B[j];
                    if(sumMap.ContainsKey(sum))
                    {
                        sumMap[sum] += 1;
                    }
                    else
                    {
                        sumMap.Add(sum, 1);
                    }
                }
            }

            int result = 0;
            for (int i = 0; i < C.Length; i++)
            {
                for (int j = 0; j < D.Length; j++)
                {
                    result += sumMap.ContainsKey(-1 * (C[i] + D[j]))? sumMap[-1 * (C[i] + D[j])] : 0;
                }
            }

            return result;
        }

        public static IList<IList<string>> GroupStrings(string[] strings)
        {
            IList<IList<string>> result = new List<IList<string>>();
            if(strings == null || strings.Length == 0)
            {
                return result;
            }

            var map = new Dictionary<string, List<string>>();
            bool added;
            foreach (string s in strings)
            {
                added = false;
                foreach(string m in map.Keys)
                {
                    if(s.Length == m.Length && IsMatch(s, m))
                    {
                        var list = map[m];
                        list.Add(s);
                        map[m] = list;
                        added = true;
                        break;
                    }
                }
                if(!added)
                {
                    map.Add(s, new List<string> { s });
                }
            }

            foreach(var list in map.Values)
            {
                result.Add(list);
            }

            return result;
        }

        private static bool IsMatch(string s, string m)
        {
            if (s.Length == 1)
            {
                return true;
            }

            for(int i = 0; i < s.Length - 1 ; i++)
            {
                if (Math.Abs(s[i] - s[i + 1] + 26) % 26 != Math.Abs(m[i] - m[i + 1] + 26) % 26)
                {
                    return false;
                }
            }

            return true;
        }

        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            var map = new Dictionary<string, List<string>>();
            bool added;
            foreach(string s in strs)
            {
                added = false;
                foreach(string k in map.Keys)
                {
                    if(k.Length == s.Length && IsAnagram(s,k))
                    {
                        var list = map[k];
                        list.Add(s);
                        map[k] = list;
                        added = true;
                        break;
                    }
                }

                if(!added)
                {
                    map.Add(s, new List<string> { s });
                }
            }

            IList<IList<string>> result = new List<IList<string>>();

            foreach(string key in map.Keys)
            {
                result.Add(map[key]);
            }

            return result;

        }

        private static bool IsAnagram(string s, string k)
        {
            var allChars = new int[256];

            foreach(char c in s)
            {
                allChars[c]++;
            }

            foreach(char c in k)
            {
                allChars[c]--;
                if(allChars[c] < 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            var numsMap = new Dictionary<int, int>();
            for(int i = 0; i < nums.Length; i++)
            {
                if(numsMap.ContainsKey(nums[i]))
                {
                    if(Math.Abs(numsMap[nums[i]] - i) <= k)
                    {
                        return true;
                    }

                    numsMap[nums[i]] = i;
                }
                else
                {
                    numsMap.Add(nums[i], i);
                }
            }

            return false;
        }

        public static int FirstUniqChar(string s)
        {
            if(string.IsNullOrWhiteSpace(s))
            {
                return -1;
            }

            var map = new Dictionary<char, int>();
            for(int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if(map.ContainsKey(c))
                {
                    map[c] = -1;
                }
                else
                {
                    map.Add(c, i);
                }
            }

            var ans = s.Length;
            foreach(var val in map.Values)
            {
                if(val != -1 && val < ans)
                {
                    ans = val;
                }
            }

            return ans == s.Length ? -1 : ans;
        }
    }
}
