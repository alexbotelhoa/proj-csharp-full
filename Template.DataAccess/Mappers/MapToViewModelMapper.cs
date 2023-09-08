using AutoMapper;
using Template.Core.DTOs;
using Template.DataAccess.Models;

namespace Template.DataAccess.Mappers
{
    public class MapToViewModelMapper : Profile
    {
        public MapToViewModelMapper()
        {
            #region User
            CreateMap<UserDTO, UsersViewModel>();
            #endregion
        }
    }
}
