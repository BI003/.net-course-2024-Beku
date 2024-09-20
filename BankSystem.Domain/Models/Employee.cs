using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Models
{
    public class Employee : Person
    {
        public string Contract {  get; set; }

        public Employee(string name, string surname, int passport, int number) : base(name, surname, passport, number)
        {
        }
    }
}
