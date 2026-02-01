using MusicApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicApp.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetUserDetailsAsync();

        Task<User> GetUserAsync(int userId);

        Task<bool> AddUserAsync(User user);

        Task<bool> UpdateUserAsync(User user);

        Task<bool> DeleteUserAsync(int id);
    }
}
