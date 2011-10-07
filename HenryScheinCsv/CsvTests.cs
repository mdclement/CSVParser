using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace HenryScheinCsv
{
    [TestFixture]
    public class CsvTests
    {
        [Test]
        public void EmptyTest()
        {
            var parser = new CsvParser();
            var parsed = parser.Parse(string.Empty);
            Assert.That(parsed.Count(), Is.EqualTo(1));
            Assert.That(parsed[0], Is.EqualTo(string.Empty));
        }

        [Test]
        public void OneSimpleValueNoQuotesTest()
        {
            var parser = new CsvParser();
            var parsed = parser.Parse("42");
            Assert.That(parsed.Count(), Is.EqualTo(1));
            Assert.That(parsed[0], Is.EqualTo("42"));
        }

        [Test]
        public void OneSimpleValueQuotesTest()
        {
            var parser = new CsvParser();
            var parsed = parser.Parse("\"42\"");
            Assert.That(parsed.Count(), Is.EqualTo(1));
            Assert.That(parsed[0], Is.EqualTo("42"));
        }

        [Test]
        public void TwoSimpleValuesNoQuotesTest()
        {
            var parser = new CsvParser();
            var parsed = parser.Parse("24,42");
            Assert.That(parsed.Count(), Is.EqualTo(2));
            Assert.That(parsed[0], Is.EqualTo("24"));
            Assert.That(parsed[1], Is.EqualTo("42"));
        }

        [Test]
        public void TwoSimpleValuesQuotesTest()
        {
            var parser = new CsvParser();
            var parsed = parser.Parse("\"24\",\"42\"");
            Assert.That(parsed.Count(), Is.EqualTo(2));
            Assert.That(parsed[0], Is.EqualTo("24"));
            Assert.That(parsed[1], Is.EqualTo("42"));
        }
    }

    public class CsvParser
    {
        public IList<string> Parse(string stringToParse)
        {
            IList<string> returnList = new List<string>();
            string[] splitString = stringToParse.Split(',');
            foreach (string partString in splitString)
            {
                returnList.Add(partString.Trim('"'));
            }
            return returnList;
        }
    }
}
