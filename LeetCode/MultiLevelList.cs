using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    class MultiLevelList
    {
        public class RandomListNode
        {
            public int label;
            public RandomListNode next, random;
            public RandomListNode(int x) { this.label = x; }
        };

        public class Node
        {
            public int val;
            public Node prev;
            public Node next;
            public Node child;

            public Node() { }
            public Node(int _val, Node _prev, Node _next, Node _child)
            {
                val = _val;
                prev = _prev;
                next = _next;
                child = _child;
            }
        }

        public static void Main()
        {
            var node1 = new Node(1, null, null, null);
            var node2 = new Node(2, null, null, null);
            var node3 = new Node(3, null, null, null);
            var node4 = new Node(4, null, null, null);
            node1.next = node2;
            node2.prev = node1;
            node2.next = node3;
            node3.prev = node2;
            node3.next = node4;
            node4.prev = node3;


            var node7 = new Node(7, null, null, null);
            var node8 = new Node(8, null, null, null);
            var node9 = new Node(9, null, null, null);
            var node10 = new Node(10, null, null, null);
            node7.next = node8;
            node8.prev = node7;
            node8.next = node9;
            node9.prev = node8;
            node9.next = node10;
            node10.prev = node9;

            var node11 = new Node(11, null, null, null);
            var node12 = new Node(12, null, null, null);
            node11.next = node12;
            node12.prev = node11;

            node3.child = node7;
            node8.child = node11;

            var getFlattenedList = Flatten(node1);
        }

        public static RandomListNode CopyRandomList(RandomListNode head)
        {
            if (head == null)
            {
                return null;
            }

            var cur = head;

            var nodeMap = new Dictionary<RandomListNode, RandomListNode>();

            while (cur != null)
            {
                nodeMap.Add(cur, new RandomListNode(cur.label));
                cur = cur.next;
            }

            cur = head;
            while(cur != null)
            {
                nodeMap[cur].next = nodeMap[cur.next];
                nodeMap[cur].random = nodeMap[cur.random];
                cur = cur.next;
            }

            return nodeMap[head];
        }

        public static Node Flatten(Node head)
        {
            FlattenList(head);
            return head;
        }

        public static Node FlattenList(Node head)
        {
            if (head == null)
            {
                return null;
            }

            var cur = head;
            while(cur.child == null && cur.next != null)
            {
                cur = cur.next;
            }

            var nextNode = cur.next;
            if(cur.child != null)
            {
                cur.next = cur.child;
                cur.next.prev = cur;
                cur.child = null;
                cur = FlattenList(cur.next);
            }

            if(nextNode != null)
            {
                cur.next = nextNode;
                nextNode.prev = cur;
                return FlattenList(nextNode);
            }
            else
            {
                return cur;
            }
        }
    }
}
