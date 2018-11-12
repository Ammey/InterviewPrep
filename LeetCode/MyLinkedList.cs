using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class MyLinkedList
    {

        class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }

        ListNode head;

        /** Initialize your data structure here. */
        public MyLinkedList()
        {
            head = null;
        }

        /** Get the value of the index-th node in the linked list. If the index is invalid, return -1. */
        public int Get(int index)
        {
            var temp = head;

            while (temp != null && index > 0)
            {
                temp = temp.next;
                index--;
            }

            if (index != 0 || temp == null)
            {
                return -1;
            }

            return temp.val;
        }

        /** Add a node of value val before the first element of the linked list. After the insertion, the new node will be the first node of the linked list. */
        public void AddAtHead(int val)
        {
            var temp = head;
            var newHead = new ListNode(val);
            newHead.next = temp;
            head = newHead;
        }

        /** Append a node of value val to the last element of the linked list. */
        public void AddAtTail(int val)
        {

            var tailNode = new ListNode(val);
            var temp = head;
            while (temp.next != null)
            {
                temp = temp.next;
            }

            temp.next = tailNode;
        }

        /** Add a node of value val before the index-th node in the linked list. If index equals to the length of linked list, the node will be appended to the end of linked list. If index is greater than the length, the node will not be inserted. */
        public void AddAtIndex(int index, int val)
        {
            if (index == 0)
            {
                AddAtHead(val);
                return;
            }

            var temp = head;

            while (temp != null && index != 1)
            {
                temp = temp.next;
                index--;
            }

            if (temp != null && index == 1)
            {
                var newNode = new ListNode(val);
                newNode.next = temp.next;
                temp.next = newNode;
            }
        }

        /** Delete the index-th node in the linked list, if the index is valid. */
        public void DeleteAtIndex(int index)
        {
            if (index == 0)
            {
                return;
            }

            var temp = head;

            while (temp != null && index != 1)
            {
                temp = temp.next;
                index--;
            }

            if (temp.next != null && index == 1)
            {
                temp.next = temp.next.next;
            }
        }

        private static void Main()
        {
            MyLinkedList obj = new MyLinkedList();
            obj.AddAtHead(1);
            obj.AddAtIndex(1, 2);
            var ans = obj.Get(1);
            ans = obj.Get(0);
            ans = obj.Get(2);
        }
    }
}
