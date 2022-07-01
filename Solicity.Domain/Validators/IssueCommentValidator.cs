using FluentValidation;
using Solicity.Domain.Entities;

namespace Solicity.Domain.Validators
{
    public class IssueCommentValidator : AbstractValidator<IssueComment>
    {
        public IssueCommentValidator()
        {
        }
    }
}
