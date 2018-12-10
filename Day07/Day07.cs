using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07
{
    class Day07
    {
        static void Part1()
        {
            string line;
            var file = new System.IO.StreamReader("..\\..\\puzzle.txt");
            int numVertices = 0;
            int[,] adjMatrix = new int[26, 26];
            bool[] vertices = new bool[26];
            int asciiStart = 65;

            for (int i = 0; i < 26; i++)
            {
                vertices[i] = true;
            }

            while ((line = file.ReadLine()) != null)
            {
                string[] splitLine = line.Split();
                int i = (int)(splitLine[1].ToCharArray()[0]) - asciiStart;
                int j = (int)(splitLine[7].ToCharArray()[0]) - asciiStart;
                adjMatrix[i, j] = 1;
                vertices[j] = false;
                numVertices = Math.Max(numVertices, i);
                numVertices = Math.Max(numVertices, j);
            }
            numVertices++;

            /*
            var writeFile = new System.IO.StreamWriter("..\\..\\adjMatrix.txt");
            for (int i = 0; i < numVertices; i++)
            {
                for (int j = 0; j < numVertices; j++)
                {
                    writeFile.Write(adjMatrix[i, j]);
                    writeFile.Write(" ");
                }

                writeFile.WriteLine("");
            }
            writeFile.Close();
            */

            List<int> toCheck = new List<int>();

            for (int i = 0; i < numVertices; i++)
            {
                if (vertices[i])
                {
                    toCheck.Add(i);
                }
            }

            char c;

            while (toCheck.Count > 0)
            {
                toCheck.Sort();
                int currentVertex = toCheck[0];
                toCheck.RemoveAt(0);
                c = (char)(currentVertex + asciiStart);
                System.Diagnostics.Debug.Write(c);
                
                for (int k = 0; k < numVertices; k++)
                {
                    if ((adjMatrix[currentVertex, k] == 1) && !vertices[k])
                    {
                        vertices[k] = true;
                        toCheck.Add(k);

                        for (int i = 0; i < numVertices; i++)
                        {
                            if ((adjMatrix[i,k] == 1) && (!vertices[i] || toCheck.Contains(i)))
                            {
                                vertices[k] = false;
                                toCheck.Remove(k);
                                break;
                            }
                        }
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("");
        }

        static void Part2()
        {
            string order = "BCADPVTJFZNRWXHEKSQLUYGMIO";

            string line;
            var file = new System.IO.StreamReader("..\\..\\puzzle.txt");
            int numVertices = 0;
            int[,] adjMatrix = new int[26, 26];
            bool[] vertices = new bool[26];
            int asciiStart = 65;

            for (int i = 0; i < 26; i++)
            {
                vertices[i] = true;
            }

            while ((line = file.ReadLine()) != null)
            {
                string[] splitLine = line.Split();
                int i = (int)(splitLine[1].ToCharArray()[0]) - asciiStart;
                int j = (int)(splitLine[7].ToCharArray()[0]) - asciiStart;
                adjMatrix[i, j] = 1;
                vertices[j] = false;
                numVertices = Math.Max(numVertices, i);
                numVertices = Math.Max(numVertices, j);
            }
            numVertices++;

            List<int> toCheck = new List<int>();

            for (int i = 0; i < numVertices; i++)
            {
                if (vertices[i])
                {
                    toCheck.Add(i);
                }
            }

            char c;
            List<WorkingElf> elves = new List<WorkingElf>();
            int maxElves = 5;
            int numWorking = 0;
            int additionalTime = 61;
            List<int> completed = new List<int>();
            int totalTime = -1;
            

            while (completed.Count < numVertices)
            {
                totalTime++;

                numWorking = elves.Count;
                for (int j = 0; j < numWorking; j++)
                {
                    elves[j].timeElapsed++;
                    if (elves[j].timeElapsed == elves[j].job+additionalTime)
                    {
                        int currentVertex = elves[j].job;
                        c = (char)(currentVertex + asciiStart);
                        System.Diagnostics.Debug.Write(c);
                        completed.Add(currentVertex);
                        elves.RemoveAt(j);
                        j--;
                        numWorking--;

                        for (int k = 0; k < numVertices; k++)
                        {
                            if ((adjMatrix[currentVertex, k] == 1) && !vertices[k])
                            {
                                vertices[k] = true;
                                toCheck.Add(k);

                                for (int i = 0; i < numVertices; i++)
                                {
                                    if ((adjMatrix[i, k] == 1) && (!vertices[i] || toCheck.Contains(i) || !completed.Contains(i)))
                                    {
                                        vertices[k] = false;
                                        toCheck.Remove(k);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                while (elves.Count < maxElves && toCheck.Count > 0)
                {
                    toCheck.Sort();
                    elves.Add(new WorkingElf(toCheck[0], 0));
                    toCheck.RemoveAt(0);

                }
            }

            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine(totalTime);
        }

        static void Main(string[] args)
        {
            // Part1();
            Part2();
        }
    }

    public class WorkingElf
    {
        public int job;
        public int timeElapsed;

        public WorkingElf(int job, int timeElapsed)
        {
            this.job = job;
            this.timeElapsed = timeElapsed;
        }
    }
}
