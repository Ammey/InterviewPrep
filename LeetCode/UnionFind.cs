using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class UnionFind
    {
        int[] rank = new int[1001];
        int[] parent = new int [1001];

        public static void Main()
        {
            int[,] a = new int[5, 2] {
                   {3, 4} ,   /*  initializers for row indexed by 0 */
                   {1, 2} ,   /*  initializers for row indexed by 1 */
                   {2, 4} ,  /*  initializers for row indexed by 2 */
                   {3, 5} ,
                   {2, 5}
                };
            a = new int[,] { { 16, 25 }, { 7, 9 }, { 3, 24 }, { 10, 20 }, { 15, 24 }, { 2, 8 }, { 19, 21 }, { 2, 15 }, { 13, 20 }, { 5, 21 }, { 7, 11 }, { 6, 23 }, { 7, 16 }, { 1, 8 }, { 17, 20 }, { 4, 19 }, { 11, 22 }, { 5, 11 }, { 1, 16 }, { 14, 20 }, { 1, 4 }, { 22, 23 }, { 12, 20 }, { 15, 18 }, { 12, 16 } };
            var obj = new UnionFind();
            var x = obj.FindRedundantConnection(a);
        }

        // Union Find Disjoint Set
        public int[] FindRedundantConnection(int[,] edges)
        {
            var result = new int[2];

            for (int i = 0; i < 1001; i++)
            {
                parent[i] = i;
            }

            for (int i = 0; i < edges.GetLength(0); i++)
            {
                if (Union(edges[i,0], edges[i,1]))
                {
                    result = new int[] { edges[i, 0], edges[i, 1] };
                }
            }
            return result;
        }

        public int Find(int i)
        {
            if(parent[i] != i)
            {
                parent[i] = Find(parent[i]);
            }
            return parent[i];
        }

        public bool Union(int i, int j)
        {
            var pi = Find(i);
            var pj = Find(j);

            if(pi == pj)
            {
                return true;
            }

            if(rank[pi] < rank[pj])
            {
                parent[pi] = pj;
            }
            else if(rank[pi] > rank[pj])
            {
                parent[pj] = pi;
            }
            else
            {
                parent[pj] = pi;
                rank[pi] += 1;
            }

            return false;
        }
    }
}
