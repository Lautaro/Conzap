using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap
{
    class HandlePersons : IConzapModule
    {
        public string Title { get; set; } = "Handle persons";
        List<Person> Persons { get; set; } 

        public HandlePersons()
        {
            Persons = new List<Person>()
            {
                new Person(){
                    Name = "Lautaro Arino",
                    Age = 41,
                    Year = new DateTime(1977, 1, 1)
                },
                new Person(){
                    Name = "Sofia",
                    Age = 23,
                    Year = new DateTime(1994, 1, 1)
                },
                new Person(){
                    Name = "Lillasyster",
                    Age = 14,
                    Year = new DateTime(2004, 1, 1)
                }

            };
        }

        public void CreatePerson()
        {
            Console.Clear();
            var now = DateTime.Now;
            var person = new Person();

            Console.WriteLine("CREATE NEW PERSON");
            person.Name = ConzapTools.StringInput("Name: ");
            person.Age = ConzapTools.NumberInput("Age: ", 25, 110);
            var year = ConzapTools.NumberInput("Year: ", 1900, DateTime.Now.Year);
            person.Year = new DateTime(year, now.Month, now.Day);

            Persons.Add(person);
        }

        public void ListPersons()
        {
            Console.Clear();
            foreach (var person in Persons)
            {
                
                ConzapTools.PrintList(person.Name.ToUpper(),
                    "Age: " + person.Age.ToString(),
                    "Year: " + person.Year.ToString());
                ConzapTools.SkipLines(2);
            }

            ConzapTools.KeyInput();
        }

        public void Execute()
        {
            while (true)
            {
                Console.Clear();
                var nr = ConzapTools.PrintMenu(header: "HANDLE PERSONS", message: "Choose...",list:  new string[] { "Create person", "List all persons" });
                switch (nr)
                {
                    case 1:
                        CreatePerson();
                        break;
                    case 2:
                        ListPersons();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    internal class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Year { get; set; }

    }
}
