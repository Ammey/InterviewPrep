using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    class CircularQueue
    {
        public class MyCircularQueue
        {
            int[] data;
            int head, tail, size;
            Queue<int> avgQueue;

            /** Initialize your data structure here. Set the size of the queue to be k. */
            public MyCircularQueue(int k)
            {
                avgQueue = new Queue<int>(k);
                data = new int[k];
                head = -1;
                tail = -1;
                size = k;
            }

            /** Insert an element into the circular queue. Return true if the operation is successful. */
            public bool EnQueue(int value)
            {
                if(tail == -1)
                {
                    head = 0;
                }
                else
                {
                    var pos = (tail + 1) % size;
                    if (pos == head)
                    {
                        return false;
                    }
                }

                tail = (tail + 1) % size;
                data[tail] = value;
                return true;
            }

            /** Delete an element from the circular queue. Return true if the operation is successful. */
            public bool DeQueue()
            {
                if(head == -1)
                {
                    return false;
                }

                if (head == tail)
                {
                    head = tail = -1;
                }
                else
                {
                    head = (head + 1) % size;
                }

                return true;
            }

            /** Get the front item from the queue. */
            public int Front()
            {
                if(head != -1)
                {
                    return data[head];
                }

                return -1;
            }

            /** Get the last item from the queue. */
            public int Rear()
            {
                if (tail != -1)
                {
                    return data[tail];
                }

                return -1;
            }

            /** Checks whether the circular queue is empty or not. */
            public bool IsEmpty()
            {
                if(head == -1)
                {
                    return true;
                }

                return false;
            }

            /** Checks whether the circular queue is full or not. */
            public bool IsFull()
            {
                if ((tail + 1)% size == head)
                {
                    return true;
                }

                return false;
            }
        }

        public static void Main()
        {
            MyCircularQueue circularQueue = new MyCircularQueue(3); // set the size to be 3
            var value = circularQueue.EnQueue(1);  // return true
            value = circularQueue.EnQueue(2);  // return true
            value = circularQueue.EnQueue(3);  // return true
            value = circularQueue.EnQueue(4);  // return false, the queue is full
            int dat = circularQueue.Rear();  // return 3
            value = circularQueue.IsFull();  // return true
            value = circularQueue.DeQueue();  // return true
            value = circularQueue.EnQueue(4);  // return true
            dat = circularQueue.Rear();  // return 4
        }
    }
}
