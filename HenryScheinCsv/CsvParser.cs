using System.Collections.Generic;
using System.Text;

namespace HenryScheinCsv
{
    public class CsvParser
    {
        public IList<string> Parse(string stringToParse)
        {
            IList<string> returnList = new List<string>();
            bool inQuotes = false;
            StringBuilder currentValue = new StringBuilder();
            foreach (var currentChar in stringToParse)
            {
                if (!inQuotes && currentChar == ',')
                {
                    returnList.Add(currentValue.ToString());
                    currentValue = new StringBuilder();
                }
                else if (!inQuotes && currentChar == '\"')
                {
                    inQuotes = true;
                }
                else if (inQuotes && currentChar == '\"')
                {
                    inQuotes = false;
                }
                else
                {
                    currentValue.Append(currentChar);
                }
            }
            returnList.Add(currentValue.ToString().Trim('"'));
            return returnList;
        }
    }
}