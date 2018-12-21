using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<char>> facility = new List<List<char>>();
            facility.Add(new List<char> { '#', '?', '#' });
            facility.Add(new List<char> { '?', 'X', '?' });
            facility.Add(new List<char> { '#', '?', '#' });

            var file = new System.IO.StreamReader("..\\..\\input.txt");
            List<char> regex;
            List<char> copy = new List<char>();
            regex = file.ReadLine().ToList();
            regex.Remove('^');

            int numRooms = FindShortestPath(ref regex, ref facility, 1, 1);

            System.Diagnostics.Debug.WriteLine(numRooms);
        }

        static int FindShortestPath(ref List<char> regex, ref List<List<char>> facility,
            int prevX, int prevY)
        {
            List<List<int>> branches = new List<List<int>>();
            branches.Add(new List<int>());
            branches[0].Add(0);

            List<int[]> branchCoords = new List<int[]>();
            branchCoords.Add(new int[2] { prevX, prevY });

            int numRooms = 0;
            while (regex[0] != '$')
            {
                char next = regex[0];
                regex.RemoveAt(0);

                if (next == 'N' || next == 'S' || next == 'W' || next == 'E')
                {
                    if (next == 'N')
                    {
                        if (prevY == 1)
                        {
                            facility.Insert(0, new List<char>());
                            facility.Insert(0, new List<char>());

                            for (int u = 0; u < branchCoords.Count; u++)
                            {
                                branchCoords[u][1] += 2;
                            }

                            for (int u = 0; u < facility[2].Count; u++)
                            {
                                facility[0].Add('#');
                                facility[1].Add('?');
                            }
                            prevY += 2;
                        }

                        prevY -= 2;

                        facility[prevY + 1][prevX] = '-';

                        if (facility[prevY][prevX] != 'X' && facility[prevY][prevX] != '+')
                        {
                            facility[prevY][prevX] = '.';
                        }
                    }
                    else if (next == 'S')
                    {
                        //for (int i = 0; i < branchCoords.Count; i++)
                        //{
                        //    if (branchCoords[i][1] > prevY + 2)
                        //    {
                        //        branchCoords[i][1] += 2;
                        //    }
                        //}

                        if (prevY == facility.Count - 2)
                        {
                            facility.Add(new List<char>());
                            facility.Add(new List<char>());
                            for (int u = 0; u < facility[prevY].Count; u++)
                            {
                                facility[prevY + 2].Add('?');
                                facility[prevY + 3].Add('#');
                            }
                        }

                        prevY += 2;

                        facility[prevY - 1][prevX] = '-';

                        if (facility[prevY][prevX] != 'X' && facility[prevY][prevX] != '+')
                        {
                            facility[prevY][prevX] = '.';
                        }
                    }
                    else if (next == 'W')
                    {
                        if (prevX == 1)
                        {
                            for (int u = 0; u < branchCoords.Count; u++)
                            {
                                branchCoords[u][0] += 2;
                            }

                            for (int u = 0; u < facility.Count; u++)
                            {
                                facility[u].Insert(0, '#');
                                facility[u].Insert(1, '?');
                            }
                            prevX += 2;
                        }

                        prevX -= 2;

                        facility[prevY][prevX + 1] = '|';

                        if (facility[prevY][prevX] != 'X' && facility[prevY][prevX] != '+')
                        {
                            facility[prevY][prevX] = '.';
                        }
                    }
                    else if (next == 'E')
                    {
                        //for (int i = 0; i < branchCoords.Count; i++)
                        //{
                        //    if (branchCoords[i][0] > prevX + 2)
                        //    {
                        //        branchCoords[i][0] += 2;
                        //    }
                        //}

                        if (prevX == facility[prevY].Count - 2)
                        {
                            for (int u = 0; u < facility.Count; u++)
                            {
                                facility[u].Insert(prevX + 2, '?');
                                facility[u].Insert(prevX + 3, '#');
                            }
                        }

                        prevX += 2;

                        facility[prevY][prevX - 1] = '|';

                        if (facility[prevY][prevX] != 'X' && facility[prevY][prevX] != '+')
                        {
                            facility[prevY][prevX] = '.';
                        }
                    }
                    int i = branches.Count - 1;
                    int j = branches[i].Count - 1;
                    branches[i][j]++;
                    if (branches[i][j] >= 1000 && facility[prevY][prevX] == '.')
                    {
                        numRooms++;
                        facility[prevY][prevX] = '+';
                    }
                }
                else if (next == '(')
                {
                    int i = branches.Count - 1;
                    int j = branches[i].Count - 1;
                    branches.Add(new List<int>());
                    branches[i + 1].Add(branches[i][j]);

                    branchCoords.Add(new int[2] { prevX, prevY });
                }
                else if (next == '|')
                {
                    int i = branches.Count - 2;
                    int j = branches[i].Count - 1;
                    branches[i + 1].Add(branches[i][j]);

                    int[] coords = branchCoords.Last();
                    prevX = coords[0];
                    prevY = coords[1];
                }
                else if (next == ')')
                {
                    int i = branches.Count - 1;
                    int j = branches[i - 1].Count - 1;
                    int longestOption = 0;
                    foreach (int branch in branches[i])
                    {
                        if (branch == branches[i - 1][j])
                        {
                            longestOption = branches[i - 1][j];
                            break;
                        }
                        longestOption = Math.Max(longestOption, branch);
                    }
                    branches.RemoveAt(branches.Count - 1);
                    branches[i - 1][j] = longestOption;

                    int[] coords = branchCoords.Last();
                    branchCoords.RemoveAt(branchCoords.Count - 1);
                    prevX = coords[0];
                    prevY = coords[1];
                }
            }

            System.Diagnostics.Debug.WriteLine(branches[0][0]);

            return (numRooms);
        }

        static void WriteFacilityToFile(List<List<char>> facility)
        {
            var writeFile = new System.IO.StreamWriter("..\\..\\facility.txt");
            for (int i = 0; i < facility.Count; i++)
            {
                for (int j = 0; j < facility[i].Count; j++)
                {
                    writeFile.Write(facility[i][j]);
                }
                writeFile.WriteLine();
            }
            writeFile.Close();
        }

        static void BuildFacility(ref List<char> regex, ref List<List<char>> facility,
            int prevX, int prevY)
        {
            char first;
            List<int[]> branchCoords = new List<int[]>();
            branchCoords.Add(new int[2] { prevX, prevY });

            while (regex[0] != '$')
            {
                first = regex[0];
                regex.RemoveAt(0);

                if (facility[prevY - 1][prevX] == '?')
                {
                    facility[prevY - 1][prevX] = '#';
                }
                if (facility[prevY + 1][prevX] == '?')
                {
                    facility[prevY + 1][prevX] = '#';
                }
                if (facility[prevY][prevX - 1] == '?')
                {
                    facility[prevY][prevX - 1] = '#';
                }
                if (facility[prevY][prevX + 1] == '?')
                {
                    facility[prevY][prevX + 1] = '#';
                }

                if (first == '(')
                {
                    branchCoords.Add(new int[2] { prevX, prevY });
                }
                else if (first == '|')
                {
                    int[] coords = branchCoords.Last();
                    prevX = coords[0];
                    prevY = coords[1];
                }
                else if (first == ')')
                {
                    int[] coords = branchCoords.Last();
                    branchCoords.RemoveAt(branchCoords.Count - 1);
                    prevX = coords[0];
                    prevY = coords[1];
                }
                else if (first == 'N' || first == 'W' || first == 'S' || first == 'E')
                {
                    if (first == 'N')
                    {
                        if (prevY == 1)
                        {
                            facility.Insert(0, new List<char>());
                            facility.Insert(0, new List<char>());

                            for (int i = 0; i < branchCoords.Count; i++)
                            {
                                branchCoords[i][1] += 2;
                            }

                            for (int i = 0; i < facility[2].Count; i++)
                            {
                                facility[0].Add('#');
                                facility[1].Add('?');
                            }
                            prevY += 2;
                        }
                        facility[prevY - 1][prevX] = '-';
                        if (facility[prevY - 2][prevX] != 'X')
                        {
                            facility[prevY - 2][prevX] = '.';
                        }

                        prevY -= 2;
                    }
                    else if (first == 'S')
                    {
                        //for (int i = 0; i < branchCoords.Count; i++)
                        //{
                        //    if (branchCoords[i][1] > prevY + 2)
                        //    {
                        //        branchCoords[i][1] += 2;
                        //    }
                        //}

                        if (prevY == facility.Count - 2)
                        {
                            facility.Add(new List<char>());
                            facility.Add(new List<char>());
                            for (int i = 0; i < facility[prevY].Count; i++)
                            {
                                facility[prevY + 2].Add('?');
                                facility[prevY + 3].Add('#');
                            }
                        }
                        if (facility[prevY + 2][prevX] != 'X')
                        {
                            facility[prevY + 2][prevX] = '.';
                        }
                        facility[prevY + 1][prevX] = '-';

                        prevY += 2;
                    }
                    else if (first == 'W')
                    {
                        if (prevX == 1)
                        {
                            for (int i = 0; i < branchCoords.Count; i++)
                            {
                                branchCoords[i][0] += 2;
                            }

                            for (int i = 0; i < facility.Count; i++)
                            {
                                facility[i].Insert(0, '#');
                                facility[i].Insert(1, '?');
                            }
                            prevX += 2;
                        }
                        facility[prevY][prevX - 1] = '|';

                        if (facility[prevY][prevX - 2] != 'X')
                        {
                            facility[prevY][prevX - 2] = '.';
                        }

                        prevX -= 2;
                    }
                    else if (first == 'E')
                    {
                        //for (int i = 0; i < branchCoords.Count; i++)
                        //{
                        //    if (branchCoords[i][0] > prevX + 2)
                        //    {
                        //        branchCoords[i][0] += 2;
                        //    }
                        //}

                        if (prevX == facility[prevY].Count - 2)
                        {
                            for (int i = 0; i < facility.Count; i++)
                            {
                                facility[i].Insert(prevX + 2, '?');
                                facility[i].Insert(prevX + 3, '#');
                            }
                        }

                        prevX += 2;

                        facility[prevY][prevX - 1] = '|';
                        if (facility[prevY][prevX] != 'X' && facility[prevY][prevX] != '+')
                        {
                            facility[prevY][prevX] = '.';
                        }
                    }
                }
                //else if (first == '(')
                //{
                //    BuildFacility(ref regex, ref facility, prevX, prevY);
                //} else if (first == ')')
                //{
                //    return;
                //} else if (first == '|')
                //{
                //    if (regex[0] == ')')
                //    {
                //        BuildFacility(ref regex, ref facility, prevX, prevY);
                //    }
                //    return;
                //}
            }

            if (facility[prevY - 1][prevX] == '?')
            {
                facility[prevY - 1][prevX] = '#';
            }
            if (facility[prevY + 1][prevX] == '?')
            {
                facility[prevY + 1][prevX] = '#';
            }
            if (facility[prevY][prevX - 1] == '?')
            {
                facility[prevY][prevX - 1] = '#';
            }
            if (facility[prevY][prevX + 1] == '?')
            {
                facility[prevY][prevX + 1] = '#';
            }
        }
    }
}
