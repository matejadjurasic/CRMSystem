using FluentValidation;

namespace CRMSystem.Application.DTOs.User.Validators
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            Include(new UserBaseDtoValidator());
        }
    }
}
