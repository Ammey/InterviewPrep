using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class LFUCache
    {
        IDictionary<int, Node> numDict;
        IDictionary<int, DLL> frequencyDict;

        int count;
        int maxCount;

        public class Node
        {
            public int key;
            public int value;
            public int count;
            public Node prev;
            public Node next;

            public Node(int key, int val)
            {
                this.key = key;
                this.value = val;
                count = 1;
            }
        }

        public class DLL
        {
            public int size;
            public Node head;
            public Node tail;

            public DLL()
            {
                head = new Node(0, 0);
                tail = new Node(0, 0);
                head.next = tail;
                tail.prev = head;
            }

            public void AddAtHead(Node node)
            {
                head.next.prev = node;
                node.prev = head;
                node.next = head.next;
                head.next = node;

                size++;
            }

            public void RemoveNode(Node node)
            {
                node.prev.next = node.next;
                node.next.prev = node.prev;
                size--;
            }

            public Node RemoveTail()
            {
                if (size > 0)
                {
                    Node node = tail.prev;
                    RemoveNode(node);
                    return node;
                }

                return null;
            }
        }

        public static void Main()
        {
            LFUCache cache = new LFUCache(2);

            cache.Put(1, 1);
            cache.Put(2, 2);
            Console.WriteLine(cache.Get(1));       // returns 1
            cache.Put(3, 3);    // evicts key 2
            Console.WriteLine(cache.Get(2));       // returns -1 (not found)
            Console.WriteLine(cache.Get(3));       // returns 3.
            cache.Put(4, 4);    // evicts key 1.
            Console.WriteLine(cache.Get(1));       // returns -1 (not found)
            Console.WriteLine(cache.Get(3));       // returns 3
            Console.WriteLine(cache.Get(4));       // returns 4
        }

        public LFUCache(int capacity)
        {
            count = 0;
            maxCount = capacity;
            numDict = new Dictionary<int, Node>();
            frequencyDict = new SortedDictionary<int, DLL>();

        }

        public int Get(int key)
        {
            if(!numDict.ContainsKey(key))
            {
                return -1;
            }

            var node = numDict[key];
            var dll = frequencyDict[node.count];
            dll.RemoveNode(node);
            if (dll.size == 0)
            {
                frequencyDict.Remove(node.count);
            }

            node.count++;
            Update(node);

            return node.value;
        }

        public void Put(int key, int value)
        {
            if (numDict.ContainsKey(key))
            {
                var node = numDict[key];
                var dll = frequencyDict[node.count];
                dll.RemoveNode(node);
                if (dll.size == 0)
                {
                    frequencyDict.Remove(node.count);
                }

                node.value = value;
                node.count++;
                numDict[key] = node;

                Update(node);
                return;
            }

            if (count == maxCount)
            {
                var lowestKvp = frequencyDict.First();
                var removedNode = lowestKvp.Value.RemoveTail();
                numDict.Remove(removedNode.key);
                count--;
            }

            var newNode = new Node(key, value);
            Update(newNode);
            numDict.Add(key, newNode);
            count++;
        }

        public void Update(Node newNode)
        {
            if (!frequencyDict.ContainsKey(newNode.count))
            {
                frequencyDict.Add(newNode.count, new DLL());
            }

            var list = frequencyDict[newNode.count];
            list.AddAtHead(newNode);
        }
    }
}
