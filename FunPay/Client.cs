using Console = Ultima.Console;
using WebBrowser = Ultima.Selenium.Program;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Ultima.Console.Attributes;
using Ultima.Console.Plugins;

namespace FunPay
{
    [PluginRequire(typeof(WebBrowser))]
    public class Client : Core
    {
        public override void LoadCFG()
        {
            return;
        }

        public override void LoadLibs()
        {
            return; 
        }

        public override void Start()
        {
            Console.Logger.WriteLine("Запуск FunPay", ConsoleColor.Cyan);
            WebBrowser.OpenWebBrowser();
            WebBrowser.GoToUrl("https://funpay.com/");
        }
    }
}
