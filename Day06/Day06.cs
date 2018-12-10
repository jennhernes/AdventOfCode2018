using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06
{
    public class ClosestDestination
    {
        public int destIndex;
        public int distance;

        public ClosestDestination(int destIndex, int distance)
        {
            this.destIndex = destIndex;
            this.distance = distance;
        }
    }

    class Day06
    {
        static void Part1()
        {
            List<List<string>> coords = new List<List<string>>();
            string line;
            string[] delims = { ", " };
            int height = 0;
            int width = 0;
            int x;
            int y;
            int destIndex = 0;

            var file = new System.IO.StreamReader("..\\..\\puzzle.txt");
            
            while ((line = file.ReadLine()) != null)
            {
                var split = line.Split(delims, System.StringSplitOptions.RemoveEmptyEntries);
                
                coords.Add(new List<string> { destIndex.ToString(), split[0], split[1] });
                x = int.Parse(split[0]);
                y = int.Parse(split[1]);
                width = Math.Max(x, width);
                height = Math.Max(y, height);
                destIndex++;
                
            }
            width++;
            height++;
            file.Close();

            int numDest = destIndex;

            ClosestDestination[,] map = new ClosestDestination[height, width];
            int distance;
            foreach (List<string> dest in coords)
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        distance = Math.Abs((int.Parse(dest[1]) - j)) + Math.Abs((int.Parse(dest[2]) - i));
                        if (map[i, j] == null)
                        {
                            map[i, j] = new ClosestDestination(int.Parse(dest[0]), distance);
                        } else if (distance == map[i,j].distance)
                        {
                            map[i, j].destIndex = numDest;
                        } else if (distance < map[i, j].distance)
                        {
                            map[i, j].destIndex = int.Parse(dest[0]);
                            map[i, j].distance = distance;
                        }
                    }
                }
            }


            bool[] infiniteDests = new bool[numDest];
            int index;
            for (int i = 0; i < height; i++)
            {
                if (i == 0 || i == height - 1)
                {
                    for (int j = 0; j < width; j++)
                    {
                        index = map[i, j].destIndex;
                        if (index < numDest)
                        {
                            infiniteDests[index] = true;
                        }
                    }
                } else
                {
                    index = map[i, 0].destIndex;
                    if (index < numDest)
                    {
                        infiniteDests[index] = true;
                    }
                    index = map[i, width-1].destIndex;
                    if (index < numDest)
                    {
                        infiniteDests[index] = true;
                    }

                }
            }

            var writeFile = new System.IO.StreamWriter("..\\..\\map.txt");
            int maxArea = 0;
            int[] area = new int[numDest];
            ClosestDestination closest;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    closest = map[i, j];
                    index = closest.destIndex;
                    if (index < numDest)
                    {
                        if (!infiniteDests[index])
                        {
                            area[index]++;
                        }
                        maxArea = Math.Max(area[index], maxArea);
                    }
                    writeFile.Write(closest.destIndex);
                    writeFile.Write(" ");
                }
                writeFile.WriteLine();
            }
            writeFile.Close();

            System.Diagnostics.Debug.WriteLine(maxArea);
            
        }

        static void Part2()
        {
            List<List<string>> coords = new List<List<string>>();
            string line;
            string[] delims = { ", " };
            int height = 0;
            int width = 0;
            int x;
            int y;
            int destIndex = 0;

            var file = new System.IO.StreamReader("..\\..\\puzzle.txt");

            while ((line = file.ReadLine()) != null)
            {
                var split = line.Split(delims, System.StringSplitOptions.RemoveEmptyEntries);

                coords.Add(new List<string> { destIndex.ToString(), split[0], split[1] });
                x = int.Parse(split[0]);
                y = int.Parse(split[1]);
                width = Math.Max(x, width);
                height = Math.Max(y, height);
                destIndex++;

            }
            width++;
            height++;
            file.Close();

            int numDest = destIndex;

            int[,] map = new int[height, width];
            int distance;
            foreach (List<string> dest in coords)
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        map[i, j] += Math.Abs((int.Parse(dest[1]) - j)) + Math.Abs((int.Parse(dest[2]) - i));
                    }
                }
            }

            int regionSize = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (map[i, j] <= 10000)
                    {
                        regionSize++;
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine(regionSize);
        }


        static void Main(string[] args)
        {
            // Part1();
            Part2();
        }
    }
}
