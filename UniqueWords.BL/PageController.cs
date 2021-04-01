using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using AngleSharp;
using AngleSharp.Dom;
using UniqueWords.Data.Entities;
using UniqueWords.Data.Repositories;

namespace UniqueWords.BL
{
    public class PageController
    {
        private readonly ISiteRepository _repository;

        public PageController(Uri currentUri)
        {
            _repository = new SiteRepository();

            CurrentUri = currentUri;
        }

        /// <summary>
        /// Uri, с которым мы сейчас работаем
        /// </summary>
        public Uri CurrentUri { get; set; }
        
        /// <summary>
        /// Разбитие текста страницы на отдельные слова с помощью списка разделителей,
        /// и получение статистики по количеству уникальных слов в тексте.
        /// </summary>
        /// <returns>Массив группировок уникальных слов</returns>
        
        public IDocument GetHtmlDocument()
        {
            if (!CurrentUri.IsWellFormedOriginalString())
            {
                throw new ArgumentException("Ошибка адреса web страницы");
            }
            
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var source = GetHtmlInString();

            return context.OpenAsync(req => req.Content(source)).Result;
        }

        public void DownloadHtml(string path = @"Sites\")
        {
            using WebClient webClient = new WebClient();
            
            Directory.CreateDirectory(path);
            
            webClient.DownloadFileAsync(CurrentUri, @$"{path}{CurrentUri.Host}.html");
        }

        public string GetHtmlInString()
        {
            using WebClient webClient = new WebClient();
            
            return webClient.DownloadString(CurrentUri);
        }
        
        public void RecordSiteOnDB(Dictionary<string, int> dictWords)
        {
            Site site = new Site()
            {
                Uri = CurrentUri.AbsoluteUri
            };
            
            List<Word> uniqueWords = new List<Word>();

            foreach (var uniqueWord in dictWords)
            {
                uniqueWords.Add(new Word()
                {
                    Name = uniqueWord.Key,
                    Count = uniqueWord.Value
                });
            }

            site.UniqueWords = uniqueWords;
            
            _repository.Add(site);
        }
    }
}