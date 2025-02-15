using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Console = System.Console;

namespace Ultima.Console
{
    public static class Logger
    {
        public static void WriteLine(string Text, ConsoleColor Color)
        {
            var color = System.Console.ForegroundColor;
            System.Console.ForegroundColor = Color;
            System.Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] {Text}");
            System.Console.ForegroundColor = color;
        }
    }
}
