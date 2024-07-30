namespace Todo.Domain.IPresistance
{
    public interface IUnitOfWork 
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
