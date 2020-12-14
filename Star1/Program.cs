using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

// silver 13727901897109
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

                // es sind 36 !! aber es sind nur integers deshalb kann ich gut BitVector32 zum umrechnen verwenden
                // note: maske ist aber 36 bit lang
                int[] masks = new int[32]; ; // Holds 32 masks.
                masks[0] = BitVector32.CreateMask();
                for (int i = 1; i < 32; i++) {
                    masks[i] = BitVector32.CreateMask(masks[i - 1]);
                }

                string[] lines = File.ReadAllLines(path);
                Dictionary<long, long> adressVal = new Dictionary<long, long>();
                
                char[] maskIn = null;  // 36 bits
                foreach ( string item in lines) {
                    if (item.StartsWith("mask")){
                        string[] maskSpl = item.Split("mask = ");
                        maskIn = maskSpl[1].ToCharArray();
                        Array.Reverse(maskIn);
                    }
                    else {
                        // mem[27041] = 56559
 
                        string[] maskSplV = item.Split("] = ");
                        int val = int.Parse(maskSplV[1]);
                        string[] maskSplA = maskSplV[0].Split("mem[");
                        int adr = int.Parse(maskSplA[1]);

                        // Berechnung beginnt
                        BitVector32 myBV = new BitVector32(val);
                        BitArray myBitArr = new BitArray(36);

                        for (int i = 0; i < 32; i++) {
                            myBitArr[i] = myBV[masks[i]];
                        }
                        
                        for( int i = 0; i < 36; i++) {
                            if(maskIn[i] == '0') {
                                myBitArr[i] = false;
                            }
                            else if (maskIn[i] == '1') {
                                myBitArr[i] = true;
                            }

                        }
                        long valnew =0;
                        for( int i = 0; i < 36; i++) {
                            if (myBitArr[i]) {
                                valnew += (long)Math.Pow( 2, i);
                            }
                        }


                        if (adressVal.ContainsKey(adr)) {
                            adressVal[adr] = valnew;
                        }
                        else {
                            adressVal.Add(adr, valnew);
                        }
                    }
                }
                long result = 0;
                foreach (KeyValuePair<long, long> kvp in adressVal) {
                    result += kvp.Value;
                }
               
                Console.WriteLine("occupied {0}", result);
            }
        }
    }
}
