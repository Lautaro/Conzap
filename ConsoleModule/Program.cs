using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleModule
{
    class Program
    {
        static void Main(string[] args)
        {
            var consapp = new Consapp();
            //consapp.Modules.Add(new ModuleOne());
            //consapp.Modules.Add(new HandlePersons());
            //consapp.Run("Welcome! ");
            Tools.DoActionMenu("DOOO EEEET!", 
                new Tools.ActionMenuItem() {Header = "Fösta", Callback = Tools.Do1 },
                new Tools.ActionMenuItem() { Header = "Andra", Callback = Tools.Do2 },
                new Tools.ActionMenuItem() { Header = "Tredje", Callback = Tools.Do3 }) ;
        
        }
    }
}
