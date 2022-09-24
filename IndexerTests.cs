using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace PocketGoogle
{
    [TestFixture]
    internal class IndexerTests
    {
        private List<Dictionary<int, string>> examplesForAdding
        { get; set; } = new List<Dictionary<int, string>>()
        {
            new Dictionary<int, string>()
            {
                { 1,  "I love you" },
                { 2, "my sweety kitty" },
                { 3,  "Anyutya"},
            },
            new Dictionary<int, string>()
            {
                { 5,  "I am not imagine" },
                { 3, "my life without you" }
            },
            new Dictionary<int, string>()
            {
                { 9,  "You are my beautiful girl" }
            },
            new Dictionary<int, string>()
            {
                { 7,  "swap sweet smeet greet" }
            }
        };
        
        [Test]
        public void AddTest()
        {
            foreach (var example in examplesForAdding)
            {
                var indexer = new Indexer();
                var expected = FillDictionaryIn(example, indexer);
                Assert.AreEqual(expected, indexer.Document);
            }
        }

        private Dictionary<int, List<string>> FillDictionaryIn(
            Dictionary<int, string> example, Indexer indexer)
        {
            var result = new Dictionary<int, List<string>>();
            foreach (var id in example.Keys)
            {
                indexer.Add(id, example[id]);
                result.Add(id, example[id].Split(' ').ToList());
            }
            return result;
        }

        [Test]
        public void GetIdsTest()
        {
            var wordParam = "sweety";
            var expected = new List<int>()
            {
                2
            };
            var indexer = new Indexer();
            FillDictionaryIn(examplesForAdding[0], indexer);
            var actual = indexer.GetIds(wordParam);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetPositionsTest()
        {
            var indexer = new Indexer();
            FillDictionaryIn(examplesForAdding[3], indexer);
            var expected = new List<int>()
            {
                0, 1
            };
            var actual = indexer.GetPositions(7, "sw");
            Assert.AreEqual(expected, actual);
        }
    }
}
