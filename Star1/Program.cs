using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// silver 151
// gold 41559
namespace Star1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test11.txt";
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                // Hashtable allBags = new Hashtable();
                List<Bags> allBags = new List<Bags>();
                foreach (string item in lines)
                {
                    Bags bag = new Bags(item);
                    allBags.Add(bag);
                }
                List<string> foundCol = new List<string>() { "shiny gold" };
                int counter = 0;
                while (counter < foundCol.Count){ 
                    foreach (Bags i in allBags){
                        if (i.ExistCol(foundCol[counter])){
                            if( !foundCol.Contains(i.First)){
                                foundCol.Add(i.First);
                            }
                        }
                    }
                    counter++;
                }
                Console.WriteLine(" many color: {0}", foundCol.Count - 1);
            }
        }
    }
    public class Bags
    {
        List<(int num, string color)> sec = new List<(int num, string color)>();

        public Bags( string i_toextract)
        {
            string[] allblock = i_toextract.Split(" contain ");
            First = allblock[0].Split("bags")[0].Trim();

            if (allblock[1].Contains(','))
            { // 1 ...bag , 3 ....bags.
                string[] secblock = allblock[1].Split(", ");
                foreach( string i in secblock)
                {
                    string[] singlecol = i.Split(' ');
                    int val = int.Parse(singlecol[0]);
                    string col = singlecol[1] + " " + singlecol[2];
                    sec.Add((val, col));
                }
            }
            else if (allblock[1].Trim().StartsWith("no"))
            { // no other bags.
                Console.Write("no fall: {0}", allblock[1]);
            }
            else
            { // 1 bright white bag.
                string[] singlecol = allblock[1].Split(' ');
                int val = int.Parse(singlecol[0]);
                string col = singlecol[1] + " " + singlecol[2];
                sec.Add((val, col));
            }
        }
        public string First { get; set; } 
        public bool ExistCol(string i_col)
        {
            foreach( (int, string) i in sec)
            {
                if (i.Item2 == i_col) return true;
            }
            return false;
        }
    }
}
