using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

// silver 20899048083289
// gold 

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
                List<Tile> tiles = new List<Tile>();

                tiles.Add(new Tile());
                foreach (string item in lines) {
                    if (string.IsNullOrWhiteSpace(item)) {
                        tiles.Add(new Tile());
                        continue;
                    }
                    else {
                        tiles.Last().AddLine(item);
                    }
                }

                long result = 1;
                foreach (Tile item in tiles) {
                    if (item.IsCorner(tiles)) {
                        Console.WriteLine("ID: {0}", item.ID);
                        result *= long.Parse(item.ID);
                    }
                }
                Console.WriteLine("result {0}", result);
            }
        }
    }
    public class Tile
    {
        public enum FliRot : int
        {
            ORG = 0,
            ORG90,
            ORG180,
            ORG270,
            ORGFLI,
            ORGFLI90,
            ORGFLI180,
            ORGFLI270
        }
        public enum Edge : int
        {
            UP = 0,
            LEFT,
            DOWN,
            RIGHT
        }

        List<string> lines = new List<string>();
        string up = string.Empty;
        string left = string.Empty;
        string down = string.Empty;
        string right = string.Empty;

        public string ID { get; set; }

        private bool CheckEdgeFree(string i_testedges, List<Tile> i_tiles)
        {
            foreach (Tile item in i_tiles) {
                if (ID != item.ID) {
                    List<string> alledges = item.AllEdges();
                    if (alledges.Contains(i_testedges)) return false;
                }
            }
            return true;
        }
        public bool IsCorner(List<Tile> i_tiles)
        {
            // with two free edges
            for (int i = 0; i <= (int)FliRot.ORGFLI270; i++) {
                bool free = true;
                if (!CheckEdgeFree(GetEdge((FliRot)i, Edge.UP), i_tiles) || !CheckEdgeFree(GetEdge((FliRot)i, Edge.LEFT), i_tiles)) {
                    free = false;
                }
                if (free == true && !CheckEdgeFree(GetEdge((FliRot)i, Edge.DOWN), i_tiles) && !CheckEdgeFree(GetEdge((FliRot)i, Edge.RIGHT), i_tiles)) {
                    return true;
                }
            }
            return false;
        }
        public List<string> AllEdges()
        {
            List<string> edges = new List<string>();
            for (int i = 0; i <= (int)FliRot.ORGFLI270; i++) {
                for (int n = 0; n <= (int)Edge.RIGHT; n++) {
                    edges.Add(GetEdge((FliRot)i, (Edge)n));
                }
            }
            return edges;
        }
        public void AddLine(string i_line)
        {
            if (i_line.StartsWith("Tile")) {
                string[] str = i_line.Split(new char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
                ID = str[1];
            }
            else {
                if (lines.Count == 0) {
                    up = i_line;
                    left += up.ElementAt(0);
                    right += up.ElementAt(9);
                }
                else if (lines.Count == 9) {
                    down = i_line;
                    left += down.ElementAt(0);
                    right += down.ElementAt(9);
                }
                else {
                    left += i_line.ElementAt(0);
                    right += i_line.ElementAt(9);
                }
                lines.Add(i_line); // all line input in original order
            }
        }
        private string GetEdge(FliRot i_fliprot, Edge i_edge)
        {
            switch (i_fliprot) {
                case FliRot.ORG:
                    if (i_edge == Edge.UP) return up;
                    else if (i_edge == Edge.LEFT) return left;
                    else if (i_edge == Edge.DOWN) return down;
                    else return right;

                case FliRot.ORG90:

                    if (i_edge == Edge.UP) return right;
                    else if (i_edge == Edge.LEFT) return new string(up.Reverse().ToArray());
                    else if (i_edge == Edge.DOWN) return left;
                    else return new string(down.Reverse().ToArray());

                case FliRot.ORG180:
                    if (i_edge == Edge.UP) return new string(down.Reverse().ToArray());
                    else if (i_edge == Edge.LEFT) return new string(right.Reverse().ToArray());
                    else if (i_edge == Edge.DOWN) return new string(up.Reverse().ToArray());
                    else return new string(left.Reverse().ToArray());


                case FliRot.ORG270:
                    if (i_edge == Edge.UP) return new string(left.Reverse().ToArray());
                    else if (i_edge == Edge.LEFT) return down;
                    else if (i_edge == Edge.DOWN) return new string(right.Reverse().ToArray());
                    else return up;

                case FliRot.ORGFLI:
                    if (i_edge == Edge.UP) return new string(up.Reverse().ToArray());
                    else if (i_edge == Edge.LEFT) return right;
                    else if (i_edge == Edge.DOWN) return new string(down.Reverse().ToArray());
                    else return left;


                case FliRot.ORGFLI90:
                    if (i_edge == Edge.UP) return new string(right.Reverse().ToArray());
                    else if (i_edge == Edge.LEFT) return new string(down.Reverse().ToArray());
                    else if (i_edge == Edge.DOWN) return new string(left.Reverse().ToArray());
                    else return new string(up.Reverse().ToArray());

                case FliRot.ORGFLI180:
                    if (i_edge == Edge.UP) return down;
                    else if (i_edge == Edge.LEFT) return new string(left.Reverse().ToArray());
                    else if (i_edge == Edge.DOWN) return up;
                    else return new string(right.Reverse().ToArray());

                case FliRot.ORGFLI270:
                    if (i_edge == Edge.UP) return left;
                    else if (i_edge == Edge.LEFT) return up;
                    else if (i_edge == Edge.DOWN) return right;
                    else return down;

                default:
                    Console.WriteLine("Error in EdgeValue");
                    return string.Empty;
            }
        }
    }
}
