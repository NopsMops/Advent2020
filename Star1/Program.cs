using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// silver 1810
// gold 969

namespace Star1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test11.txt";
            if (File.Exists(path))
            {
                List<(string com, int vorint, int val)> instList = new List<(string com, int vorint, int val)>();
                string[] lines = File.ReadAllLines(path);
                foreach (string item in lines) {
                    int vorint = -1;
                    string[] op = item.Split(' ');
                    if (op[1].StartsWith('+')) vorint = 1;
                    instList.Add((op[0], vorint, int.Parse(op[1].Substring(1))));
                }
                int accumulator = 0, index = 0;
                while (true) {
                    if (instList[index].com == "acc") {
                        accumulator += instList[index].vorint * instList[index].val;
                        instList[index] = ("stop", instList[index].vorint, instList[index].val);
                        index++;
                    }
                    else if (instList[index].com == "jmp") {
                        instList[index] = ("stop", instList[index].vorint, instList[index].val);
                        index += instList[index].vorint * instList[index].val;
                    }
                    else if (instList[index].com == "nop") {
                        instList[index] = ("stop", instList[index].vorint, instList[index].val);
                        index++;
                    }
                    else break;
                }
                Console.WriteLine("accumulator: {0}", accumulator);
            }
        }
    }
}
