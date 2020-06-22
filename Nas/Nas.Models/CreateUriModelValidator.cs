using FluentValidation;

namespace Nas.Models
{
    public class CreateUriModelValidator : AbstractValidator<CreateUriModel>
    {
        public CreateUriModelValidator()
        {
            RuleFor(x => x.Link)
                .Matches(
                    @"^http(s)?:\/\/([\w-]+.)+[\w-]+(\/[\w- .\/?%&=])?$");
            RuleFor(x => x.Slug).MinimumLength(2);
        }
    }
}