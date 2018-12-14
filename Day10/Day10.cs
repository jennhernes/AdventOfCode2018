using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    public class Light
    {
        public int posx;
        public int posy;
        public int vx;
        public int vy;

        public Light(int posx, int posy, int vx, int vy)
        {
            this.posx = posx;
            this.posy = posy;
            this.vx = vx;
            this.vy = vy;
        }

        public void updatePosition()
        {
            this.posx += this.vx;
            this.posy += this.vy;
        }

        public void reversePosition()
        {
            this.posx -= this.vx;
            this.posy -= this.vy;
        }
    }
    class Day10
    {
        static void Part1()
        {
            var file = new System.IO.StreamReader("..\\..\\puzzle.txt");
            int x;
            int y;
            int maxX = 0;
            int maxY = 0;
            int minX = 0;
            int minY = 0;
            string line;
            string[] delims = { "=", "<", ">", ",", " " };
            List<Light> lights = new List<Light>();
            int seconds = 0;

            while ((line = file.ReadLine()) != null)
            {
                string[] split = line.Split(delims, System.StringSplitOptions.RemoveEmptyEntries);
                x = int.Parse(split[1]);
                y = int.Parse(split[2]);
                minX = Math.Min(minX, x);
                maxX = Math.Max(maxX, x);
                minY = Math.Min(minY, y);
                maxY = Math.Max(maxY, y);
                lights.Add(new Light(x, y, int.Parse(split[4]), int.Parse(split[5])));
            }

            int oldDeltaX = int.MaxValue;
            int oldDeltaY = int.MaxValue;
            int newDeltaX = int.MaxValue;
            int newDeltaY = int.MaxValue;
            int oldMinX = int.MaxValue;
            int oldMaxX = int.MinValue;
            int oldMinY = int.MaxValue;
            int oldMaxY = int.MinValue;
            minX = int.MaxValue;
            maxX = int.MinValue;
            minY = int.MaxValue;
            maxY = int.MinValue;

            while (oldDeltaX >= newDeltaX || oldDeltaY >= newDeltaY)
            {
                oldDeltaX = newDeltaX;
                oldDeltaY = newDeltaY;

                oldMinX = minX;
                oldMaxX = maxX;
                oldMinY = minY;
                oldMaxY = maxY;
                minX = int.MaxValue;
                maxX = int.MinValue;
                minY = int.MaxValue;
                maxY = int.MinValue;

                for (int i = 0; i < lights.Count; i++)
                {
                    lights[i].updatePosition();

                    x = lights[i].posx;
                    y = lights[i].posy;

                    minX = Math.Min(minX, x);
                    maxX = Math.Max(maxX, x);
                    minY = Math.Min(minY, y);
                    maxY = Math.Max(maxY, y);
                }

                newDeltaX = Math.Abs(maxX - minX);
                newDeltaY = Math.Abs(maxY - minY);
                seconds++;
            }

            char[,] sky = new char[oldDeltaY + 1, oldDeltaX + 1];

            for (int i = 0; i < lights.Count; i++)
            {
                lights[i].reversePosition();

                x = lights[i].posx;
                y = lights[i].posy;

                sky[y - oldMinY, x - oldMinX] = '#';
            }

            var writeFile = new System.IO.StreamWriter("..\\..\\sky.txt");
            for (int i = 0; i < oldDeltaY + 1; i++)
            {
                for (int j = 0; j < oldDeltaX + 1; j++)
                {
                    if (sky[i, j] == '#')
                    {
                        writeFile.Write('#');
                    }
                    else
                    {
                        writeFile.Write('.');
                    }
                }
                writeFile.WriteLine();
            }

            writeFile.Close();

            System.Diagnostics.Debug.WriteLine(seconds);
        }

        static void Main(string[] args)
        {
            Part1();
            // Part2();
        }
    }
}
