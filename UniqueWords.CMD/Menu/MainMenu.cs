using System;
using UniqueWords.Menu.Abstract;
using UniqueWords.Menu.Enums;

namespace UniqueWords.Menu
{
    public class MainMenu : MenuBase<MainMenuPoints>
    {
        public override void ShowMenu()
        {
            Console.WriteLine("----------Главное меню----------");
            Console.WriteLine("0) Завершить программу");
            Console.WriteLine("1) Вывести статистику уникальных слов на web странице");
        }
    }
}