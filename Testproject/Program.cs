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
       static HandlePersons handlePersons = new HandlePersons();

        static void Main(string[] args)
        {
            // Uncomment one of the options to se an example of Conzap

            // First example
            // ReflectedConzapMenu();

            // Second example
            ConzapMenuWithDelegates();

            // Third example
            //InstantiateConzapMenu(handlePersons);

        }

        private static void InstantiateConzapMenu()
        {
            var menu = new ConzapMenu()
            {
                Header = "Handle Persons",
                MenuItems = new List<ConzapMenuItem>()
                {
                    new ConzapMenuItem()
                    {
                        Title = "Create person",
                        Callback = () =>  handlePersons.CreatePerson()
                    },
                      new ConzapMenuItem()
                    {
                        Title = "List persons",
                        Callback = () =>  handlePersons.ListPersons()
                    }
                }
            };

            menu.Run();
        }

        private static void ConzapMenuWithDelegates()
        {
            ConzapTools.RunMenu("Handle Persons",
                new ConzapMenuItem("Create Person", () => handlePersons.CreatePerson()),
                new ConzapMenuItem("List Persons", () => handlePersons.ListPersons())
                );
        }

        private static void ReflectedConzapMenu()
        {
            ConzapTools.RunMenu(typeof(ReflectedConzapMenu));
        }
    }
}
