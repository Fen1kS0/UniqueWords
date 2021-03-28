using System;
using System.IO;
using System.Net;
using AngleSharp;
using AngleSharp.Dom;

namespace UniqueWords.BL
{
    public class HtmlController
    {
        public IDocument GetHtmlDocument(Uri uri)
        {
            if (!uri.IsWellFormedOriginalString())
                throw new ArgumentException("Ошибка адреса web страницы");
            
            var config = Configuration.Default;

            var context = BrowsingContext.New(config);
            
            var source = GetHtmlInString(uri);

            return context.OpenAsync(req => req.Content(source)).Result;
        }

        public void DownloadHtml(Uri uri)
        {
            using WebClient webClient = new WebClient();
            
            string path = Directory.GetCurrentDirectory() + @"\Sites\";
            Directory.CreateDirectory(path);
            
            webClient.DownloadFileAsync(uri, @$"{path}{uri.Host}.html");
        }

        public string GetHtmlInString(Uri uri)
        {
            using WebClient webClient = new WebClient();
            return webClient.DownloadString(uri);
        } 
    }
}