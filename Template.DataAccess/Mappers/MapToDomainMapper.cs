using AutoMapper;
using Template.Core.DTOs;
using Template.DataAccess.Models;

namespace Template.DataAccess.Mappers
{
    public class MapToDomainMapper : Profile
    {
        public MapToDomainMapper()
        {
            #region User
            CreateMap<UsersViewModel, UserDTO>();
            #endregion
        }
    }
}
