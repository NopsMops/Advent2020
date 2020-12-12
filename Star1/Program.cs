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

            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test11.txt";
            if (File.Exists(path)) {
               
                string[] lines = File.ReadAllLines(path);

                int colSize = lines[0].Length;
                int rowSize = lines.Length;               
                char[][] seatsA = new char[rowSize][];

                for( int i = 0; i < rowSize; i++) {
                    seatsA[i] = lines[i].ToCharArray();
                }
 
                bool found = true;
                while (found) {
                    found = false;

                    char[][] seatsB = new char[rowSize][];                   
                    for (int i = 0; i < rowSize; i++) {
                        seatsB[i] = new char[colSize];
                        for (int n = 0; n < colSize; n++) {
                            
                            if (seatsA[i][n] == 'L') {
                                seatsB[i][n] = 'L';
                                if (0 == CountOcc(seatsA, i, n) ) {
                                    seatsB[i][n] = '#';
                                    found = true;  
                                }                              
                            }                           
                            else if (seatsA[i][n] == '#' ) {
                                seatsB[i][n] = '#';
                                if ( CountOcc(seatsA, i, n) > 3) {
                                    seatsB[i][n] = 'L';
                                    found = true;
                                }
                            }
                            else seatsB[i][n] = '.';
                        }
                    }
                    // vertauche a b
                    for (int i = 0; i < rowSize; i++) {
                        seatsA[i] = (char[])seatsB[i].Clone();
                    }                    
                }

                int counter = 0;
                for (int i = 0; i < rowSize; i++) {                    
                    for (int n = 0; n < colSize; n++) {
                        if (seatsA[i][n] == '#') {
                            counter++;
                        }
                    }
                }
                Console.WriteLine("occupied {0}", counter);
            }
        }
        public static bool IsValid(char[][] i_seats, int i_row, int i_col)
        {
            if (i_row < 0 || i_col < 0 || i_row >= i_seats.Length || i_col >= i_seats[0].Length) return false;
            return true;
        }
        public static int CountOcc(char[][] i_seats, int i, int n)
        {
            int sitocc = 0;
            if (IsValid(i_seats, i, n - 1) && i_seats[i][n - 1] == '#') { sitocc++; }
            if (IsValid(i_seats, i + 1, n - 1) && i_seats[i + 1][n - 1] == '#') { sitocc++; }
            if (IsValid(i_seats, i + 1, n) && i_seats[i + 1][n] == '#') { sitocc++; }
            if (IsValid(i_seats, i + 1, n + 1) && i_seats[i + 1][n + 1] == '#') { sitocc++; }
            if (IsValid(i_seats, i, n + 1) && i_seats[i][n + 1] == '#') { sitocc++; }
            if (IsValid(i_seats, i - 1, n + 1) && i_seats[i - 1][n + 1] == '#') { sitocc++; }
            if (IsValid(i_seats, i - 1, n) && i_seats[i - 1][n] == '#') { sitocc++; }
            if (IsValid(i_seats, i - 1, n - 1) && i_seats[i - 1][n - 1] == '#') { sitocc++; }
            return sitocc;
        }
    }
}            