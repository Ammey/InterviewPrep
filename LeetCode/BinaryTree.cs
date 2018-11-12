using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    class BinaryTree
    {
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }

        public static void Main()
        {
            var treeNode = new TreeNode(1);
            var node2 = new TreeNode(2);
            var node3 = new TreeNode(3);
            node2.left = node3;
            treeNode.right = node2;
            var inorderList = InorderTraversal(treeNode);
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
