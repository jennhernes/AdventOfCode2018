using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    class Nanobot
    {
        public int signalR;
        public int x;
        public int y;
        public int z;

        public Nanobot(int signalR, int x, int y, int z)
        {
            this.signalR = signalR;
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var file = new System.IO.StreamReader("..\\..\\input.txt");
            string line;
            string[] delims = { "=", "<", ",", ">", " " };
            List<Nanobot> bots = new List<Nanobot>();
            int maxR = 0;
            int strongestIndex = 0;
            int minX = 0;
            int minY = 0;
            int minZ = 0;
            int maxX = 0;
            int maxY = 0;
            int maxZ = 0;

            while ((line = file.ReadLine()) != null)
            {
                string[] split = line.Split(delims, System.StringSplitOptions.RemoveEmptyEntries);
                bots.Add(new Nanobot(int.Parse(split[5]), int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3])));
                if (bots[bots.Count - 1].signalR > maxR)
                {
                    maxR = bots[bots.Count - 1].signalR;
                    strongestIndex = bots.Count - 1;
                }
                minX = Math.Min(minX, bots[bots.Count - 1].x);
                minY = Math.Min(minY, bots[bots.Count - 1].y);
                minZ = Math.Min(minZ, bots[bots.Count - 1].z);
                maxX = Math.Max(maxX, bots[bots.Count - 1].x);
                maxY = Math.Max(maxY, bots[bots.Count - 1].y);
                maxZ = Math.Max(maxZ, bots[bots.Count - 1].z);
            }
            Nanobot strongBot = bots[strongestIndex];
            int numInRange = 0;

            foreach (Nanobot b in bots)
            {
                int distance = Math.Abs(strongBot.x - b.x) + Math.Abs(strongBot.y - b.y) + Math.Abs(strongBot.z - b.z);
                if (distance <= strongBot.signalR)
                {
                    numInRange++;
                }
            }

            int[,,] cave = new int[Math.Abs(strongBot.x), Math.Abs(strongBot.y), Math.Abs(strongBot.z)];

            int maxNumBots = 0;
            int posManDis = 0;
            int posx = 0;
            int posy = 0;
            int posz = 0;
            for (int i = 0; i < cave.GetLength(0); i++)
            {
                for (int j = 0; j < cave.GetLength(1); j++)
                {
                    for (int k = 0; k < cave.GetLength(2); k++)
                    {
                        foreach (Nanobot b in bots)
                        {
                            int x = i + strongBot.x;
                            int y = j + strongBot.y;
                            int z = k + strongBot.z;

                            int distance = Math.Abs(x - b.x) + Math.Abs(y - b.y) + Math.Abs(z - b.z);
                            if (distance <= b.signalR)
                            {
                                cave[i, j, k]++;
                            }
                        }
                    }
                }
            }

            //System.Diagnostics.Debug.WriteLine(numInRange + " " + maxR);

            //Dictionary<string, int> cave = new Dictionary<string, int>();
            //foreach (Nanobot b in bots)
            //{
            //    int left = Math.Max(minX, b.x - b.signalR);
            //    int right = Math.Min(maxX, b.x + b.signalR);
            //    int up = Math.Max(minY, b.y - b.signalR);
            //    int down =Math.Min(maxY, b.y + b.signalR);
            //    int back = Math.Max(minZ, b.z - b.signalR);
            //    int forward =Math.Min(maxZ, b.z + b.signalR);

            //    for (int i = left; i < right + 1; i++)
            //    {
            //        for (int j = up; j < down + 1; j++)
            //        {
            //            for (int k = back; k < forward + 1; k++)
            //            {
            //                int distance = Math.Abs(i - b.x) + Math.Abs(j - b.y) + Math.Abs(k - b.z);
            //                if (distance <= b.signalR)
            //                {
            //                    string pos = i.ToString() + "," + j.ToString() + "," + k.ToString();
            //                    if (cave.ContainsKey(pos))
            //                    {
            //                        cave[pos]++;
            //                    }
            //                    else
            //                    {
            //                        cave.Add(pos, 1);
            //                    }
            //                }
            //            }

            //        }
            //    }
            //}

            //int maxNumBots = 0;
            //int posManDis = 0;
            //int posx = 0;
            //int posy = 0;
            //int posz = 0;

            //foreach (KeyValuePair<string, int> kvp in cave)
            //{
            //    string k = kvp.Key;
            //    int x = int.Parse(k.Split(',')[0]);
            //    int y = int.Parse(k.Split(',')[1]);
            //    int z = int.Parse(k.Split(',')[2]);

            //    int distance = x + y + z;

            //    int v = kvp.Value;
            //    if ((v > maxNumBots) || (v == maxNumBots && distance < posManDis))
            //    {
            //        posx = x;
            //        posy = y;
            //        posz = z;
            //        posManDis = distance;
            //        maxNumBots = v;
            //    }
            //}

            System.Diagnostics.Debug.WriteLine(posManDis + " " + posx + " " + posy + " " + posz + " " + maxNumBots);
        }
    }
}
