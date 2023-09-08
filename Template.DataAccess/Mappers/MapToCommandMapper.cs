using AutoMapper;
using Template.Core.DTOs;
using Template.DataAccess.Commands;
using Template.Core.DTOs.Requests.User;

namespace Template.DataAccess.Mappers
{
    public class MapToCommandMapper : Profile
    {
        public MapToCommandMapper()
        {
            #region User
            CreateMap<GetUserRequestDTO, GetAllUsersCommand>();
            CreateMap<GetUserRequestDTO, GetByIdUserCommand>();
            CreateMap<UserDTO, CreateUserCommand>();
            CreateMap<UserDTO, UpdateUserCommand>();
            CreateMap<GetUserRequestDTO, DeleteUserCommand>();
            #endregion
        }
    }
}
