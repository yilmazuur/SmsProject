namespace SmsProject.App.Data.DAL.Interface
{
    public interface IGenericRepository<TKey, T> : IPersistRepository<T>,
        IReadOnlyRepository<TKey, T> where T : class, IEntityKey<TKey>
    {
    }
}
