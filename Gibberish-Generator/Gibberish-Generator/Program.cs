using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gibberish_Generator
{
    class Vowel {

        Random rand;
        public Vowel(Random r)
        {
            rand = r;
        }
        

        public bool isVowel(char letter)
        {
            return (letter.Equals('a') || letter.Equals('e') || 
                    letter.Equals('i') || letter.Equals('o') || 
                    letter.Equals('u'));
        }

        public string makeVowelSound() 
        {
            int vowelRandom = rand.Next(0, 5);
            switch (vowelRandom) {
                case 0:
                    return "a" + secondRule("a");
                case 1:
                    return "e"+ secondRule("e");
                case 2:
                    return "i"+ secondRule("i");
                case 3:
                    return "o"+ secondRule("o");
                case 4:
                    return "u"+ secondRule("u");
                case 5:
                    return "y";
                default:
                    throw new Exception("But I thought you wanted a vowel!");
            }
        }

        public string secondRule(string firstVowel)
        {
            int boolSecondVowel = rand.Next(0, 2);
            int vowelRandom;
            if (boolSecondVowel == 0)
                return "";

            string[] vowels = { "a", "e", "i", "o", "u", "y", "ye" };
            List<string> vowelChoices = new List<string>(vowels);
            
            if (firstVowel.Equals("a") || firstVowel.Equals("i") || firstVowel.Equals("u") || firstVowel.Equals("y") || firstVowel.Equals("ye"))
            {
                vowelChoices.Remove(firstVowel);
            }

            vowelRandom = rand.Next(0, vowelChoices.Count());
            
            return vowelChoices[vowelRandom];
            
        }
    }

    class Consonant
    {
        Random rand;
        public Consonant(Random r)
        {
            rand = r;
        }

        public string makeConsonantSound()
        {
            int doNot = rand.Next(0, 2);
            /*if (doNot == 0)   This does some weird things.
                return "";*/
            int consonant = rand.Next(0, 20);
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };
            List<string> consonantChoices = new List<string>(consonants);

            return consonantChoices[consonant];
        }
    }

    class Syllable
    {
        static Random rand = new Random();
        Vowel v = new Vowel(rand);
        Consonant c = new Consonant(rand);
        //string syllable;

        /*public Syllable()
        {
         // Don't need this right now  
        }*/

        public string getSyllables(int i)
        {
            string word = "";
            for (int j=0; j < i; ++j) 
            {
                word += getSyllable();    
            }

            return word;
        }

        public string getSyllable()
        {
            return c.makeConsonantSound() + v.makeVowelSound() + c.makeConsonantSound();
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            Syllable s = new Syllable();
            Console.WriteLine(s.getSyllables(3));

            Console.ReadLine();
        }
    }
}
