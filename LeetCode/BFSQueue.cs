using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class BFSQueue
    {
        int[,] box;
        static int[,] flood;
        char[,] box1;
        static int INFINITY = int.MaxValue;

        public static void Main()
        {
            var rooms = new List<IList<int>>()
            {
                new List<int> { 1, 3 },
                new List<int> { 3, 0, 1 },
                new List<int> { 2 },
                new List<int> { 0 },
            };

            var canVisit = CanVisitAllRooms(rooms);

            //FloodFill
            flood = new int[3, 3];
            flood[0, 0] = 0;
            flood[0, 1] = 0;
            flood[0, 2] = 1;
            flood[1, 0] = 1;
            flood[1, 1] = 1;
            flood[1, 2] = 1;
            flood[2, 0] = 9;
            flood[2, 1] = 4;
            flood[2, 2] = 2;
            UpdateMatrix(flood);
            flood = FloodFill(flood, 1, 1, 1);

            var q = new BFSQueue
            {
                box = new int[4, 4]
            };
            q.box[0, 0] = INFINITY;
            q.box[0, 1] = -1;
            q.box[0, 2] = 0;
            q.box[0, 3] = INFINITY;
            q.box[1, 0] = INFINITY;
            q.box[1, 1] = INFINITY;
            q.box[1, 2] = INFINITY;
            q.box[1, 3] = -1;
            q.box[2, 0] = INFINITY;
            q.box[2, 1] = -1;
            q.box[2, 2] = INFINITY;
            q.box[2, 3] = -1;
            q.box[3, 0] = 0;
            q.box[3, 1] = -1;
            q.box[3, 2] = INFINITY;
            q.box[3, 3] = INFINITY;

            q.WallsAndGates(q.box);

            q.box1 = new char[5, 5];
            q.box1[0, 0] = '1';
            q.box1[0, 1] = '1';
            q.box1[0, 2] = '0';
            q.box1[0, 3] = '0';
            q.box1[0, 4] = '0';
            q.box1[1, 0] = '1';
            q.box1[1, 1] = '1';
            q.box1[1, 2] = '0';
            q.box1[1, 3] = '0';
            q.box1[1, 4] = '0';
            q.box1[2, 0] = '0';
            q.box1[2, 1] = '0';
            q.box1[2, 2] = '1';
            q.box1[2, 3] = '0';
            q.box1[2, 4] = '0';
            q.box1[3, 0] = '0';
            q.box1[3, 1] = '0';
            q.box1[3, 2] = '0';
            q.box1[3, 3] = '1';
            q.box1[3, 4] = '1';
            var numIslands = q.NumIslands(q.box1);
        }

        public static bool CanVisitAllRooms(IList<IList<int>> rooms)
        {
            if(rooms == null)
            {
                return false;
            }

            var roomCount = rooms.Count;
            var seen = new HashSet<int>();
            seen.Add(0);
            var roomsArr = rooms.ToArray();
            var firstRoom = roomsArr[0];
            var roomQ = new Queue<int>();
            foreach(int r in firstRoom)
            {
                roomQ.Enqueue(r);
                seen.Add(r);
            }

            while(roomQ.Any() && seen.Count != roomCount)
            {
                foreach(int key in roomsArr[roomQ.Dequeue()])
                {
                    if(!seen.Contains(key))
                    {
                        seen.Add(key);
                        roomQ.Enqueue(key);
                    }
                }
            }

            return seen.Count == roomCount ? true :false;
        }

        // Minimum distance from 0
        public static int[,] UpdateMatrix(int[,] matrix)
        {
            int rowMax = matrix.GetLength(0);
            int colMax = matrix.GetLength(1);

            var zeroQ = new Queue<Tuple<int, int>>();
            for(int i=0; i< rowMax; i++)
            {
                for (int j=0; j< colMax; j++)
                {
                    if(matrix[i,j] == 0)
                    {
                        zeroQ.Enqueue(Tuple.Create(i, j));
                    }
                    else
                    {
                        matrix[i,j] = int.MaxValue;
                    }
                }
            }

            var dirs = new List<Tuple<int, int>> {
                Tuple.Create(-1, 0 ),
                Tuple.Create(1,0 ),
                Tuple.Create(0, -1 ),
                Tuple.Create(0, 1 )
            };

            while(zeroQ.Any())
            {
                var first = zeroQ.Dequeue();
                foreach(var dir in dirs)
                {
                    int row = first.Item1 + dir.Item1;
                    int col = first.Item2 + dir.Item2;

                    if(row < 0 || col < 0 || row >= rowMax || col >= colMax || matrix[row, col] <= matrix[first.Item1, first.Item2] + 1)
                    {
                        continue;
                    }
                    zeroQ.Enqueue(Tuple.Create(row, col));
                    matrix[row, col] = matrix[first.Item1, first.Item2] + 1;
                }
            }

            return matrix;
        }

        public static int[,] FloodFill(int[,] image, int sr, int sc, int newColor)
        {
            var rowCount = image.GetLength(0);
            var colCount = image.GetLength(1);

            var start = new Tuple<int, int> (sr, sc);
            var startColor = image[sr, sc];
            if(startColor == newColor)
            {
                return image;
            }

            image[sr, sc] = newColor;

            var imageStack = new Stack<Tuple<int,int>>();
            imageStack.Push(start);

            while(imageStack.Any())
            {
                var pixel = imageStack.Pop();
                var row = pixel.Item1;
                var col = pixel.Item2;

                if(row> 0 && image[row-1, col] == startColor)
                {
                    imageStack.Push(new Tuple<int, int>(row-1, col));
                    image[row - 1, col] = newColor;
                }
                if (col > 0 && image[row, col - 1] == startColor)
                {
                    imageStack.Push(new Tuple<int, int>(row, col - 1));
                    image[row, col - 1] = newColor;
                }
                if (row < rowCount - 1 && image[row + 1, col] == startColor)
                {
                    imageStack.Push(new Tuple<int, int>(row + 1, col));
                    image[row + 1, col] = newColor;
                }

                if (col < colCount - 1 && image[row, col + 1] == startColor)
                {
                    imageStack.Push(new Tuple<int, int>(row, col + 1));
                    image[row, col + 1] = newColor;
                }
            }

            return image;
        }

        public int openLock(String[] deadends, String target)
        {
            var q = new Queue<String>();
            var deads = new HashSet<string>(deadends.ToList());
            var visited = new HashSet<string>();
            q.Enqueue("0000");
            visited.Add("0000");
            int level = 0;
            while (!q.Any())
            {
                int size = q.Count;
                while (size > 0)
                {
                    String s = q.Peek();
                    if (deads.Contains(s))
                    {
                        size--;
                        continue;
                    }
                    if (s.Equals(target))
                    {
                        return level;
                    }

                    var sb = s;
                    var cArr = sb.ToCharArray();
                    for (int i = 0; i < 4; i++)
                    {
                        char c = cArr[i];
                        String s1 = sb.Substring(0, i) + (c == '9' ? 0 : c - '0' + 1) + sb.Substring(i + 1);
                        String s2 = sb.Substring(0, i) + (c == '0' ? 9 : c - '0' - 1) + sb.Substring(i + 1);
                        if (!visited.Contains(s1) && !deads.Contains(s1))
                        {
                            q.Enqueue(s1);
                            visited.Add(s1);
                        }
                        if (!visited.Contains(s2) && !deads.Contains(s2))
                        {
                            q.Enqueue(s2);
                            visited.Add(s2);
                        }
                    }
                    size--;
                }
                level++;
            }
            return -1;
        }

        public int NumIslands(char[,] grid)
        {
            var islands = 0;

            if(grid == null)
            {
                return islands;
            }

            int rowSize = grid.GetLength(0);
            if (rowSize == 0)
            {
                return islands;
            }
            int colSize = grid.GetLength(1);

            var dataQ = new Queue<Tuple<int, int>>();

            for(int i=0; i<rowSize; i++)
            {
                for(int j=0; j<colSize; j++)
                {
                    var node = new Tuple<int, int> (i, j);

                    if(grid[i,j] == '0')
                    {
                        continue;
                    }

                    if (grid[i, j] == '1')
                    {
                        islands++;
                        grid[i, j] = '0';
                        dataQ.Enqueue(node);
                        while(dataQ.Any())
                        {
                            var data = dataQ.Dequeue();
                            var curRow = data.Item1;
                            var curCol = data.Item2;

                            if (curRow < rowSize - 1 && grid[curRow + 1, curCol] == '1')
                            {
                                grid[curRow + 1, curCol] = '0';
                                dataQ.Enqueue(new Tuple<int, int>(curRow + 1, curCol));
                            }

                            if (curCol < colSize - 1 && grid[curRow, curCol + 1] == '1')
                            {
                                grid[curRow, curCol + 1] = '0';
                                dataQ.Enqueue(new Tuple<int, int>(curRow, curCol + 1));
                            }

                            if (curRow > 0 && grid[curRow - 1, curCol] == '1')
                            {
                                grid[curRow - 1, curCol] = '0';
                                dataQ.Enqueue(new Tuple<int, int>(curRow - 1, curCol));
                            }

                            if (curCol > 0 && grid[curRow, curCol - 1] == '1')
                            {
                                grid[curRow, curCol - 1] = '0';
                                dataQ.Enqueue(new Tuple<int, int>(curRow, curCol - 1));
                            }
                        }
                    }
                }
            }

            return islands;
        }
        

        public void WallsAndGates(int[,] rooms)
        {
            if (rooms == null)
                return;

            int rowSize = rooms.GetLength(0);
            if (rowSize == 0)
                return;
            int colSize = rooms.GetLength(1);
            var dataQ = new Queue<int[]>();

            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < colSize; j++)
                {
                    if (rooms[i, j] == 0)
                    {
                        dataQ.Enqueue(new int[] {i, j});
                    }
                }
            }

            while(dataQ.Any())
            {
                var val = dataQ.Peek();
                var dataModified = false;
                var curRow = val[0];
                var curCol = val[1];

                if(curRow < rowSize - 1 && rooms[curRow + 1, curCol] == INFINITY)
                {
                    var curVal = rooms[curRow + 1, curCol];

                    if(curVal == -1)
                    {
                        rooms[curRow + 1, curCol] = rooms[curRow, curCol] + 1;
                    }
                    else
                    {
                        rooms[curRow + 1, curCol] = Math.Min(rooms[curRow + 1, curCol], rooms[curRow, curCol] + 1);
                    }

                    dataModified = true;
                    dataQ.Enqueue(new int[] { curRow + 1, curCol });
                }

                if (curCol < colSize - 1 && rooms[curRow, curCol + 1] == INFINITY)
                {
                    var curVal = rooms[curRow, curCol + 1];

                    if (curVal == -1)
                    {
                        rooms[curRow, curCol + 1] = rooms[curRow, curCol] + 1;
                    }
                    else
                    {
                        rooms[curRow, curCol + 1] = Math.Min(rooms[curRow, curCol + 1], rooms[curRow, curCol] + 1);
                    }

                    dataModified = true;
                    dataQ.Enqueue(new int[] { curRow, curCol + 1 });
                }

                if (curRow > 0 && rooms[curRow - 1, curCol] == INFINITY)
                {
                    var curVal = rooms[curRow - 1, curCol];

                    if (curVal == -1)
                    {
                        rooms[curRow - 1, curCol] = rooms[curRow, curCol] + 1;
                    }
                    else
                    {
                        rooms[curRow - 1, curCol] = Math.Min(rooms[curRow - 1, curCol], rooms[curRow, curCol] + 1);
                    }

                    dataModified = true;
                    dataQ.Enqueue(new int[] { curRow - 1, curCol });
                }

                if (curCol > 0 && rooms[curRow, curCol - 1] == INFINITY)
                {
                    var curVal = rooms[curRow, curCol - 1];

                    if (curVal == -1)
                    {
                        rooms[curRow, curCol - 1] = rooms[curRow, curCol] + 1;
                    }
                    else
                    {
                        rooms[curRow, curCol - 1] = Math.Min(rooms[curRow, curCol - 1], rooms[curRow, curCol] + 1);
                    }

                    dataModified = true;
                    dataQ.Enqueue(new int[] { curRow, curCol - 1 });
                }

                if(!dataModified && rooms[curRow, curCol] == 0)
                {
                    rooms[curRow, curCol] = INFINITY;
                }

                dataQ.Dequeue();
            }
        }
    }
}
