using System;

namespace UniqueWords.Menu.Abstract
{
    public abstract class MenuBase<TEnum> where TEnum : Enum
    {
        public abstract void ShowMenu();

        public TEnum GetMenuPoint()
        {
            ShowMenu();
            
            int point = ConsoleHelper.Parse(
                parseFunc: int.Parse,
                description: "Ваш выбор: ",
                condition: p => 0 <= p && p < Enum.GetValues(typeof(TEnum)).Length);
            
            return (TEnum)Enum.ToObject(typeof(TEnum), point);
        }
    }
}