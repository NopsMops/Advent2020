using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

// silver 1696
// gold 37385

namespace Star1
{
    class Program
    {
        static void Main(string[] args)
        {

             List<int> numbers = new List<int>() { 12, 1, 16, 3, 11, 0 };  // puzzle 12,1,16,3,11,0 -> result silver star
            //List<int> numbers = new List<int>() { 0, 3, 6 };  // test 1 -> result 436

            int currpos = numbers.Count; 
            while (true) {
                int diff = 0;
                int prevval = numbers[currpos - 1];
                for( int i = currpos - 2; i >= 0; i--) {
                    if( numbers[i] == prevval) {
                        diff = currpos - 1 - i;
                        break;
                    }
                }
                if( diff > 0) {
                    // exist
                    numbers.Add(diff);
                }
                else {
                    // new number -> 0
                    numbers.Add(0);
                }
                currpos++;
                if (numbers.Count == 2020) break;
            }

            int result = numbers.Last();
            Console.WriteLine("occupied {0}", result);
           
        }
    }
}
