using Microsoft.EntityFrameworkCore;
using SocialApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Data.Services
{
    public class UsersService : IUsersService
    {
        private readonly AppDbContext _appDbContext;
        public UsersService(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }
        public async Task<User> GetUser(int loggedInUserId)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(n => n.Id == loggedInUserId) ?? new User();
        }

        public async Task UpdateUserProfilePicture(int userId, string profilePictureUrl)
        {
            var userDb = await _appDbContext.Users.FirstOrDefaultAsync(n => n.Id == userId);

            if(userDb != null)
            {
                userDb.ProfilePrictureUrl = profilePictureUrl;
                _appDbContext.Users.Update(userDb);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
