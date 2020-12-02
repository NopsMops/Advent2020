using System;
using System.IO;
using System.Linq;

// star 1 silver 439
// star 1 gold 584

namespace Star1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star2_org.txt";
            int validPass = 0;
            if (File.Exists(path))
            {
                Pass[] query = (from p in File.ReadAllLines(path) select new Pass(p)).ToArray();
                foreach (Pass item in query)
                    if (item.CheckValidity()) validPass++;
            }
            Console.WriteLine("key: {0}", validPass);
        }
    }

    public class Pass
    {
        public Pass(string line)
        {
            string[] linesp = line.Split(" ");
            string[] frequence = linesp[0].Split("-");

            Firstv = int.Parse(frequence[0]);
            Secv = int.Parse(frequence[1]);
            Key = linesp[1][0];
            Passwort = linesp[2];
        }

        public bool CheckValidity()
        {
            char[] singlet = Passwort.ToCharArray();
            if (singlet[Firstv - 1] == Key && singlet[Secv - 1] != Key) return true;
            else if (singlet[Firstv - 1] != Key && singlet[Secv - 1] == Key) return true;
            return false;
        }

        public int Firstv { get; set; }
        public int Secv { get; set; }
        public char Key { get; set; }
        public string Passwort { get; set; }

    }
}