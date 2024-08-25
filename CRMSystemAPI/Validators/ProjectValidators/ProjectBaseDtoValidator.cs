using CRMSystemAPI.Models.DataTransferModels.ProjectTransferModels;
using FluentValidation;

namespace CRMSystemAPI.Validators.ProjectValidators
{
    public class ProjectBaseDtoValidator : AbstractValidator<ProjectBaseDto>
    {
        public ProjectBaseDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.")
                              .Length(1, 100).WithMessage("Title must be between 1 and 100 characters.");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description can't be more than 500 characters.");
            RuleFor(x => x.Deadline).Must(d => d > DateTime.UtcNow).WithMessage("Deadline must be in the future.");
        }
    }
}
