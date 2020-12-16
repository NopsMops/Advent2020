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
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test21.txt";
            if (File.Exists(path)) {

                string[] lines = File.ReadAllLines(path);
                Dictionary<string, long> adressVal = new Dictionary<string, long>();

                char[] maskIn = null;  // 36 bits
                int numOfXinMask = 0;
                string[] permut = null;
                foreach (string item in lines) {
                    if (item.StartsWith("mask")) {
                        string[] maskSpl = item.Split("mask = ");
                        maskIn = maskSpl[1].ToCharArray();
                        Array.Reverse(maskIn);
                        numOfXinMask = maskIn.Count(x => x == 'X' );
                        permut = GetPermutation(numOfXinMask);
                    }
                    else {
                        string[] maskSplV = item.Split("] = ");
                        int val = int.Parse(maskSplV[1]);
                        string[] maskSplA = maskSplV[0].Split("mem[");
                        int adr = int.Parse(maskSplA[1]);
                        char[] adrBits = ConvertIntTo36Bit(adr);

                        if (numOfXinMask > 0) {
                            for (int i = 0; i < permut.Length; i++) {

                                char[] maskBitsNew = GetNewBitMaskWithAdr(maskIn, adrBits);

                                for (int n = 0; n < numOfXinMask; n++) {
                                    // tausche X gegen 0 oder 1
                                    string indexStr = new string(maskBitsNew);
                                    int ind = indexStr.LastIndexOf('X');
                                    char[] varbin = permut[i].ToArray();
                                    maskBitsNew[ind] = varbin[n];
                                }
                                ToMemory(adressVal, maskBitsNew, val);
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
        public static void ToMemory(Dictionary<string, long> i_adressVal, char[] i_adrBits, long i_val)
        {
            string adr = new string(i_adrBits);
            if (i_adressVal.ContainsKey(adr)) {
                i_adressVal[adr] = i_val;
            }
            else {
                i_adressVal.Add(adr, i_val);
            }
        }
        public static char[] ConvertIntTo36Bit(int i_adr)
        {
            char[] space36bit = Enumerable.Repeat('0', 36).ToArray();
            char[] adrBit = Convert.ToString(i_adr, 2).ToArray();
            Array.Reverse(adrBit);
            adrBit.CopyTo(space36bit, 0);
            return space36bit;
        }
        public static string[] GetPermutation(int places)
        {
            // nh: copy from www
            ArrayList output = new ArrayList();
            GetPermutationPerRef(places, new char[] { '0', '1' }, ref output);
            return output.ToArray(typeof(string)) as string[];
        }
        private static void GetPermutationPerRef(int places, char[] chars, ref ArrayList output, string outputPart = "")
        {
            if (places == 0) {
                output.Add(outputPart);
            }
            else {
                // Für die Stelle rechts im Element, werden alle Zeichenmöglichkeiten durchlaufen
                foreach (char c in chars) {
                    // Danach wird für jedes dieser Zeichen, basierend auf der Anzahl der Stellen, wieder ein neuer
                    // foreach-Vorgang begonnen, der alle Zeichen der nächsten Stelle hinzufügt

                    GetPermutationPerRef(places - 1,   // Die Stellen Anzahl wird verwindert, bis 0
                        chars,                         // Benötigte Variablen werden
                        ref output,                    // mitübergeben
                        outputPart + c);               // An diesen letzen string werden alle anderen Stellen angehängt
                }
            }
        }
    }
}
     
