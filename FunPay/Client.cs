using Console = Ultima.Console;
using WebBrowser = Ultima.Selenium.Program;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Ultima.Console.Attributes;
using Ultima.Console.Plugins;
using System.IO;
using FunPay.Data;
using Newtonsoft.Json;

namespace FunPay
{
    [PluginRequire(typeof(WebBrowser))]
    public class Client : Core
    {
        public UserAuth userAuth;

        public Client()
        {
            CFGDirectory = $"configs/{nameof(FunPay)}";
        }

        public override void LoadCFG()
        {
            Console.Logger.WriteLine(CFGDirectory, ConsoleColor.Green);

            if (Directory.Exists(CFGDirectory) == false)
                Directory.CreateDirectory(CFGDirectory);

            var userFile = Path.Combine(CFGDirectory, "user.json");
            if (File.Exists(userFile) == false)
            {
                userAuth = new UserAuth("login", "password");
                var text = JsonConvert.SerializeObject(userAuth);
                File.WriteAllText(userFile, text);
            }
            else
            {
                var text = File.ReadAllText(userFile);
                userAuth = JsonConvert.DeserializeObject<UserAuth>(text);
            }

            return;
        }

        public override void LoadLibs()
        {
            return; 
        }

        public override void Start()
        {
            Console.Logger.WriteLine("Запуск FunPay", ConsoleColor.Cyan);

            WebBrowser.SetDirectory(nameof(FunPay));
            WebBrowser.OpenWebBrowser();
            WebBrowser.GoToUrl("https://funpay.com/");
        }

    }
}
