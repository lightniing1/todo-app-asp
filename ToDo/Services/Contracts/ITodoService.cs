using ToDo.Models;

namespace ToDo.Services.Contracts
{
    public interface ITodoService
    {
        Task Delete(int id);
        Task<IList<Todo>> GetAll();
        Task<Todo> GetById(int id);
        Task Insert(Todo todo);
        Task Update(Todo todo);
    }
}