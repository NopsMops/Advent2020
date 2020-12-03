using System;
using System.IO;
using System.Linq;

// star silver 280
// star gold 4355551200

namespace Star1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";

            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                int len = lines[0].Length;
                int trees = 0;
                int pos = 0;
                foreach (string item in lines.Skip(1))
                {
                    pos = pos + 3;
                    if (pos >= len) pos = pos - len;
                    if (item[pos] == '#') trees++;
                }
                Console.WriteLine("trees: {0}", trees);
            }
        }
    }
}