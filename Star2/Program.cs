using System;
using System.IO;
using System.Linq;

namespace Star2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star2_org.txt";
            long starkey = 0;
            if (File.Exists(path))
            {
                string[] fieldStr = File.ReadAllLines(path);
                int[] query = (from string num in fieldStr select int.Parse(num)).ToArray();

                foreach (int i in query)
                {
                    foreach (int k in query.Skip(1))
                    {
                        foreach (int n in query.Skip(2))
                        {
                            if ((k + i +n) == 2020)
                            {
                                starkey = k * i * n;
                                break;
                            }
                        }
                        if (starkey != 0) break;
                    }
                    if (starkey != 0) break;
                }
            }
            Console.WriteLine("key: {0}", starkey);
        }
    }
}
