using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultima.Console.Plugins;

namespace Ultima.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Запуск программы");
            //DllImporter.Load();

            Importer pluginManager = new Importer();
            List<Core> plugins = pluginManager.LoadPlugins();
            pluginManager.StartPlugins(plugins);

            while (true) ;
        }
    }
}
