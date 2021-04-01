using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;

namespace UniqueWords.BL
{
    public class Parser
    {
        private readonly char[] _separators =
        {
            ' ', ',', '.', '!', '?','\'', '"', ';',
            ':', '[', ']', '(', ')','»', '‑', '«', '<', '>', '/',
            '{', '}', '|', '-', '-', '\n', '\r', '\t', '@'
        };

        /// <summary>
        /// Теги, в которых может находиться текст
        /// </summary>
        private readonly string[] _textTags =
        {
            "div", "span", "li", "a", "p", 
            "h1", "h2", "h3", "h4", "h5", "h6",
            "article", "address", "b", "i", "blockquote",
            "cite", "dd", "del", "dfn", "em", "ins",
            "label", "legend", "pre", "q", "s",
            "small", "strong", "sup", "sub",
            "th", "td", "u"
        };

        public Parser(IDocument document)
        {
            Document = document;
        }

        public IDocument Document { get; set; }
        
        
        /// <summary>
        /// Разбитие текста страницы на отдельные слова с помощью списка разделителей,
        /// и получение статистики по количеству уникальных слов в тексте.
        /// </summary>
        /// <returns>Массив группировок уникальных слов</returns>
        public Dictionary<string, int> GetWordDictionary(Uri uri)
        {
            return Document.All
                .Where(e => _textTags.Contains(e.LocalName))
                .Select(e => e.TextContent.ToUpper())
                .Select(s => s.Split(_separators, StringSplitOptions.RemoveEmptyEntries))
                .SelectMany(s => s)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Where(s => s.All(Char.IsLetter))
                .GroupBy(s => s)
                .OrderBy(s => s.Count())
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}