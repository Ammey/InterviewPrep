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
    }
}
