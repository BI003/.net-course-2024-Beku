using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Passport { get; set; }
        public int Number {  get; set; }

        public Person(string name, string surname, int passport, int number) 
        {
            Name = name;
            Surname = surname;
            Passport = passport;
            Number = number;
        }
    }
}
