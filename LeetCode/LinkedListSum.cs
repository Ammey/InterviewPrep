using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//You are given two non-empty linked lists representing two non-negative integers.The digits are stored in reverse order and each of their nodes contain a single digit.Add the two numbers and return it as a linked list.

//You may assume the two numbers do not contain any leading zero, except the number 0 itself.

//Example

//Input: (2 -> 4 -> 3) + (5 -> 6 -> 4)
//Output: 7 -> 0 -> 8
//Explanation: 342 + 465 = 807.
// 42 + 465 = 407
namespace LeetCode
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
        public ListNode(int x, ListNode node) { val = x; next = node; }
    }

    class LinkedListSum
    {
        public static void Main()
        {
            var list1 = new ListNode(1);
            list1.next = new ListNode(2);
            

            var list2 = new ListNode(3);
            list2.next = new ListNode(4);
            list1.next.next = list2;
            ReorderList(list1);
            var ans = AddTwoNumbers(list1, list2);
        }

        public ListNode MiddleNode(ListNode head)
        {
            if(head == null)
            {
                return null;
            }

            var slow = head;
            var fast = head;

            while(fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }

            return slow;
        }

        public ListNode SortList(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }

            var head2 = Split(head);

            var p1 = SortList(head);
            var p2 = SortList(head2);

            return MergeLists(p1, p2);
        }

        private ListNode MergeLists(ListNode l1, ListNode l2)
        {
            ListNode l = new ListNode(0), p = l;

            while (l1 != null && l2 != null)
            {
                if (l1.val < l2.val)
                {
                    p.next = l1;
                    l1 = l1.next;
                }
                else
                {
                    p.next = l2;
                    l2 = l2.next;
                }
                p = p.next;
            }

            if (l1 != null)
                p.next = l1;

            if (l2 != null)
                p.next = l2;

            return l.next;
        }

        public ListNode Split(ListNode head)
        {
            ListNode slow = head;
            ListNode fast = head.next;

            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }

            ListNode head2 = slow.next;
            slow.next = null;
            return head2;
        }

        public static ListNode Reverse(ListNode head)
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

        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode temp = new ListNode(0);
            ListNode ans = temp;
            int carry = 0;
            int sum = 0;

            while (l1 != null || l2 != null)
            {
                var val1 = l1 != null ? l1.val : 0;
                var val2 = l2 != null ? l2.val : 0;
                sum = carry + val1 + val2;

                temp.next = new ListNode(sum % 10);

                carry = sum / 10;
                if(l1 != null)
                {
                    l1 = l1.next;
                }

                if (l2 != null)
                {
                    l2 = l2.next;
                }

                temp = temp.next;
            }

            if(carry != 0)
            {
                temp.next = new ListNode(carry);
            }

            return ans.next;
        }
    }
}
