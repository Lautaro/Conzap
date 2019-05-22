using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap.Module
{
    /// <summary>
    /// Container for IConzapModules. Creates a menu with one option for each module.
    /// </summary>
    public class ConzapModuleContainer
    {
        public ConzapModuleContainer()
        {
            Modules = new List<IConzapModule>();
        }
        public List<IConzapModule> Modules { get; set; }
        public void Run(string message = "") {

            while (true)
            {
                Console.Clear();

                var index = ConzapTools.PrintMenu(message, menuItems: Modules.Select(m => m.Title).ToArray())-1;

                Console.Clear();
                Modules[index].Execute();
            }
        }
    }
}
