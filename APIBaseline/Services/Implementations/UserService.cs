using APIBaseline.Models;
using APIBaseline.Repositories;
using APIBaseline.Services.Interfaces;

namespace APIBaseline.Services.Implementations
{
    public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;

		public UserService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<User> GetUserAsync(int id)
		{
			return await _unitOfWork.Users.GetByIdAsync(id);
		}

		public async Task<IEnumerable<User>> GetAllUsersAsync()
		{
			return await _unitOfWork.Users.GetAllAsync();
		}

		public async Task<IEnumerable<User>> GetUsersAsync(string nameFilter, int pageNumber, int pageSize)
		{
			return await _unitOfWork.Users.GetAsync(
				predicate: u => u.Name.Contains(nameFilter),
				orderBy: q => q.OrderBy(u => u.Name),
				selector: s => s.Select(u => new User
				{
					Id = u.Id,
					Name = u.Name,
					UserRoles = null
				}),
				skip: (pageNumber - 1) * pageSize,
				take: pageSize
			);
		}

		public async Task CreateUserAsync(User user, IEnumerable<UserRole> roles)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				await _unitOfWork.Users.AddAsync(user);
				foreach (UserRole role in roles)
				{
					await _unitOfWork.UserRoles.AddAsync(role);
				}

				await _unitOfWork.CompleteAsync();
				await _unitOfWork.CommitAsync();
			}
			catch
			{
				await _unitOfWork.RollbackAsync();
				throw;
			}
		}

		public async Task UpdateUserAsync(User user)
		{
			await _unitOfWork.Users.UpdateAsync(user);
			await _unitOfWork.CompleteAsync();
		}

		public async Task DeleteUserAsync(int id)
		{
			await _unitOfWork.Users.DeleteAsync(id);
			await _unitOfWork.CompleteAsync();
		}
	}
}
