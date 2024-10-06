namespace BankSystem.Domain.Models
{
    public struct Currency
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public int Price { get; set; }
    }
}
