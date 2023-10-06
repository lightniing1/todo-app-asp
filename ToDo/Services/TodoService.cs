using ToDo.Controllers;
using ToDo.Infrastructure.Contracts;
using ToDo.Models;
using ToDo.Dtos;
using ToDo.Services.Contracts;

namespace ToDo.Services
{
    public class TodoService : ITodoService
    {
        private readonly ILogger<TodoService> _logger;
        private readonly IGenericRepository<Todo> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public TodoService(
            ILogger<TodoService> logger,
            IGenericRepository<Todo> repository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Insert(Todo todo)
        {
            await _unitOfWork.UseTransactionScope(async () =>
            {
                await _repository.Add(todo);
            });
        }

        public async Task Update(Todo todo)
        {
            await _unitOfWork.UseTransactionScope(() =>
            {
                _repository.Update(todo);
            });
        }

        public async Task Delete(int id)
        {
            var todo = await GetById(id);

            await _unitOfWork.UseTransactionScope(() =>
            {
                _repository.Delete(todo);
            });
        }

        public async Task<Todo> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IList<Todo>> GetAll()
        {
            var result = await _repository.GetAll();

            return result.ToList();
        }
    }
}
