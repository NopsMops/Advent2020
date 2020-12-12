using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// silver 
// gold 

namespace Star2
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test11.txt";
            if (File.Exists(path)) {


                string[] lines = File.ReadAllLines(path);
                List<(int val, char to)> doList = new List<(int val, char to)>();
                (int x, int y) curr = (0, 0);
                (int x, int y) wayp = (10, 1);

                foreach (string item in lines) {
                    doList.Add((int.Parse(item.Substring(1)), item.ToCharArray()[0]));
                }

                foreach ((int val, char to) item in doList) {

                    if (item.to == 'E') {
                        wayp.x += item.val;
                    }
                    else if (item.to == 'N') {
                        wayp.y += item.val;
                    }
                    else if (item.to == 'S') {
                        wayp.y -= item.val;
                    }
                    else if (item.to == 'W') {
                        wayp.x -= item.val;
                    }
                    else if (item.to == 'F') {
                        curr.x += wayp.x * item.val;
                        curr.y += wayp.y * item.val;
                    }
                    else if (item.to == 'R' || item.to == 'L' ) {
                        int tmpx = wayp.x;
                        int tmpy = wayp.y;
                        int factor = 1;
                        if (item.to == 'L') factor = -1;

                        int cos = 0;
                        int sin = 0;

                        if(item.val == 90) {
                            cos = 0;
                            sin = -1;
                        }
                        else if(item.val == 180) {
                            cos = -1;
                            sin = 0;
                        }
                        else if (item.val == 270) {
                            cos = 0;
                            sin = 1;
                        }
                        else {
                            Console.WriteLine("!! da stimmt was nicht !!");
                            return;
                        }
                        wayp.x = tmpx * cos - tmpy * sin* factor;
                        wayp.y = tmpx * sin* factor + tmpy * cos; 
                    }

                    else {
                        Console.WriteLine("!! da stimmt was nicht !!");
                        return;
                    }

                }
                Console.WriteLine("E/W + N/S {0} ", Math.Abs(curr.x) + Math.Abs(curr.y));
            }
        }
    }
}
