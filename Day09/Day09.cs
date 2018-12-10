using System;
using System.Collections.Generic;

namespace Day09
{
    class Day09
    {
        static void Main(string[] args)
        {
            var file = new System.IO.StreamReader("..\\..\\puzzle.txt");

            string[] line = file.ReadLine().Split(' ');
            UInt64 numPlayers = UInt64.Parse(line[0]);
            UInt64 lastMarble = UInt64.Parse(line[6]) * 100;

            // int highScore = int.Parse(line[11]);

            LinkedList<UInt64> marbles = new LinkedList<UInt64>();
            UInt64[] playerScores = new UInt64[numPlayers];
            UInt64 currentScore = 0;
            LinkedListNode<UInt64> currentNode;
            UInt64 currentMarble = 0;
            UInt64 currentPlayer = 0;
            UInt64 circleSize = 0;

            marbles.AddFirst(currentMarble);
            currentNode = marbles.Find(currentMarble);
            currentMarble++;


            marbles.AddFirst(currentMarble);
            currentNode = marbles.Find(currentMarble);
            currentMarble++;
            currentPlayer++;

            while (currentMarble <= lastMarble)
            {
                if (currentMarble % 1000000 == 0)
                {
                    System.Diagnostics.Debug.WriteLine(currentMarble);
                }
                circleSize = (UInt64)marbles.Count;
                if (currentMarble % 23 == 0)
                {
                    currentScore = currentMarble;
                    for (int i = 0; i < 7; i++)
                    {
                        if (currentNode.Previous == null)
                        {
                            currentNode = marbles.Last;
                        }
                        else
                        {
                            currentNode = currentNode.Previous;
                        }
                    }
                    currentScore += currentNode.Value;
                    currentNode = currentNode.Next;
                    marbles.Remove(currentNode.Previous.Value);
                    playerScores[currentPlayer] += currentScore;
                } else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (currentNode.Next == null)
                        {
                            currentNode = marbles.First;
                        }
                        else
                        {
                            currentNode = currentNode.Next;
                        }
                    }
                    marbles.AddBefore(currentNode, currentMarble);
                    currentNode = currentNode.Previous;
                }
                currentMarble++;
                currentPlayer = (currentPlayer + 1) % numPlayers;
            }

            System.Diagnostics.Debug.WriteLine(currentMarble);

            UInt64 winningScore = 0;
            foreach (UInt64 score in playerScores)
            {
                winningScore = Math.Max(winningScore, score);
                System.Diagnostics.Debug.Write(score);
                System.Diagnostics.Debug.Write(" ");
            }
            System.Diagnostics.Debug.WriteLine("");

            System.Diagnostics.Debug.WriteLine(winningScore);
        }
    }
}
