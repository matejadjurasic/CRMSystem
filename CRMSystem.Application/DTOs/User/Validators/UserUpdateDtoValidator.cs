using FluentValidation;

namespace CRMSystem.Application.DTOs.User.Validators
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            Include(new UserBaseDtoValidator());
        }
    }
}
