using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    class BinaryTree
    {
        int maxSum = int.MinValue;
        int leafSum = 0;
        int preIndex = 0;
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }

        public class Node
        {
            public int val;
            public Node left;
            public Node right;
            public Node next;

            public Node() { }
            public Node(int _val, Node _left, Node _right, Node _next)
            {
                val = _val;
                left = _left;
                right = _right;
                next = _next;
            }
        }

        public string serialize(TreeNode root)
        {
            if (root == null) return "null";
            return root.val + " " + serialize(root.left) + " " + serialize(root.right);
        }

        // Decodes your encoded data to tree.
        public TreeNode deserialize(string data)
        {
            List<TreeNode> list = new List<TreeNode>();

            if (data == "null") return null;

            string[] words = data.Split(' ');
            TreeNode root = new TreeNode(Convert.ToInt32(words[0]));
            list.Add(root);

            bool goLeft = true;
            for (int i = 1; i < words.Count(); ++i)
            {
                if (words[i] == "null")
                {
                    if (goLeft) goLeft = false;
                    else list.RemoveAt(list.Count() - 1);
                }
                else
                {
                    TreeNode node = new TreeNode(Convert.ToInt32(words[i]));
                    if (goLeft)
                    {
                        list[list.Count() - 1].left = node;
                    }
                    else
                    {
                        list[list.Count() - 1].right = node;
                        list.RemoveAt(list.Count() - 1);
                    }
                    list.Add(node);
                    goLeft = true;
                }
            }

            return root;
        }

        public static void Main()
        {
            var nTreeNode = new Node(4, null, null, null);
            var nnode2 = new Node(9, null, null, null);
            var nnode3 = new Node(10, null, null, null);
            var nnode4 = new Node(5, null, null, null);
            var nnode5 = new Node(1, null, null, null);
            nnode2.right = nnode5;
            nnode2.left = nnode4;
            nTreeNode.right = nnode3;
            nTreeNode.left = nnode2;
            var obj = new BinaryTree();
            var next = obj.Connect(nTreeNode);
            var preorder = new int[] { 3, 9, 20, 15, 7 };
            var inorder = new int[] { 9, 3, 15, 20, 7 };
            var postOrder = new int[] { 9, 15, 7, 20, 3 };
            var pRoot = obj.BuildTree(inorder, postOrder);
            var cRoot = obj.BuildPreOrderTree(preorder, inorder);
            var treeNode = new TreeNode(4);
            var node2 = new TreeNode(9);
            var node3 = new TreeNode(10);
            var node4 = new TreeNode(5);
            var node5 = new TreeNode(1);
            node2.right = node5;
            node2.left = node4;
            treeNode.right = node3;
            treeNode.left = node2;
            var leaves = obj.FindLeaves(treeNode);
            var zigZag = obj.AverageOfLevels(treeNode);
            var allPaths = obj.BinaryTreePaths(treeNode);
            var resultList = obj.PathSum2(treeNode, 14);
            bool hasSum = obj.HasPathSum(treeNode, 18);
            obj.SumNumbers(treeNode);

            treeNode = new TreeNode(-10);
            node2 = new TreeNode(20);
            node3 = new TreeNode(20);
            node4 = new TreeNode(15);
            node5 = new TreeNode(7);
            node3.left = node4;
            node3.right = node5;
            treeNode.left = node2;
            treeNode.right = node3;
            //var depth = obj.MaxDepth(treeNode);
            //var sum = obj.MaxPathSum(treeNode);
            //var inorderList = InorderTraversal(treeNode);
            //var preOrderList = PreorderTraversal(treeNode);
            //var postOrderList = PostorderTraversal(treeNode);
            //var levelOrderLists = LevelOrder(treeNode);
            var lca = obj.LowestCommonAncestor(treeNode, node2, node4);
        }

        public IList<IList<int>> FindLeaves(TreeNode root)
        {
            var result = new List<IList<int>>();

            if(root == null)
            {
                return result;
            }

            while (root != null)
            {
                var leaves = new List<int>();
                root = RemoveLeaves(root, leaves);
                result.Add(leaves);
            }

            return result;
        }

        private TreeNode RemoveLeaves(TreeNode root, IList<int> cur)
        {
            if(root == null)
            {
                return null;
            }

            if (root.left == null && root.right == null)
            {
                cur.Add(root.val);
                return null;
            }

            root.left = RemoveLeaves(root.left, cur);
            root.right = RemoveLeaves(root.right, cur);

            return root;
        }

        public int MaxDepth(TreeNode root)
        {
            if(root == null)
            {
                return 0;
            }

            var lDepth = MaxDepth(root.left);
            var rDepth = MaxDepth(root.right);

            return Math.Max(rDepth, lDepth) + 1;
        }

        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            var result = new List<IList<int>>();

            if(root == null)
            {
                return result;
            }

            var st = new Queue<TreeNode>();
            st.Enqueue(root);
            st.Enqueue(null);

            bool ltr = true;

            var cur = new List<int>();

            while(st.Any())
            {
                var node = st.Dequeue();

                if (node == null)
                {
                    if(!ltr)
                    {
                        cur.Reverse();
                    }

                    ltr = !ltr;

                    result.Add(cur);
                    if (st.Any())
                    {
                        st.Enqueue(null);
                    }

                    cur = new List<int>();
                }
                else
                {
                    cur.Add(node.val);

                    if (node.left != null)
                    {
                        st.Enqueue(node.left);
                    }
                    if (node.right != null)
                    {
                        st.Enqueue(node.right);
                    }
                }
            }

            return result;
        }

        public IList<double> AverageOfLevels(TreeNode root)
        {
            var result = new List<double>();

            if (root == null)
            {
                return result;
            }

            var st = new Queue<TreeNode>();
            st.Enqueue(root);
            st.Enqueue(null);

            int sum = 0;
            int count = 0;
            while (st.Any())
            {
                var node = st.Dequeue();

                if (node == null)
                {
                    result.Add((double)sum/count);
                    if (st.Any())
                    {
                        st.Enqueue(null);
                    }

                    sum = 0;
                    count = 0;
                }
                else
                {
                    sum += node.val;
                    count++;

                    if (node.left != null)
                    {
                        st.Enqueue(node.left);
                    }
                    if (node.right != null)
                    {
                        st.Enqueue(node.right);
                    }
                }
            }

            return result;
        }

        public IList<IList<int>> LevelOrderBottom(TreeNode root)
        {
            var result = new List<IList<int>>();

            if (root == null)
            {
                return result;
            }

            var st = new Queue<TreeNode>();
            st.Enqueue(root);
            st.Enqueue(null);

            var cur = new List<int>();

            while (st.Any())
            {
                var node = st.Dequeue();

                if (node == null)
                {
                    result.Add(cur);
                    if (st.Any())
                    {
                        st.Enqueue(null);
                    }

                    cur = new List<int>();
                }
                else
                {
                    cur.Add(node.val);

                    if (node.left != null)
                    {
                        st.Enqueue(node.left);
                    }
                    if (node.right != null)
                    {
                        st.Enqueue(node.right);
                    }
                }
            }

            result.Reverse();
            return result;
        }

        public Node Connect(Node root)
        {
            if(root == null)
            {
                return root;
            }

            var q = new Queue<Node>();
            q.Enqueue(root);
            q.Enqueue(null);

            while(q.Count != 0)
            {
                var node = q.Dequeue();
                if(node == null)
                {
                    if (q.Count() == 0)
                    {
                        break;
                    }
                    else
                    {
                        q.Enqueue(null);
                    }
                }
                else
                {
                    if(node.left != null)
                    {
                        q.Enqueue(node.left);
                    }
                    if(node.right != null)
                    {
                        q.Enqueue(node.right);
                    }

                    node.next = q.Peek();
                }
            }

            return root;
        }

        public TreeNode BuildTree(int[] inorder, int[] postorder)
        {
            var length = postorder.Count();
            var root = BuildTreeHelper(postorder, 0, length - 1, inorder, 0, length - 1);
            return root;
        }

        private TreeNode BuildTreeHelper(int[] postorder, int postStart, int postEnd, int[] inorder, int inStart, int inEnd)
        {
            if (postStart > postEnd || inStart > inEnd)
            {
                return null;
            }

            var node = new TreeNode(postorder[postEnd]);

            var inorderIndex = Search(inorder, node.val);

            node.left = BuildTreeHelper(postorder, postStart, postStart + inorderIndex - inStart - 1, inorder, inStart, inorderIndex - 1);
            node.right = BuildTreeHelper(postorder, postStart + inorderIndex - inStart, postEnd - 1, inorder, inorderIndex + 1, inEnd);

            return node;
        }

        private int Search(int[] inorder, int val)
        {
            for(int i=0; i< inorder.Length; i++)
            {
                if(inorder[i] == val)
                {
                    return i;
                }
            }
            return -1;
        }

        public TreeNode BuildPreOrderTree(int[] preorder, int[] inorder)
        {
            var length = preorder.Count();
            var root = BuildPreOrderTreeHelper(preorder, inorder, 0, length - 1);
            return root;
        }

        private TreeNode BuildPreOrderTreeHelper(int[] preorder, int[] inorder, int start, int end)
        {
            if(start > end)
            {
                return null;
            }

            var node = new TreeNode(preorder[preIndex++]);

            if(start == end)
            {
                return node;
            }

            var inorderIndex = Search(inorder, start, end, node.val);

            node.left = BuildPreOrderTreeHelper(preorder, inorder, start, inorderIndex - 1);
            node.right = BuildPreOrderTreeHelper(preorder, inorder, inorderIndex + 1, end);

            return node;
        }

        public int Search(int[] arr, int strt, int end, int value)
        { 
            for (int i = strt; i <= end; i++)
            {
                if (arr[i] == value)
                {
                    return i;
                }
            }
            return -1;
        }

        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if(root == null)
            {
                return null;
            }

            if(root.val == p.val || root.val == q.val)
            {
                return root;
            }

            var lca = LowestCommonAncestor(root.left, p, q);
            var rca = LowestCommonAncestor(root.right, p, q);

            if(lca != null && rca != null)
            {
                return root;
            }

            return lca ?? rca;
        }

        public int countUnivalSubtrees(TreeNode root)
        {
            int[] count = new int[1];
            UnivalHelper(root, count);
            return count[0];
        }

        private bool UnivalHelper(TreeNode node, int[] count)
        {
            if (node == null)
            {
                return true;
            }
            bool left = UnivalHelper(node.left, count);
            bool right = UnivalHelper(node.right, count);
            if (left && right)
            {
                if (node.left != null && node.val != node.left.val)
                {
                    return false;
                }
                if (node.right != null && node.val != node.right.val)
                {
                    return false;
                }
                count[0]++;
                return true;
            }
            return false;
        }

        public bool IsSymmetric(TreeNode root)
        {
            return root == null || IsSymmetricHelper(root.left, root.right);
        }

        private bool IsSymmetricHelper(TreeNode left, TreeNode right)
        {
            if (left == null || right == null)
            {
                return left == right;
            }

            if (left.val != right.val)
            {
                return false;
            }

            return IsSymmetricHelper(left.left, right.right) && IsSymmetricHelper(left.right, right.left);
        }

        // MED : https://leetcode.com/problems/sum-root-to-leaf-numbers/
        public int SumNumbers(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            if(root.left == null && root.right == null)
            {
                return root.val;
            }

            GetLeafSum(root, 0);

            return leafSum;
        }

        public void GetLeafSum(TreeNode node, int sum)
        {
            if(node == null)
            {
                return;
            }

            if(node.left == null && node.right == null)
            {
                leafSum += sum + node.val;
            }

            int tempSum = 10 * (sum + node.val);

            GetLeafSum(node.left, tempSum);
            GetLeafSum(node.right, tempSum);
        }

        public bool HasPathSum(TreeNode root, int sum)
        {
            return GetPathSum(root, sum, 0);
        }

        public bool GetPathSum(TreeNode node, int sum, int tempSum)
        {
            if (node == null)
            {
                return false;
            }

            tempSum += node.val;

            if(node.left == null && node.right == null)
            {
                return tempSum == sum;
            }

            return GetPathSum(node.left, sum, tempSum) || GetPathSum(node.right, sum, tempSum);
        }

        public int PathSum(TreeNode root, int sum)
        {
            var dict = new Dictionary<int, int> { { 0, 1 } };
            return HelperSum(root, 0, sum, dict);
        }

        public int HelperSum(TreeNode root, int currSum, int target, IDictionary<int, int> preSum)
        {
            if (root == null)
            {
                return 0;
            }

            currSum += root.val;
            int res = preSum.ContainsKey(currSum - target) ? preSum[currSum - target] : 0;
            if(preSum.ContainsKey(currSum))
            {
                preSum[currSum] += 1;
            }
            else
            {
                preSum.Add(currSum, 1);
            }

            res += HelperSum(root.left, currSum, target, preSum) + HelperSum(root.right, currSum, target, preSum);
            preSum[currSum] -= 1;
            return res;
        }

        // HARD : https://leetcode.com/problems/binary-tree-maximum-path-sum/
        public int MaxPathSum(TreeNode root)
        {
            MaxSum(root);
            return maxSum;
        }

        private int MaxSum(TreeNode root)
        {
            if(root == null)
            {
                return 0;
            }

            var leftSum = Math.Max(MaxSum(root.left), 0);
            var rightSum = Math.Max(MaxSum(root.right), 0);

            var pathSum = root.val + leftSum + rightSum;

            maxSum = Math.Max(maxSum, pathSum);

            return root.val + Math.Max(leftSum, rightSum);
        }

        public IList<string> BinaryTreePaths(TreeNode root)
        {
            var result = new List<string>();
            PrintPaths(root, "", result);
            return result;
        }

        private void PrintPaths(TreeNode root, string path, List<string> result)
        {
            if (root != null)
            {
                path += root.val.ToString();
                if ((root.left == null) && (root.right == null))
                    result.Add(path);
                else
                {
                    path += "->";
                    PrintPaths(root.left, path, result);
                    PrintPaths(root.right, path, result);
                }
            }
        }

        // MED : https://leetcode.com/problems/path-sum-ii/
        public IList<IList<int>> PathSum2(TreeNode root, int sum)
        {
            var result = new List<IList<int>>();
            SumHelper(root, sum, 0, new List<int>(), result);
            return result;
        }

        private void SumHelper(TreeNode root, int sum, int curSum, List<int> curNums, List<IList<int>> result)
        {
            if(root == null)
            {
                return;
            }

            curNums.Add(root.val);
            curSum += root.val;

            if(curSum == sum && root.left == null && root.right == null)
            {
                result.Add(new List<int> (curNums));
                curNums.RemoveAt(curNums.Count - 1);
                return;
            }

            SumHelper(root.left, sum, curSum, curNums, result);
            SumHelper(root.right, sum, curSum, curNums, result);

            curNums.RemoveAt(curNums.Count - 1);
        }

        public static IList<IList<int>> LevelOrder(TreeNode root)
        {
            var result = new List<IList<int>>();

            if (root == null)
            {
                return result;
            }

            var treeQ = new Queue<TreeNode>();
            treeQ.Enqueue(root);
            treeQ.Enqueue(null);
            var curList = new List<int>();

            while(treeQ.Any())
            {
                var node = treeQ.Dequeue();
                if(node == null)
                {
                    result.Add(curList);
                    curList = new List<int>();
                    if (treeQ.Any())
                    {
                        treeQ.Enqueue(null);
                    }
                    continue;
                }

                curList.Add(node.val);

                if(node.left != null)
                {
                    treeQ.Enqueue(node.left);
                }

                if (node.right != null)
                {
                    treeQ.Enqueue(node.right);
                }
            }

            return result;

        }

        public static IList<int> PreorderTraversal(TreeNode root)
        {
            var result = new List<int>();

            if (root == null)
            {
                return result;
            }
            var treeStack = new Stack<TreeNode>();
            treeStack.Push(root);

            while(treeStack.Any())
            {
                var top = treeStack.Pop();
                result.Add(top.val);

                if(top.right != null)
                {
                    treeStack.Push(top.right);
                }

                if (top.left != null)
                {
                    treeStack.Push(top.left);
                }
            }

            return result;
        }

        public static IList<int> PostorderTraversal(TreeNode root)
        {
            var result = new List<int>();

            if (root == null)
            {
                return result;
            }

            var treeStack = new Stack<TreeNode>();
            treeStack.Push(root);

            while (treeStack.Any())
            {
                var top = treeStack.Pop();

                if(top.right == null && top.left == null)
                {
                    result.Add(top.val);
                    continue;
                }

                treeStack.Push(top);

                if (top.right != null)
                {
                    treeStack.Push(top.right);
                    top.right = null;
                }

                if (top.left != null)
                {
                    treeStack.Push(top.left);
                    top.left = null;
                }
            }

            return result;
        }

        public static IList<int> InorderTraversal(TreeNode root)
        {
            var result = new List<int>();

            if(root == null)
            {
                return result;
            }
            var treeStack = new Stack<TreeNode>();
            treeStack.Push(root);

            while(treeStack.Any())
            {
                var top = treeStack.Pop();

                if (top.left != null)
                {
                    treeStack.Push(top);
                    treeStack.Push(top.left);
                    top.left = null;
                }
                else
                {
                    result.Add(top.val);
                    if (top.right != null)
                    {
                        treeStack.Push(top.right);
                    }
                }
            }

            return result;
        }
    }
}
