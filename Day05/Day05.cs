using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05
{
    class Day05
    {
        static List<char> ReducePolymer(string polymer, char removedUnit = '\0')
        {
            int asciiDistance = 97 - 65;
            int currentAscii;
            int lastAscii;
            List<char> finalChain = new List<char>();
            finalChain.Add(polymer[0]);
            char prevC = polymer[0];
            removedUnit = Char.ToLower(removedUnit);

            for (int i = 1; i < polymer.Length; i++)
            {
                char c = polymer[i];
                if (Char.ToLower(c) != removedUnit)
                {
                    currentAscii = System.Convert.ToInt32(c);
                    lastAscii = System.Convert.ToInt32(prevC);
                    if (!(Math.Abs(currentAscii - lastAscii) == asciiDistance))
                    {
                        finalChain.Add(c);
                        prevC = c;
                    }
                    else if (finalChain.Count > 0)
                    {
                        finalChain.RemoveAt(finalChain.Count - 1);
                        if (finalChain.Count > 0)
                        {
                            prevC = finalChain.ElementAt(finalChain.Count - 1);
                        }
                        else
                        {
                            prevC = ' ';
                        }
                    }

                }
            }

            return finalChain;
        }

        static void Part1()
        {
            var file = new System.IO.StreamReader("C:\\Users\\jenn_\\Documents\\AdventOfCode2018\\Day05\\puzzle.txt");
            string polymer = file.ReadLine();
            List<char> finalChain = new List<char>();

            finalChain = ReducePolymer(polymer);

            System.Diagnostics.Debug.WriteLine(finalChain.Count);
        }

        static void Part2()
        {
            var file = new System.IO.StreamReader("C:\\Users\\jenn_\\Documents\\AdventOfCode2018\\Day05\\puzzle.txt");
            string polymer = file.ReadLine();
            int[] lengths = new int[26];
            int minLength = int.MaxValue;

            for (int i = 0; i < 26; i++)
            {
                lengths[i] = ReducePolymer(polymer, Convert.ToChar(i+65)).Count;
                minLength = Math.Min(lengths[i], minLength);
            }

            System.Diagnostics.Debug.WriteLine(minLength);
        }

        static void Main(string[] args)
        {
            // Part1();
            Part2();
        }
    }
}
