using FluentValidation;

namespace Nas.Models
{
    public class RedirectUriModelValidator : AbstractValidator<RedirectUriModel>
    {
        public RedirectUriModelValidator()
        {
            RuleFor(x => x.Slug).MinimumLength(2);
        }
    }
}