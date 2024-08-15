using APIBaseline.Models;
using APIBaseline.Repositories;
using APIBaseline.Services.Interfaces;

namespace APIBaseline.Services.Implementations
{
	public class UserRoleService : IUserRoleService
	{
		private readonly IUnitOfWork _unitOfWork;

		public UserRoleService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<UserRole> GetUserRoleAsync(int id)
		{
			return await _unitOfWork.UserRoles.GetByIdAsync(id);
		}

		public async Task<IEnumerable<UserRole>> GetAllUserRolesAsync()
		{
			return await _unitOfWork.UserRoles.GetAllAsync();
		}

		public async Task CreateUserRoleAsync(UserRole userRole)
		{
			await _unitOfWork.UserRoles.AddAsync(userRole);
			await _unitOfWork.CompleteAsync();
		}

		public async Task UpdateUserRoleAsync(UserRole userRole)
		{
			await _unitOfWork.UserRoles.UpdateAsync(userRole);
			await _unitOfWork.CompleteAsync();
		}

		public async Task DeleteUserRoleAsync(int id)
		{
			await _unitOfWork.UserRoles.DeleteAsync(id);
			await _unitOfWork.CompleteAsync();
		}
	}
}
