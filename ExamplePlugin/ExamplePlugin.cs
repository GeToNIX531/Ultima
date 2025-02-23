using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultima.Console.Attributes;
using Ultima.Console.Plugins;
using Console = Ultima.Console;

public class ExamplePlugin : Core
{
    public override void LoadCFG()
    {
        Console.Logger.WriteLine(nameof(LoadCFG), ConsoleColor.Green);
    }

    public override void LoadLibs()
    {
        Console.Logger.WriteLine(nameof(LoadLibs), ConsoleColor.Green);
    }

    public override void Start()
    {
        Console.Logger.WriteLine(nameof(Start), ConsoleColor.Green);
    }
}
