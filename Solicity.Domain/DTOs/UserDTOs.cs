using Solicity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicity.Domain.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImage { get; set; }
        public bool Enabled { get; set; }


        public static implicit operator UserDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                UpdatedAt = user.UpdatedAt,
                CreatedBy = user.CreatedBy,
                CreatedAt = user.CreatedAt,
                UpdatedBy = user.UpdatedBy,

                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Enabled = user.Enabled,
                ProfileImage = user.ProfileImage
            };
        }
    }

    public class UserCredentialDTO
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserCreationDTO
    {
        [Required]
        public string Username { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }


        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImage { get; set; }
    }
}
