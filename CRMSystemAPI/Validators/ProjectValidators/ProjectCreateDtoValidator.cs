using CRMSystemAPI.Models.DataTransferModels.ProjectTransferModels;
using FluentValidation;

namespace CRMSystemAPI.Validators.ProjectValidators
{
    public class ProjectCreateDtoValidator : AbstractValidator<ProjectCreateDto>
    {
        public ProjectCreateDtoValidator()
        {
            Include(new ProjectBaseDtoValidator());
        }
    }
}
