using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// silver
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
                
                string[] lines = File.ReadAllLines(path);
                int colSize = lines[0].Length;
                int rowSize = lines.Length;

                long starttime = long.Parse(lines[0]);
                string[] title = lines[1].Split(',');
                Regex pattern = new Regex("^[0-9]+$");

                
                List<(long id, long backt, long beforet)> sched = new List<(long id, long backt, long beforet)>();

                foreach (string item in title) {
                    if(pattern.IsMatch(item)) {
                        long busid = long.Parse(item);
                        (long backt, long beforet) ret = Calcbb(starttime, busid);
                        sched.Add((busid, ret.backt, ret.beforet));
                    }
                }
                long takebus = sched[0].id;
                long difft = sched[0].beforet- starttime;

                for( int i = 0; i < sched.Count; i++) {

                    if( (sched[i].beforet - starttime) < difft) {
                        takebus = sched[i].id;
                        difft = sched[i].beforet - starttime;
                    }
                }              
                Console.WriteLine("result: {0}", takebus* difft);
            }
        }
        public static (long backt, long beforet ) Calcbb( long i_startt, long i_id)
        {
            (long backt, long beforet) ret = (0,0);

            // backt
            for (long i = i_startt; i >= 0; i-- ) {
                if( (i % i_id) == 0) {
                    ret.backt = i;
                    break;
                }
            }
            // before
            for (long i = i_startt; i >= 0; i++) {
                if ((i % i_id) == 0) {
                    ret.beforet = i;
                    break;
                }
            }
            return ret;
        }
    }
}
