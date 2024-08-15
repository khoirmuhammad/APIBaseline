using APIBaseline.Models;
using System.Linq.Expressions;

namespace APIBaseline.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
		Task<IEnumerable<T>> GetAsync(
		    Expression<Func<T, bool>> predicate = null,
		    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
		    Func<IQueryable<T>, IQueryable<T>> selector = null,
		    int? skip = null,
		    int? take = null
	    );
		Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }

    public interface IUserRepository : IRepository<User>
    {
    }
    public interface IUserRoleRepository : IRepository<UserRole>
    {
    }
}
