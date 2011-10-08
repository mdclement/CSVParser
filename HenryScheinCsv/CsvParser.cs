using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HenryScheinCsv
{
    public class CsvParser
    {
        private const char delimiter = ',';

        public IList<IList<string>> Parse(string stringToParse)
        {
            IList<IList<string>> returnList = new List<IList<string>>();
            IList<string> currentList = new List<string>();
            bool inQuotes = false;
            var currentValue = new StringBuilder();
            for (int i = 0; i < stringToParse.Length; i++)
            {
                char currentChar = stringToParse[i];
                char nextChar = ' ';
                if (i + 1 < stringToParse.Length)
                {
                    nextChar = stringToParse[i + 1];
                }
                if (EndOfValue(currentChar, inQuotes))
                {
                    currentList.Add(currentValue.ToString());
                    currentValue = new StringBuilder();
                }
                else if (StartQuotes(currentChar, inQuotes))
                {
                    inQuotes = true;
                }
                else if (EndQuotes(currentChar, nextChar, inQuotes))
                {
                    inQuotes = false;
                }
                else if (DoubleQuotesInQuotes(currentChar, nextChar, inQuotes))
                {
                    currentValue.Append(currentChar);
                    i++;
                }
                else if (EndOfLine(currentChar, inQuotes))
                {
                    currentList.Add(currentValue.ToString());
                    currentValue = new StringBuilder();
                    returnList.Add(currentList);
                    currentList = new List<string>();
                }
                else
                {
                    currentValue.Append(currentChar);
                }
            }
            currentList.Add(currentValue.ToString());
            returnList.Add(currentList);
            return returnList;
        }

        private static bool EndOfLine(char currentChar, bool inQuotes)
        {
            return !inQuotes && currentChar == '\n';
        }

        private static bool DoubleQuotesInQuotes(char currentChar, char nextChar, bool inQuotes)
        {
            return inQuotes && currentChar == '\"' && nextChar == '\"';
        }

        private bool EndQuotes(char currentChar, char nextChar, bool inQuotes)
        {
            return inQuotes && currentChar == '\"' && nextChar != '\"';
        }

        private static bool StartQuotes(char currentChar, bool inQuotes)
        {
            return !inQuotes && currentChar == '\"';
        }

        private static bool EndOfValue(char currentChar, bool inQuotes)
        {
            return !inQuotes && currentChar == delimiter;
        }

        public string PrepForExport(IList<IList<string>> values)
        {
            IList<string> lines = values.
                Select(valueLine => valueLine.
                    Select(x => string.Format("[{0}]", x))).
                        Select(bracketedList => string.Join(" ", bracketedList.ToArray())).ToList();
            string finalOutput = string.Join("\n", lines.ToArray());
            return finalOutput;
        }
    }
}