using System;
using System.Collections.Generic;
using UniqueWords.BL;
using UniqueWords.Menu;
using UniqueWords.Menu.Enums;
using UniqueWords.Windows.Abstract;

namespace UniqueWords.Windows
{
    public class MainWindow : IWindow
    {
        public void Launch()
        {
            MainMenuPoints point;
            MainMenu menu = new MainMenu();

            do
            {
                Console.Clear();

                point = menu.GetMenuPoint();

                switch (point)
                {
                    case MainMenuPoints.ShowUniqueWords:
                        ParseUniqueWords();
                        break;
                }

                ConsoleHelper.Wait();
            } while (point is not MainMenuPoints.CloseProgram);
        }

        private void ParseUniqueWords()
        {
            Uri uri = ConsoleHelper.Parse(
                s => new Uri(s),
                "Введите адрес страницы: ",
                u => u.IsWellFormedOriginalString());

            PageController pageController = new PageController(uri);
            Logger logger = new Logger();
            
            try
            {
                Parser parser = new Parser(pageController.GetHtmlDocument());

                var result = parser.GetWordDictionary(uri);
                pageController.DownloadHtml();
                pageController.RecordSiteOnDB(result);
                
                ShowUniqueWords(result);
            }
            catch (System.Net.WebException)
            {
                logger.AddLog($"{DateTime.Now} Connection to {uri.AbsoluteUri} is failed");
                Console.WriteLine("К серверу невозможно подключиться, введите другой");
            }
        }

        private void ShowUniqueWords(Dictionary<string, int> groupingWords)
        {
            foreach (var word in groupingWords)
            {
                Console.WriteLine($"{word.Key} - {word.Value}");
            }
        }
    }
}