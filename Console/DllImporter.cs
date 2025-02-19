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

        public const string pluginsDirectory = "plugins/";
        public const string configsDirectory = "configs/";
        public static void Load()
        {
            var domain = AppDomain.CurrentDomain;

            if (Directory.Exists(pluginsDirectory) == false)
            {
                Directory.CreateDirectory(pluginsDirectory);
                return;
            }

            if (Directory.Exists(configsDirectory) == false)
            {
                Directory.CreateDirectory(configsDirectory);
                return;
            }

            var files = Directory.GetFiles(pluginsDirectory, "*.dll");
            plugins = new Assembly[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];
                var plugin = Assembly.LoadFile(Directory.GetCurrentDirectory() + "/" + file);
                plugins[i] = plugin;
            }

            var target = typeof(ImportCore);

            var classes =
            from assembly in plugins
            from type in assembly.GetTypes()
            where target.IsAssignableFrom(type)
            select type;


            foreach(var instance in classes)
            {
                var name = instance.Namespace;

                var fieldInfo = typeof(ImportCore).GetField(nameof(ImportCore.ConfigDirectory));
                fieldInfo.SetValue(instance, $"{configsDirectory}{name}");


                var start = instance.GetMethod(nameof(ImportCore.Start));
                start.Invoke(null, null);
            }
        }
    }
}
