using FluentValidation;
using TaskFlow.Dtos.Requests.Project;

namespace TaskFlow.Validators.Project;

public class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);
    }
}