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

            // # ->  L wenn min 5 # in sicht
            // L -> #  wenn kein # in sicht


            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test11.txt";
            if (File.Exists(path)) {

                string[] lines = File.ReadAllLines(path);

                int colSize = lines[0].Length;
                int rowSize = lines.Length;
                char[][] seatsA = new char[rowSize][];

                for (int i = 0; i < rowSize; i++) {
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
                                if (SeeOcc(seatsA, i, n) == 0) {
                                    seatsB[i][n] = '#';
                                    found = true;
                                }
                            }
                            else if (seatsA[i][n] == '#') {
                                seatsB[i][n] = '#';
                                if (SeeOcc(seatsA, i, n) > 4) {
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

                    // print
                    //Print(seatsA);
                }
                int counter = 0;
                for (int i = 0; i < rowSize; i++) {

                    for (int n = 0; n < colSize; n++) {
                        if (seatsA[i][n] == '#')
                            counter++;
                    }
                }
                Console.WriteLine("occupied {0}", counter);
            }
        }
        public static void Print(char[][] i_seat )
        {
            // print
            int colSize = i_seat[0].Length;
            int rowSize = i_seat.Length;

            for (int i = 0; i < rowSize; i++) {
                for (int n = 0; n < colSize; n++) {
                    Console.Write("{0} ", i_seat[i][n]);
                }
                Console.WriteLine();
            }
        }
        public static int SeeOcc(char[][] i_seatsA, int i_row ,int i_col )
        {
            int colSize = i_seatsA[0].Length;
            int rowSize = i_seatsA.Length;

            int occu = 0;

            // rechts
            for ( int i=0 ; i < colSize; i++) {
                if (IsValid(i_seatsA, i_row, i_col + i + 1) && (i_seatsA[i_row][i_col + i +1 ] == '#' || i_seatsA[i_row][i_col + i + 1] == 'L')) {
                    if (i_seatsA[i_row][i_col + i + 1] == '#') {
                        occu++;
                    }
                    break;
                }
            }

            // rechts oben
            int stepsH = colSize - i_col;
            int stepsV = i_row+1;            
            for (int i = 1, n = 1; n < stepsV && i < stepsH; n++, i++) {
                if (i_seatsA[i_row - n][i_col + i] == '#' || i_seatsA[i_row - n][i_col + i] == 'L') {
                    if (i_seatsA[i_row - n][i_col + i] == '#') {
                        occu++;                       
                    }
                    break;
                }
            }

            // oben
            stepsV = i_row+1;
            for (int n = 1; n < stepsV; n++) {
                if (i_seatsA[i_row - n][i_col] == '#' || i_seatsA[i_row - n][i_col] == 'L') {
                    if (i_seatsA[i_row - n][i_col] == '#') {
                        occu++;                       
                    }
                    break;
                }
            }

            // links oben
            stepsH = i_col+1;
            stepsV = i_row+1;                       
            for ( int i=1, n = 1; n < stepsV && i < stepsH; n++, i++) {
                if (i_seatsA[i_row - n][i_col - i] == '#' || i_seatsA[i_row - n][i_col - i] == 'L') {
                    if (i_seatsA[i_row - n][i_col - i] == '#') {
                        occu++;                       
                    }
                    break;
                }
            } 
            
            // links
            stepsH = i_col+1;
            for (int i = 1; i < stepsH; i++) {
                if (i_seatsA[i_row][i_col - i] == '#' || i_seatsA[i_row][i_col - i] == 'L') {
                    if (i_seatsA[i_row][i_col - i] == '#') {
                        occu++;                       
                    }
                    break;
                }
            }

            // links unten
            stepsH = i_col+1;
            stepsV = rowSize - i_row;           
            for (int i=1, n = 1; n < stepsV && i < stepsH; n++, i++) {
                if (i_seatsA[i_row + n][i_col - i] == '#' || i_seatsA[i_row + n][i_col - i] == 'L') {
                    if (i_seatsA[i_row + n][i_col - i] == '#') {
                        occu++;                       
                    }
                    break;
                }
            }
            
            // unten
            stepsH = 0;
            stepsV = rowSize - i_row;
            for (int n = 1; n < stepsV; n++) {
                if (i_seatsA[i_row + n][i_col] == '#' || i_seatsA[i_row + n][i_col] == 'L') {
                    if (i_seatsA[i_row + n][i_col] == '#') {
                        occu++;                       
                    }
                    break;
                }
            }

            // rechts unten
            stepsH = colSize - i_col;
            stepsV = rowSize - i_row;           
            for (int i=1, n = 1; n < stepsV && i < stepsH; n++,i++) {
                if (i_seatsA[i_row + n][i_col + i] == '#' || i_seatsA[i_row + n][i_col + i] == 'L') {
                    if (i_seatsA[i_row + n][i_col + i] == '#') {
                        occu++;                        
                    }
                    break;
                }
            }
            return occu;
        }
        public static bool IsValid(char[][] i_seats, int i_row, int i_col)
        {
            if (i_row < 0 || i_col < 0 || i_row >= i_seats.Length || i_col >= i_seats[0].Length) return false;
            return true;
        }
    }
}                                