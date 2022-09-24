using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        public Dictionary<int, HashSet<string>> Document { get; set; } =
            new Dictionary<int, HashSet<string>>();
        private Dictionary<int, string> WholeText { get; set; } =
            new Dictionary<int, string>();

        public void Add(int id, string documentText)
        {
            var splitters = new[] 
            { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };
            var wordsById = documentText.Split(splitters
                , StringSplitOptions.RemoveEmptyEntries);
            Document.Add(id, wordsById.ToHashSet());
            WholeText.Add(id, documentText);
        }

        // Облегчить
        public List<int> GetIds(string word)
        {
            var result = new List<int>();
            foreach (var id in Document.Keys)
            {
                if (Document[id].Contains(word))
                    result.Add(id);
            }
            return result;
        }

        // Облегчить
        public List<int> GetPositions(int id, string word)
        {
            var result = new List<int>();
            var index = 0;
            while (index >= 0)
            {
                index = WholeText[id].IndexOf(word, index);
                if (index >= 0) result.Add(index);
            }
            return result;
        }

        public void Remove(int id)
        {
            Document.Remove(id);
            WholeText.Remove(id);
        }
    }
}
