using System.Threading.Tasks;

using Template.DataAccess.Models;
using System.Collections.Generic;

namespace Template.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UsersViewModel>> GetAllUsersAsync(int skip, int take);
        Task<UsersViewModel> GetByIdUserAsync(int userId);
        Task<UsersViewModel> CreateUserAsync(UsersViewModel user);
        Task<UsersViewModel> UpdateUserAsync(UsersViewModel user);
        Task<bool> DeleteUserAsync(int userId);
    }
}
