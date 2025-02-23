using System;

namespace Ultima.Console.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class PluginRequireAttribute : Attribute
    {
        public Type PluginType { get; }

        public PluginRequireAttribute(Type pluginType)
        {
            PluginType = pluginType;
        }
    }
}
