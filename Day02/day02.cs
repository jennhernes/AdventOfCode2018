using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    class day02
    {
        static void Part1()
        {
            string line;
            bool found2 = false;
            bool found3 = false;
            int twos = 0;
            int threes = 0;
            int index;
            int checksum = 0;
            var letters = new int[26];
            var file = new System.IO.StreamReader("C:\\Users\\jenn_\\Documents\\AdventOfCode2018\\Day02\\puzzle.txt");

            while ((line = file.ReadLine()) != null)
            {
                foreach (char c in line)
                {
                    index = (int)c - 97;
                    letters[index] += 1;
                }

                for (int i = 0; i < 26; i++)
                {
                    if (letters[i] == 2)
                    {
                        found2 = true;
                    } else if (letters[i] == 3)
                    {
                        found3 = true;
                    }

                    letters[i] = 0;
                }

                if (found2)
                {
                    twos++;
                }
                if (found3)
                {
                    threes++;
                }
                found2 = false;
                found3 = false;
            }

            checksum = twos * threes;
            System.Diagnostics.Debug.WriteLine(checksum);
        }

        static void Part2()
        {
            bool diffFound = false;
            string box1 = "";
            string box2 = "";
            string commonID;
            int diffIndex = 0;

            var file = new System.IO.StreamReader("C:\\Users\\jenn_\\Documents\\AdventOfCode2018\\Day02\\puzzle.txt");

            string contents = file.ReadToEnd();
            string[] split = { "\r\n" };

            string[] idList = contents.Split(split, System.StringSplitOptions.RemoveEmptyEntries);


            foreach (string id1 in idList)
            {
                foreach (string id2 in idList)
                {
                    for (int i = 0; i < 26; i++)
                    {
                        if (id1[i] != id2[i])
                        {
                            if (diffFound)
                            {
                                diffFound = false;
                                diffIndex = -1;
                                break;
                            } else
                            {
                                diffFound = true;
                                diffIndex = i;
                            }
                        }
                    }

                    if (diffFound)
                    {
                        box1 = id1;
                        box2 = id2;
                        break;
                    }
                }

                if (diffFound)
                {
                    break;
                }
            }

            commonID = box1.Substring(0,diffIndex) + box1.Substring(diffIndex+1);
            System.Diagnostics.Debug.WriteLine(commonID);
        }

        static void Main(string[] args)
        {
            //Part1();
            Part2();
        }
    }
}
