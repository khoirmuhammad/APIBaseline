using APIBaseline.Models;

namespace APIBaseline.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetUsersAsync(string nameFilter, int pageNumber, int pageSize);
		Task CreateUserAsync(User user, IEnumerable<UserRole> roles);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
