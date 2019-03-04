using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    class SlidingWindow
    {
        public static void Main()
        {
            var emails = new List<string> { "test.email+alex@leetcode.com", "test.e.mail+bob.cathy@leetcode.com", "testemail+david@lee.tcode.com" };
            var count = NumUniqueEmails(emails.ToArray());
            var fruits = new int[] { 2, 3, 2, 2 , 1};
            Console.WriteLine(TotalFruit(fruits));
            Console.ReadLine();
        }

        public static int NumUniqueEmails(string[] emails)
        {
            var uniqueAddresses = new HashSet<string>();

            foreach (var email in emails)
            {
                string[] parts = email.Split('@');
                string[] local = parts[0].Split('+');
                uniqueAddresses.Add(local[0].Replace(".", "") + "@" + parts[1]); 
            }
            return uniqueAddresses.Count();
        }

            public static int TotalFruit(int[] tree)
        {
            if(tree == null || tree.Count() == 0)
            {
                return 0;
            }

            int lastIndex = 0;
            int count = 0;
            int maxCount = 0;
            var fruitBasket = new HashSet<int>();
            for(int i = 0; i < tree.Count(); i++)
            {
                if(fruitBasket.Contains(tree[i]) || fruitBasket.Count < 2)
                {
                    if(tree[lastIndex] != tree[i])
                    {
                        lastIndex = i;
                    }
                    count++;

                    if (fruitBasket.Count < 2)
                    {
                        fruitBasket.Add(tree[i]);
                    }
                }
                else
                {
                    maxCount = Math.Max(maxCount, count);
                    count = i - lastIndex + 1;
                    fruitBasket = new HashSet<int> { tree[lastIndex], tree[i] };
                    lastIndex = i;
                }
            }

            return Math.Max(count, maxCount);
        }
    }
}
