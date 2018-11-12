using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
    public class TrieNode
    {
        public IDictionary<char, TrieNode> children;
        public bool isWord;
        public int value;

        public TrieNode()
        {
            children = new Dictionary<char, TrieNode>();
        }
    }

    public class MapSum
    {
        TrieNode tNode;
        IDictionary<string, int> seen;

        /** Initialize your data structure here. */
        public MapSum()
        {
            tNode = new TrieNode();
            seen = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        }

        public void Insert(string key, int val)
        {
            var cur = tNode;
            if(seen.ContainsKey(key))
            {
                val = val - seen[key];
            }
            else
            {
                seen.Add(key, val);
            }

            foreach(char c in key)
            {
                if(cur.children.ContainsKey(c))
                {
                    cur.children[c].value = cur.children[c].value + val;
                }
                else
                {
                    cur.children.Add(c, new TrieNode());
                    cur.children[c].value = val;
                }

                cur = cur.children[c];
            }
        }

        public int Sum(string prefix)
        {
            var cur = tNode;
            int sum = 0;
            foreach (char c in prefix)
            {
                if(cur.children.ContainsKey(c))
                {
                    cur = cur.children[c];
                }
                else
                {
                    return 0;
                }
                sum = cur.value;
            }

            return sum;
        }
    }

    public class Trie
    {
        TrieNode tNode;

        public static void Main()
        {
            var xorSet = new int[] { 3, 10, 5, 25, 2, 8 };
            var ans = FindMaximumXOR(xorSet);
            var dict = new List<string> { "a", "b", "c" };
            var sentence = "aadsfasf absbs bbab cadsfafs";
            var output = ReplaceWords(dict, sentence);
            var trieSum = new MapSum();
            trieSum.Insert("a", 3);
            var sum = trieSum.Sum("ap");

            trieSum.Insert("apple", 3);
            var search = trieSum.Sum("app");   // returns true
            trieSum.Insert("app", 2);
            search = trieSum.Sum("app");     // returns true
            trieSum.Insert("app", 4);
            search = trieSum.Sum("appl");
            search = trieSum.Sum("apple");

            //var trie = new Trie();
            //trie.Insert("apple");
            //var search = trie.Search("apple");   // returns true
            //search = trie.Search("app");     // returns false
            //search = trie.StartsWith("app"); // returns true
            //trie.Insert("app");
            //search = trie.Search("app");     // returns true
        }

        public static int FindMaximumXOR(int[] nums)
        {
            int max = 0, mask = 0;
            for (int i = 31; i >= 0; i--)
            {
                mask = mask | (1 << i);
                var set = new HashSet<int>();
                foreach (int num in nums)
                {
                    set.Add(num & mask);
                }
                int tmp = max | (1 << i);
                foreach (int prefix in set)
                {
                    if (set.Contains(tmp ^ prefix))
                    {
                        max = tmp;
                        break;
                    }
                }
            }
            return max;
        }

        public static string ReplaceWords(IList<string> dict, string sentence)
        {
            dict = dict.OrderBy(s => s.Length).ToList();

            var split = sentence.Split(' ');
            foreach(string root in dict)
            {
                for (int i= 0; i < split.Length; i++)
                {
                    if(split[i].StartsWith(root))
                    {
                        split[i] = root;
                    }
                }
            }

            return string.Join(" ", split);
        }

        /** Initialize your data structure here. */
        public Trie()
        {
            tNode = new TrieNode();
        }

        /** Inserts a word into the trie. */
        public void Insert(string word)
        {
            var cur = tNode;
            foreach(char c in word)
            {
                if(!cur.children.ContainsKey(c))
                {
                    cur.children.Add(c, new TrieNode());
                }

                cur = cur.children[c];
            }
            cur.isWord = true;
        }

        /** Returns if the word is in the trie. */
        public bool Search(string word)
        {
            var cur = tNode;
            foreach (char c in word)
            {
                if (cur.children.ContainsKey(c))
                {
                    cur = cur.children[c];
                }
                else
                {
                    return false;
                }
            }

            return cur.isWord;

        }

        /** Returns if there is any word in the trie that starts with the given prefix. */
        public bool StartsWith(string prefix)
        {
            var cur = tNode;
            foreach (char c in prefix)
            {
                if (cur.children.ContainsKey(c))
                {
                    cur = cur.children[c];
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class WordDictionary
    {
        public class TrieNode
        {
            public TrieNode[] children = new TrieNode[26];
            public String item = "";
        }

        private TrieNode root = new TrieNode();

        public void AddWord(String word)
        {
            TrieNode node = root;
            foreach (char c in word)
            {
                if (node.children[c - 'a'] == null)
                {
                    node.children[c - 'a'] = new TrieNode();
                }
                node = node.children[c - 'a'];
            }
            node.item = word;
        }

        public bool Search(String word)
        {
            return Match(word.ToCharArray(), 0, root);
        }

        private bool Match(char[] chs, int k, TrieNode node)
        {
            if (k == chs.Length) return !node.item.Equals("");
            if (chs[k] != '.')
            {
                return node.children[chs[k] - 'a'] != null && Match(chs, k + 1, node.children[chs[k] - 'a']);
            }
            else
            {
                for (int i = 0; i < node.children.Length; i++)
                {
                    if (node.children[i] != null)
                    {
                        if (Match(chs, k + 1, node.children[i]))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
