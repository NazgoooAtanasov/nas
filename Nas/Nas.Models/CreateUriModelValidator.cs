using FluentValidation;

namespace Nas.Models
{
    public class CreateUriModelValidator : AbstractValidator<CreateUriModel>
    {
        public CreateUriModelValidator()
        {
            RuleFor(x => x.Link).Matches(@"https:\/\/\w+.[a-z]+\/?");
            RuleFor(x => x.Slug).MinimumLength(2);
        }
    }
}