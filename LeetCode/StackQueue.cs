using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    class StackQueue
    {
        private static void Main()
        {
            var s = new MyStack();
            s.Push(1);
            s.Push(2);
            var top = s.Pop();
            top = s.Top();
            var sEmpty = s.Empty();
            var q = new MyQueue();
            q.Push(1);
            q.Push(2);
            var peek = q.Peek();
            peek = q.Pop();
            var empty = q.Empty();
        }

        public class MyStack
        {
            Queue<int> q1;
            Queue<int> q2;

            /** Initialize your data structure here. */
            public MyStack()
            {
                q1 = new Queue<int>();
                q2 = new Queue<int>();
            }

            /** Push element x onto stack. */
            public void Push(int x)
            {
                if(Empty())
                {
                    q1.Enqueue(x);
                    return;
                }

                q2 = new Queue<int>();
                q2.Enqueue(x);

                while(!Empty())
                {
                    q2.Enqueue(q1.Dequeue());
                }

                q1 = q2;
            }

            /** Removes the element on top of the stack and returns that element. */
            public int Pop()
            {
                return q1.Dequeue();
            }

            /** Get the top element. */
            public int Top()
            {
                return q1.Peek();
            }

            /** Returns whether the stack is empty. */
            public bool Empty()
            {
                if(q1.Any())
                {
                    return false;
                }

                return true;
            }
        }

        public class MyQueue
        {
            Stack<int> stack1;
            Stack<int> stack2;

            /** Initialize your data structure here. */
            public MyQueue()
            {
                stack1 = new Stack<int>();
                stack2 = new Stack<int>();
            }

            /** Push element x to the back of queue. */
            public void Push(int x)
            {
                if (Empty())
                {
                    stack1.Push(x);
                }
                else
                {
                    while (!Empty())
                    {
                        stack2.Push(stack1.Pop());
                    }

                    stack2.Push(x);

                    while (stack2.Any())
                    {
                        stack1.Push(stack2.Pop());
                    }
                }
            }

            /** Removes the element from in front of queue and returns that element. */
            public int Pop()
            {
                return stack1.Pop();
            }

            /** Get the front element. */
            public int Peek()
            {
                return stack1.Peek();
            }

            /** Returns whether the queue is empty. */
            public bool Empty()
            {
                if (stack1.Count == 0)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
