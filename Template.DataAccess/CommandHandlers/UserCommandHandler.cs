using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Template.Core.DTOs;
using Template.Core.Helpers;
using Template.DataAccess.Models;
using Template.DataAccess.Commands;
using Template.DataAccess.Repositories.Interfaces;

namespace Template.DataAccess.CommandHandlers
{
    public class UserCommandHandler : 
        IRequestHandler<GetAllUsersCommand, IActionResult>,
        IRequestHandler<GetByIdUserCommand, IActionResult>,
        IRequestHandler<CreateUserCommand, IActionResult>,
        IRequestHandler<UpdateUserCommand, IActionResult>,
        IRequestHandler<DeleteUserCommand, IActionResult>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Handle(GetAllUsersCommand command, CancellationToken cancellationToken)
        {
            var result = new ApplicationResult<IEnumerable<UserDTO>>();
            result.Result = _mapper.Map<IEnumerable<UserDTO>>(await _userRepository.GetAllUsersAsync(command.skip, command.take));
            result.SetHttpStatusToOk();

            return result;
        }

        public async Task<IActionResult> Handle(GetByIdUserCommand command, CancellationToken cancellationToken)
        {
            var result = new ApplicationResult<UserDTO>();
            result.Result = _mapper.Map<UserDTO>(await _userRepository.GetByIdUserAsync(command.userId));
            
            if (result.Result != null)
                result.SetHttpStatusToOk();
            else
                result.SetHttpStatusToNoContent();

            return result;
        }

        public async Task<IActionResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<UsersViewModel>(command);

            var result = new ApplicationResult<UserDTO>();
            result.Result = _mapper.Map<UserDTO>(await _userRepository.CreateUserAsync(user));
            result.SetHttpStatusToCreated();

            return result;
        }

        public async Task<IActionResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<UsersViewModel>(command);

            var result = new ApplicationResult<UserDTO>();
            result.Result = _mapper.Map<UserDTO>(await _userRepository.UpdateUserAsync(user));

            if (result.Result != null)
                result.SetHttpStatusToOk();
            else
                result.SetHttpStatusToUnprocesssableEntity();

            return result;
        }

        public async Task<IActionResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var result = new ApplicationResult<bool>();
            result.Result = _mapper.Map<bool>(await _userRepository.DeleteUserAsync(command.userId));
            
            if (result.Result)
                result.SetHttpStatusToOk();
            else
                result.SetHttpStatusToUnprocesssableEntity();

            return result;
        }
    }
}
