using ContactList.Application.Contracts;
using ContactList.Application.Contracts.Repositories;
using ContactList.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitofWork, IDisposable
    {
        private readonly DbFactory _dbFactory;
        private readonly ICurrentUserService _currentUserService;

        public UnitOfWork(DbFactory dbFactory, ICurrentUserService currentUserService)
        {
            _dbFactory = dbFactory;
            _currentUserService = currentUserService;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            await using var transaction = await _dbFactory.DbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var affectedRows = await _dbFactory.DbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return affectedRows;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            finally
            {
                await _dbFactory.DbContext.Database.CloseConnectionAsync();
                await _dbFactory.DbContext.DisposeAsync();
            }
        }

        public void Dispose()
        {
            _dbFactory?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
