using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Star2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] key = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid" };
            int[] val = { 1, 2, 4, 8, 16, 32, 64, 128 };
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test22.txt";
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star2_org.txt";

            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                int counter = 0;
                int validpass = 0;
                List<string> passCheck = new List<string>();
                foreach (string item in lines)
                {

                    for (int i = 0; i < key.Length; i++)
                    {
                        if (item.Contains(key[i]))
                        {
                            if (CheckVal(key[i], item))
                            {
                                counter += val[i];
                            }
                        }
                    }

                    if (counter == 127 || counter == 255)
                    {
                        // auswertung
                        validpass++;
                        counter = 0;
                    }

                    if (item.Length == 0)
                    {
                        counter = 0;
                    }
                }
                Console.WriteLine("validpass: {0}", validpass);
            }
        }
        static public bool CheckVal(string i_key, string i_str)
        {

            string[] strpiece = i_str.Split(i_key + ":");
            if (strpiece.Length == 0) return false;

            string[] strsp = strpiece[1].Split(" ");
            string strcheck = strsp[0];

            if (i_key == "byr")
            {
                int birth = int.Parse(strcheck);
                if (birth >= 1920 && birth <= 2002) return true;
            }
            else if (i_key == "iyr")
            {
                int birth = int.Parse(strcheck);
                if (birth >= 2010 && birth <= 2020) return true;
            }
            else if (i_key == "eyr")
            {
                int birth = int.Parse(strcheck);
                if (birth >= 2020 && birth <= 2030) return true;
            }
            else if (i_key == "hgt")
            {
                if (strcheck.EndsWith("cm"))
                {
                    string[] strbodylen = strcheck.Split("cm");
                    int bodylen = int.Parse(strbodylen[0]);
                    if (bodylen >= 150 && bodylen <= 193) return true;
                }
                else if (strcheck.EndsWith("in"))
                {
                    string[] strbodylen = strcheck.Split("in");
                    int bodylen = int.Parse(strbodylen[0]);
                    if (bodylen >= 59 && bodylen <= 76) return true;
                }
            }
            else if (i_key == "hcl")
            {
                // 7 char -> # 0-9 or a-f               
                Regex rx = new Regex("^#[0-9a-f]{6}$");
                MatchCollection matches = rx.Matches(strcheck);
                if (matches.Count == 1) return true;
            }
            else if (i_key == "ecl")
            {
                string[] eyecolor = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                foreach (string item in eyecolor)
                {
                    if (item == strcheck) return true;
                }
            }
            else if (i_key == "pid")
            {
                Regex rx = new Regex("^[0-9]{9}$");
                MatchCollection matches = rx.Matches(strcheck);
                if (matches.Count == 1) return true;
            }
            else if (i_key == "cid")
            {
                return true;
            }
            return false;
        }
    }
}
