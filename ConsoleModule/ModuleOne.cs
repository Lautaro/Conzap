using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleModule
{
    class ModuleOne : IConsoleModule
    {
        public string Title { get; } = "This is module one";

        public void Execute()
        {
            Tools.KeyInput("Hello from module one!");
        }
    }
}
