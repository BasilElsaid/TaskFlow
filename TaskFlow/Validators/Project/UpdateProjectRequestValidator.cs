using FluentValidation;
using TaskFlow.Dtos.Requests.Project;

namespace TaskFlow.Validators.Project;

public class UpdateProjectRequestValidator : AbstractValidator<UpdateProjectRequest>
{
    public UpdateProjectRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);
    }
}