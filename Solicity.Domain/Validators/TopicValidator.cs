using FluentValidation;
using Solicity.Domain.Entities;

namespace Solicity.Domain.Validators
{
    public class TopicValidator : AbstractValidator<Topic>
    {
        public TopicValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.Enabled).NotNull();
        }
    }
}
