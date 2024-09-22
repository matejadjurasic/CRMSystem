using FluentValidation;

namespace CRMSystem.Application.DTOs.User.Validators
{
    public class UserBaseDtoValidator : AbstractValidator<UserBaseDto>
    {
        public UserBaseDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
                            .Length(1, 50).WithMessage("Name must be between 1 and 50 characters.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                                 .EmailAddress().WithMessage("A valid email is required.");
        }
    }
}
