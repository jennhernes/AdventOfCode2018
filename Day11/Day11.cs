using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    class Day11
    {
        static void Part1()
        {
            // int gridSerialNum = 42;
            int gridSerialNum = 4842;
            int gridSize = 300;

            int[,] grid = new int[gridSize, gridSize];
            int maxFuel = 0;
            int x = 0;
            int y = 0;

            for (int i = gridSize - 1; i >= 0; i--)
            {
                for (int j = gridSize - 1; j >= 0; j--)
                {
                    int rackID = (i + 1) + 10;
                    int powerLevel = rackID * (j + 1);
                    powerLevel += gridSerialNum;
                    powerLevel = powerLevel * rackID;
                    powerLevel = (powerLevel / 100) % 10;
                    powerLevel -= 5;
                    grid[i, j] = powerLevel;

                    if (i < gridSize - 2 && j < gridSize - 2)
                    {
                        int fuel = grid[i, j] + grid[i, j + 1] + grid[i, j + 2] +
                            grid[i + 1, j] + grid[i + 1, j + 1] + grid[i + 1, j + 2] +
                            grid[i + 2, j] + grid[i + 2, j + 2] + grid[i + 2, j + 2];
                        if (fuel > maxFuel)
                        {
                            maxFuel = fuel;
                            x = i + 1;
                            y = j + 1;
                        }
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine(x + "," + y);
        }

        static void Part2()
        {
            // int gridSerialNum = 18;
            int gridSerialNum = 4842;
            int gridSize = 300;

            int[,] grid = new int[gridSize, gridSize];
            int maxFuel = int.MinValue;
            int fuel = 0;
            int x = 0;
            int y = 0;
            int d = 0;

            for (int i = gridSize - 1; i >= 0; i--)
            {
                for (int j = gridSize - 1; j >= 0; j--)
                {
                    int rackID = (i + 1) + 10;
                    int powerLevel = rackID * (j + 1);
                    powerLevel += gridSerialNum;
                    powerLevel = powerLevel * rackID;
                    powerLevel = (powerLevel / 100) % 10;
                    powerLevel -= 5;
                    grid[i, j] = powerLevel;

                    int maxPossibleSize = Math.Min(gridSize - i, gridSize - j);
                    fuel = 0;
                    for (int m = 0; m < maxPossibleSize; m++)
                    {
                        for (int a = 0; a < m+1; a++)
                        {
                            fuel += grid[i + a, j + m];
                        }
                        for (int b = 0; b < m; b++)
                        {
                            fuel += grid[i + m, j + b];
                        }
                        if (fuel > maxFuel)
                        {
                            maxFuel = fuel;
                            x = i + 1;
                            y = j + 1;
                            d = m + 1;
                        }
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine(x + "," + y + "," + d);
        }

            static void Main(string[] args)
        {
            // Part1();
            Part2();
        }
    }
}
