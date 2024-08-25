using CRMSystemAPI.Models.DataTransferModels.UserTransferModels;
using FluentValidation;

namespace CRMSystemAPI.Validators.UserValidators
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            Include(new UserBaseDtoValidator());
        }
    }
}
