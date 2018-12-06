using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04
{
    public class GuardSleepSchedule
    {
        public string id;
        public int[] minutes;

        public GuardSleepSchedule(string id)
        {
            this.id = id;
            this.minutes = new int[60];
        }
    }

    class Day04
    {
        static void Part1()
        {
            string line;
            string[] delims = { "[", "] " };
            List<string> sortedRecords = new List<string>();
            List<string[]> splitRecords = new List<string[]>();

            var file = new System.IO.StreamReader("C:\\Users\\Jenn\\Documents\\AdventOfCode2018\\Day04\\puzzle.txt");
            while ((line = file.ReadLine()) != null)
            {
                sortedRecords.Add(line);

            }
            sortedRecords.Sort();
            foreach (string record in sortedRecords)
            {
                splitRecords.Add(record.Split(delims, System.StringSplitOptions.RemoveEmptyEntries));
            }

            string sleepiestID = splitRecords[0][1].Substring(6,5);
            string currentID = "";
            List<GuardSleepSchedule> guardSleepSchedules = new List<GuardSleepSchedule>();
            int sleepStart = 0;
            int sleepEnd = 0;
            Dictionary<string, int> guardSleepMins = new Dictionary<string, int>();


            foreach (string[] record in splitRecords)
            {
                if (record[1].Substring(0, 5) == "Guard")
                {
                    currentID = record[1].Substring(6, 5);
                    if (!guardSleepMins.ContainsKey(currentID))
                    {
                        guardSleepMins.Add(currentID, 0);
                        guardSleepSchedules.Add(new GuardSleepSchedule(currentID));
                    }

                }
                else if (record[1] == "falls asleep")
                {
                    sleepStart = int.Parse(record[0].Substring(14, 2));
                }
                else // == "wakes up"
                {
                    sleepEnd = int.Parse(record[0].Substring(14, 2));
                    guardSleepMins[currentID] += sleepEnd - sleepStart;
                    if (currentID != sleepiestID &&
                        guardSleepMins[currentID] > guardSleepMins[sleepiestID])
                    {
                        sleepiestID = currentID;
                    }

                    for (int i = sleepStart; i < sleepEnd; i++)
                    {
                        GuardSleepSchedule schedule = guardSleepSchedules.Where<GuardSleepSchedule>(x => return (x.id == currentID); ).Single<GuardSleepSchedule>();
                        int index = guardSleepSchedules.IndexOf(schedule);
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine(sleepiestID);
        }

        static void Main(string[] args)
        {
            Part1();
            //Part2();
        }
    }
}
