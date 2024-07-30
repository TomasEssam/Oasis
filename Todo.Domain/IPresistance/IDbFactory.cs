using Todo.Domain.Entities.Context;

namespace Todo.Domain.IPresistance
{
    public interface IDbFactory : IDisposable
    {
        TodoContext Init();
    }
}
