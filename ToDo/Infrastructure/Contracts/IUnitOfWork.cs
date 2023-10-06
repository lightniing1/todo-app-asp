namespace ToDo.Infrastructure.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task UseTransactionScope(Action action);
    }
}
