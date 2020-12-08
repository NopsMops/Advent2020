using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Star2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test11.txt";
            if (File.Exists(path))
            {
                List<(string com, int vorint, int val)> instList = new List<(string com, int vorint, int val)>();
                string[] lines = File.ReadAllLines(path);
                foreach (string item in lines)
                {
                    char vor = '+';
                    int vorint = 1;
                    int val = 0;
                    string[] op = item.Split(' ');
                    if (op[1].StartsWith('+'))
                    {
                        vor = '+';
                        vorint = 1;
                    }
                    else
                    {
                        vor = '-';
                        vorint = -1;
                    }

                    val = int.Parse(op[1].Trim(vor));
                    instList.Add((op[0], vorint, val));
                }
                bool back = false;
                bool found = false;
                for (int i = 0; i < instList.Count(); i++)
                {
                    if (found) break;
 
                    if(back)
                    {
                        if (instList[i-1].com == "jmp")
                        {

                            (string com, int vorint, int val) t = instList[i-1];
                            t.com = "nop";
                            instList[i-1] = t;
                            back = false;
                        }
                        else if (instList[i-1].com == "nop")
                        {
                            (string com, int vorint, int val) t = instList[i-1];
                            t.com = "jmp";
                            instList[i-1] = t;
                            back = false;
                        }
                    }

                    if( instList[i].com == "jmp" )
                    {

                        (string com, int vorint, int val) t = instList[i];
                        t.com = "nop";
                        instList[i] = t;
                        back = true;
                    }
                    else if(instList[i].com == "nop")
                    {
                        (string com, int vorint, int val) t = instList[i];
                        t.com = "jmp";
                        instList[i] = t;
                        back = true;
                    }

                    List<int> place = new List<int>();

                    int accumulator = 0;
                    int index = 0;
                    place.Add(index);

                    while (true )
                    {
                        if ((index >= instList.Count))
                        {
                            found = true;
                            break;
                        }
                        if (instList[index].com == "acc")
                        {
                            accumulator += instList[index].vorint * instList[index].val;
                            index++;
                            if (place.Contains(index))
                            {
                                break;
                            }
                            place.Add(index);
                        }
                        else if (instList[index].com == "jmp")
                        {
                            index += instList[index].vorint * instList[index].val;
                            if (place.Contains(index))
                            {
                                break;
                            }
                            place.Add(index);
                        }
                        else
                        {
                            index++;
                            if (place.Contains(index))
                            {
                                break;
                            }
                            place.Add(index);
                        }
                    }
                    Console.WriteLine("accumulator: {0}", accumulator);
                }

               

            }
        }
    }
}
