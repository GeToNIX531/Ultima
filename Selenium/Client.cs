using System;
using System.IO;
using Console = Ultima.Console;

using System.IO.Compression;
using System.Reflection;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome; // or more specific its ok you get no error till here

namespace Selenium
{
    public class Client : Console.ImportCore
    {
        public new static bool IsOnline() => true;

        public static new void Start()
        {
            Client.CreateConfig();
            InstallConfigs();

            Console.Logger.WriteLine("Запуск Selenium", ConsoleColor.Cyan);
            Console.Logger.WriteLine(Client.ConfigDirectory, ConsoleColor.Cyan);

            AppDomain.CurrentDomain.AppendPrivatePath($"{ConfigDirectory}/");

            OpenWebBrowser();
        }

        public static new void InstallConfigs()
        {
            var files = Directory.GetFiles($"{ConfigDirectory}/");
            foreach (var file in files)
                File.Delete(file);

            File.WriteAllBytes($"{ConfigDirectory}/{nameof(Properties.Resources.chromedriver)}.exe", Properties.Resources.chromedriver);

            File.WriteAllBytes($"{ConfigDirectory}/{nameof(Properties.Resources.dlls)}.zip", Properties.Resources.dlls);
            ZipFile.ExtractToDirectory($"{ConfigDirectory}/{nameof(Properties.Resources.dlls)}.zip", $"{ConfigDirectory}/");
            File.Delete($"{ConfigDirectory}/{nameof(Properties.Resources.dlls)}.zip");
        }

        public static void OpenWebBrowser()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", $"{ConfigDirectory}/ChromeDriver/");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddArguments("disable-infobars");

            try
            {
                IWebDriver Driver = new ChromeDriver($"{ConfigDirectory}/", chromeOptions);
            }
            catch (Exception e) { Console.Logger.WriteLine(e.Message, ConsoleColor.Red); }

        }
    }
}
