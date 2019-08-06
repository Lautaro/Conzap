using ConsoleApp2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conzap;
using Conzap.Menu;
using Conzap.ObjectPrinting;

namespace ConsoleApp2
{
    class HandlePersons 
    {
        public string Title { get; set; } = "Handle persons";
        List<Person> Persons { get; set; }

        public HandlePersons()
        {
            Persons = new List<Person>()
            {
                new Person(){
                    Name = "John",
                    Age = 41,
                    Year = new DateTime(1977, 1, 1)
                },
                new Person(){
                    Name = "Some guy",
                    Age = 23,
                    Year = new DateTime(1994, 1, 1)
                },
                new Person(){
                    Name = "Stella",
                    Age = 14,
                    Year = new DateTime(2004, 1, 1)
                }

            };
        }

        [ConzapMenuItem("Create new person",0)]
        public void CreatePerson()
        {
            Console.Clear();
            var now = DateTime.Now;
            var person = new Person();

            Console.WriteLine("CREATE NEW PERSON");
            person.Name = ConzapTools.ChooseString("Name: ");
            person.Age = ConzapTools.ChooseInt("Age: ", 0, 110);
            var year = ConzapTools.ChooseInt("Year: ", 1900, DateTime.Now.Year);
            person.Year = new DateTime(year, now.Month, now.Day);

            Persons.Insert(0,person);
        }

        [ConzapMenuItem("List all persons", 0)]
        public void ListPersons()
        {
            Console.Clear();

            ConzapTools.PrintCustomObjects(Persons)
                .CustomField("", p => p.Name.ToUpper())
                .CustomField("Age", p => p.Age.ToString())
                .CustomField("Year", p => p.Year.ToString())
                .Print();
        }
    }
}
