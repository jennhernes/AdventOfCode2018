using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    class Day17
    {
        static void Part1()
        {
            var file = new System.IO.StreamReader("..\\..\\input.txt");
            string line;
            int maxX = 0;
            int maxY = 0;
            int minY = int.MaxValue;
            List<string[]> input = new List<string[]>();
            string[] delims = { "=", ",", " ", ".." };

            while ((line = file.ReadLine()) != null)
            {
                input.Add(line.Split(delims, System.StringSplitOptions.RemoveEmptyEntries));
                string[] lastLine = input.Last();
                if (lastLine[0] == "x")
                {
                    maxX = Math.Max(maxX, int.Parse(lastLine[1]));
                    maxY = Math.Max(maxY, int.Parse(lastLine[4]));
                    minY = Math.Min(minY, int.Parse(lastLine[3]));
                }
                else if (lastLine[0] == "y")
                {
                    maxY = Math.Max(maxY, int.Parse(lastLine[1]));
                    minY = Math.Min(minY, int.Parse(lastLine[1]));
                    maxX = Math.Max(maxX, int.Parse(lastLine[4]));
                }
            }

            char[,] ground = new char[maxY + 1, maxX + 1];

            for (int i = 0; i < maxY + 1; i++)
            {
                for (int j = 0; j < maxX + 1; j++)
                {
                    ground[i, j] = ' ';
                }
            }

            ground[0, 500] = '+';

            int x = 0;
            int y = 0;
            for (int k = 0; k < input.Count; k++)
            {
                string[] coords = input[k];
                if (coords[0] == "x")
                {
                    x = int.Parse(coords[1]);
                    for (int j = int.Parse(coords[3]); j < int.Parse(coords[4]) + 1; j++)
                    {
                        ground[j, x] = '#';
                    }
                }
                else if (coords[0] == "y")
                {
                    y = int.Parse(coords[1]);
                    for (int i = int.Parse(coords[3]); i < int.Parse(coords[4]) + 1; i++)
                    {
                        ground[y, i] = '#';
                    }
                }
            }

            var writeFile = new System.IO.StreamWriter("..\\..\\ground.txt");

            for (int i = 0; i < ground.GetLength(0); i++)
            {
                for (int j = 0; j < ground.GetLength(1); j++)
                {
                    writeFile.Write(ground[i, j]);
                }
                writeFile.WriteLine();
            }

            writeFile.Close();

            FillWithWater(ref ground, 500, 0);

            WriteFilledFile(ground);

            int waterSquares = 0;
            for (int i = minY; i < maxY + 1; i++)
            {
                for (int j = 0; j < ground.GetLength(1); j++)
                {
                    if (ground[i, j] == '~') // || ground[i, j] == '|')
                    {
                        waterSquares++;
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine(waterSquares);
        }

        static void WriteFilledFile(char[,] ground)
        {
            var f = new System.IO.StreamWriter("..\\..\\filled.txt");
            for (int i = 0; i < ground.GetLength(0); i++)
            {
                for (int j = 0; j < ground.GetLength(1); j++)
                {
                    f.Write(ground[i, j]);
                }
                f.WriteLine();
            }

            f.Close();
        }

        static void FillWithWater(ref char[,] ground, int x, int y)
        {
            // Reached the bottom of the ground, will go onto infinity
            if (y == ground.GetLength(0) - 1)
            {
                ground[y, x] = '|';
                return;
            }

            // somehow ended up in the walls, just return
            else if (ground[y, x] == '#')
            {
                System.Diagnostics.Debug.WriteLine(x + " " + y);
                return;
            }

            // the meat of the fill

            // water falls downwards while still above sand
            while ((ground[y, x] == '|' || ground[y, x] == '+') && ground[y + 1, x] == ' ')
            {
                y++;
                ground[y, x] = '|';
                if ((y == ground.GetLength(0) - 1) || ground[y + 1,x] == '|')
                {
                    return;
                }
            }

            // water will fill to the left and right above a horizontal line of clay or standing water

            // find the leftmost point the water will fall towards
            int leftX = x;
            while (leftX > 0 && (ground[y, leftX - 1] != '#') && (ground[y + 1, leftX] == '#' || ground[y + 1, leftX] == '~'))
            {
                leftX--;
            }

            // find the rightmost point the water will fall towards
            int rightX = x;
            while (rightX < ground.GetLength(1) - 1 && (ground[y, rightX + 1] != '#') &&
                (ground[y + 1, rightX] == '#' || ground[y + 1, rightX] == '~'))
            {
                rightX++;
            }

            // water still contained by walls
            if (ground[y, leftX - 1] == '#' && ground[y, rightX + 1] == '#')
            {
                // fill the row of standing water
                for (int i = leftX; i < rightX + 1; i++)
                {
                    ground[y, i] = '~';
                }

                // recursive call to the row above now that the water level has risen
                y--;
                FillWithWater(ref ground, x, y);
            }
            // water not contained by walls on at least one side
            else
            {
                // fill the row of flowing water
                for (int i = leftX; i < rightX + 1; i++)
                {
                    ground[y, i] = '|';
                }
                // water not contained on the LHS, means that the point to the left of leftX is empty sand
                if (ground[y, leftX - 1] == ' ' || ground[y, leftX - 1] == '|')
                {
                    // recursive call for the water to start falling from leftX
                    FillWithWater(ref ground, leftX, y);
                }

                // water not contained on the RHS, means that the point to the right of rightX is empty sand
                if (ground[y, rightX + 1] == ' ' || ground[y, rightX + 1] == '|')
                {
                    // recursive call for the water to start falling from rightX
                    FillWithWater(ref ground, rightX, y);
                }
            }
        }

        static void Main(string[] args)
        {
            Part1();
            // Part2();
        }
    }
}
