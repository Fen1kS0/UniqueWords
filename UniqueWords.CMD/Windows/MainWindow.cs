using System;
using System.Linq;
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
                        ShowUniqueWords();
                        break;
                }

                ConsoleHelper.Wait();
            } while (point is not MainMenuPoints.CloseProgram);
        }

        private void ShowUniqueWords()
        {
            Parser parser = new Parser();

            Uri uri = ConsoleHelper.Parse(
                CreateUri,
                "Введите адрес страницы: ",
                u => u.IsWellFormedOriginalString());

            try
            {
                var result = parser.GetWordGrouping(uri);

                foreach (var word in result)
                {
                    Console.WriteLine($"{word.Key} - {word.Count()}");
                }
            }
            catch (System.Net.WebException)
            {
                Console.WriteLine("К серверу невозможно подключиться, введите другой");
            }
        }

        private Uri CreateUri(string uriString)
        {
            return new Uri(uriString);
        }
    }
}