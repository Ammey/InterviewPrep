using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    class MinStack
    {
        Node top;

        class Node
        {
            public int value;
            public int minSoFar;
            public Node next;
            public Node(int x)
            {
                value = x;
                minSoFar = x;
            }
        }

        public class UndirectedGraphNode
        {
            public int label;
            public IList<UndirectedGraphNode> neighbors;
            public UndirectedGraphNode(int x) { label = x; neighbors = new List<UndirectedGraphNode>(); }
        };

        private static void Main()
        {
            // "2[z]2[2[y]pq1[2[jk]e1[f]]]ef"
            var decoded = DecodeString("2[z]2[2[y]pq1[2[jk]e1[f]]]ef");
            var node = new UndirectedGraphNode(0);
            node.neighbors = new List<UndirectedGraphNode> { node, node };
            var clone = CloneGraph(node);
            var postfix = EvalRPN(new string[] { "10", "6", "9", "3", "+", "-11", "*", "/", "*", "17", "+", "5", "+" });
            var temperatures = new int[] { 73, 74, 75, 71, 69, 72, 76, 73 };
            var result = DailyTemperatures(temperatures);

            var mStack = new MinStack();
            mStack.Push(2147483646);
            mStack.Push(2147483646);
            mStack.Push(2147483647);
            var top = mStack.Top();
            mStack.Pop();
            var getMin = mStack.GetMin();
            mStack.Pop();
            getMin = mStack.GetMin();
            mStack.Pop();
            mStack.Push(2147483647);
            top = mStack.Top();
            getMin = mStack.GetMin();
            mStack.Push(-2147483648);
            top = mStack.Top();
            getMin = mStack.GetMin();
            mStack.Pop();
            getMin = mStack.GetMin();
        }

        public static string DecodeString(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return s;
            }

            var decodeStack = new Stack<char>();
            var result = new StringBuilder();

            foreach (char c in s)
            {
                if (!c.Equals(']'))
                {
                    decodeStack.Push(c);
                    continue;
                }
                else
                {
                    var temp = new StringBuilder();
                    while (decodeStack.Peek() != '[')
                    {
                        temp.Append(decodeStack.Pop());
                    }

                    var word = temp.ToString().Reverse();
                    decodeStack.Pop();

                    temp = new StringBuilder();
                    while(decodeStack.Any() && Char.IsDigit(decodeStack.Peek()))
                    {
                        temp.Insert(0, decodeStack.Pop());
                    }

                    var digit = int.Parse(temp.ToString());
                    while (digit != 0)
                    {
                        foreach (char ch in word)
                        {
                            decodeStack.Push(ch);
                        }
                        digit--;
                    }
                }
            }

            while(decodeStack.Any())
            {
                result.Insert(0, decodeStack.Pop());
            }

            return result.ToString();
        }

        static IDictionary<UndirectedGraphNode, UndirectedGraphNode> nodeMap = new Dictionary<UndirectedGraphNode, UndirectedGraphNode>();

        public static UndirectedGraphNode CloneGraph(UndirectedGraphNode node)
        {
            return DFSClone(node);
        }

        private static UndirectedGraphNode DFSClone(UndirectedGraphNode node)
        {
            if(node == null)
            {
                return null;
            }

            if(nodeMap.ContainsKey(node))
            {
                return nodeMap[node];
            }

            var newNode = new UndirectedGraphNode(node.label);
            nodeMap.Add(node, newNode);
            foreach(var n in node.neighbors)
            {
                var childNode = DFSClone(n);
                newNode.neighbors.Add(childNode);
            }

            return newNode;
        }

        public static int EvalRPN(string[] tokens)
        {
            var result = 0;
            var numStack = new Stack<int>();

            foreach(string token in tokens)
            {
                switch(token)
                {
                    case "+":
                        result = numStack.Pop() + numStack.Pop();
                        numStack.Push(result);
                        break;
                    case "*":
                        result = numStack.Pop() * numStack.Pop();
                        numStack.Push(result);
                        break;
                    case "/":
                        var num1 = numStack.Pop();
                        var num2 = numStack.Pop();
                        numStack.Push(num2 / num1);
                        break;
                    case "-":
                        var sub1 = numStack.Pop();
                        var sub2 = numStack.Pop();
                        numStack.Push(sub2 - sub1);
                        break;
                    default :
                        numStack.Push(int.Parse(token));
                        break;
                }
            }

            return numStack.Pop();
        }

        public static int[] DailyTemperatures(int[] temperatures)
        {
            var count = temperatures.Count();
            var result = new int[count];
            var tStack = new Stack<Tuple<int,int>>(count);

            for(int i=0; i < count; i++)
            {
                var curTemp = temperatures[i];
                if (!tStack.Any())
                {
                    tStack.Push(new Tuple<int, int>(curTemp, i));
                    continue;
                }

                var top = tStack.Peek();

                if(top.Item1 >= temperatures[i])
                {
                    tStack.Push(new Tuple<int, int>(curTemp, i));
                }
                else
                {
                    while(tStack.Any() && tStack.Peek().Item1 < curTemp)
                    {
                        top = tStack.Pop();
                        result[top.Item2] = i - top.Item2;
                    }

                    tStack.Push(new Tuple<int, int>(curTemp, i));
                }
            }

            return result;
        }

        /** initialize your data structure here. */
        public MinStack()
        {
        }

        public void Push(int x)
        {
            var newNode = new Node(x)
            {
                next = top
            };

            if (top != null)
            {
                newNode.minSoFar = newNode.value < top.minSoFar ? newNode.value : top.minSoFar;
            }
            top = newNode;
        }

        public void Pop()
        {
            if(top != null)
            {
                top = top.next;
            }
        }

        public int Top()
        {
            if (top != null)
            {
                return top.value;
            }

            return 0;
        }

        public int GetMin()
        {
            return top.minSoFar;
        }
    }
}
