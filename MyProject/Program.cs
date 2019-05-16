using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ConsoleModule;

namespace MyProject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);

            var consapp = new Consapp();
            //consapp.Modules.Add(new ModuleOne());
            //consapp.Modules.Add(new HandlePersons());
            //consapp.Run("Welcome! ");
            Tools.DoActionMenu("DOOO EEEET!",
                new Tools.ActionMenuItem() { Header = "Fösta", Callback = Tools.Do1 },
                new Tools.ActionMenuItem() { Header = "Andra", Callback = Tools.Do2 },
                new Tools.ActionMenuItem() { Header = "Tredje", Callback = Tools.Do3 });
        }
    }
}
