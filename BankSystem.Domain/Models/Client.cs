using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Models
{
    public class Client : Person
    {
        public Client(string name, string surname, int passport, int number) : base(name, surname, passport, number)
        {
        }
    }
}
