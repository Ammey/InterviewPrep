using System;

namespace LeetCode
{

    class LinkedListPalindrome
    {
        public static void Main()
        {
            var list1 = new ListNode(2);
            list1.next = new ListNode(2);
            list1.next.next = new ListNode(1);
            list1.next.next.next = new ListNode(1);
            list1.next.next.next.next = new ListNode(1);


            Console.WriteLine(IsPalindrome(list1));
            Console.ReadLine();
        }

        public static bool IsPalindrome(ListNode head)
        {
            ListNode fast = head;
            ListNode slow = head;

            while (fast != null && fast.next != null)
            {
                fast = fast.next.next;
                slow = slow.next;
            }

            if (fast != null)
            {
                slow = slow.next;
            }

            slow = reverse(slow);
            fast = head;

            while (slow != null)
            {
                if (fast.val != slow.val)
                {
                    return false;
                }
                fast = fast.next;
                slow = slow.next;
            }
            return true;
        }

        public static ListNode reverse(ListNode head)
        {
            ListNode prev = null;
            while (head != null)
            {
                ListNode next = head.next;
                head.next = prev;
                prev = head;
                head = next;
            }
            return prev;
        }
    }
}
