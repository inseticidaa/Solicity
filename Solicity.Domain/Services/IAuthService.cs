using Solicity.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicity.Domain.Services
{
    public interface IAuthService
    {
        Task<UserDTO> RegisterAsync(UserCreationDTO userCreationDTO);
        Task<UserDTO> AuthenticateAsync(UserCredentialDTO userCredentialDTO);
    }
}
