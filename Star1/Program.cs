using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// silver 8929569623593
// gold 231235959382961

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

                long sum = 0;
                string[] lines = File.ReadAllLines(path);
                foreach (string item in lines) {
                    Simple simple = new Simple(item);
                    sum += simple.Result;
                }

                Console.WriteLine("result: {0}", sum);
            }
        }


    }
    public class Simple
    {
        public Simple(string i_line)
        {
            Result = 0;
            Index = 0;
            Line = i_line.Trim();
            Result = Expr();

        }
        private long Expr()
        {
            long result = 0;

            Func<long, long, long> arith = null;

            while (Index < Line.Length) {
                if (Regex.IsMatch(Line.Substring(Index), @"^[0-9]+")) {
                    long b = long.Parse(Line.Substring(Index, 1));
                    if (arith != null) {
                        result = arith(result, b);
                    }
                    else result = b;
                }
                else if (Line.Substring(Index).StartsWith("+")) {
                    arith = (x, y) => x + y;
                }
                else if (Line.Substring(Index).StartsWith("*")) {
                    arith = (x, y) => x * y;
                }
                else if (Line.Substring(Index).StartsWith(")")) {
                    //Index++;
                    return result;
                }
                else if (Line.Substring(Index).StartsWith("(")) {
                    Index++;
                    long b = Expr();
                    if (arith != null) {
                        result = arith(result, b);
                    }
                    else result = b;
                }
                Index++;
            }
            return result;
        }
        public long Result { get; set; }
        private int Index { get; set; }
        private string Line { get; set; }

    }
}