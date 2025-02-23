using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultima.Console
{
    public abstract class ImportCore
    {
        public static string ConfigDirectory;
        public static void Start() => CreateConfig();

        public static bool IsOnline() => false;

        public static void CreateConfig()
        {
            if (Directory.Exists($"{ConfigDirectory}") == false)
            {
                Directory.CreateDirectory($"{ConfigDirectory}");
                return;
            }
        }

        public static void InstallConfigs() => throw new Exception("Не установлены конфиги!");

        public static int Priority() => 0;
    }
}
