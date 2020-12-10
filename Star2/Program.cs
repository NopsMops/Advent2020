using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
               
                string[] lines = File.ReadAllLines(path);
                foreach (string item in lines) {
                    inList.Add(int.Parse(item));
                }
                inList.Sort();
                inList.Add(inList[inList.Count - 1] + 3);
                int varianten2 = 0; // 2 gruppe
                int varianten4 = 0; // 3 gruppe aber es folgt ein sprung von 3
                int varianten6 = 0; // 3 gruppe es folgt ein sprung von 2
                int varianten7 = 0; // 3 gruppe es folgt ein sprung von 1

                List<int> startAd = new List<int>() { 0 };
                List<int> investAd = new List<int>();
                //HashSet<int> investAd = new HashSet<int>();

                while (inList.Count > 1) {
                    foreach (int item in startAd) {
 
                        for (int i = 1; i <= 3; i++) {
                            if (inList.Contains(item + i) && !investAd.Contains(item + i)) {
                                investAd.Add(item + i);                                
                            }
                        }
                        if(investAd.Count == 2) {
                            varianten2++;
                        }
                        if (investAd.Count == 3) {
                            if (inList.Contains(investAd[2] + 1)){
                                varianten7++;
                            }
                            else if ( !(inList.Contains(investAd[2] + 1 )) && (inList.Contains(investAd[2] + 2))) {
                                varianten6++;
                            }
                            else{
                                varianten4++;
                            }
                        }
                    }
                    // remove start from inList

                    startAd.Clear();
                    startAd.AddRange(investAd);
                    // remove invest from inList
                    foreach (int item in investAd) {
                        inList.Remove(item);
                    }
                    investAd.Clear();
                }
                long result = (long)Math.Pow(2, varianten2) * (long)Math.Pow(4, varianten4) * (long)Math.Pow(6, varianten6) * (long)Math.Pow(7, varianten7);
                Console.WriteLine("varianten :  {0} ", result );
            }
        }
    }
}