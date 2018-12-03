using System;
using System.Collections.Generic;


namespace Day03
{
    public class Rect
    {
        public int xOffset;
        public int yOffset;
        public int width;
        public int height;

        public Rect(int xOffset, int yOffset, int width, int height)
        {
            this.xOffset = xOffset;
            this.yOffset = yOffset;
            this.width = width;
            this.height = height;
        }
    }

    public class Claim
    {
        public string id;
        public Rect rect;

        public Claim(string id, Rect rect)
        {
            this.id = id;
            this.rect = rect;
        }
    }
    class Day03
    {
        static void Part1()
        {
            int fabricWidth = 0;
            int fabricHeight = 0;
            string line;
            List<Rect> claims = new List<Rect>();
            int numClaims = 0;
            int overLaps = 0;

            var file = new System.IO.StreamReader("C:\\Users\\Jenn\\Documents\\AdventOfCode2018\\Day03\\puzzle.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] pieces = line.Split();
                string[] topCorner = pieces[2].Split(',', ':');
                string[] dims = pieces[3].Split('x');

                claims.Add(new Rect(int.Parse(topCorner[0]), int.Parse(topCorner[1]), int.Parse(dims[0]), int.Parse(dims[1])));

                int newWidth = claims[numClaims].xOffset + claims[numClaims].width;
                int newHeight = claims[numClaims].yOffset + claims[numClaims].height;
                
                fabricWidth = Math.Max(fabricWidth, newWidth);
                fabricHeight = Math.Max(fabricHeight, newHeight);

                numClaims++;
            }

            int[,] fabricVisual = new int[fabricHeight, fabricWidth];
            for (int i = 0; i < numClaims; i++)
            {
                int yLast = claims[i].yOffset + claims[i].height;
                for (int y = claims[i].yOffset; y < yLast; y++)
                {
                    int xLast = claims[i].xOffset + claims[i].width;
                    for (int x  = claims[i].xOffset; x < xLast; x++)
                    {
                        fabricVisual[y,x]++;
                    }
                }
            }

            for (int i = 0; i < fabricHeight; i++)
            {
                for (int j = 0; j < fabricWidth; j++)
                {
                    if (fabricVisual[i,j] > 1)
                    {
                        overLaps++;
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine(overLaps);
        }

        static void Part2()
        {
            int fabricWidth = 0;
            int fabricHeight = 0;
            string line;
            List<Claim> claims = new List<Claim>();
            int numClaims = 0;
            int overLaps = 0;

            var file = new System.IO.StreamReader("C:\\Users\\Jenn\\Documents\\AdventOfCode2018\\Day03\\puzzle.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] pieces = line.Split();
                string[] topCorner = pieces[2].Split(',', ':');
                string[] dims = pieces[3].Split('x');

                claims.Add(new Claim(pieces[0], new Rect(int.Parse(topCorner[0]), int.Parse(topCorner[1]), int.Parse(dims[0]), int.Parse(dims[1]))));

                int newWidth = claims[numClaims].rect.xOffset + claims[numClaims].rect.width;
                int newHeight = claims[numClaims].rect.yOffset + claims[numClaims].rect.height;

                fabricWidth = Math.Max(fabricWidth, newWidth);
                fabricHeight = Math.Max(fabricHeight, newHeight);

                numClaims++;
            }

            int[,] fabricVisual = new int[fabricHeight, fabricWidth];
            for (int i = 0; i < numClaims; i++)
            {
                int yLast = claims[i].rect.yOffset + claims[i].rect.height;
                for (int y = claims[i].rect.yOffset; y < yLast; y++)
                {
                    int xLast = claims[i].rect.xOffset + claims[i].rect.width;
                    for (int x = claims[i].rect.xOffset; x < xLast; x++)
                    {
                        fabricVisual[y, x]++;
                    }
                }
            }

            for (int i = 0; i < numClaims; i++)
            {
                int yLast = claims[i].rect.yOffset + claims[i].rect.height;
                for (int y = claims[i].rect.yOffset; y < yLast; y++)
                {
                    int xLast = claims[i].rect.xOffset + claims[i].rect.width;
                    for (int x = claims[i].rect.xOffset; x < xLast; x++)
                    {
                        if (fabricVisual[y,x] > 1)
                        {
                            claims[i].id = "overlapping";
                        }
                    }
                }
            }

            for (int i = 0; i < numClaims; i++)
            {
                if (claims[i].id != "overlapping")
                {
                    System.Diagnostics.Debug.WriteLine(claims[i].id);
                    break;
                }
            }
            System.Diagnostics.Debug.WriteLine(overLaps);
        }

        static void Main(string[] args)
        {
            //Part1();
            Part2();
        }
    }
}
