using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// silver
// gold 
namespace Star2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test11.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test22.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test23.txt";

            if (File.Exists(path)) {

                string[] lines = File.ReadAllLines(path);
                                
                string[] title = lines[1].Split(',');
                Regex pattern = new Regex("^[0-9]+$");

                List<(long id, long col)> sched = new List<(long id, long col)>();
                for (int i = 0; i < title.Length; i++) {
                    if (pattern.IsMatch(title[i])) {
                        sched.Add((long.Parse(title[i]), i));
                    }
                    
                }

                long timestamp = 0;
                sched.Sort();
                sched.Reverse();
                long start = (100000000000000 / sched[0].id)* sched[0].id;
               

                for (long t = start; t < long.MaxValue; t += sched[0].id) {
                    int comp = 0;
                    timestamp = t - sched[0].col;
                    for (int s = 1; s < sched.Count; s++) {
                        if ((timestamp + sched[s].col) % sched[s].id == 0) {
                            comp++;
                        }
                        else break;
                    }
                    if (comp == sched.Count-1) {
                        break;
                    }
                }

                Console.WriteLine("result: {0}", timestamp);
                Console.ReadLine();
            }
        }
    }
}
