using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

// silver 13727901897109
// gold 

namespace Star2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test21.txt";
            if (File.Exists(path)) {

                string[] lines = File.ReadAllLines(path);
                Dictionary<string, long> adressVal = new Dictionary<string, long>();

                char[] maskIn = null;  // 36 bits
                int numOfXinMask = 0;
                List<List<char>> per = null;
                foreach (string item in lines) {
                    if (item.StartsWith("mask")) {
                        string[] maskSpl = item.Split("mask = ");
                        maskIn = maskSpl[1].ToCharArray();
                        Array.Reverse(maskIn);
                        numOfXinMask = NumOfXInBitArr(maskIn);
                        per = permute(numOfXinMask, i => i[0] == 1 && i[1] == 1 && i[2] == 0);
                    }
                    else {
                        string[] maskSplV = item.Split("] = ");
                        int val = int.Parse(maskSplV[1]);
                        string[] maskSplA = maskSplV[0].Split("mem[");
                        int adr = int.Parse(maskSplA[1]);
                        char[] adrBits = ConvertIntTo36Bit(adr);                       

                        if (numOfXinMask > 0) {                            
                            for (int i = 0; i < per.Count; i++) {

                                char[] maskBitsNew = GetNewBitMaskWithAdr(maskIn, adrBits);

                                for (int n = 0; n < numOfXinMask; n++) {
                                    // tausche X gegen 0 oder 1
                                    string indexStr = new string(maskBitsNew);
                                    int ind = indexStr.LastIndexOf('X');
                                    maskBitsNew[ind] = per[i][n];
                                }
                                ToMemory(adressVal, maskBitsNew, val);
                            }
                            if (per.Count < 1) {
                                Console.WriteLine("hier ist was falsch {0}", adr);
                            }
                        }
                        else {
                            char[] maskBitsNew = GetNewBitMaskWithAdr(maskIn, adrBits);
                            ToMemory(adressVal, maskBitsNew, val);
                        }                        
                    }                      
                }
                long result = 0; 
                foreach (KeyValuePair<string, long> kvp in adressVal) {
                    result += kvp.Value;
                }
                Console.WriteLine("value {0}", result);
            }
        }
        public static char[] GetNewBitMaskWithAdr(char[] i_maskOrg, char[] i_adrBits)
        {
            char[] maskBitsNew = (char[])i_maskOrg.Clone();

            // setze adresse in maskBitsCl
            for (int c = 0; c < 36; c++) {
                if (maskBitsNew[c] == 'X') {
                    // keep
                }
                else if (maskBitsNew[c] == '0' && i_adrBits[c] == '0') {
                    maskBitsNew[c] = '0';
                }
                else {
                    maskBitsNew[c] = '1';
                }
            }
            return maskBitsNew;
        }
        public static void ToMemory(Dictionary<string, long> i_adressVal, char[] i_adrBits, long i_val )
        {
            string adr = new string(i_adrBits);
            if (i_adressVal.ContainsKey(adr)) {
                i_adressVal[adr] = i_val;
            }
            else {
                i_adressVal.Add(adr, i_val);
            }
        }
        public static int NumOfXInBitArr(char[] i_arrBits)
        {
            int ret = 0;
            foreach (char item in i_arrBits) {
                if (item == 'X') {
                    ret++;
                }
            }
            return ret;
        }
        public static char[] ConvertIntTo36Bit(int i_adr)
        {
            // es sind 36 !! aber es sind nur integers deshalb kann ich gut BitVector32 zum umrechnen verwenden
            // note: maske ist aber 36 bit lang
            int[] masks = new int[32]; ; // Holds 32 masks.
            masks[0] = BitVector32.CreateMask();
            for (int i = 1; i < 32; i++) {
                masks[i] = BitVector32.CreateMask(masks[i - 1]);
            }

            BitVector32 myBV = new BitVector32(i_adr);

            char[] adrBit = new char[36];
            for (int i = 0; i < 36; i++) {
                if (i < 32) {
                    adrBit[i] = '0';
                    if (myBV[masks[i]]) adrBit[i] = '1';
                }
                else adrBit[i] = '0';
            }
            return adrBit;
        }
        public static List<List<char>> permute( int k, Predicate<int[]> decider)
        {

            List<List<char>> ret = new List<List<char>>();
            char[] a = new char[k + 2];
            a[0] = '0';
            a[1] = '1';
            for( int i = 2; i < k+2; i++) {
                a[i] = 'X';
            }
 
            int n = a.Length;
            if (k < 1 || k > n)
                Console.WriteLine("Illegal number of positions.");

            int[] indexes = new int[n];
            uint total = (uint)Math.Pow(n, k);

            while (total-- > 0) {
                List<char> nperm = new List<char>();
                bool valid = true;
                for (int i = 0; i < n - (n - k); i++) {
                    if (a[indexes[i]] == 'X') {
                        valid = false;
                        break;
                    }
                    nperm.Add(a[indexes[i]]);
                }
                if( valid) ret.Add(nperm);


                for (int i = 0; i < n; i++) {
                    if (indexes[i] >= n - 1) {
                        indexes[i] = 0;
                    }
                    else {
                        indexes[i]++;
                        break;
                    }
                }
            }
            return ret;
        }
    }
}
