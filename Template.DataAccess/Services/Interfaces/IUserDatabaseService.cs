using System.Threading.Tasks;
using System.Collections.Generic;

using Template.Core.DTOs;
using Template.Core.Helpers;
using Template.Core.DTOs.Requests.User;

namespace Template.DataAccess.Services.Interfaces
{
    public interface IUserDatabaseService
    {
        Task<ApiResponse<IEnumerable<UserDTO>>> GetAllUsers(HeaderRequestHelper headerRequest, GetUserRequestDTO queryRequest);
        Task<ApiResponse<UserDTO>> GetByIdUser(HeaderRequestHelper headerRequest, GetUserRequestDTO queryRequest);
        Task<ApiResponse<UserDTO>> CreateUser(HeaderRequestHelper headerRequest, UserDTO bodyRequest);
        Task<ApiResponse<UserDTO>> UpdateUser(HeaderRequestHelper headerRequest, UserDTO bodyRequest, GetUserRequestDTO queryRequest);
        Task<ApiResponse<bool>> DeleteUser(HeaderRequestHelper headerRequest, GetUserRequestDTO queryRequest);
    }
}
