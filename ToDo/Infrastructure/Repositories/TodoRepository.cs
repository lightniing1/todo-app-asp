using ToDo.Infrastructure.Contracts;
using ToDo.Models;

namespace ToDo.Infrastructure.Repositories
{
    public class TodoRepository : GenericRepository<Todo>, ITodoRepository
    {
        public TodoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
