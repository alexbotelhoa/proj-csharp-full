using System.Net;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using Template.Core.DTOs;
using Template.Core.Helpers;
using Template.Core.DTOs.Requests.User;
using Template.Integration.Services.Interfaces;
using Template.Integration.Integrations.Interfaces;

namespace Template.Integration.Services
{
    public class UserIntegrationService : IUserIntegrationService
    {
        private readonly IUserIntegration _serviceIntegration;

        public UserIntegrationService(IUserIntegration serviceIntegration)
        {
            _serviceIntegration = serviceIntegration;
        }

        private readonly List<UserDTO> _users = new List<UserDTO>();

        public async Task<ApiResponse<IEnumerable<UserDTO>>> GetAllUsers(HeaderRequestHelper headerRequest)
        {
            var endpointSerice = $"/api/v1/users";
            var result = await _serviceIntegration.CreateCall("GET", headerRequest, endpointSerice, null, null);
            IEnumerable<UserDTO> data = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(result);

            if (data.Count() == 0)
                return ApiResponseHelper.Create(HttpStatusCode.NoContent, "Registros não encontrados!", data);

            return ApiResponseHelper.Create(HttpStatusCode.OK, "Registro encontrado com sucesso!", data);
        }

        public async Task<ApiResponse<UserDTO>> GetByIdUser(HeaderRequestHelper headerRequest, GetUserRequestDTO queryRequest)
        {
            var endpointSerice = $"/api/v1/users/{queryRequest.userId}";
            var result = await _serviceIntegration.CreateCall("GET", headerRequest, endpointSerice, queryRequest, null);
            IEnumerable<UserDTO> users = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(result);

            UserDTO user = users.FirstOrDefault(u => u.Id == queryRequest.userId);

            if (user == null)
                return ApiResponseHelper.Create(HttpStatusCode.NoContent, "Registro não encontrado!", user);

            return ApiResponseHelper.Create(HttpStatusCode.OK, "Registro encontrado com sucesso!", user);
        }

        public async Task<ApiResponse<UserDTO>> CreateUser(HeaderRequestHelper headerRequest, UserDTO bodyRequest)
        {
            var endpointSerice = $"/api/v1/users";
            var result = await _serviceIntegration.CreateCall("POST", headerRequest, endpointSerice, null, bodyRequest);
            IEnumerable<UserDTO> data = JsonConvert.DeserializeObject<List<UserDTO>>(result);

            //if (data == null) return data;

            UserDTO user = new UserDTO
            {
                Id = 99,
                Name = bodyRequest.Name,
                Email = bodyRequest.Email,
            };

            _users.Add(user);

            string selfLink = $"https://api.example.com/users/{user.Id}";

            return ApiResponseHelper.Create(HttpStatusCode.Created, "Registro criado com sucesso!", user, selfLink);
        }

        public async Task<ApiResponse<UserDTO>> UpdateUser(HeaderRequestHelper headerRequest, UserDTO bodyRequest, int userId)
        {
            var endpointSerice = $"/api/v1/xxxxx";
            //var result = await _serviceIntegration.CreateCall("PUT", headerRequest, endpointSerice, null, bodyRequest);
            //var data = JsonConvert.DeserializeObject<List<GetUserResponseDTO>>(result);

            //if (data == null) return data;

            UserDTO user = new UserDTO
            {
                Id = userId,
                Name = bodyRequest.Name,
                Email = bodyRequest.Email,
            };

            return ApiResponseHelper.Create(HttpStatusCode.OK, "Registro atualizado com sucesso!", user);
        }

        public async Task<ApiResponse<bool>> DeleteUser(HeaderRequestHelper headerRequest, int userId)
        {
            var endpointSerice = $"/api/v1/xxxxx";
            //var result = await _serviceIntegration.CreateCall("DELETE", headerRequest, endpointSerice, userId, null);
            //var data = JsonConvert.DeserializeObject<List<GetUserResponseDTO>>(result);

            //if (data == null) return data;

            return ApiResponseHelper.Create(HttpStatusCode.OK, "Registro deletado com sucesso!", true);
        }

        private Task<IEnumerable<UserDTO>> GetUsersFromDatabaseAsync()
        {
            var users = new List<UserDTO>
            {
                new UserDTO
                {
                    Id = 1,
                    Name = "Usuário 1",
                    Email = "usuario1@example.com"
                },
                new UserDTO
                {
                    Id = 2,
                    Name = "Usuário 2",
                    Email = "usuario2@example.com"
                }
            };

            return Task.FromResult<IEnumerable<UserDTO>>(users);
        }
    }
}
