using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class Logger
    {
        IDictionary<string, int> MessageMap;

        /** Initialize your data structure here. */
        public Logger()
        {
            MessageMap = new Dictionary<string, int>();
        }

        /** Returns true if the message should be printed in the given timestamp, otherwise returns false.
            If this method returns false, the message will not be printed.
            The timestamp is in seconds granularity. */
        public bool ShouldPrintMessage(int timestamp, string message)
        {
            if(MessageMap.ContainsKey(message))
            {
                if(timestamp - MessageMap[message] < 10)
                {
                    return false;
                }
                else
                {
                    MessageMap[message] = timestamp;
                    return true;
                }
            }

            MessageMap.Add(message, timestamp);
            return true;
        }

        public static void Main()
        {
            Logger logger = new Logger();

            // logging string "foo" at timestamp 1
            Console.WriteLine(logger.ShouldPrintMessage(1, "foo")); ///returns true;

            // logging string "bar" at timestamp 2
            Console.WriteLine(logger.ShouldPrintMessage(2, "bar")); //returns true;

            // logging string "foo" at timestamp 3
            Console.WriteLine(logger.ShouldPrintMessage(3, "foo")); //returns false;

            // logging string "bar" at timestamp 8
            Console.WriteLine(logger.ShouldPrintMessage(8, "bar")); ///returns false;

            // logging string "foo" at timestamp 10
            Console.WriteLine(logger.ShouldPrintMessage(10, "foo")); //returns false;

            // logging string "foo" at timestamp 11
            Console.WriteLine(logger.ShouldPrintMessage(11, "foo"));// returns true;

            Console.ReadLine();
        }
    }
}
