using System;

namespace UniqueWords
{
    public static class ConsoleHelper
    {
        public static void RemoveLastString()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine(new string(' ', 100));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        public static T Parse<T>(Func<string, T> parseFunc, string description, Predicate<T> condition)
        {
            while (true)
            {
                Console.Write(description);
                try
                {
                    T result = parseFunc.Invoke(Console.ReadLine());
                    if (condition.Invoke(result))
                        return result;
                    
                    RemoveLastString();
                }
                catch (FormatException)
                {
                    RemoveLastString();
                }
            }
        }

        public static void Wait()
        {
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}