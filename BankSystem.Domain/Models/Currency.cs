﻿namespace BankSystem.Domain.Models
{
    public struct Currency
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
    }
}
