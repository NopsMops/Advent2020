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

                List<List<char>> seatsA = new List<List<char>>();
              
                foreach (string item in lines) {
                    List<char> tmp = new List<char>();
                    tmp.AddRange(item.ToCharArray());
                    seatsA.Add(tmp);
                }

                bool found = true;
                while (found) {
                    found = false;

                    char[,] seatsB = new char[rowSize, colSize];

                    for (int i = 0; i < rowSize; i++) {

                        for (int n = 0; n < colSize; n++) {
                            seatsB[i, n] = seatsA[i][n];
                        }
                    }

                    for (int i = 0; i < rowSize; i++) {
                        for (int n = 0; n < colSize; n++) {


                            if (seatsA[i][n] == 'L') {
                                int sitocc = SeeOcc(seatsA, i,n) ;
                                if (sitocc == 0) {
                                    seatsB[i, n] = '#';
                                    found = true;
                                }
                            }

                            else if (seatsA[i][n] == '#') {
                                int sitocc = SeeOcc(seatsA, i, n);
                                if (sitocc > 4) {
                                    seatsB[i, n] = 'L';
                                    found = true;
                                }
                            }
                        }
                    }
                    // vertauche a b
                    seatsA.Clear();

                    for (int i = 0; i < rowSize; i++) {
                        List<char> tmp1 = new List<char>();
                        for (int n = 0; n < colSize; n++) {
                            tmp1.Add(seatsB[i, n]);
                        }
                        seatsA.Add(tmp1);
                    }
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
        public static int SeeOcc(List<List<char>> i_seatsA, int i_row ,int i_col )
        {
            int colSize = i_seatsA[0].Count;
            int rowSize = i_seatsA.Count;

            int occu = 0;


            // rechts
            int stepsH = colSize - i_col;
            int stepsV = 0;
            for ( int i=1 ; i < stepsH; i++) {
                if (i_seatsA[i_row][i_col + i] == '#' || i_seatsA[i_row][i_col + i] == 'L') {
                    if (i_seatsA[i_row][i_col + i] == '#') {
                        occu++;
                    }
                    break;
                }
            }

            // rechts oben
            stepsH = colSize - i_col;
            stepsV = i_row+1;            
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
    }
}                                