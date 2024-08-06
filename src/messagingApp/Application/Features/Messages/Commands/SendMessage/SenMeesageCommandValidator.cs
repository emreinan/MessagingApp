using FluentValidation;

namespace Application.Features.Messages.Commands.SendMessage;

public class SenMeesageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SenMeesageCommandValidator()
    {
        RuleFor(x => x.ChatId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Content).NotEmpty().When(x=> string.IsNullOrEmpty(x.FileIdentifier));
        RuleFor(x => x.FileIdentifier).NotEmpty().When(x=> string.IsNullOrEmpty(x.Content));

    }
}
