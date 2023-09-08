using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using Template.DataAccess.Models;
using Template.DataAccess.Context;
using Template.DataAccess.Repositories.Interfaces;

namespace Template.DataAccess.Repositories
{
    public class UserRepository : RepositoryBase<UsersViewModel>, IUserRepository
    {
        private readonly ILogger _logger; 
        private readonly CoreContext _context;

        public UserRepository(CoreContext context, ILogger<RepositoryBase<UsersViewModel>> logger) : base(context, logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<UsersViewModel>> GetAllUsersAsync(int skip, int take)
        {
            return await GetAllAsync(skip, take);
        }

        public async Task<UsersViewModel> GetByIdUserAsync(int userId)
        {
            return await GetByIdAsync(userId);
        }

        public async Task<UsersViewModel> CreateUserAsync(UsersViewModel user)
        {
            return await CreateAsync(user);
        }

        public async Task<UsersViewModel> UpdateUserAsync(UsersViewModel user)
        {
            return await UpdateAsync(user, user.Id);
        }
        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await DeleteAsync(userId);
        }
    }
}
