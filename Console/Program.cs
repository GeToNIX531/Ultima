using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultima.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Запуск программы");
            DllImporter.Load();

            while (true) ;
        }
    }
}
