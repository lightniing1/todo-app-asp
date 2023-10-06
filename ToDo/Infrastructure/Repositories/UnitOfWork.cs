using Npgsql.Replication.PgOutput.Messages;
using ToDo.Infrastructure.Contracts;
using ToDo.Models;

namespace ToDo.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private async Task Commit()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _dbContext.Database.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UseTransactionScope(Action action)
        {
            using var transaction = _dbContext.Database.BeginTransactionAsync();
            action();
            await Commit();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

    }
}
