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
            Assert.That(parsed[0].Count(), Is.EqualTo(1));
            Assert.That(parsed[0][0], Is.EqualTo(string.Empty));
        }

        [Test]
        public void OneSimpleValueNoQuotesTest()
        {
            var parser = new CsvParser();
            var parsed = parser.Parse("42");
            Assert.That(parsed.Count(), Is.EqualTo(1));
            Assert.That(parsed[0][0], Is.EqualTo("42"));
        }

        [Test]
        public void OneSimpleValueQuotesTest()
        {
            var parser = new CsvParser();
            var parsed = parser.Parse("\"42\"");
            Assert.That(parsed.Count(), Is.EqualTo(1));
            Assert.That(parsed[0][0], Is.EqualTo("42"));
        }

        [Test]
        public void TwoSimpleValuesNoQuotesTest()
        {
            var parser = new CsvParser();
            var parsed = parser.Parse("24,42");
            Assert.That(parsed[0].Count(), Is.EqualTo(2));
            Assert.That(parsed[0][0], Is.EqualTo("24"));
            Assert.That(parsed[0][1], Is.EqualTo("42"));
        }

        [Test]
        public void TwoSimpleValuesQuotesTest()
        {
            var parser = new CsvParser();
            var parsed = parser.Parse("\"24\",\"42\"");
            Assert.That(parsed[0].Count(), Is.EqualTo(2));
            Assert.That(parsed[0][0], Is.EqualTo("24"));
            Assert.That(parsed[0][1], Is.EqualTo("42"));
        }

        [Test]
        public void OneCommaValueQuotesTest()
        {
            var parser = new CsvParser();
            var parsed = parser.Parse("\"4,2\"");
            Assert.That(parsed[0].Count(), Is.EqualTo(1));
            Assert.That(parsed[0][0], Is.EqualTo("4,2"));
        }

        [Test]
        public void TwoCommaValueQuotesTest()
        {
            var parser = new CsvParser();
            var parsed = parser.Parse("\"4,2\",\"2,4\"");
            Assert.That(parsed[0].Count(), Is.EqualTo(2));
            Assert.That(parsed[0][0], Is.EqualTo("4,2"));
            Assert.That(parsed[0][1], Is.EqualTo("2,4"));
        }

        [Test]
        public void TwoValuesQuotesNoQuotesTest()
        {
            var parser = new CsvParser();
            var parsed = parser.Parse("24,\"42\"");
            Assert.That(parsed[0].Count(), Is.EqualTo(2));
            Assert.That(parsed[0][0], Is.EqualTo("24"));
            Assert.That(parsed[0][1], Is.EqualTo("42"));
        }

        [Test]
        public void TwoValuesEmptyQuotesNoQuotesTest()
        {
            var parser = new CsvParser();
            var parsed = parser.Parse(",\"\"");
            Assert.That(parsed[0].Count(), Is.EqualTo(2));
            Assert.That(parsed[0][0], Is.EqualTo(string.Empty));
            Assert.That(parsed[0][1], Is.EqualTo(string.Empty));
        }

        [Test]
        public void TwoValuesDoubleQuotesNoQuotesTest()
        {
            var parser = new CsvParser();
            var parsed = parser.Parse("42,\"-\"\"42\"\"-\"");
            Assert.That(parsed[0].Count(), Is.EqualTo(2));
            Assert.That(parsed[0][0], Is.EqualTo("42"));
            Assert.That(parsed[0][1], Is.EqualTo("-\"42\"-"));
        }

        [Test]
        public void WithNewLineTest()
        {
            var parser = new CsvParser();
            var parsed = parser.Parse("12,34\n56,78");
            Assert.That(parsed.Count(), Is.EqualTo(2));
            Assert.That(parsed[0].Count(), Is.EqualTo(2));
            Assert.That(parsed[1].Count(), Is.EqualTo(2));
            Assert.That(parsed[0][0], Is.EqualTo("12"));
            Assert.That(parsed[0][1], Is.EqualTo("34"));
            Assert.That(parsed[1][0], Is.EqualTo("56"));
            Assert.That(parsed[1][1], Is.EqualTo("78"));
        }

        [Test]
        public void WithNewLineInValueTest()
        {
            var parser = new CsvParser();
            var parsed = parser.Parse("\"12\n12\",34\n56,78");
            Assert.That(parsed.Count(), Is.EqualTo(2));
            Assert.That(parsed[0].Count(), Is.EqualTo(2));
            Assert.That(parsed[1].Count(), Is.EqualTo(2));
            Assert.That(parsed[0][0], Is.EqualTo("12\n12"));
            Assert.That(parsed[0][1], Is.EqualTo("34"));
            Assert.That(parsed[1][0], Is.EqualTo("56"));
            Assert.That(parsed[1][1], Is.EqualTo("78"));
        }
    }
}
