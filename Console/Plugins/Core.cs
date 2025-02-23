using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultima.Console.Attributes;

namespace Ultima.Console.Plugins
{
    public abstract class Core
    {
        public string CFGDirectory;
        public int Priority { get; private set; }  // Поле для хранения приоритета
        public bool cfg = false;

        public abstract void Start();
        public abstract void LoadCFG();
        public abstract void LoadLibs();

        public virtual void SetPriority(int priority)  // Метод для установки приоритета
        {
            Priority = priority;
        }

        public virtual List<string> GetDependencies()
        {
            var dependencies = new List<string>();
            var type = GetType();

            // Получаем атрибуты PluginRequire
            var attributes = type.GetCustomAttributes(typeof(PluginRequireAttribute), false);
            foreach (PluginRequireAttribute attr in attributes)
            {
                dependencies.Add(attr.PluginType.FullName);
            }

            return dependencies;
        }

        public virtual int GetPriority()
        {
            return Priority;  // Возвращаем храненое значение приоритета
        }
    }
}
