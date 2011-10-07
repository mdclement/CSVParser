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
    }

    public class CsvParser
    {
        public IList<string> Parse(string stringToParse)
        {
            throw new NotImplementedException();
        }
    }
}
