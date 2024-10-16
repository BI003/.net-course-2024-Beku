namespace BankSystem.App.Interfaces
{
    public interface IStorage<T>
    {
         void Add(T item);

         void Update(T item);

         void Delete(T item);
    }
}
