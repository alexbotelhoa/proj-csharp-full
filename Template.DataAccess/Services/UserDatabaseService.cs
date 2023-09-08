using MediatR;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;

using Template.Core.DTOs;
using Template.Core.Helpers;
using Template.DataAccess.Commands;
using Template.Core.DTOs.Requests.User;
using Template.DataAccess.Services.Interfaces;

namespace Template.DataAccess.Services
{
    public class UserDatabaseService : IUserDatabaseService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserDatabaseService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ApiResponse<IEnumerable<UserDTO>>> GetAllUsers(HeaderRequestHelper headerRequest, GetUserRequestDTO queryRequest)
        {
            GetAllUsersCommand command = _mapper.Map<GetAllUsersCommand>(queryRequest);
            ApplicationResult<IEnumerable<UserDTO>> result = (ApplicationResult<IEnumerable<UserDTO>>)await _mediator.Send(command);
            return ApiResponseHelper.Create(result.HttpStatusCode, result.Message, result.Result);
        }

        public async Task<ApiResponse<UserDTO>> GetByIdUser(HeaderRequestHelper headerRequest, GetUserRequestDTO queryRequest)
        {
            GetByIdUserCommand command = _mapper.Map<GetByIdUserCommand>(queryRequest);
            ApplicationResult<UserDTO> result = (ApplicationResult<UserDTO>)await _mediator.Send(command);
            return ApiResponseHelper.Create(result.HttpStatusCode, result.Message, result.Result, result.Url);
        }

        public async Task<ApiResponse<UserDTO>> CreateUser(HeaderRequestHelper headerRequest, UserDTO bodyRequest)
        {
            CreateUserCommand command = _mapper.Map<CreateUserCommand>(bodyRequest);
            ApplicationResult<UserDTO> result = (ApplicationResult<UserDTO>)await _mediator.Send(command);
            return ApiResponseHelper.Create(result.HttpStatusCode, result.Message, result.Result, result.Url);
        }

        public async Task<ApiResponse<UserDTO>> UpdateUser(HeaderRequestHelper headerRequest, UserDTO bodyRequest, GetUserRequestDTO queryRequest)
        {
            UpdateUserCommand command = _mapper.Map<UpdateUserCommand>(bodyRequest);
            command.Id = queryRequest.userId;
            ApplicationResult<UserDTO> result = (ApplicationResult<UserDTO>)await _mediator.Send(command);
            return ApiResponseHelper.Create(result.HttpStatusCode, result.Message, result.Result, result.Url);
        }

        public async Task<ApiResponse<bool>> DeleteUser(HeaderRequestHelper headerRequest, GetUserRequestDTO queryRequest)
        {
            DeleteUserCommand command = _mapper.Map<DeleteUserCommand>(queryRequest);
            ApplicationResult<bool> result = (ApplicationResult<bool>)await _mediator.Send(command);
            return ApiResponseHelper.Create(result.HttpStatusCode, result.Message, result.Result);
        }
    }
}
