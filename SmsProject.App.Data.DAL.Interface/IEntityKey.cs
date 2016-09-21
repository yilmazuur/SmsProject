namespace SmsProject.App.Data.DAL.Interface
{
    public interface IEntityKey<out TKey>
    {
        TKey Id { get; }
    }
}