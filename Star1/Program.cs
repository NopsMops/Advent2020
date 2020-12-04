using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// star silver 204
// star gold 179

namespace Star1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] key = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid" };
            int[] val = { 1, 2, 4, 8, 16, 32, 64, 128 };
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";

            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                int counter = 0;
                int validpass = 0;
                foreach (string item in lines)
                {
                    
                    for (int i = 0; i < key.Length; i++)
                    {
                        if (item.Contains(key[i])) 
                            counter += val[i]; 
                    }

                    if (counter == 127 || counter == 255)
                    {
                        // auswertung
                        validpass++;
                        counter = 0;
                    }
                    
                    if( item.Length == 0) 
                        counter = 0;
                }                
                Console.WriteLine("validpass: {0}", validpass);
            }
        }
    }
}