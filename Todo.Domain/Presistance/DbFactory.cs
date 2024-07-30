using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities.Context;
using Todo.Domain.IPresistance;

namespace Todo.Domain.Presistance
{
    public class DbFactory : Disposable, IDbFactory
    {
        DbContextOptions<TodoContext> _options;
        public DbFactory(DbContextOptions<TodoContext> options)
        {
            _options = options;
        }
        TodoContext dbContext;

        public TodoContext Init()
        {
            return dbContext ?? (dbContext = new TodoContext(_options));
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
