using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    class Day14
    {
        static void Part1()
        {
            int input = 077201;
            List<int> recipes = new List<int>();
            recipes.Add(3);
            recipes.Add(7);
            int elf1Index = 0;
            int elf2Index = 1;

            while (recipes.Count < input + 10)
            {
                int newRecipe = recipes[elf1Index] + recipes[elf2Index];
                recipes.Add(newRecipe % 10);
                newRecipe = newRecipe / 10;
                while (newRecipe > 0)
                {
                    recipes.Insert(recipes.Count - 1, newRecipe % 10);
                    newRecipe = newRecipe / 10;
                }

                elf1Index = (elf1Index + recipes[elf1Index] + 1) % recipes.Count;
                elf2Index = (elf2Index + recipes[elf2Index] + 1) % recipes.Count;
            }

            for (int i = input; i < input + 10; i++)
            {
                System.Diagnostics.Debug.Write(recipes[i]);
            }
            System.Diagnostics.Debug.WriteLine(" ");
        }

        static void Part2()
        {
            int input = 077201;
            List<int> recipes = new List<int>();
            recipes.Add(3);
            recipes.Add(7);
            int elf1Index = 0;
            int elf2Index = 1;
            int finalRecipes = 0;
            int recipeCount = 0;

            while (finalRecipes != input)
            {
                finalRecipes = 0;
                int newRecipe = recipes[elf1Index] + recipes[elf2Index];
                recipes.Add(newRecipe % 10);
                newRecipe = newRecipe / 10;
                while (newRecipe > 0)
                {
                    recipes.Insert(recipes.Count - 1, newRecipe % 10);
                    newRecipe = newRecipe / 10;
                }

                elf1Index = (elf1Index + recipes[elf1Index] + 1) % recipes.Count;
                elf2Index = (elf2Index + recipes[elf2Index] + 1) % recipes.Count;

                if (recipes.Count > 6)
                {
                    for (int i = 5; i >= 0; i--)
                    {
                        finalRecipes += (recipes[recipes.Count - (i + 1)] * (int)Math.Pow((double)10, (double)i));
                    }
                    if (finalRecipes == input)
                    {
                        recipeCount = recipes.Count - 6;
                        break;
                    }

                    finalRecipes = 0;
                    for (int i = 5; i >= 0; i--)
                    {
                        finalRecipes += (recipes[recipes.Count - (i + 2)] * (int)Math.Pow((double)10, (double)i));
                    }
                    if (finalRecipes == input)
                    {
                        recipeCount = recipes.Count - 7;
                        break;
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine(recipeCount);
        }

        static void Main(string[] args)
        {
            // Part1();
            Part2();
        }
    }
}
