using CRMSystemAPI.Models.DataTransferModels.UserTransferModels;
using FluentValidation;

namespace CRMSystemAPI.Validators.UserValidators
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            Include(new UserBaseDtoValidator());
        }
    }
}
