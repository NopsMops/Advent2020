using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

namespace Star2
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, int> numTurns = new Dictionary<int, int>();

            // int[] input = new int[] { 0, 3, 6 };
            int[] input = new int[] { 12, 1, 16, 3, 11, 0 };


            for (int i = 0; i < input.Length - 1; i++) {
                numTurns.Add(input[i], i + 1);
            }

            int lastspoken = input.Last();
            for (int i = input.Length; i < 30000000; i++) {

                if (numTurns.ContainsKey(lastspoken)) {
                    int diff = i - numTurns[lastspoken];
                    numTurns[lastspoken] = i;
                    lastspoken = diff;
                }
                else {
                    numTurns.Add(lastspoken, i);
                    lastspoken = 0; // da letzter aufruf nicht vorhanden
                }
            }
            //int result = numTurns.Last();
            Console.WriteLine("lastspoken {0}", lastspoken);
        }
    }
}
