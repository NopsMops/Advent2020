using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// silver 2368
// gold 2124

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
                int colSize = lines[0].Length;
                int rowSize = lines.Length;

                List<List<char>> seatsA = new List<List<char>>();
                List<char> frameu = new List<char>();
                for ( int i = 0; i < colSize + 2; i++) {
                    frameu.Add('.');
                }
                seatsA.Add(frameu);

                foreach (string item in lines) {

                    List<char> row = new List<char>();
                    row.Add('.');
                    row.AddRange(item.ToCharArray());
                    row.Add('.');
                    seatsA.Add(row);
                }

                seatsA.Add(frameu);

                bool found = true;
                while (found) {
                    found = false;

                    char[,] seatsB = new char[rowSize + 2, colSize + 2];

                    for (int i = 0; i < rowSize + 2; i++) {

                        for (int n = 0; n < colSize + 2; n++) {
                            seatsB[i, n] = seatsA[i][n];
                        }

                    }

                    for (int i = 1; i < rowSize + 1; i++) {
                        for (int n = 1; n < colSize + 1; n++) {

                            
                            if (seatsA[i][n] == 'L') {
                                int sitfree = 0; 
                                if (seatsA[i][n - 1] == 'L' || seatsA[i][n - 1] == '.') { sitfree++; }
                                if (seatsA[i + 1][n - 1] == 'L' || seatsA[i + 1][n - 1] == '.') { sitfree++; }
                                if (seatsA[i + 1][n] == 'L' || seatsA[i + 1][n] == '.') { sitfree++; }
                                if (seatsA[i + 1][n + 1] == 'L' || seatsA[i + 1][n + 1] == '.') { sitfree++; }
                                if (seatsA[i][n + 1] == 'L' || seatsA[i][n + 1] == '.') { sitfree++; }
                                if (seatsA[i - 1][n + 1] == 'L' || seatsA[i - 1][n + 1] == '.') { sitfree++; }
                                if (seatsA[i - 1][n] == 'L' || seatsA[i - 1][n] == '.') { sitfree++; }
                                if (seatsA[i - 1][n - 1] == 'L' || seatsA[i - 1][n - 1] == '.') { sitfree++; }
                                if (sitfree == 8) {
                                    seatsB[i, n] = '#';
                                    found = true;  
                                }                              
                            }
                           
                            else if (seatsA[i][n] == '#' ) {
                                int sitocc = 0;
                                if (seatsA[i][n - 1] == '#') { sitocc++; }
                                if (seatsA[i + 1][n - 1] == '#') { sitocc++; }
                                if (seatsA[i + 1][n] == '#') { sitocc++; }
                                if (seatsA[i + 1][n + 1] == '#') { sitocc++; }
                                if (seatsA[i][n + 1] == '#') { sitocc++; }
                                if (seatsA[i - 1][n + 1] == '#') { sitocc++; }
                                if (seatsA[i - 1][n] == '#') { sitocc++; }
                                if (seatsA[i - 1][n - 1] == '#') { sitocc++; }
                                if (sitocc > 3) {
                                    seatsB[i, n] = 'L';
                                    found = true;
                                }
                            }

                        }
                    }
                    // vertauche a b
                    seatsA.Clear();

                    for (int i = 0; i < rowSize + 2; i++) {
                        List<char> tmp1 = new List<char>();
                        for(int n = 0; n < colSize + 2; n++) {
                            tmp1.Add(seatsB[i, n]);
                        }
                        seatsA.Add(tmp1);
                    }
                }
                int counter = 0;
                for (int i = 1; i < rowSize + 1; i++) {
                    
                    for (int n = 1; n < colSize + 1; n++) {
                        if(seatsA[i][n] == '#')
                            counter++;
                    }
 
                }
                Console.WriteLine("occupied {0}", counter);
            }
        }
    }
}
            }                              