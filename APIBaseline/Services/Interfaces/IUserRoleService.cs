using APIBaseline.Models;

namespace APIBaseline.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<UserRole> GetUserRoleAsync(int id);
        Task<IEnumerable<UserRole>> GetAllUserRolesAsync();
        Task CreateUserRoleAsync(UserRole userRole);
        Task UpdateUserRoleAsync(UserRole userRole);
        Task DeleteUserRoleAsync(int id);
    }
}
