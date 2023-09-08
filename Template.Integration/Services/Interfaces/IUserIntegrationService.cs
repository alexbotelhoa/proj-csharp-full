using System.Threading.Tasks;
using System.Collections.Generic;

using Template.Core.DTOs;
using Template.Core.Helpers;
using Template.Core.DTOs.Requests.User;

namespace Template.Integration.Services.Interfaces
{
    public interface IUserIntegrationService
    {
        Task<ApiResponse<IEnumerable<UserDTO>>> GetAllUsers(HeaderRequestHelper headerRequest);
        Task<ApiResponse<UserDTO>> GetByIdUser(HeaderRequestHelper headerRequest, GetUserRequestDTO queryRequest);
        Task<ApiResponse<UserDTO>> CreateUser(HeaderRequestHelper headerRequest, UserDTO bodyRequest);
        Task<ApiResponse<UserDTO>> UpdateUser(HeaderRequestHelper headerRequest, UserDTO bodyRequest, int userId);
        Task<ApiResponse<bool>> DeleteUser(HeaderRequestHelper headerRequest, int userId);
    }
}
