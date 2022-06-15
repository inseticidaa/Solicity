using FluentValidation;
using Solicity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
