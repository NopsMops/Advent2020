using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// silver 
// gold 

// use star1 function and set new brackets -> bad programm finally -> shit program :-{

namespace Star2
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
                    string str = Brackets(item);
                    Simple simple = new Simple(str);
                    sum += simple.Result;
                }

                Console.WriteLine("result: {0}", sum);
            }
        }
        private static string Brackets( string i_str)
        {           
            List<char> charL = new List<char>(i_str.ToCharArray());
            SetBracket(charL);
            string modStr = string.Concat(charL);
            return string.Concat(charL);
        }
        private static bool SetBracketRight(List<char> i_charL, int i_start)
        {
            bool ret = false;
            int innerR = i_start + 1;
            while (innerR < i_charL.Count) {
                if (char.IsDigit(i_charL[innerR])) {
                    i_charL.Insert(innerR + 1, ')');
                    ret = true;
                    break;
                }
                else if (i_charL[innerR] == '(') {
                    int counterB = 1;
                    int innerBR = innerR + 1;
                    while (innerBR < i_charL.Count) {
                        if (i_charL[innerBR] == '(') {
                            counterB++;
                        }
                        else if (i_charL[innerBR] == ')') {
                            counterB--;
                            if (counterB == 0) {
                                i_charL.Insert(innerBR, ')');
                                ret = true;
                                break;
                            }
                        }
                        innerBR++;
                    }
                    break;
                }
                innerR++;
            }
            return ret;
        }
        private static bool SetBracketLeft(List<char> i_charL, int i_start)
        {
            bool ret = false;
            int innerL = i_start - 1;
            while (innerL >= 0) {
                if (char.IsDigit(i_charL[innerL])) {
                    i_charL.Insert(innerL, '(');
                    ret = true;
                    break;
                }
                else if (i_charL[innerL] == ')') {
                    int counterB = 1;
                    int innerBL = innerL - 1;
                    while (innerBL >= 0) {
                        if (i_charL[innerBL] == ')') {
                            counterB++;
                        }
                        else if (i_charL[innerBL] == '(') {
                            counterB--;
                            if (counterB == 0) {
                                i_charL.Insert(innerBL, '(');
                                ret = true;
                                break;
                            }
                        }
                        innerBL--;
                    }
                    break;
                }
                innerL--;
            }
            return ret;
        }
        private static void SetBracket(List<char> i_charL)
        {           
            int index = 0;
            while( index < i_charL.Count) {
                if(i_charL[index] == '+') {
                   if(SetBracketRight(i_charL, index)) {
                        SetBracketLeft(i_charL, index);
                        index++; // set bracket left moves + to left -> therefor ++
                    }
                }
                index++;
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