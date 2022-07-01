using FluentValidation;
using Microsoft.Extensions.Configuration;
using Solicity.Domain.DTOs;
using Solicity.Domain.Entities;
using Solicity.Domain.Ports;
using Solicity.Domain.Services;
using Solicity.Domain.Validators;

namespace Solicity.Application.Services
{
    public class AuthService : IAuthService
    {
        private IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public AuthService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDTO> RegisterAsync(UserCreationDTO userCreationDTO)
        {
            var emailInUse = await _unitOfWork.Users.GetByEmailAsync(userCreationDTO.Email);
            if (emailInUse != null) throw new Exception("Email aready in use");

            var usernameInUse = await _unitOfWork.Users.GetByUsernameAsync(userCreationDTO.Username);
            if (usernameInUse != null) throw new Exception("Username aready in use");

            var user_id = Guid.NewGuid();
            var user = new User
            {
                Id = user_id,
                CreatedAt = DateTime.Now,
                CreatedBy = user_id,
                UpdatedAt = DateTime.Now,
                UpdatedBy = user_id,

                Username = userCreationDTO.Username.ToLower(),
                Email = userCreationDTO.Email.ToLower(),
                Password = userCreationDTO.Password,
                FirstName = userCreationDTO.FirstName,
                LastName = userCreationDTO.LastName,
                ProfileImage = userCreationDTO.ProfileImage,
                Enabled = true,
            };

            var validator = new UserValidator();
            await validator.ValidateAndThrowAsync(user);

            await _unitOfWork.Users.InsertAsync(user);

            return (UserDTO)user;
        }

        public async Task<UserDTO> AuthenticateAsync(UserCredentialDTO userCredentialDTO)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(userCredentialDTO.Email);

            if (user == null) throw new Exception("User not exists");

            if (!user.CheckPassword(userCredentialDTO.Password))
            {
                throw new UnauthorizedAccessException();
            }

            return (UserDTO)user;
        }
    }
}
