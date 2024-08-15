using APIBaseline.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace APIBaseline.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
	{
		protected readonly AppDBContext _context;
		protected readonly DbSet<T> _dbSet;

		public Repository(AppDBContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<IEnumerable<T>> GetAsync(
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			Func<IQueryable<T>, IQueryable<T>> selector = null,
			int? skip = null,
			int? take = null
		)
		{
			IQueryable<T> query = _dbSet;

			if (predicate != null)
			{
				query = query.Where(predicate);
			}

			if (orderBy != null)
			{
				query = orderBy(query);
			}

			if (selector != null)
			{
				query = selector(query);
			}

			if (skip.HasValue)
			{
				query = query.Skip(skip.Value);
			}

			if (take.HasValue)
			{
				query = query.Take(take.Value);
			}

			return await query.ToListAsync();
		}

		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public async Task UpdateAsync(T entity)
		{
			_dbSet.Update(entity);
			await Task.CompletedTask;
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _dbSet.FindAsync(id);
			if (entity != null)
			{
				_dbSet.Remove(entity);
			}
		}
	}

	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(AppDBContext context) : base(context)
		{
		}
	}

	public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
	{
		public UserRoleRepository(AppDBContext context) : base(context)
		{
		}
	}
}
