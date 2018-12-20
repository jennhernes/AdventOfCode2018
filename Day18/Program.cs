using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            int finalGeneration = 1000000000; // 10;
            var file = new System.IO.StreamReader("..\\..\\input.txt");
            string line;

            List<List<List<char>>> generations = new List<List<List<char>>>();
            List<int> totals = new List<int>();

            generations.Add(new List<List<char>>());
            while ((line = file.ReadLine()) != null)
            {
                generations[0].Add(line.ToList());
            }

            WriteLandscapeToFile(generations[0]);

            int treeAcres = 0;
            int yardAcres = 0;

            for (int i = 0; i < generations[0].Count; i++)
            {
                for (int j = 0; j < generations[0][i].Count; j++)
                {
                    if (generations[0][i][j] == '|')
                    {
                        treeAcres++;
                    }
                    else if (generations[0][i][j] == '#')
                    {
                        yardAcres++;
                    }
                }
            }

            int total = treeAcres * yardAcres;
            totals.Add(total);

            int cycleLength = 0;
            int final = 0;
            for (int currentGen = 1; currentGen <= finalGeneration; currentGen++)
            {
                generations.Add(new List<List<char>>());

                for (int i = 0; i < generations[currentGen - 1].Count; i++)
                {
                    generations[currentGen].Add(new List<char>());

                    for (int j = 0; j < generations[currentGen - 1][i].Count; j++)
                    {
                        generations[currentGen][i].Add(generations[currentGen - 1][i][j]);

                        int neighbourTrees = 0;
                        int neighbourYards = 0;
                        for (int di = -1; di <= 1; di++)
                        {
                            for (int dj = -1; dj <= 1; dj++)
                            {
                                if (di == 0 && dj == 0)
                                {
                                    continue;
                                }
                                else if (i + di < 0 || i + di == generations[currentGen - 1].Count ||
                                    j + dj < 0 || j + dj == generations[currentGen - 1][i + di].Count)
                                {
                                    continue;
                                }
                                else if (generations[currentGen - 1][i + di][j + dj] == '|')
                                {
                                    neighbourTrees++;
                                }
                                else if (generations[currentGen - 1][i + di][j + dj] == '#')
                                {
                                    neighbourYards++;
                                }
                            }
                        }


                        if (generations[currentGen - 1][i][j] == '.')
                        {
                            if (neighbourTrees >= 3)
                            {
                                generations[currentGen][i][j] = '|';
                                treeAcres++;
                            }
                        }
                        else if (generations[currentGen - 1][i][j] == '|')
                        {
                            if (neighbourYards >= 3)
                            {
                                generations[currentGen][i][j] = '#';
                                treeAcres--;
                                yardAcres++;
                            }
                        }
                        else if (generations[currentGen - 1][i][j] == '#')
                        {
                            if (neighbourTrees < 1 || neighbourYards < 1)
                            {
                                generations[currentGen][i][j] = '.';
                                yardAcres--;
                            }
                        }
                    }
                }

                total = treeAcres * yardAcres;
                int firstIndex = totals.IndexOf(total);
                // int firstIndex = generations.IndexOf(generations[currentGen]);
                //if (firstIndex == -1)
                //{
                //    break;
                //}
                if (firstIndex > -1 && firstIndex < currentGen - 2) // if (totals.Contains(total))
                {
                    if (currentGen - firstIndex == cycleLength)
                    {
                        final = (finalGeneration - currentGen) % cycleLength + firstIndex;
                        break;
                    }

                    cycleLength = currentGen - firstIndex;
                }

                totals.Add(total);

                WriteLandscapeToFile(generations[currentGen]);
                // generations.RemoveAt(0);
            }

            System.Diagnostics.Debug.WriteLine(totals[final]);
            System.Diagnostics.Debug.WriteLine(totals.Count);
            System.Diagnostics.Debug.WriteLine(totals[totals.Count - 1]);
            System.Diagnostics.Debug.WriteLine(cycleLength);
            System.Diagnostics.Debug.WriteLine(final);


        }

        static void WriteLandscapeToFile(List<List<char>> gen)
        {

            var writeFile = new System.IO.StreamWriter("..\\..\\generations.txt");

            foreach (List<char> row in gen)
            {
                foreach (char acre in row)
                {
                    writeFile.Write(acre);
                }

                writeFile.WriteLine();
            }

            writeFile.Close();
        }
    }
}
