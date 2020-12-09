using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// silver 400480901
// gold 

namespace Star2
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
                long silver = 400480901;
                int pre = 0;
                for( int i = 0; i < numList.Count; i++) {
                    if(numList[i] == silver) {
                        pre = i;
                        break;
                    }
                }

                int counter = 0;
                long result = 0;
                while (true) {
                    
                    for (int i = counter; i < pre + counter - 1; i++) {
                        result = numList[i];
                        for (int n = i + 1; n < pre + counter; n++) {
                            result += numList[n];
                            if ( result == silver) {
                                List<long> valid = new List<long>();
                                for (int p = i; p <= n; p++) {
                                    valid.Add(numList[p]);
                                }
                                valid.Sort();
                                Console.WriteLine("result: {0}", valid[0]+valid[valid.Count-1]);
                                return;
                            }
                        }
                    }
                    counter++;
                }
            }
        }
    }
}
