using Template.Core.DTOs;

namespace Template.Core.Mocks
{
    public class UserResponseMock : UserDTO
    {
        public UserResponseMock()
        {
            Id = 99;
            Name = "Usuário 99";
            Email = "usuario99@example.com";
        }
    }
}
