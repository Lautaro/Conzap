using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap
{
    class ModuleOne : IConzapModule
    {
        public string Title { get; } = "This is module one";

        public void Execute()
        {
            ConzapTools.KeyInput("Hello from module one!");
        }
    }
}
