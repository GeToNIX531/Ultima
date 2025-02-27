using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultima.Console.Plugins;
using Console = Ultima.Console;

namespace Anti_Captcha
{
    public class Program : Core
    {
        private static string Api;

        public override void LoadCFG()
        {
            Console.Logger.WriteLine(CFGDirectory, ConsoleColor.Green);

            if (Directory.Exists(CFGDirectory) == false)
                Directory.CreateDirectory(CFGDirectory);

            var userFile = Path.Combine(CFGDirectory, "token.json");
            if (File.Exists(userFile) == false)
            {
                Api = string.Empty;
                var text = JsonConvert.SerializeObject(Api);
                File.WriteAllText(userFile, text);
            }
            else
            {
                var text = File.ReadAllText(userFile);
                Api = JsonConvert.DeserializeObject<string>(text);
            }

            return;
        }

        public override void LoadLibs()
        {
            return;
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }
    }
}
