using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    class Day12
    {
        static void Part1()
        {
            Int64 finalGeneration = 20;
            var file = new System.IO.StreamReader("..\\..\\puzzle.txt");
            string line;

            List<List<char>> generations = new List<List<char>>();

            line = file.ReadLine();

            string[] split = line.Split();
            generations.Add(split[2].ToList<char>());
            generations[0].Insert(0, '.');
            generations[0].Insert(0, '.');
            generations[0].Add('.');
            generations[0].Add('.');

            List<char[]> rules = new List<char[]>();
            while ((line = file.ReadLine()) != null)
            {
                if (line != "" && line[9] == '#')
                {
                    rules.Add(line.Substring(0, 5).ToCharArray());
                }
            }

            char[] situation = new char[5];
            Int64 zeroPos = 2;
            Int64 numPlants = 0;
            Int64 prevNumPlants= 0;
            int numConsistentGen = 0;
            Int64 currentGen;
            Int64 prevPlantShift = 0;
            Int64 currentPlantShift = 0;
            for (currentGen = 1; currentGen <= finalGeneration; currentGen++)
            {
                while (generations[0][0] == '.' &&
                    generations[0][1] == '.' &&
                    generations[0][2] == '.')
                {
                    generations[0].RemoveAt(0);
                    zeroPos--;
                }

                generations.Add(new List<char>());
                for (int plantIndex = 0; plantIndex < generations[0].Count; plantIndex++)
                {
                    if (plantIndex > 1)
                    {
                        situation[0] = generations[0][plantIndex - 2];
                        situation[1] = generations[0][plantIndex - 1];
                    } else if (plantIndex > 0)
                    {
                        situation[0] = '.';
                        situation[1] = generations[0][plantIndex - 1];
                    } else
                    {
                        situation[0] = '.';
                        situation[1] = '.';
                    }

                    situation[2] = generations[0][plantIndex];

                    if (plantIndex < generations[0].Count - 2)
                    {
                        situation[3] = generations[0][plantIndex + 1];
                        situation[4] = generations[0][plantIndex + 2];
                    }
                    else if (plantIndex < generations[0].Count - 1)
                    {
                        situation[3] = generations[0][plantIndex + 1];
                        situation[4] = '.';
                    }
                    else
                    {
                        situation[3] = '.';
                        situation[4] = '.';
                    }

                    generations[1].Add('.');

                    foreach (char[] rule in rules)
                    {
                        if (rule[0] == situation[0] &&
                            rule[1] == situation[1] &&
                            rule[2] == situation[2] &&
                            rule[3] == situation[3] &&
                            rule[4] == situation[4])
                        {
                            generations[1][plantIndex] = '#';
                            if (plantIndex == 1)
                            {
                                generations[0].Insert(0, '.');
                                generations[1].Insert(0, '.');
                                plantIndex++;
                                zeroPos++;
                            }
                            else if (plantIndex == 0)
                            {
                                generations[0].Insert(0, '.');
                                generations[0].Insert(0, '.');
                                generations[1].Insert(0, '.');
                                generations[1].Insert(0, '.');
                                plantIndex += 2;
                                zeroPos += 2;
                            }
                            else if (plantIndex == generations[0].Count - 2)
                            {
                                generations[0].Add('.');
                                generations[1].Add('.');
                                plantIndex++;
                            }
                            else if (plantIndex == generations[0].Count - 1)
                            {
                                generations[0].Add('.');
                                generations[0].Add('.');

                                generations[1].Add('.');
                                generations[1].Add('.');
                                plantIndex += 2;
                            }
                            break;
                        }
                    }
                }

                prevNumPlants = numPlants;
                numPlants = 0;
                for (int i = 0; i < generations[1].Count; i++)
                {
                    if (generations[1][i] == '#')
                    {
                        numPlants++;
                    }
                }

                if (prevNumPlants == numPlants)
                {
                    currentPlantShift = generations[1].IndexOf('#') - generations[0].IndexOf('#');
                    if (numConsistentGen > 2 && currentPlantShift == prevPlantShift)
                    {
                        System.Diagnostics.Debug.WriteLine(numPlants);
                        generations.RemoveAt(0);
                        break;
                    }
                    else
                    {
                        numConsistentGen++;
                        prevPlantShift = currentPlantShift;
                    }
                }
                else
                {
                    numConsistentGen = 0;
                }

                generations.RemoveAt(0);
            }

            Int64 total = 0;
            Int64 finalPosDiff = zeroPos - (finalGeneration - currentGen) * currentPlantShift;
            foreach (List<char> gen in generations)
            {
                for (int i = 0; i < gen.Count; i++)
                {
                    if (gen[i] == '#')
                    {
                        total += i - finalPosDiff;
                    }
                }
            }
            
            System.Diagnostics.Debug.WriteLine(total);

            var writeFile = new System.IO.StreamWriter("..\\..\\generations.txt");
            foreach (List<char> gen in generations)
            {
                foreach(char pot in gen)
                {
                    writeFile.Write(pot);
                }
                writeFile.WriteLine();
            }

            writeFile.Close();
        }

        static void Main(string[] args)
        {
            Part1();
            // Part2();
        }
    }
}
