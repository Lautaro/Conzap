using Conzap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var handlePersons = new HandlePersons();

            ConzapTools.RunMenu("Handle Persons",
                new ConzapMenuItem("Create Person", () => handlePersons.CreatePerson()),
                new ConzapMenuItem("List Persons", () => handlePersons.ListPersons())
                );
            

            //var menu = new ConzapMenu()
            //{
            //    Header = "Handle Persons",
            //    MenuItems = new List<ConzapMenuItem>()
            //    {
            //        new ConzapMenuItem()
            //        {
            //            Header = "Create Person",
            //            Callback = () =>  handlePersons.CreatePerson()
            //        }
            //    }
            //};

            //menu.Run();
        }
    }
}
