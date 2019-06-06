using ConsoleApp2;
using Conzap;
using Conzap.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void CreatePerson()
        {
            Console.Clear();
            var now = DateTime.Now;
            var person = new Person();

            Console.WriteLine("CREATE NEW PERSON");
            person.Name = ConzapTools.AskForString("Name: ");
            person.Age = ConzapTools.AskForInt("Age: ", 0, 110);
            var year = ConzapTools.AskForInt("Year: ", 1900, DateTime.Now.Year);
            person.Year = new DateTime(year, now.Month, now.Day);

            Persons.Insert(0,person);
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

            ConzapTools.AskForKey();
        }
    }

    internal class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Year { get; set; }

    }
}
