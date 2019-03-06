using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    class Palindrome
    {
        private int low, maxLen;

        public static void Main()
        {
            var obj = new Palindrome();
            var lps = obj.LongestPalindrome("babad");
            var x = 0;
            Console.WriteLine(IsPalindrome(x));
            var xx = IsPalindromeNumber(54545);
            Console.ReadLine();
        }

        public bool CanPermutePalindrome(string s)
        {
            var set = new HashSet<char>();
            for (int i = 0; i < s.Length; ++i)
            {
                if (!set.Contains(s[i]))
                    set.Add(s[i]);
                else
                    set.Remove(s[i]);
            }
            return set.Count() == 0 || set.Count() == 1;
        }
        
        public string LongestPalindrome(string s)
        {
            int len = s.Length;
            if (len < 2)
                return s;

            for (int i = 0; i < len - 1; i++)
            {
                ExtendPalindrome(s, i, i);
                ExtendPalindrome(s, i, i + 1);
            }
            return s.Substring(low, maxLen);
        }

        private void ExtendPalindrome(string s, int j, int k)
        {
            while (j >= 0 && k < s.Length && s[j] == s[k])
            {
                j--;
                k++;
            }
            if (maxLen <= k - j - 1)
            {
                low = j + 1;
                maxLen = k - j - 1;
            }
        }

        public static bool IsPalindrome(int x)
        {
            if (x < 0)
            {
                return false;
            }

            var charNum = x.ToString().ToCharArray();
            var length = charNum.Length;
            for(int i=0; i<length/2; i++)
            {
                if(charNum[i] != charNum[length-1-i])
                {
                    return false;
                }
            }
            return true;
        }

        static bool IsPalindromeNumber(int n)
        {
            // Find the appropriate divisor 
            // to extract the leading digit 
            int divisor = 1;
            while (n / divisor >= 10)
                divisor *= 10;

            while (n != 0)
            {
                int leading = n / divisor;
                int trailing = n % 10;

                // If first and last digits are 
                // not same then return false 
                if (leading != trailing)
                    return false;

                // Removing the leading and trailing 
                // digits from the number 
                n = (n % divisor) / 10;

                // Reducing divisor by a factor 
                // of 2 as 2 digits are dropped 
                divisor = divisor / 100;
            }
            return true;
        }
    }
}
