using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day24
{
    public class Group
    {
        public int id;
        public int target;
        public int damageToTarget;
        public int units;
        public int HP; // HP per unit
        public int AD; // AD per unit
        public string type;
        public int speed;
        public List<string> weaknesses;
        public List<string> immunities;

        public Group(int id, int units, int HP, int AD, string type, int speed,
            List<string> weaknesses, List<string> immunities)
        {
            this.id = id;

            this.target = -1;

            this.damageToTarget = 0;

            this.units = units;

            this.HP = HP;

            this.AD = AD;

            this.type = type;

            this.speed = speed;

            this.weaknesses = new List<string>();
            foreach (string s in weaknesses)
            {
                this.weaknesses.Add(s);
            }

            this.immunities = new List<string>();
            foreach (string s in immunities)
            {
                this.immunities.Add(s);
            }
        }
    }

    public class GroupCompareEP : IComparer<Group>
    {
        public int Compare(Group g1, Group g2)
        {
            int EP1 = g1.units * g1.AD;
            int EP2 = g2.units * g2.AD;

            if (EP1 < EP2)
            {
                return -1;
            }
            else if (EP1 > EP2)
            {
                return 1;
            }
            else
            {
                if (g1.speed > g2.speed)
                {
                    return -1;
                }
                else if (g1.speed < g2.speed)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    public class GroupCompareSpeed : IComparer<Group>
    {
        public int Compare(Group g1, Group g2)
        {
            if (g1.speed > g2.speed)
            {
                return -1;
            }
            else if (g1.speed < g2.speed)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var file = new System.IO.StreamReader("..\\..\\test.txt");
            string line;
            string[] delims = { "(", ")", ",", " ", ";" };
            List<List<Group>> armies = new List<List<Group>>();
            armies.Add(new List<Group>());
            armies.Add(new List<Group>());

            line = file.ReadLine();
            int id = 0;

            //  read immune system groups
            while ((line = file.ReadLine()) != "")
            {
                string[] split = line.Split(delims, System.StringSplitOptions.RemoveEmptyEntries);
                int units = int.Parse(split[0]);
                int HP = int.Parse(split[4]);

                int i = 7;
                List<string> weaknesses = new List<string>();
                List<string> immunities = new List<string>();
                if (split[i] == "weak")
                {
                    i += 2;
                    while (split[i] != "immune" && split[i] != "with")
                    {
                        weaknesses.Add(split[i]);
                        i++;
                    }
                }
                else if (split[i] == "immune")
                {
                    i += 2;
                    while (split[i] != "weak" && split[i] != "with")
                    {
                        immunities.Add(split[i]);
                        i++;
                    }
                }
                if (split[i] == "weak")
                {
                    i += 2;
                    while (split[i] != "immune" && split[i] != "with")
                    {
                        weaknesses.Add(split[i]);
                        i++;
                    }
                }
                else if (split[i] == "immune")
                {
                    i += 2;
                    while (split[i] != "weak" && split[i] != "with")
                    {
                        immunities.Add(split[i]);
                        i++;
                    }
                }

                i += 5;
                int AD = int.Parse(split[i]);
                i++;
                string type = split[i];
                i += 4;
                int speed = int.Parse(split[i]);


                armies[0].Add(new Group(id, units, HP, AD, type, speed, weaknesses, immunities));
                id++;
            }

            line = file.ReadLine();

            // read infection groups
            while ((line = file.ReadLine()) != null)
            {
                string[] split = line.Split(delims, System.StringSplitOptions.RemoveEmptyEntries);
                int units = int.Parse(split[0]);
                int HP = int.Parse(split[4]);

                int i = 7;
                List<string> weaknesses = new List<string>();
                List<string> immunities = new List<string>();
                if (split[i] == "weak")
                {
                    i += 2;
                    while (split[i] != "immune" && split[i] != "with")
                    {
                        weaknesses.Add(split[i]);
                        i++;
                    }
                }
                else if (split[i] == "immune")
                {
                    i += 2;
                    while (split[i] != "weak" && split[i] != "with")
                    {
                        immunities.Add(split[i]);
                        i++;
                    }
                }
                if (split[i] == "weak")
                {
                    i += 2;
                    while (split[i] != "immune" && split[i] != "with")
                    {
                        weaknesses.Add(split[i]);
                        i++;
                    }
                }
                else if (split[i] == "immune")
                {
                    i += 2;
                    while (split[i] != "weak" && split[i] != "with")
                    {
                        immunities.Add(split[i]);
                        i++;
                    }
                }

                i += 5;
                int AD = int.Parse(split[i]);
                i++;
                string type = split[i];
                i += 4;
                int speed = int.Parse(split[i]);


                armies[1].Add(new Group(id, units, HP, AD, type, speed, weaknesses, immunities));
                id++;
            }

            while (armies[0].Count > 0 && armies[1].Count > 0)
            {
                armies[0].Sort(new GroupCompareSpeed());
                armies[1].Sort(new GroupCompareSpeed());

                List<int> groupsBeingAttacked = new List<int>();

                int immuneIndex = 0;
                int infectionIndex = 0;
                int chosenArmy = 0;
                int enemyArmy = 1;
                int chosenIndex = 0;
                while (immuneIndex < armies[0].Count || infectionIndex < armies[1].Count)
                {
                    int highestSpeed = 0;
                    if (immuneIndex < armies[0].Count)
                    {
                        highestSpeed = armies[0][immuneIndex].speed;
                        chosenArmy = 0;
                        chosenIndex = immuneIndex;
                    }
                    if (infectionIndex < armies[1].Count)
                    {
                        int speed = armies[1][infectionIndex].speed;
                        if (speed > highestSpeed)
                        {
                            highestSpeed = speed;
                            chosenArmy = 1;
                            chosenIndex = infectionIndex;
                            infectionIndex++;
                        }
                        else
                        {
                            immuneIndex++;
                        }
                    }
                    else
                    {
                        immuneIndex++;
                    }

                    enemyArmy = (chosenArmy + 1) % 2;

                    int maxDamage = 0;
                    int targetID = -1;
                    int targetSpeed = -1;
                    int targetEP = 0;
                    foreach (Group g in armies[enemyArmy])
                    {
                        if (!groupsBeingAttacked.Contains(g.id))
                        {
                            int damage = armies[chosenArmy][chosenIndex].units * armies[chosenArmy][chosenIndex].AD;
                            if (g.weaknesses.Contains(armies[chosenArmy][chosenIndex].type))
                            {
                                damage *= 2;
                            }
                            if (g.immunities.Contains(armies[chosenArmy][chosenIndex].type))
                            {
                                damage = 0;
                            }

                            if (damage > maxDamage)
                            {
                                targetID = g.id;
                                targetSpeed = g.speed;
                                targetEP = g.units * g.AD;
                                maxDamage = damage;
                                armies[chosenArmy][chosenIndex].damageToTarget = damage;
                                armies[chosenArmy][chosenIndex].target = targetID;

                            }
                            else if (damage == maxDamage)
                            {
                                int EP = g.units * g.AD;
                                if (EP > targetEP || EP == targetEP && g.speed > targetSpeed)
                                {
                                    targetID = g.id;
                                    targetSpeed = g.speed;
                                    armies[chosenArmy][chosenIndex].target = targetID;

                                }
                            }
                        }
                    }

                    if (targetID != -1)
                    {
                        groupsBeingAttacked.Add(targetID);
                    }
                }

                immuneIndex = 0;
                infectionIndex = 0;
                chosenArmy = 0;
                enemyArmy = 1;
                chosenIndex = 0;
                while (immuneIndex < armies[0].Count || infectionIndex < armies[1].Count)
                {
                    int highestSpeed = 0;
                    if (immuneIndex < armies[0].Count)
                    {
                        chosenArmy = 0;
                        chosenIndex = immuneIndex;
                        highestSpeed = armies[0][immuneIndex].speed;
                    }

                    if (infectionIndex < armies[1].Count &&
                        armies[1][infectionIndex].speed > highestSpeed)
                    {
                        highestSpeed = armies[1][infectionIndex].speed;
                        chosenArmy = 1;
                        chosenIndex = infectionIndex;
                        infectionIndex++;
                    }
                    else
                    {
                        immuneIndex++;
                    }
                    enemyArmy = (chosenArmy + 1) % 2;

                    int target = armies[chosenArmy][chosenIndex].target;

                    if (target != -1)
                    {
                        for (int i = 0; i < armies[enemyArmy].Count; i++)
                        {
                            if (armies[enemyArmy][i].id == target)
                            {
                                int damage = armies[chosenArmy][chosenIndex].damageToTarget;
                                int HP = armies[enemyArmy][i].HP;
                                int oldUnits = armies[enemyArmy][i].units;
                                int newUnits = (int)((double)(HP * oldUnits - damage) / (double)HP + 1);
                                armies[enemyArmy][i].units = newUnits;
                                if (armies[enemyArmy][i].units <= 0)
                                {
                                    armies[enemyArmy].RemoveAt(i);
                                    i--;
                                }
                                break;
                            }
                        }
                    }
                }

                System.Diagnostics.Debug.WriteLine("BREAKPOINT");
            }

            System.Diagnostics.Debug.WriteLine("BREAKPOINT");
        }
    }
}
