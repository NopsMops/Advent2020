using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
//using System.Collections.Generic;
using System.Text.RegularExpressions;

// silver 33098
// gold 

namespace Star1
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test11.txt";
            if (File.Exists(path)) {
                Queue<int> staple1 = new Queue<int>();
                Queue<int> staple2 = new Queue<int>();

                int ret = 0;

                bool player2 = false;
                string[] lines = File.ReadAllLines(path);
                foreach(string item in lines) {
                    if (string.IsNullOrEmpty(item)){
                        continue;
                    }
                    else if ( item.StartsWith("Player 2:")) {
                        player2 = true;
                        continue;
                    }
                    else if (item.StartsWith("Player 1:")) {
                        continue;
                    }

                    else if( player2 == false) {
                        staple1.Enqueue(int.Parse(item));
                    }
                    else if( player2 == true) {
                        staple2.Enqueue(int.Parse(item));
                    }
                }

                while (staple1.Count > 0 && staple2.Count > 0) {
                    int card1 = staple1.Dequeue();
                    int card2 = staple2.Dequeue();
                    if( card1 > card2) {
                        staple1.Enqueue(card1);
                        staple1.Enqueue(card2);
                    }
                    else {
                        staple2.Enqueue(card2);
                        staple2.Enqueue(card1);
                    }
                }
                
                if (staple1.Count > 0) {
                    int counter = staple1.Count;
                    while (counter > 0) { 
                        ret += counter * staple1.Dequeue();
                        counter--;
                    }
                }
                else {
                    int counter = staple2.Count;
                    while (counter > 0) {
                        ret += counter * staple2.Dequeue();
                        counter--;
                    }
                }

                Console.WriteLine("result {0}", ret);
            }
        }
    }
}