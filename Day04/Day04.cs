using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04
{
    class Day04
    {
        static void Part1()
        {
            string line;
            string[] delims = { "[", "] " };
            List<string> sortedRecords = new List<string>();
            List<string[]> splitRecords = new List<string[]>(); // sorted and split

            var file = new System.IO.StreamReader("..\\..\\puzzle.txt");
            while ((line = file.ReadLine()) != null)
            {
                sortedRecords.Add(line);

            }
            sortedRecords.Sort();
            foreach (string record in sortedRecords)
            {
                /*
                 * splitRecords[i][0] == date and time without []
                 * splitRecords[i][1] == start shift / falls asleep / wakes up
                 */
                splitRecords.Add(record.Split(delims, System.StringSplitOptions.RemoveEmptyEntries));
            }

            string sleepiestGuard = "";
            Dictionary<string, int[]> guards = new Dictionary<string, int[]>();
            int startSleep = 0;
            int endSleep = 0;
            int minutesSlept = 0;
            int maxMinutesSlept = 0;
            string currentGuard = "";
            for (int i = 0; i < splitRecords.Count; i++)
            {
                string action = splitRecords[i][1];
                if (action.Substring(0,5) == "Guard")
                {
                    if (currentGuard != "")
                    {
                        minutesSlept = guards[currentGuard][60];
                        if (minutesSlept > maxMinutesSlept)
                        {
                            maxMinutesSlept = minutesSlept;
                            sleepiestGuard = currentGuard;
                        }
                    }
                    currentGuard = splitRecords[i][1].Split()[1];
                    if (!guards.ContainsKey(currentGuard))
                    {
                        guards.Add(currentGuard, new int[61]);
                    }
                    minutesSlept = 0;
                } else if (action == "falls asleep")
                {
                    startSleep = int.Parse(splitRecords[i][0].Substring(14, 2));
                } else if (action == "wakes up")
                {
                    endSleep = int.Parse(splitRecords[i][0].Substring(14, 2));
                    guards[currentGuard][60] += (endSleep - startSleep);
                    for (int k = startSleep; k < endSleep; k++)
                    {
                        guards[currentGuard][k]++;
                    }
                }
            }

            int sleepiestMinute = 0;
            int maxTimesAsleep = 0;
            int[] sleepSchedule = guards[sleepiestGuard];
            for (int i = 0; i < 60; i++)
            {
                if (sleepSchedule[i] > maxTimesAsleep)
                {
                    sleepiestMinute = i;
                    maxTimesAsleep = sleepSchedule[i];
                }
            }

            int answer = int.Parse(sleepiestGuard.Substring(1)) * sleepiestMinute;
            System.Diagnostics.Debug.WriteLine(answer);
        }

        static void Part2()
        {
            string line;
            string[] delims = { "[", "] " };
            List<string> sortedRecords = new List<string>();
            List<string[]> splitRecords = new List<string[]>(); // sorted and split

            var file = new System.IO.StreamReader("..\\..\\puzzle.txt");
            while ((line = file.ReadLine()) != null)
            {
                sortedRecords.Add(line);

            }
            sortedRecords.Sort();
            foreach (string record in sortedRecords)
            {
                /*
                 * splitRecords[i][0] == date and time without []
                 * splitRecords[i][1] == start shift / falls asleep / wakes up
                 */
                splitRecords.Add(record.Split(delims, System.StringSplitOptions.RemoveEmptyEntries));
            }

            Dictionary<string, int[]> guards = new Dictionary<string, int[]>();
            int startSleep = 0;
            int endSleep = 0;
            int maxTimesSlept = 0;
            int chosenMinute = 0;
            string sleepiestGuard = "";
            string currentGuard = "";
            for (int i = 0; i < splitRecords.Count; i++)
            {
                string action = splitRecords[i][1];
                if (action.Substring(0, 5) == "Guard")
                {
                    currentGuard = splitRecords[i][1].Split()[1];
                    if (!guards.ContainsKey(currentGuard))
                    {
                        guards.Add(currentGuard, new int[61]);
                    }
                }
                else if (action == "falls asleep")
                {
                    startSleep = int.Parse(splitRecords[i][0].Substring(14, 2));
                }
                else if (action == "wakes up")
                {
                    endSleep = int.Parse(splitRecords[i][0].Substring(14, 2));
                    guards[currentGuard][60] += (endSleep - startSleep);
                    for (int k = startSleep; k < endSleep; k++)
                    {
                        guards[currentGuard][k]++;
                        if (guards[currentGuard][k] > maxTimesSlept)
                        {
                            maxTimesSlept = guards[currentGuard][k];
                            chosenMinute = k;
                            sleepiestGuard = currentGuard;
                        }
                    }
                }
            }

            int sleepiestMinute = 0;
            int maxTimesAsleep = 0;
            int[] sleepSchedule = guards[sleepiestGuard];
            for (int i = 0; i < 60; i++)
            {
                if (sleepSchedule[i] > maxTimesAsleep)
                {
                    sleepiestMinute = i;
                    maxTimesAsleep = sleepSchedule[i];
                }
            }

            int answer = int.Parse(sleepiestGuard.Substring(1)) * sleepiestMinute;
            System.Diagnostics.Debug.WriteLine(answer);
        }

        static void Main(string[] args)
        {
            // Part1();
            Part2();
        }
    }
}
