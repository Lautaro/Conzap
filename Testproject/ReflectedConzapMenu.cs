using Conzap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class ReflectedConzapMenu
    {
        HandlePersons handlePersons = new HandlePersons();

        [ConzapMenuItem("List all persons", 0)]
        public void ListPersons()
        {
            handlePersons.ListPersons();
        }

        [ConzapMenuItem("Create new person", 0)]
        public void CreatePersons()
        {
            handlePersons.CreatePerson();
        }
    }
}
