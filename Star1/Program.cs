using System;
using System.IO;
using System.Linq;

// star 1 silver 138379
// star 1 gold 85491920

namespace Star1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";
            long starkey = 0;
            if (File.Exists(path))
            {
                string[] fieldStr = File.ReadAllLines(path);
                int[] query = (from string num in fieldStr select int.Parse(num)).ToArray();

                foreach (int i in query)
                {
                    foreach (int k in query.Skip(1))
                    {
                        if ((k + i) == 2020)
                        {
                            starkey = k * i;
                            break;
                        }
                    }
                    if (starkey != 0) break;
                }
            }
            Console.WriteLine("key: {0}", starkey);
        }
    }
}
