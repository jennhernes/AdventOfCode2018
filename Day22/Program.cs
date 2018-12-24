using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day22
{
    class Region
    {
        public char type;
        public int erosionLevel;
        public List<int> tools;

        public Region(char type, int erosionLevel)
        {
            this.type = type;
            this.erosionLevel = erosionLevel;
            tools = new List<int>();
            if (type == '.')
            {
                tools.Add(1);
                tools.Add(2);
            }
            else if (type == '~')
            {
                tools.Add(0);
                tools.Add(2);
            }
            else if (type == '|')
            {
                tools.Add(1);
                tools.Add(0);
            }
            else if (type == 'X')
            {
                tools.Add(1);
            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int depth = 510;
            int targetX = 10;
            int targetY = 10;
            //int depth = 3066;
            //int targetX = 13;
            //int targetY = 726;
            int riskLevel = 0;
            int geoIndex;
            int erosionLevel;
            int type;
            Region[,] cave = new Region[targetY + 8, targetX + 8];

            for (int i = 0; i < targetY + 8; i++)
            {
                for (int j = 0; j < targetX + 8; j++)
                {
                    if (i != 0 && j != 0 && !(i == targetY && j == targetX))
                    {
                        geoIndex = cave[i - 1, j].erosionLevel * cave[i, j - 1].erosionLevel;
                    }
                    else if (i == 0 && j != 0)
                    {
                        geoIndex = j * 16807;
                    }
                    else if (j == 0 && i != 0)
                    {
                        geoIndex = i * 48271;
                    }
                    else
                    {
                        geoIndex = 0;
                    }

                    erosionLevel = (geoIndex + depth) % 20183;
                    type = erosionLevel % 3;
                    if ((i == 0 && j == 0) || (i == targetY && j == targetX))
                    {
                        cave[i, j] = new Region('X', erosionLevel);
                    }
                    else if (type == 0)
                    {
                        cave[i, j] = new Region('.', erosionLevel);
                    }
                    else if (type == 1)
                    {
                        cave[i, j] = new Region('~', erosionLevel);
                    }
                    if (type == 2)
                    {
                        cave[i, j] = new Region('|', erosionLevel);
                    }
                    if (i <= targetY && j <= targetX)
                    {
                        riskLevel += type;
                    }
                }
            }

            var writeFile = new System.IO.StreamWriter("..\\..\\cave.txt");
            for (int i = 0; i < targetY + 8; i++)
            {
                for (int j = 0; j < targetX + 8; j++)
                {
                    writeFile.Write(cave[i, j].type);
                }
                writeFile.WriteLine();
            }
            writeFile.Close();

            System.Diagnostics.Debug.WriteLine(riskLevel);

            FindPath(cave, targetX, targetY, 0, 0, 1, 0);
        }

        static void FindPath(Region[,] cave, int tx, int ty, int cx, int cy, int tool, int time)
        {
            
        }
    }
}
