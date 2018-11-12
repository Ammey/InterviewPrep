using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    class Strings
    {
        public static void Main()
        {
            var reverse = ReverseWords("the sky if blue ");
            var nums = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            Rotate(nums, 3);
            var min = MinSubArrayLen(7, nums);
            var ones = FindMaxConsecutiveOnes(nums);
            var remove = RemoveElement(nums, 2);
            var indexes = TwoSum(nums, 9);

            var sum = ArrayPairSum(nums);
            var strList = new[] { "flower", "fl", "bre"};
            var prefix = LongestCommonPrefix(strList);
            var pat = "AAAAB";
            var str = "AAAAAAAAAAAAAAAAAB";
            var leet = new Strings();
            var index = leet.StrStr(str, pat);
            var a = "11";
            var b = "11";
            string ans = AddBinary(a, b);
        }

        public static string ReverseWords(string s)
        {
            string[] strs = s.Split(' ');

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strs.Length; i++)
            { 
                sb.Append(string.Join("", strs[i].Reverse()));
                sb.Append(" ");
            }

            return sb.ToString().Trim();
        }

        public static string ReverseWords1(string s)
        {
            string[] strs = s.Split(' ');

            StringBuilder sb = new StringBuilder();
            for (int i = strs.Length - 1; i >= 0; i--)
            {
                if (strs[i].Trim() == string.Empty) continue;

                sb.Append(strs[i]);
                sb.Append(" ");
            }

            return sb.ToString().Trim();
        }

        public static void Rotate(int[] nums, int k)
        {
            if(nums == null || nums.Length < 2)
            {
                return;
            }

            var length = nums.Length;
            k = k % length;
            Reverse(nums, 0, length - k - 1);
            Reverse(nums, length - k, length - 1);
            Reverse(nums, 0, length - 1);
        }

        private static void Reverse(int[] nums, int start, int end)
        {
            int temp = 0;
            while (start < end)
            {
                temp = nums[start];
                nums[start] = nums[end];
                nums[end] = temp;
                start++;
                end--;
            }
        }

        public static int MinSubArrayLen(int s, int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return 0;
            }

            int i = 0, j = 0, sum = 0, min = int.MaxValue;

            while (j < nums.Length)
            {
                sum += nums[j++];

                while (sum >= s)
                {
                    min = Math.Min(min, j - i);
                    sum -= nums[i++];
                }
            }

            return min == int.MaxValue ? 0 : min;
        }

        public static int FindMaxConsecutiveOnes(int[] nums)
        {
            int maxSum = 0;

            if (nums.Length == 0)
            {
                return maxSum;
            }

            int sum = nums[0];

            for(int i = 1; i < nums.Length; i++)
            {
                if(nums[i] == 1)
                {
                    sum += 1;
                }
                else
                {
                    maxSum = maxSum < sum ? sum : maxSum;
                    sum = 0;
                }
            }

            return maxSum < sum ? sum : maxSum;
        }

        public static int RemoveElement(int[] nums, int val)
        {
            int index = 0;

            for(int i = 0; i < nums.Length; i++)
            {
                if(nums[i] != val)
                {
                    nums[index] = nums[i];
                    index++;
                }
            }

            return index;
        }

        public static int[] TwoSum(int[] numbers, int target)
        {
            int[] result = new int[2];
            IDictionary<int, int> dict = new Dictionary<int, int>
            {
                { numbers[0], 1 }
            };

            for (int i = 1; i < numbers.Length; i++)
            {
                var diff = target - numbers[i];
                if(dict.ContainsKey(diff))
                {
                    result[0] = dict[diff];
                    result[1] = i + 1;
                    break;
                }

                if(!dict.ContainsKey(numbers[i]))
                {
                    dict.Add(numbers[i], i + 1);
                }
            }

            return result;
        }

        public static int ArrayPairSum(int[] nums)
        {
            if(nums == null || nums.Length == 0)
            {
                return 0;
            }

            var length = nums.Length;

            if(length == 2)
            {
                return Math.Min(nums[0], nums[1]);
            }

            Array.Sort(nums);

            int sum = 0;
            for(int i = 0; i < length; i = i+2)
            {
                sum += Math.Min(nums[i], nums[i + 1]);
            }

            return sum;
        }

        public static string ReverseString(string s)
        {
            return string.Join("",s.Reverse());
        }

        public static string LongestCommonPrefix(string[] strs)
        {
            if(strs.Length == 0)
            {
                return string.Empty;
            }
            if(strs.Length == 1)
            {
                return strs[0];
            }

            if(strs.Length > 2)
            {
                Array.Sort(strs);
            }

            var first = strs[0];
            var last = strs[strs.Length - 1];
            var resultBuilder = new StringBuilder();

            for(int i = 0; i < first.Length; i++)
            {
                if(last.Length > i && first[i] == last[i])
                {
                    resultBuilder.Append(first[i]);
                }
                else
                {
                    break;
                }
            }
            return resultBuilder.ToString();
        }

        public int StrStr(string haystack, string needle)
        {
            if(string.IsNullOrWhiteSpace(needle))
            {
                return 0;
            }

            var patLength = needle.Length;
            var strLength = haystack.Length;

            if(patLength > strLength)
            {
                return -1;
            }

            int[] lps = GenerateLPS(needle, patLength);

            int i = 0;
            int j = 0;
            while(i < strLength)
            {
                if(haystack[i] == needle[j])
                {
                    j++;
                    i++;
                }

                if(j == patLength)
                {
                    // Match found
                    return i - j;
                }
                else if(i < strLength && haystack[i] != needle[j])
                {
                    if(j != 0)
                    {
                        j = lps[j - 1];
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            return -1;
        }

        private static int[] GenerateLPS(string pat, int patLength)
        {
            var lps = new int[patLength];
            lps[0] = 0;
            int len = 0;
            int i = 1;

            while (i < patLength)
            {
                if(pat[i] == pat[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if(len != 0)
                    {
                        len = lps[len - 1];
                    }
                    else
                    {
                        lps[i] = len;
                        i++;
                    }
                }
            }

            return lps;
        }

        public static string AddBinary(string a, string b)
        {
            if (string.IsNullOrWhiteSpace(a))
            {
                return b;
            }

            if (string.IsNullOrWhiteSpace(b))
            {
                return a;
            }

            var fArray = a.ToCharArray();
            var sArray = b.ToCharArray();
            var aLength = a.Length - 1;
            var bLength = b.Length - 1;
            var length = aLength <= bLength ? aLength : bLength;
            int carry = 0;
            var result = new List<char>();

            for (int i = 0; i <= length; i++)
            {
                var add = int.Parse(fArray[aLength - i].ToString()) + int.Parse(sArray[bLength - i].ToString()) + carry;
                if (add == 2)
                {
                    carry = 1;
                    result.Add('0');
                }
                else if (add == 3)
                {
                    carry = 1;
                    result.Add('1');
                }
                else
                {
                    carry = 0;
                    result.Add(char.Parse(add.ToString()));
                }
            }

            if (bLength != aLength)
            {
                int rem = 0;
                if (bLength > aLength)
                {
                    fArray = sArray;
                    rem = bLength - aLength;
                }
                else
                {
                    rem = aLength - bLength;
                }

                for (int i = rem - 1; i >= 0; i--)
                {
                    var add = int.Parse(fArray[i].ToString()) + carry;
                    if (add == 2)
                    {
                        carry = 1;
                        result.Add('0');
                    }
                    else if (add == 3)
                    {
                        carry = 1;
                        result.Add('1');
                    }
                    else
                    {
                        carry = 0;
                        result.Add(char.Parse(add.ToString()));
                    }
                }
            }

            if (carry == 1)
            {
                result.Add(char.Parse(carry.ToString()));
            }

            result.Reverse();
            return string.Join("", result);
        }
    }
}
