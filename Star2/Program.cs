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
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star2_org.txt";
            if (File.Exists(path))
            {
                int sum = 0;
                string[] lines = File.ReadAllLines(path);
                bool isFirst = true; 
                HashSet<char> setFirstPerson = new HashSet<char>();

                foreach (string item in lines)
                {
                    if (String.IsNullOrWhiteSpace(item))
                    {
                        sum += setFirstPerson.Count();
                        setFirstPerson = new HashSet<char>();
                        isFirst = true;
                        continue;
                    }

                    HashSet<char> setchars = new HashSet<char>();
                    char[] cAr = item.ToCharArray();
                    if (isFirst)
                    {
                        isFirst = false;
                        foreach (char lett in cAr)
                        {
                            setFirstPerson.Add(lett);
                        }
                    }
                    else
                    {
                        // pepare first person list
                        foreach( char buch in cAr)
                        {
                            setchars.Add(buch);
                        }
                        HashSet<char> removelist = new HashSet<char>();
                        foreach (char i in setFirstPerson)
                        {
                            if (setchars.Contains(i) != true)
                            {
                                removelist.Add(i);
                            }
                        }
                        foreach (char i in removelist)
                        {
                            setFirstPerson.Remove(i);
                        }                      
                    }                   
                }
                Console.WriteLine("sum: {0}", sum);
            }
        }
    }
}