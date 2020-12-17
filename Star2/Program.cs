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
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test11.txt";
            if (File.Exists(path)) {

                // right x
                // down y
                // up z
                // w


                List<(int, int, int, int)> kooActiv = new List<(int, int, int, int)>();

                int x = 0;
                int y = 0;
                int z = 0;
                int w = 0;
                string[] lines = File.ReadAllLines(path);
                foreach (string item in lines) {
                    char[] charsinline = item.ToArray();
                    x = 0;
                    foreach (char cube in charsinline) {
                        if (cube == '#') {
                            kooActiv.Add((x, y, 0, 0));
                        }
                        x++;
                    }
                    y++;
                }

                int loop = 0;
                CalcNewKooActiv(kooActiv, ref loop);

            }
        }

        public static void CalcNewKooActiv(List<(int, int, int, int)> i_kooactiv, ref int io_loop)
        {

            List<int> rangex = new List<int>();
            List<int> rangey = new List<int>();
            List<int> rangez = new List<int>();
            List<int> rangew = new List<int>();
            foreach ((int, int, int, int ) xyzw in i_kooactiv) {
                rangex.Add(xyzw.Item1);
                rangey.Add(xyzw.Item2);
                rangez.Add(xyzw.Item3);
                rangew.Add(xyzw.Item4);
            }
            rangex.Sort();
            rangey.Sort();
            rangez.Sort();
            rangew.Sort();
            int xlow = rangex.First();
            int xup = rangex.Last();
            int ylow = rangey.First();
            int yup = rangey.Last();
            int zlow = rangez.First();
            int zup = rangez.Last();
            int wlow = rangew.First();
            int wup = rangew.Last();

            (int xl, int yl, int zl, int wl, int xu, int yu, int zu, int wu) blockRanges = (--xlow, --ylow, --zlow, --wlow, ++xup, ++yup, ++zup, ++wup);
            List<(int, int, int, int )> newkooactiv = new List<(int, int, int, int)>();

            for (int x = blockRanges.xl; x <= blockRanges.xu; x++) {
                for (int y = blockRanges.yl; y <= blockRanges.yu; y++) {
                    for (int z = blockRanges.zl; z <= blockRanges.zu; z++) {
                        for (int w = blockRanges.wl; w <= blockRanges.wu; w++) {
                            (int x, int y, int z, int w) check = (x, y, z, w);
                            int numOfActiv = NumOfActivNei(i_kooactiv, check);
                            if (i_kooactiv.Contains(check)) {   // active cube
                                if (numOfActiv == 2 || numOfActiv == 3) {
                                    if (newkooactiv.Contains(check) == false) {
                                        newkooactiv.Add(check);
                                    }
                                }
                            }
                            else {  // inactive cube
                                if (numOfActiv == 3) {
                                    if (newkooactiv.Contains(check) == false) {
                                        newkooactiv.Add(check);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            io_loop++;
            if (io_loop == 6) {
                int counter = newkooactiv.Count();
                Console.WriteLine("active cubs {0}", counter);
                return;
            }
            CalcNewKooActiv(newkooactiv, ref io_loop);

            return;
        }

        private static int NumOfActivNei(List<(int, int, int, int)> i_kooactiv, (int x, int y, int z, int w) i_check)
        {
            int ret = 0;
            // 27 coo to check
            for (int x = i_check.x - 1; x <= i_check.x + 1; x++) {
                for (int y = i_check.y - 1; y <= i_check.y + 1; y++) {
                    for (int z = i_check.z - 1; z <= i_check.z + 1; z++) {
                        for (int w = i_check.w - 1; w <= i_check.w + 1; w++) {
                            if (i_kooactiv.Contains((x, y, z, w)) && (x, y, z,w) != (i_check.x, i_check.y, i_check.z, i_check.w)) {
                                ret++;
                            }
                        }
                    }
                }
            }
            return ret;
        }
    }
}