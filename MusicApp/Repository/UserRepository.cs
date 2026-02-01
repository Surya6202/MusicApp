using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Threading.Tasks;

namespace MusicApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext ?? 
                throw new ArgumentNullException(nameof(appDbContext));
        }

        public async Task<List<User>> GetUserDetailsAsync()
        {
            var users = await _dbContext.Users.ToListAsync();

            if (users.Count  == 0)
            {
                return null;
            }

            return users;
        }

        public async Task<User> GetUserAsync(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            var isSuccess = false;
            if (user == null)
            {
                return isSuccess;
            }

            try
            {
                user.IsAdmin = IsAdmin(user.UserName);
                user.UserName = GetUserName(user.UserName);
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                isSuccess = true;
            }
            catch (Exception)
            {
                return false;
            }

            return isSuccess;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var isSuccess = false;
            if (user == null)
            {
                return isSuccess;
            }

            try
            {
                var existingUser = await GetUserAsync(user.UserId);

                if (existingUser == null)
                    return isSuccess;

                existingUser.IsAdmin = existingUser.IsAdmin ? existingUser.IsAdmin : IsAdmin(user.UserName);
                existingUser.UserName = GetUserName(user.UserName);
                existingUser.Password = user.Password;

                await _dbContext.SaveChangesAsync();

                isSuccess = true;
            }
            catch (Exception)
            {
                return false;
            }

            return isSuccess;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var isSuccess = false;
            var user = await GetUserAsync(id);

            if (user == null)
            {
                return isSuccess;
            }

            try
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                isSuccess = true;
            }
            catch (Exception)
            {
                return false;
            }

            return isSuccess;
        }

        private static bool IsAdmin(string userName)
        {
            return userName.ToLower(CultureInfo.InvariantCulture).Contains("_admin") ? true : default;
        }

        private static string GetUserName(string userName)
        {
            var updatedName = IsAdmin(userName) ? 
                userName.Substring(0, userName.IndexOf('_')) : userName;

            return updatedName;
        }
    }
}