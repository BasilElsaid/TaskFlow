using FluentValidation;
using TaskFlow.Dtos.Requests.Task;

namespace TaskFlow.Validators.Task;

public class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
{
    public UpdateTaskRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);
        
        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow)
            .When(x => x.DueDate.HasValue);
    }
}