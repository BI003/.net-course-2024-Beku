using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Models
{
    public class Account
    {
        public string Currency { get; set; }
        public decimal Amount {  get; set; }
    }
}
