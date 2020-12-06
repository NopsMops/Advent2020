using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// silver 6742
// gold 3447

namespace Star1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";
            if (File.Exists(path))
            {
                int sum = 0;
                string[] lines = File.ReadAllLines(path);

                HashSet<char> setchars = new HashSet<char>();
                foreach (string item in lines)
                {
                    char[] cAr = item.ToCharArray();
                    foreach( char lett in cAr)
                    {
                        setchars.Add(lett);
                    }

                    if (String.IsNullOrWhiteSpace(item))
                    {
                        sum += setchars.Count();
                        setchars = new HashSet<char>();
                    } 
                }
                Console.WriteLine("sum: {0}", sum);
            }    
        }
    }
}
