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
            if (args.Length != 2 && args.Length != 0)
            {
                Console.WriteLine("need two parameters: file name to parse, file name to export");
                return;
            }
            string inputFilePath = "input.txt";
            string exportFilePath = "output.txt";

            if (args.Length == 2)
            {
                inputFilePath = args[0];
                exportFilePath = args[1];
            }

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("{0} (input file) doesn't exist", inputFilePath);
            }

            string inputString;
            using (var reader = new StreamReader(inputFilePath))
            {
                inputString = reader.ReadToEnd();
            }
            var parser = new CsvParser();
            IList<IList<string>> parsedValues = parser.Parse(inputString);
            string outputString = parser.PrepForExport(parsedValues);
            using (var writer = new StreamWriter(exportFilePath))
            {
                writer.Write(outputString);
            }

        }
    }
}
