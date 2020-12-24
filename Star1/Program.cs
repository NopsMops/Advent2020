using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
//using System.Collections.Generic;
using System.Text.RegularExpressions;

// silver 2125
// gold phc,spnd,zmsdzh,pdt,fqqcnm,lsgqf,rjc,lzvh

namespace Star1
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = @"C:\sw\AdventOfCode_2020\Advent\Input\star1_org.txt";
            //string path = @"C:\sw\AdventOfCode_2020\Advent\Input\test11.txt";
            if (File.Exists(path)) {

                List<Meal> meals = new List<Meal>();

                string[] lines = File.ReadAllLines(path);
                foreach (string item in lines) {
                    meals.Add(new Meal(item));
                }
                Queue<string> quAllerg = new Queue<string>();
                foreach ( Meal item in meals) {
                    item.PushAllerg(quAllerg);
                }
                while( quAllerg.Count > 0) {
                    string testallg = quAllerg.Dequeue();
                    List<Meal> mealwithAllerg = new List<Meal>();
                    foreach( Meal item in meals) {
                        if( item.ExistsAllerg(testallg)) {
                            mealwithAllerg.Add(item);
                        }
                    }
 
                    if (mealwithAllerg.Count > 0) {
                        List<string> onlyone = onlyone = mealwithAllerg[0].GetIngOnlyOne(mealwithAllerg);
                        if( onlyone.Count != 1) {
                            quAllerg.Enqueue(testallg);
                        }
                        else if(onlyone.Count == 1) {
                            foreach (Meal item in meals) {
                                item.RemoveIng(onlyone[0]);
                                item.RemoveAllerg(testallg);
                            }
                        }   
                    }                    
                }
                int ret = 0;
                foreach( Meal item in meals) {
                    ret += item.GetNumIng();
                }
                
                Console.WriteLine("ingredients {0}", ret);
            }
        }
    }
    class Meal
    {
        List<string> ingedients = new List<string>();
        List<string> allerg = new List<string>();

        public Meal( string i_str)
        {
            string[] line = i_str.Split('(');
            string[] first = line[0].Split(' ');
            foreach( string ing in first) {
                if(!string.IsNullOrEmpty(ing)) {
                    ingedients.Add(ing);
                }
            }
            string[] sec = line[1].Split(' ');
            foreach( string allg in sec) {
                if(!allg.StartsWith("contains")) {
                    allerg.Add(allg.Trim(',').Trim(')'));
                }
            }
        }
        public bool NotEmptyIng()
        {
            if (ingedients.Count > 0) return true;
            return false;
        }
        public void PushAllerg(Queue<string> io_allerg)
        {
            foreach( string item in allerg) {
                if (!io_allerg.Contains(item)) {
                    io_allerg.Enqueue(item);
                }
            }
        }
        public bool ExistsAllerg( string i_allerg)
        { 
            if (allerg.Contains(i_allerg)) return true;
            return false;
        }
        public bool ExistsIng(string i_ing)
        {
            if (ingedients.Contains(i_ing)) return true;
            return false;
        }
        public List<string> GetIngOnlyOne(List<Meal> i_list)
        {
            List<string> onlyone = new List<string>();
            foreach( string item in ingedients) {
                bool found = true;
                foreach( Meal eat in i_list) {
                    if(!eat.ExistsIng(item)) {
                        found = false;
                    }
                }
                if( found == true) {
                    onlyone.Add(item);
                }
            }
            return onlyone;
        }

        public void RemoveIng( string i_ing)
        {
            ingedients.Remove(i_ing);
        }
        public void RemoveAllerg( string i_allerg)
        {
            allerg.Remove(i_allerg);
        }
        public int GetNumIng()
        {
            return ingedients.Count;
        }
    }
}