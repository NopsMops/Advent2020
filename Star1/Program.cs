using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// silver 845
// gold 27016

namespace Star1
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
                (int x, int y, char face) curr = (0, 0, 'E');

                foreach( string item in lines) {
                    doList.Add( ( int.Parse( item.Substring(1)), item.ToCharArray()[0] ) );
                }

                foreach((int val, char to) item in doList) {
                   
                    if( item.to == 'E') {
                        curr.x += item.val;
                    }
                    else if( item.to == 'N') {
                        curr.y += item.val;
                    }
                    else if( item.to == 'S') {
                        curr.y -= item.val;
                    }
                    else if( item.to == 'W' ){
                        curr.x -= item.val;
                    }
                    else if( item.to == 'F') {

                        if (curr.face == 'E') {
                            curr.x += item.val;
                        }
                        else if (curr.face == 'N') {
                            curr.y += item.val;
                        }
                        else if (curr.face == 'S') {
                            curr.y -= item.val;
                        }
                        else if (curr.face == 'W') {
                            curr.x -= item.val;
                        }
                        else {
                            Console.WriteLine("!! da stimmt was nicht !!");
                            return;
                        }
                    }
                    else if( item.to == 'R') {
                        if( item.val == 0) {
                            // keine Änderung
                        }
                        else if( item.val == 90) {
                            if (curr.face == 'E') curr.face = 'S';
                            else if( curr.face == 'N') curr.face = 'E';
                            else if (curr.face == 'S') curr.face = 'W';
                            else if (curr.face == 'W') curr.face = 'N';
                        }
                        else if( item.val == 180) {
                            if (curr.face == 'E') curr.face = 'W';
                            else if (curr.face == 'N') curr.face = 'S';
                            else if (curr.face == 'S') curr.face = 'N';
                            else if (curr.face == 'W') curr.face = 'E';
                        }
                        else if (item.val == 270) {
                            if (curr.face == 'E') curr.face = 'N';
                            else if (curr.face == 'N') curr.face = 'W';
                            else if (curr.face == 'S') curr.face = 'E';
                            else if (curr.face == 'W') curr.face = 'S';
                        }
                        else {
                            Console.WriteLine("!! da stimmt was nicht !!");
                            return;
                        }

                    }
                    else if( item.to == 'L') {
                        if (item.val == 0) {
                            // keine Änderung
                        }
                        else if (item.val == 90) {
                            if (curr.face == 'E') curr.face = 'N';
                            else if (curr.face == 'N') curr.face = 'W';
                            else if (curr.face == 'S') curr.face = 'E';
                            else if (curr.face == 'W') curr.face = 'S';
                        }
                        else if (item.val == 180) {
                            if (curr.face == 'E') curr.face = 'W';
                            else if (curr.face == 'N') curr.face = 'S';
                            else if (curr.face == 'S') curr.face = 'N';
                            else if (curr.face == 'W') curr.face = 'E';
                        }
                        else if (item.val == 270) {
                            if (curr.face == 'E') curr.face = 'S';
                            else if (curr.face == 'N') curr.face = 'E';
                            else if (curr.face == 'S') curr.face = 'W';
                            else if (curr.face == 'W') curr.face = 'N';
                        }
                        else {
                            Console.WriteLine("!! da stimmt was nicht !!");
                            return;
                        }
                    }
                    else {
                        Console.WriteLine("!! da stimmt was nicht !!");
                        return;
                    }

                }               
                Console.WriteLine("E/W + N/S {0} ", Math.Abs( curr.x ) +  Math.Abs( curr.y )  );
            }
        }
    }
}
                                  