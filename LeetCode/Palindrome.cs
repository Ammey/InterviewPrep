using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    class Palindrome
    {
        public static void Main()
        {
            var x = 0;
            Console.WriteLine(IsPalindrome(x));
            var xx = IsPalindromeNumber(54545);
            Console.ReadLine();
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
