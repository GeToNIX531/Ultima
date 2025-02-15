using System;
using Console = Ultima.Console;

namespace Selenium
{
    public class Client : Console.ImportCore
    {
        public new static bool IsOnline() => true;

        public new static void Start()
        {
            Console.Logger.WriteLine("Запуск Selenium", ConsoleColor.Cyan);
        }
    }
}
