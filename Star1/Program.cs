using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// silver 400480901
// gold 67587168

namespace Star1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test11.txt";
            if (File.Exists(path)) {
                List<long> numList = new List<long>();
                string[] lines = File.ReadAllLines(path);
                foreach (string item in lines) {
                    numList.Add(long.Parse(item));
                }
                int counter = 0;
                long result = 0;
                int pre = 25;
                bool found = true;
                while (found) {
                    found = false;
                    for( int i = counter; i < pre + counter -1 ; i++) {
                        for (int n = i + 1; n < pre + counter; n++) {
                            result = numList[i] + numList[n];
                            if ( numList[counter + pre] == result) {
                                found = true;
                                break;
                            }
                        }
                        if (found) break; 
                    }
                    counter++;
                }
                Console.WriteLine("result: {0}", numList[counter+pre-1]);
            }
        }
    }
}
