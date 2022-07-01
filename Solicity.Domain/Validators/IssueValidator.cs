using FluentValidation;
using Solicity.Domain.Entities;

namespace Solicity.Domain.Validators
{
    public class IssueValidator : AbstractValidator<Issue>
    {
        public IssueValidator()
        {
        }
    }
}
