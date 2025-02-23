using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using System.IO;

namespace Ultima.Console
{
    public class DllImporter
    {
        public static Assembly[] plugins;

        public const string dllDirectory = "libs/";
        public const string pluginsDirectory = "plugins/";
        public const string configsDirectory = "configs/";
        public static void Load()
        {
            var domain = AppDomain.CurrentDomain;

            if (Directory.Exists(pluginsDirectory) == false)
            {
                Directory.CreateDirectory(pluginsDirectory);
            }

            if (Directory.Exists(configsDirectory) == false)
            {
                Directory.CreateDirectory(configsDirectory);
            }

            if (Directory.Exists(dllDirectory) == false)
            {
                Directory.CreateDirectory(dllDirectory);
            }

            var files = Directory.GetFiles(pluginsDirectory, "*.dll");
            plugins = new Assembly[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];
                var plugin = Assembly.LoadFile(Directory.GetCurrentDirectory() + "/" + file);
                plugins[i] = plugin;
            }

            int count = plugins.Length;
            Type[] classes = new Type[count];
            for (int i = 0; i < count; i++)
                classes[i] = plugins[i].GetType(nameof(ImportCore));

            

            classes = classes.OrderByDescending(T => T.GetMethod(nameof(ImportCore.Priority)).Invoke(null, null)).ToArray();

            foreach (var instance in classes)
            {
                var name = instance.Namespace;
                System.Console.WriteLine(name);


                var fieldInfo = typeof(ImportCore).GetField(nameof(ImportCore.ConfigDirectory));
                fieldInfo.SetValue(instance, $"{configsDirectory}{name}");

                var start = instance.GetMethod(nameof(ImportCore.Start));
                start.Invoke(null, null);
            }
        }
    }
}
