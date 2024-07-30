using Todo.Domain.Entities.Context;
using Todo.Domain.IPresistance;

namespace Todo.Domain.Presistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private TodoContext _dbcontext;
        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
            _dbcontext = _dbcontext ?? (_dbcontext = dbFactory.Init());
        }
        public TodoContext DbContext
        {
            get { return _dbcontext ?? (_dbcontext = _dbFactory.Init()); }

        }

        public void SaveChanges()
        {
            _dbcontext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbcontext.SaveChangesAsync();
        }
    }
}
