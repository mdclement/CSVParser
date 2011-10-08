using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HenryScheinCsv;

namespace HenryScheinCsvParser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("need one parameter: file name to parse");
                return;
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("{0} (input file) doesn't exist", args[0]);
            }
            using (StreamReader reader = new StreamReader(args[0]))
            {
                var inputString = reader.ReadToEnd();
                CsvParser parser = new CsvParser();
                var parsedValues = parser.Parse(inputString);
            }
        }
    }
}
