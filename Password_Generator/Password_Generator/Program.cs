using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace Password_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            FileStream fs = File.Create("./list.txt");
            
            RandomSequence passwordGen = new RandomSequence(rand);
            string[] passList = passwordGen.MakeSequence(1000);
            for (int i = 0; i < 1000; i++)
            {
                
                Byte[] data = new UTF8Encoding(true).GetBytes(passList[i]+"\n");
                fs.Write(data, 0, data.Length);
            }
            
            Console.ReadLine();
        }

    }

    class RandomSequence
    {
        DataBaseConnection dbconn;
        Random rand;

        public RandomSequence(Random r)
        {
            dbconn = new DataBaseConnection();
            rand = r;
        }

        public string [] MakeSequence(int count)
        {
            string[] separator = { "_", "-", "%", "#", "&", "+", "=", "^" };
            List<string> wordList = new List<string>(GetWordList(count*3));
            string[] passwordList = new string[count];

            for (int i = 0; i < count; i++) // iterate over each password string
            {
                int len = rand.Next(2, 4);
                string temp = "";
                for (int j = 0; j < len; j++) //string together 2-3 random words with separators
                {
                    temp += wordList[0]; //using a list like a stack. top and pop.
                    wordList.Remove(wordList[0]);
                    if (j < len - 1)
                        temp += separator[rand.Next(0, separator.Length)];
                } Console.WriteLine(temp);
                
                passwordList[i] = temp;
            }

            return passwordList;
        }

        private List<string> GetWordList(int count)
        {
            return this.AddSomeFluff(dbconn.GetRandomWordList(count));
        }

        private char CharacterReplace(char character)
        {
            Dictionary<char, List<char>> alphas = new Dictionary<char, List<char>>();
            char[] a = { '4', '@' };
            alphas.Add('a', new List<char>(a));

            char[] e = { '3' };
            alphas.Add('e', new List<char>(e));

            char[] h = { '#' };
            alphas.Add('h', new List<char>(h));

            char[] i = { '1', '!' };
            alphas.Add('i', new List<char>(i));

            char[] o = { '0' };
            alphas.Add('o', new List<char>(o));

            char[] r = { '2' };
            alphas.Add('r', new List<char>(r));

            char[] s = { '5' };
            alphas.Add('s', new List<char>(s));

            char[] t = { '7' };
            alphas.Add('t', new List<char>(t));

            if (alphas.ContainsKey(character))
            {
                List<char> opts = new List<char>();
                opts = alphas[character];
                int opt = this.rand.Next(0, opts.Count());
                
                character = opts[opt];
            }
                return character;
        }
        public List<string> AddSomeFluff(List<string> pass)
        {
            for (int i = 0; i < pass.Count; i++)
            {
                string replacer = "";
                for (int j = 0; j < pass[i].Length; j++)
                {
                    if (this.rand.Next(0, 50) <= 2)
                        replacer += CharacterReplace(pass[i][j]);
                    else replacer += pass[i][j];
                } 
                pass[i] = replacer;
            }
            return pass;

        }
    }
    class DataBaseConnection
    {
        SQLiteConnection db = new SQLiteConnection("Data Source=dictionary_database.db;Version=3");

        public DataBaseConnection()
        {
            
            db.Open();
        }

        ~DataBaseConnection()
        {
            db.Close();
        }

        public List<string> GetRandomWordList(int count) 
        {
            List<string> word = new List<string>();
            string sql = "SELECT * FROM words ORDER BY RANDOM() LIMIT " + count;

            SQLiteCommand command = new SQLiteCommand(sql, db);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            for (int i = 0; i < count; i++)
            {
                word.Add(reader["word"].ToString());
                reader.Read();
            }
            

            return word;
        }
        
    }

}
