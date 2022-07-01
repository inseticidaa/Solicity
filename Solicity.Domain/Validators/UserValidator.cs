using FluentValidation;
using Solicity.Domain.Entities;

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
