using FluentValidation;

namespace CRMSystem.Application.DTOs.Project.Validators
{
    public class ProjectUpdateDtoValidator : AbstractValidator<ProjectUpdateDto>
    {
        public ProjectUpdateDtoValidator()
        {
            Include(new ProjectBaseDtoValidator());
        }
    }
}
