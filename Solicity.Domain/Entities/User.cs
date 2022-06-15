using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicity.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImage { get; set; }

        public bool Enabled { get; set; }

        public string Password
        {

            set
            {
                Hash = BCrypt.Net.BCrypt.HashPassword(value);
            }
        }

        public bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Hash);
        }

        public User()
        {

        }

        public User(string username, string email, string password)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            CreatedBy = Id;
            UpdatedAt = DateTime.Now;
            UpdatedBy = Id;

            Username = username;
            Email = email;
            Password = password;
            Enabled = true;
        }
    }
}
