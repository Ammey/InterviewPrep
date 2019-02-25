

using System;
using System.Collections.Generic;

namespace LeetCode
{
    public class LRUCache
    {
        IDictionary<int, DLL> numDict;
        int count;
        int maxCount;
        DLL head;
        DLL tail;

        public class DLL
        {
            public int key;
            public int value;
            public DLL left;
            public DLL right;
        }

        public static void Main()
        {
            LRUCache cache = new LRUCache(2);

            cache.Put(1, 1);
            cache.Put(2, 2);
            var val = cache.Get(1);       // returns 1
            cache.Put(3, 3);    // evicts key 2
            val = cache.Get(2);       // returns -1 (not found)
            cache.Put(4, 4);    // evicts key 1
            val = cache.Get(1);       // returns -1 (not found)
            val = cache.Get(3);       // returns 3
            val = cache.Get(4);       // returns 4
        }

        public void AddAtHead(DLL node)
        {
            head.right.left = node;
            node.right = head.right;
            node.left = head;
            head.right = node;
        }

        private void RemoveNode(DLL node)
        {
            node.right.left = node.left;
            node.left.right = node.right;
        }

        private int RemoveTail()
        {
            var nodeToDelete = tail.left;
            RemoveNode(nodeToDelete);
            return nodeToDelete.key;
        }

        public LRUCache(int capacity)
        {
            numDict = new Dictionary<int, DLL>();
            count = 0;
            maxCount = capacity;
            head = new DLL();
            head.left = null;

            tail = new DLL();
            tail.left = head;
            tail.right = null;
            head.right = tail;
        }

        public int Get(int key)
        {
            if(!numDict.ContainsKey(key))
            {
                return -1;
            }

            var node = numDict[key];
            RemoveNode(node);
            AddAtHead(node);
            return node.value;
        }

        public void Put(int key, int value)
        {
            if(numDict.ContainsKey(key))
            {
                var node = numDict[key];
                node.value = value;
                RemoveNode(node);
                AddAtHead(node);
            }
            else
            {
                if(count >= maxCount)
                {
                    var deletedKey = RemoveTail();
                    numDict.Remove(deletedKey);
                }

                var node = new DLL
                {
                    key = key,
                    value = value
                };

                numDict.Add(key, node);
                AddAtHead(node);
                count++;
            }
        }
    }
}
