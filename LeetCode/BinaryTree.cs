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
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }

        public static void Main()
        {
            var obj = new BinaryTree();
            var treeNode = new TreeNode(4);
            var node2 = new TreeNode(9);
            var node3 = new TreeNode(0);
            var node4 = new TreeNode(5);
            var node5 = new TreeNode(1);
            node2.right = node5;
            node2.left = node4;
            treeNode.right = node3;
            treeNode.left = node2;
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
            var sum = obj.MaxPathSum(treeNode);
            var inorderList = InorderTraversal(treeNode);
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
