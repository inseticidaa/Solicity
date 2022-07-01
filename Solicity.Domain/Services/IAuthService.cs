using Solicity.Domain.DTOs;

namespace Solicity.Domain.Services
{
    public interface IAuthService
    {
        Task<UserDTO> RegisterAsync(UserCreationDTO userCreationDTO);
        Task<UserDTO> AuthenticateAsync(UserCredentialDTO userCredentialDTO);
    }
}
