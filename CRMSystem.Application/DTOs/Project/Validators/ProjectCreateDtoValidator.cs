using FluentValidation;

namespace CRMSystem.Application.DTOs.Project.Validators
{
    public class ProjectCreateDtoValidator : AbstractValidator<ProjectCreateDto>
    {
        public ProjectCreateDtoValidator()
        {
            Include(new ProjectBaseDtoValidator());
        }
    }
}
