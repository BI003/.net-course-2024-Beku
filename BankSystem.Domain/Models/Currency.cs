namespace BankSystem.Domain.Models
{
    public struct Currency
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public Currency (string name, int price) 
        { 
            Name = name; 
            Price = price; 
        }
    }
}
