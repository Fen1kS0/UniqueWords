using System;
using System.Linq;
using System.Net;
using AngleSharp;
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
        private readonly string[] _greenList =
        {
            "div", "span", "li", "a", "p", 
            "h1", "h2", "h3", "h4", "h5", "h6",
            "article", "address", "b", "i", "blockquote",
            "cite", "dd", "del", "dfn", "em", "ins",
            "label", "legend", "pre", "q", "s",
            "small", "strong", "sup", "sub",
            "th", "td", "u"
        };

        public IDocument GetHtmlDocument(Uri uri)
        {
            if (!uri.IsWellFormedOriginalString())
                throw new ArgumentException("Ошибка адреса web страницы");
            
            var config = Configuration.Default;

            var context = BrowsingContext.New(config);

            var source = GetHtmlInString(uri);

            return context.OpenAsync(req => req.Content(source)).Result;
        }

        public string GetHtmlInString(Uri uri)
        {
            using WebClient webClient = new WebClient();
            
            return webClient.DownloadString(uri);
        } 
        
        /// <summary>
        /// Разбитие текста страницы на отдельные слова с помощью списка разделителей,
        /// и получение статистики по количеству уникальных слов в тексте.
        /// </summary>
        /// <returns>Массив группировок уникальных слов</returns>
        public IGrouping<string, string>[] GetWordGrouping(Uri uri)
        {
            var document = GetHtmlDocument(uri);
            
            return document.All
                .Where(e => _greenList.Contains(e.LocalName))
                .Select(e => e.TextContent.ToUpper())
                .Select(s => s.Split(_separators, StringSplitOptions.RemoveEmptyEntries))
                .SelectMany(s => s)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Where(s => s.All(Char.IsLetter))
                .GroupBy(s => s)
                .OrderBy(s => s.Count())
                .ToArray();
        }
    }
}