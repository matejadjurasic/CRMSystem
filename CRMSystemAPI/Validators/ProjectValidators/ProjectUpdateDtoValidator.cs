using CRMSystemAPI.Models.DataTransferModels.ProjectTransferModels;
using FluentValidation;

namespace CRMSystemAPI.Validators.ProjectValidators
{
    public class ProjectUpdateDtoValidator : AbstractValidator<ProjectUpdateDto>
    {
        public ProjectUpdateDtoValidator()
        {
            Include(new ProjectBaseDtoValidator());
        }
    }
}
