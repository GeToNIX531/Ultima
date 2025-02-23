using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultima.Console.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class PluginPriorityAttribute : Attribute
    {
        public int Priority { get; }

        public PluginPriorityAttribute(int priority)
        {
            Priority = priority;
        }
    }
}
