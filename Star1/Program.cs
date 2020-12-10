using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// silver 1820
// gold 3454189699072

namespace Star1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test11.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test12.txt";
            if (File.Exists(path)) {
                List<int> inList = new List<int>();
                int count1 = 0;
                int count3 = 0;
                string[] lines = File.ReadAllLines(path);
                foreach (string item in lines) {
                    inList.Add(int.Parse(item));
                }
                inList.Sort();
                inList.Add(inList[inList.Count - 1] + 3);
                

                List<int> startAd = new List<int>() { 0 };
                List<int> investAd = new List<int>();
                //HashSet<int> investAd = new HashSet<int>();

                while (inList.Count>1) {
                    foreach( int item in startAd) {
                        bool first = false;
                        for( int i = 1; i <= 3; i++) {
                            if( inList.Contains(item + i) && !investAd.Contains(item+1) ) {
                                investAd.Add(item + i);
                                if (first == false) {
                                    if (i == 1) count1++;
                                    else if (i == 3) count3++;
                                    first = true;
                                }
                            }
                        }
                        
                        if(investAd.Count() == 2) {
                            if( (investAd[1]- investAd[0]) == 1) {
                                count1++;
                            }
                        }
                        else if(investAd.Count() == 3) {
                            count1 += 2;
                        }
                    }
                    // remove start from inList

                    startAd.Clear();
                    startAd.AddRange(investAd);
                    // remove invest from inList
                    foreach( int item in investAd) {
                        inList.Remove(item);
                    } 
                    investAd.Clear();
                }
                Console.WriteLine("Result {0}", count1 * (count3 + 1));
            }
        }
    }
}
