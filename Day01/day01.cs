using System;
using System.Collections.Generic;

public class Class1
{
    public static void Part1()
    {
        int freq = 0;

        string line;

        System.IO.StreamReader file = new System.IO.StreamReader("C:\\Users\\jenn_\\Documents\\AdventOfCode2018\\Day01\\puzzle.txt");

        while ((line = file.ReadLine()) != null)
        {
            freq += int.Parse(line);
        }

        file.Close();
        System.Diagnostics.Debug.WriteLine(freq);
    }
    
    public static void Part2()
    {
        bool foundDouble = false;
        int freq = 0;
        string line;
        HashSet<int> pastFreq = new HashSet<int>();

        while (!foundDouble)
        {
            System.IO.StreamReader file = new System.IO.StreamReader("C:\\Users\\jenn_\\Documents\\AdventOfCode2018\\Day01\\puzzle.txt");

            while ((line = file.ReadLine()) != null)
            {
                freq += int.Parse(line);
                if (pastFreq.Contains(freq))
                {
                    System.Diagnostics.Debug.WriteLine(freq);
                    foundDouble = true;
                    break;
                }
                else
                {
                    pastFreq.Add(freq);
                }
            }

            file.Close();
        }
    }

    public static void Main()
    {
        Part1();
        Part2();
    }
}
