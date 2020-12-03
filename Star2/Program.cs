using System;
using System.IO;
using System.Linq;

// right 1, down 1
// right 3, down 1
// right 5, down 1
// right 7, down 1
// right 1, down 2

namespace Star2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star2_org.txt";

            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                int len = lines[0].Length;
                
                int[] right = { 1, 3, 5, 7, 1 };
                int[] down = { 1, 1, 1, 1, 2 };
                long mult = 1;

                for (int n = 0; n < right.Length; n++)
                {
                    int pos = 0;
                    int trees = 0;
                    for (int i = down[n]; i < lines.Length; i = i + down[n])
                    {
                        pos = pos + right[n];
                        if (pos >= len)
                            pos = pos - len;

                        if (lines[i][pos] == '#') trees++;

                    }
                    mult *= trees;
                }
                Console.WriteLine("trees: {0}", mult);
            }
        }
    }
}
