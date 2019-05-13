using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleModule
{
    public class Consapp
    {
        public Consapp()
        {
            Modules = new List<IConsoleModule>();
        }
        public List<IConsoleModule> Modules { get; set; }
        public void Run(string message = "") {

            while (true)
            {
                Console.Clear();

                var index = Tools.PrintMenu(message, list: Modules.Select(m => m.Title).ToArray())-1;

                Console.Clear();
                Modules[index].Execute();
            }
        }
    }
}
