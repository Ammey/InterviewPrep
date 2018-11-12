using System;
using System.Collections.Generic;

namespace LeetCode
{
    class ReverseLinkedList
    {
        public static void Main()
        {
            //var a = ChampagneTower(2, 0, 0);
            //var numSquares = CountPrimes(16);
            //var nums = new[] { 1, 3, 4, 2, 2 };
            //var findDuplicate = FindDuplicate(nums);
            //var uniqueNum = MissingNumber(nums);

            var list1 = new ListNode(1);
            list1.next = new ListNode(3);
            list1.next.next = new ListNode(4);
            list1.next.next.next = new ListNode(5);
            var list2 = new ListNode(0);
            list2.next = new ListNode(1);
            //list2.next.next = new ListNode(2);
            //list2.next.next.next = new ListNode(8);

            var list3 = new ListNode(0);
            var node3 = new ListNode(1);
            var node4 = new ListNode(2);

            list3.next = node3;
            node3.next = node4;
            //node4.next = list3;

            var rotate = RotateRight(list3, 4);
            var cyclic = Insert(list3, 0);
            var sortedList = MergeTwoLists(list1, list2);
            var oddEven = OddEvenList(list1);
            var node = RemoveElements(list1, 5);
            var ans = ReverseBetween(list1, 1, 5);
            Console.ReadLine();
        }

        public static double ChampagneTower(int poured, int query_row, int query_glass)
        {
           var tower = new double[101, 101];
            tower[0,0] = poured;

            for (int i = 0; i <= query_row; ++i)
            {
                for (int j = 0; j <= i; ++j)
                {
                    var current = (tower[i,j] - 1.0) / 2.0;
                    if (current > 0)
                    {
                        tower[i + 1, j] += current;
                        tower[i + 1, j + 1] += current;
                    }
                }
            }

            return Math.Min(1, tower[query_row, query_glass]);
        }

        public static int CountPrimes(int n)
        {
            // Create a boolean array "prime[0..n]" and initialize
            // all entries it as true. A value in prime[i] will
            // finally be false if i is Not a prime, else true.

            bool[] prime = new bool[n + 1];

            for (int i = 0; i < n; i++)
                prime[i] = true;

            for (int p = 2; p * p <= n; p++)
            {
                // If prime[p] is not changed,
                // then it is a prime
                if (prime[p] == true)
                {
                    // Update all multiples of p
                    for (int i = p * 2; i <= n; i += p)
                        prime[i] = false;
                }
            }

            int totalCount = 0;
            for (int i = 2; i <= n; i++)
            {
                if (prime[i] == true)
                    totalCount++;
            }

            return totalCount;
        }

        public static int NumSquares(int n)
        {
            var numSquares = new int[n+1];
            numSquares[0] = 0;
            numSquares[1] = 1;
            numSquares[2] = 2;
            numSquares[3] = 3;

            for(int i=4; i<=n; i++)
            {
                numSquares[i] = i;

                for(int j=1; j<i; j++)
                {
                    var sum = j * j;

                    if(sum > i)
                    {
                        break;
                    }

                    numSquares[i] = Math.Min(numSquares[i], 1 + numSquares[i - sum]);
                }
            }
            return numSquares[n];
        }

        public static int NthUglyNumber(int n)
        {
            int[] uglynos = new int[n];
            int i2 = 0, i3 = 0, i5 = 0;
            int ugly2 = 2;
            int ugly3 = 3;
            int ugly5 = 5;

            uglynos[0] = 1;
            for (int i=1; i<n; i++)
            {
                int nextUgly = Math.Min(ugly2, Math.Min(ugly3, ugly5));
                uglynos[i] = nextUgly;

                if (nextUgly == ugly2)
                {
                    i2++;
                    ugly2 = uglynos[i2] * 2;
                }
                if (nextUgly == ugly3)
                {
                    i3++;
                    ugly3 = uglynos[i3] * 3;
                }
                if (nextUgly == ugly5)
                {
                    i5++;
                    ugly5 = uglynos[i5] * 5;
                }
            }

            return uglynos[n - 1];
        }

        public static int MissingNumber(int[] nums)
        {
            var length = nums.Length;
            var total = (length * (length + 1)) / 2;

            var sum = 0;
            foreach(int num in nums)
            {
                sum = sum + num;
            }

            return total - sum;
        }

        public static int FindDuplicate(int[] nums)
        {
            for (int i= 0; i < nums.Length; i++)
            {
                if(nums[Math.Abs(nums[i])] < 0)
                {
                    return Math.Abs(nums[i]);
                }
                nums[Math.Abs(nums[i])] = -nums[Math.Abs(nums[i])];
            }
            return 0;
        }

        public int FindDuplicate2(int[] nums)
        {
            // Find the intersection point of the two runners.
            int tortoise = nums[0];
            int hare = nums[0];
            do
            {
                tortoise = nums[tortoise];
                hare = nums[nums[hare]];
            } while (tortoise != hare);

            // Find the "entrance" to the cycle.
            int ptr1 = nums[0];
            int ptr2 = tortoise;
            while (ptr1 != ptr2)
            {
                ptr1 = nums[ptr1];
                ptr2 = nums[ptr2];
            }

            return ptr1;
        }

        public static int[] SetMismatch(int[] nums)
        {
            var length = nums.Length;
            var total = (length * (length + 1)) / 2;

            var seenNumbers = new Dictionary<int, int>();
            var sum = 0;
            var duplicateNum = 0;
            foreach (int num in nums)
            {
                if(seenNumbers.ContainsKey(num))
                {
                    duplicateNum = num;
                }
                else
                {
                    seenNumbers.Add(num, 1);
                }

                sum = sum + num;
            }

            return new int[] { duplicateNum, ((total - sum) + duplicateNum)};
        }

        public static ListNode DetectCycle(ListNode head)
        {
            var tortoise = head;
            var hare = head;
            bool cycleFound = false;

            while (hare != null && hare.next != null)
            {
                tortoise = tortoise.next;
                hare = hare.next.next;
                if (tortoise == hare)
                {
                    //cycle detected
                    cycleFound = true;
                    break;
                }
            }

            if (cycleFound)
            {
                hare = head;
                while (tortoise != hare)
                {
                    tortoise = tortoise.next;
                    hare = hare.next;
                }

                return tortoise;
            }

            return null;
        }

        public static ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            var l1 = 0;
            var l2 = 0;

            var temp = headA;
            while (temp != null)
            {
                temp = temp.next;
                l1++;
            }

            temp = headB;
            while (temp != null)
            {
                temp = temp.next;
                l2++;
            }

            if (l1 == 0 || l2 == 0)
            {
                return null;
            }

            var diff = 0;
            if (l1 > l2)
            {
                diff = l1 - l2;
                while (diff != 0 && headA.next != null)
                {
                    headA = headA.next;
                    diff--;
                }
            }
            else if (l1 < l2)
            {
                diff = l2 - l1;
                while (diff != 0 && headB.next != null)
                {
                    headB = headB.next;
                    diff--;
                }
            }

            if (diff != 0)
            {
                return null;
            }

            while (headA != null && headB != null)
            {
                if (headA == headB)
                {
                    return headA;
                }

                headA = headA.next;
                headB = headB.next;
            }

            return null;
        }

        public static ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            if (head == null)
                return null;

            var fastPointer = head;
            var slowPointer = head;

            while (n != 0 && fastPointer != null)
            {
                fastPointer = fastPointer.next;
                n--;
            }

            if (n != 0)
            {
                return head;
            }

            if (fastPointer == null)
            {
                return head.next;
            }

            while (fastPointer.next != null)
            {
                fastPointer = fastPointer.next;
                slowPointer = slowPointer.next;
            }

            slowPointer.next = slowPointer.next.next;
            return head;
        }

        public ListNode ReverseList(ListNode head)
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

        public static ListNode OddEvenList(ListNode head)
        {
            var odd = head;
            var even = head.next;

            var evenFirst = even;
            while (even != null && even.next != null)
            {
                odd.next = odd.next.next;
                even.next = even.next.next;
                odd = odd.next;
                even = even.next;
            }

            odd.next = evenFirst;

            return head;
        }

        public static ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            var temp = new ListNode(0);
            var head = temp;
            while(l1 != null && l2 != null)
            {
                if(l1.val <= l2.val)
                {
                    temp.next = l1;
                    l1 = l1.next;
                }
                else
                {
                    temp.next = l2;
                    l2 = l2.next;
                }
                temp = temp.next;
            }

            if (l1 != null)
            {
                temp.next = l1;
            }
            else if (l2 != null)
            {
                temp.next = l2;
            }

            return head.next;
        }

        public static ListNode RemoveElements(ListNode head, int val)
        {
            if (head == null)
                return head;

            var prev = new ListNode(0)
            {
                next = head
            };

            var temp = prev;

            while (temp != null)
            {
                if (temp.next != null && temp.next.val == val)
                {
                    temp.next = temp.next.next;
                }
                else
                {
                    temp = temp.next;
                }
            }

            return prev.next;
        }

        public static ListNode Insert(ListNode head, int insertVal)
        {
            var newNode = new ListNode(insertVal);

            if (head == null)
            {
                return newNode;
            }

            var temp = head;

            // Single node case
            if (temp.next == temp)
            {
                temp.next = newNode;
                newNode.next = head;
                return head;
            }

            ListNode prev = temp;
            temp = temp.next;
            while (!(temp.val >= insertVal && prev.val <= insertVal
                || temp.val <= prev.val && insertVal <= temp.val
                || temp.val <= prev.val && insertVal >= prev.val))
            {
                prev = temp;
                temp = temp.next;
                if (temp == head)
                    break;
            }

            prev.next = new ListNode(insertVal, temp);
            return head;
        }

        public static ListNode ReverseBetween(ListNode head, int m, int n)
        {
            if(m == n)
            {
                return head;
            }

            var diff = n  - m;
            ListNode tempHead = head;
            ListNode prev = null;
            while(m != 1)
            {
                prev = tempHead;
                tempHead = tempHead.next;
                m--;
            }

            ListNode startHead = tempHead;
            ListNode previous = null;

            while (diff >= 0)
            {
                var next = tempHead.next;
                tempHead.next = previous;
                previous = tempHead;
                tempHead = next;
                diff--;
            }

            startHead.next = tempHead;
            if (prev == null)
            {
                return previous;
            }

            prev.next = previous;

            return head;
        }

        public static ListNode RotateRight(ListNode head, int k)
        {
            var dummyNode = new ListNode(0, head);
            if(head == null || head.next == null || k == 0)
            {
                return head;
            }

            var temp = dummyNode;
            int length = 0;
            while(temp.next != null)
            {
                length++;
                temp = temp.next;
            }

            k = k % length;

            var shiftNode = dummyNode;
            for(int i=0; i<length-k; i++)
            {
                shiftNode = shiftNode.next;
            }

            temp.next = head;
            var newHead = shiftNode.next;
            shiftNode.next = null;

            return newHead;
        }
    }
}
