using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace APIBaseline.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDBContext _context;
		private IDbContextTransaction _transaction;

		private IUserRepository _userRepository;
		private IUserRoleRepository _userRoleRepository;

		public UnitOfWork(AppDBContext context)
		{
			_context = context;
		}

		public IUserRepository Users => _userRepository ??= new UserRepository(_context);
		public IUserRoleRepository UserRoles => _userRoleRepository ??= new UserRoleRepository(_context);

		public async Task<int> CompleteAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public async Task BeginTransactionAsync()
		{
			if (_transaction != null)
			{
				throw new InvalidOperationException("Transaction already started.");
			}

			_transaction = await _context.Database.BeginTransactionAsync();
		}

		public async Task CommitAsync()
		{
			if (_transaction == null)
			{
				throw new InvalidOperationException("Transaction not started.");
			}

			try
			{
				await _context.SaveChangesAsync();
				await _transaction.CommitAsync();
			}
			catch
			{
				await RollbackAsync();
				throw;
			}
			finally
			{
				_transaction.Dispose();
				_transaction = null;
			}
		}

		public async Task RollbackAsync()
		{
			if (_transaction != null)
			{
				await _transaction.RollbackAsync();
				_transaction.Dispose();
				_transaction = null;
			}
		}

		public void Dispose()
		{
			_transaction?.Dispose();
			_context.Dispose();
		}
	}
}
