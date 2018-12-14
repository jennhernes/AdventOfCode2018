using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08
{
    public class Node
    {
        public int numChildren;
        public int numMeta;
        public List<int> metaData;
        public List<Node> children;

        public Node(int numChildren, int numMeta)
        {
            this.numChildren = numChildren;
            this.numMeta = numMeta;
            this.metaData = new List<int>();
            this.children = new List<Node>();
        }
    }
    class Day08
    {
        static Node BuildTree(ref List<int> input)
        {
            Node node = new Node(input[0], input[1]);
            input.RemoveRange(0, 2);
            for (int i = 0; i < node.numChildren; i++)
            {
                node.children.Add(BuildTree(ref input));
            }

            for (int i = 0; i < node.numMeta; i++)
            {
                node.metaData.Add(input[0]);
                input.RemoveAt(0);
            }

            return node;
        }

        //static int GetMetaTotal(Node node, int prevTotal)
        //{
        //    int newTotal = 0;
        //    foreach (Node child in node.children)
        //    {
        //        int addition = GetMetaTotal(child, prevTotal);
        //        newTotal += addition;
        //        System.Diagnostics.Debug.WriteLine("From children: " + addition);
        //    }

        //    foreach (int value in node.metaData)
        //    {
        //        newTotal += value;
        //        System.Diagnostics.Debug.WriteLine("From self: " + value);
        //    }

        //    return newTotal;
        //}

        static int GetMetaTotal(Node node, int prevTotal)
        {
            int newTotal = 0;

            if (node.numChildren == 0)
            {
                foreach (int value in node.metaData)
                {
                    newTotal += value;
                    System.Diagnostics.Debug.WriteLine("From self: " + value);
                }
                return newTotal;
            }

            if (node.numChildren > 0)
            {
                foreach (int childIndex in node.metaData)
                {
                    if (childIndex < node.numChildren+1)
                    {
                        int addition = GetMetaTotal(node.children[childIndex-1], prevTotal);
                        newTotal += addition;
                        System.Diagnostics.Debug.WriteLine("From children: " + addition);
                    }
                }
            }

            return newTotal;
        }


        static void PrintTree(Node node)
        {
            System.Diagnostics.Debug.Write("Number of Children: " + node.numChildren + ", MetaData: ");
            for (int i = 0; i < node.numMeta; i++)
            {
                System.Diagnostics.Debug.Write(node.metaData[i] + " ");
            }
            System.Diagnostics.Debug.WriteLine("");
            foreach (Node child in node.children)
            {
                PrintTree(child);
            }
        }

        static void Part1()
        {
            var file = new System.IO.StreamReader("..\\..\\puzzle.txt");
            string line = file.ReadLine();
            string[] pieces = line.Split();
            List<int> inputList = new List<int>();

            for (int i = 0; i < pieces.Length; i++)
            {
                inputList.Add(int.Parse(pieces[i]));
            }

            Node tree = BuildTree(ref inputList);
            PrintTree(tree);

            int metaTotal = GetMetaTotal(tree, 0);

            System.Diagnostics.Debug.WriteLine(metaTotal);
        }

        static void Part2()
        {
            var file = new System.IO.StreamReader("..\\..\\puzzle.txt");
            string line = file.ReadLine();
            string[] pieces = line.Split();
            List<int> inputList = new List<int>();

            for (int i = 0; i < pieces.Length; i++)
            {
                inputList.Add(int.Parse(pieces[i]));
            }

            Node tree = BuildTree(ref inputList);
            PrintTree(tree);

            int metaTotal = GetMetaTotal(tree, 0);

            System.Diagnostics.Debug.WriteLine(metaTotal);
        }

        static void Main(string[] args)
        {
            // Part1();
            Part2();
        }
    }
}
