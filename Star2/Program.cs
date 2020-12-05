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
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test11.txt";
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                int seatid = 0;
                List<int> ids = new List<int>();
                foreach (string item in lines)
                {
                    char[] cAr = item.ToCharArray();
                    (int low, int upper) s = (0, 127);
                    for (int i = 0; i < 7; i++)
                    {
                        s = calcu(cAr[i], s);
                    }
                    (int low, int upper) r = (0, 7);
                    for (int i = 7; i < 10; i++)
                    {
                        r = calcu(cAr[i], r);
                    }

                    // auswertung von setaid
                    // row * 8 + col
                    int val = s.low * 8 + r.upper;
                    ids.Add(val);
                    if (val > seatid) seatid = val;

                }
                ids.Sort();
                for( int i =  0; i < ids.Count()-1; i++)
                {
                    if( (ids[i] + 1) != ids[i+1])
                    {
                        Console.WriteLine("my seat is: {0}", ids[i] +1);
                    }
                }

            }
        }
        static public (int low, int upper) calcu(char i_lett, (int i_low, int i_upper) i_s)
        {
            int low = 0;
            int upper = 0;
            int seats = (i_s.i_upper - i_s.i_low + 1) / 2;

            if (i_lett == 'F' || i_lett == 'L')
            {
                // low   
                low = i_s.i_low;
                upper = i_s.i_low + seats - 1;
            }
            else if (i_lett == 'B' || i_lett == 'R')
            {
                // upper
                low = i_s.i_low + seats;
                upper = i_s.i_upper;

            }
            return (low, upper);
        }
    }
}
