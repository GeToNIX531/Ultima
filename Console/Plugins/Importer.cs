using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ultima.Console.Attributes;

namespace Ultima.Console.Plugins
{
    public class Importer
    {
        public const string dirLibs = "libs/";
        public const string dirPlugins = "plugins/";
        public const string dirConfigs = "configs/";

        public Importer()
        {
            Instalization();
        }

        private void CreateDirectory(string dir)
        {
            if (Directory.Exists(dir) == false)
                Directory.CreateDirectory(dir);
        }

        public List<Core> LoadPlugins()
        {
            List<Core> plugins = new List<Core>();
            Dictionary<string, Core> pluginInstances = new Dictionary<string, Core>();
            Dictionary<string, List<string>> dependenciesMap = new Dictionary<string, List<string>>();

            // Сначала просматриваем все плагины и собираем их зависимости
            foreach (string file in Directory.GetFiles(dirPlugins, "*.dll"))
            {
                Assembly assembly = Assembly.LoadFrom(file);
                var types = assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && typeof(Core).IsAssignableFrom(t));

                foreach (var type in types)
                {
                    var instance = Activator.CreateInstance(type) as Core;
                    var dependencies = instance.GetDependencies();

                    // Устанавливаем приоритет плагина
                    var attr = (PluginPriorityAttribute)Attribute.GetCustomAttribute(type, typeof(PluginPriorityAttribute));
                    instance.SetPriority(attr != null ? attr.Priority : 0); // Устанавливаем приоритет или 0, если атрибут не задан

                    dependenciesMap[type.FullName] = dependencies; // Сохраняем зависимости

                    // Сохраняем экземпляр плагина (пока, не проверив зависимости)
                    pluginInstances[type.FullName] = instance;
                }
            }

            // Теперь сортируем плагины по приоритету
            var sortedPlugins = pluginInstances.Values.OrderBy(p => p.GetPriority()).ToList();

            // Проверяем зависимости и окончательно создаем список плагинов без незавершенных зависимостей
            foreach (var instance in sortedPlugins)
            {
                var type = instance.GetType().FullName;
                var dependencies = dependenciesMap[type];

                // Проверяем, удовлетворены ли все зависимости
                bool allDependenciesSatisfied = true;
                foreach (var dependency in dependencies)
                {
                    if (!pluginInstances.ContainsKey(dependency))
                    {
                        allDependenciesSatisfied = false;
                        break;
                    }
                }

                if (allDependenciesSatisfied)
                {
                    plugins.Add(instance);
                }
                else
                {
                    Console.Logger.WriteLine($"Plugin {type} has unsatisfied requirements: {string.Join(", ", dependencies)}", ConsoleColor.Red);
                }
            }

            return plugins;
        }

        public void Instalization()
        {
            CreateDirectory(dirLibs);
            CreateDirectory(dirPlugins);
            CreateDirectory(dirConfigs);
        }

        public void StartPlugins(List<Core> plugins)
        {
            foreach (var plugin in plugins)
            {
                plugin.LoadCFG();
                plugin.LoadLibs();
                plugin.Start();
            }
        }
    }
}
