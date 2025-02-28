using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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
        static ChromeOptions m_Options;
        static WebDriverWait wait;
        public override void Start()
        {
            m_Options = new ChromeOptions();
            m_Options.AddUserProfilePreference("download.default_directory", $"{CFGDirectory}/ChromeDriver/");
            m_Options.AddUserProfilePreference("disable-popup-blocking", "true");
            m_Options.AddUserProfilePreference("download.prompt_for_download", false);
            chromeDir = $"{CFGDirectory}/";
            m_Options.AddArguments("disable-infobars");
            m_Options.AddArguments("--disable-dev-shm-usage");
        }

        public static void OpenWebBrowser()
        {
            try
            {
                var timeout = TimeSpan.FromSeconds(180);

                driver = new ChromeDriver(chromeDir, m_Options, timeout);
                wait = new WebDriverWait(driver, timeout);
                GoToUrl("https://github.com/GeToNIX531/Ultima");
            }
            catch (Exception e) { Console.Logger.WriteLine(e.Message, ConsoleColor.Red); }
        }

        public static void SetDirectory(string Name)
        {
            if (Directory.Exists($"{chromeDir}{Name}/") == false)
                Directory.CreateDirectory($"{chromeDir}{Name}/");

            m_Options.AddArgument($"--user-data-dir={Directory.GetCurrentDirectory()}/{chromeDir}{Name}/");
            m_Options.AddArgument("--disable-extensions");
            m_Options.AddArguments("--no-sandbox");
            m_Options.AddArguments("--remote-debugging-pipe");
        }

        public static void GoToUrl(string Url) => driver?.Navigate().GoToUrl(Url);
        public static void Back() => driver?.Navigate().Back();

        public static IWebElement[] FindElements(By by) => driver.FindElements(by).ToArray();
        public static bool FindElement(By by, out IWebElement Element)
        {
            var elements = FindElements(by);
            if (elements == null || elements.Length == 0)
            {
                Element = null;
                return false;
            }

            Element = elements[0];
            return true;
        }


        public static IWebElement[] waitFindElements(By by) => wait.Until(drv => drv.FindElements(by)).ToArray();
        public static bool waitFindElement(By by, out IWebElement Element)
        {
            var elements = waitFindElements(by);
            if (elements == null || elements.Length == 0)
            {
                Element = null;
                return false;
            }

            Element = elements[0];
            return true;
        }
    }
}
