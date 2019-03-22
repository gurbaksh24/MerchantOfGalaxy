using System;
using System.Collections.Generic;
using System.Linq;

namespace MerchantOfGalaxy
{
    class Program
    {
        static Dictionary<string, char> intergallacticValues = new Dictionary<string, char>();
        static Dictionary<string, double> PerUnitPriceOfMetal = new Dictionary<string, double>();

        static void Main(string[] args)
        {
            Console.WriteLine("Merchant Guide to the Galaxy!!!!!");
            
            while(true)
            {
                var statement = Console.ReadLine();
                if (statement.Equals("\n"))
                    Environment.Exit(0);
                try
                {
                    ChooseTypeOfStatement(statement);
                }
                catch(Exception)
                {
                    Console.WriteLine("I have no idea what you are talking about");
                }

            }
        }

        private static void ChooseTypeOfStatement(string statement)
        {
            string[] words = statement.Split(' ');
            if(words.Count() == 3)
            {
                AssignableStatement(words);
            }
            else if(statement.Contains("how much"))
            {
                HowMuchStatement(words);
            }
            else if(statement.Contains("how many"))
            {
                HowManyStatement(words);
            }
            else
            {
                PerUnitRateStatement(words);
            }
        }

        private static void PerUnitRateStatement(string[] words)
        {
            var count = words.Count();

            var charList = new List<char>();
            
            for(int index = 0; index < count-4; index++)
            {
                charList.Add(intergallacticValues.First(x => x.Key.Equals(words[index])).Value);
            }

            var value = Convert.ToDouble(words[count - 2]) / RomanToArabicConverter(charList);
            PerUnitPriceOfMetal.Add(words[count - 4],  value);
        }

        private static void HowMuchStatement(string[] words)
        {
            var charList = new List<char>();
            for (int index = 3; index < words.Count()-1; index++)
            {
                charList.Add(intergallacticValues.First(x => x.Key.Equals(words[index])).Value);
            }

            var answer = RomanToArabicConverter(charList);

            for (int index = 3; index < words.Count() - 1; index++)
            {
                Console.Write(words[index]+" ");
            }
            Console.WriteLine("is " + answer);
        }

        private static void HowManyStatement(string[] words)
        {
            var count = words.Count();

            var charList = new List<char>();

            for (int index = 4; index < count - 2; index++)
            {
                charList.Add(intergallacticValues.First(x => x.Key.Equals(words[index])).Value);
            }

            var answer = PerUnitPriceOfMetal.First(x => x.Key.Equals(words[count - 2])).Value * RomanToArabicConverter(charList);

            for (int index = 4; index < count - 1; index++)
            {
                Console.Write(words[index] + " ");
            }
            Console.WriteLine("is " + answer + " Credits");
        }

        private static void AssignableStatement(string[] words)
        {
            intergallacticValues.Add(words.First(), Convert.ToChar(words[2]));
        }


        private static double RomanToArabicConverter(List<char> roman)
        {
            double sum = 0.0;
            var AppendedDigits = RomanToDigit(roman);
            for (int index = 0; index < AppendedDigits.Count; index++)
            {
                if(index + 1 > AppendedDigits.Count-1 || AppendedDigits[index] >= AppendedDigits[index+1])
                {
                    sum += AppendedDigits[index];
                }
                else if(AppendedDigits[index] < AppendedDigits[index+1])
                {
                    sum += AppendedDigits[index + 1] - AppendedDigits[index];
                    index++;
                }
            }
            return sum;
        }

        private static List<double> RomanToDigit(List<char> roman)
        {
            var convertedDigits = new List<double>();

            var countIXCM = 0;
            var countDLV = 0;
            foreach (var element in roman)
            {
                switch (element)
                {
                    case 'I':
                        convertedDigits.Add(1);
                        countIXCM++;
                        countDLV = 0;
                        break;
                    case 'V':
                        convertedDigits.Add(5);
                        countIXCM = 0;
                        countDLV++;
                        break;
                    case 'X':
                        convertedDigits.Add(10);
                        countIXCM++;
                        countDLV = 0;
                        break;
                    case 'L':
                        convertedDigits.Add(50);
                        countIXCM = 0;
                        countDLV++;
                        break;
                    case 'C':
                        convertedDigits.Add(100);
                        countIXCM++;
                        countDLV = 0;
                        break;
                    case 'D':
                        convertedDigits.Add(500);
                        countIXCM = 0;
                        countDLV++;
                        break;
                    case 'M':
                        convertedDigits.Add(1000);
                        countIXCM++;
                        countDLV = 0;
                        break;
                    default:
                        return null;
                }
                if (countIXCM > 3)
                    throw new Exception();
                if (countDLV > 1)
                    throw new Exception();
            }
            return convertedDigits;
        }
    }
}
