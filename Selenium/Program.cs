using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultima.Console.Attributes;
using Ultima.Console.Plugins;

using Console = Ultima.Console;
using Properties = Selenium.Properties;

namespace Ultima.Selenium
{
    [PluginPriority(-100)]
    public class Program : Core
    {
        public Program()
        {
            CFGDirectory = $"configs/{nameof(Selenium)}";
        }

        public override void LoadCFG()
        {
            if (Directory.Exists($"{CFGDirectory}/") == false)
            {
                Directory.CreateDirectory($"{CFGDirectory}/");
            }

            var files = Directory.GetFiles($"{CFGDirectory}/");
            foreach (var file in files)
                File.Delete(file);

            File.WriteAllBytes($"{CFGDirectory}/{nameof(Properties.Resources.chromedriver)}.exe", Properties.Resources.chromedriver);
        }

        public override void LoadLibs()
        {
            File.WriteAllBytes($"{CFGDirectory}/{nameof(Properties.Resources.dlls)}.zip", Properties.Resources.dlls);
            using (var zp = ZipFile.OpenRead($"{CFGDirectory}/{nameof(Properties.Resources.dlls)}.zip"))
            {
                var entry = zp.Entries;
                foreach (var data in entry)
                {
                    data.ExtractToFile($"{Importer.dirLibs}/{data.FullName}", true);
                }
            }

            //ZipFile.ExtractToDirectory($"{CFGDirectory}/{nameof(Properties.Resources.dlls)}.zip", $"{Importer.dirLibs}");
            File.Delete($"{CFGDirectory}/{nameof(Properties.Resources.dlls)}.zip");
        }

        static IWebDriver driver;
        static string chromeDir;
        static ChromeOptions chromeOptions;
        public override void Start()
        {
            chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", $"{CFGDirectory}/ChromeDriver/");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddArguments("disable-infobars");


            chromeDir = $"{CFGDirectory}/";
           
        }

        public static void OpenWebBrowser()
        {
            try
            {
                driver = new ChromeDriver(chromeDir, chromeOptions);
                GoToUrl("https://github.com/GeToNIX531/Ultima");
            }
            catch (Exception e) { Console.Logger.WriteLine(e.Message, ConsoleColor.Red); }
        }

        public static void GoToUrl(string Url) => driver?.Navigate().GoToUrl(Url);
    }
}
