using FluentValidation;
using Solicity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicity.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Username).NotNull().NotEmpty();
            RuleFor(user => user.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(user => user.Hash).NotNull().NotEmpty().MinimumLength(8);
        }
    }
}
