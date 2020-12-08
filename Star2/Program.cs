using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Star2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test11.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test21.txt";
            if (File.Exists(path)){
                string[] lines = File.ReadAllLines(path);
                List<Bags> allBags = new List<Bags>();
                Bags shinyGoldBag = null;
                foreach (string item in lines){
                    Bags bag = new Bags(item);
                    allBags.Add(bag);
                    if (bag.First == "shiny gold"){
                        shinyGoldBag = bag;
                    }
                }
                shinyGoldBag.Fit(allBags);
                Console.WriteLine(" many bags: {0}", shinyGoldBag.CountBags());
            }
        }
    }
    public class Bags
    {
        List<(int num, string color)> sec = new List<(int num, string color)>();
        
        public Bags(string i_toextract)
        {
            string[] allblock = i_toextract.Split(" bags contain ");
            First = allblock[0];

            if (allblock[1].Contains(',')) { // 1 ...bag, 3 ....bags.
                string[] secblock = allblock[1].Split(", ");
                foreach (string i in secblock){
                    string[] singlecol = i.Split(' ');
                    int val = int.Parse(singlecol[0]);
                    string col = singlecol[1] + " " + singlecol[2];
                    sec.Add((val, col));
                }
            }
            else if (allblock[1].Trim().StartsWith("no")) { // no other bags.
                Console.Write("no fall: {0}", allblock[1]);
            }
            else{ // 1 bright white bag.
                string[] singlecol = allblock[1].Split(' ');
                int val = int.Parse(singlecol[0]);
                string col = singlecol[1] + " " + singlecol[2];
                sec.Add((val, col));
            }
        }
        public string First { get; set; }
        public int CountBags() 
        {
            int sum = 0;
            foreach ((int num, string color) item in sec) sum += item.num;
            return sum;
        }
        public List<(int num, string color)> GetMultSec(int mult)
        {
            List<(int num, string color)> secCopy = new List<(int num, string color)>();
            foreach((int num, string color) item in sec) {
                secCopy.Add((item.num * mult, item.color));
            }
            return secCopy;
        }
        public void Fit(List<Bags> i_allBags)
        {
            int i = 0;
            while (i < sec.Count()) {
                foreach( Bags item in i_allBags) {
                    if (sec[i].color == item.First) {
                        sec.AddRange(item.GetMultSec(sec[i].num));
                        break;
                    }
                }
                i++;
            }
        }
    }
}
