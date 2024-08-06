﻿using FluentValidation;

namespace Application.Features.Messages.Commands.SendMessage;

public class SendMeesageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SendMeesageCommandValidator()
    {
        RuleFor(x => x.ChatId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x).Must(HaveContentOrFileIdentifier)
                       .WithMessage("Content ya da FileIdentifier alanlarından en az birisi dolu olmalıdır.");
        RuleFor(x => x).Must(NotHaveBothContentAndFileIdentifier)
                       .WithMessage("Content ve FileIdentifier alanları aynı anda dolu olamaz.");
    }

    private bool HaveContentOrFileIdentifier(SendMessageCommand command)
    {
        return !string.IsNullOrEmpty(command.Content) || !string.IsNullOrEmpty(command.FileIdentifier);
    }

    private bool NotHaveBothContentAndFileIdentifier(SendMessageCommand command)
    {
        return string.IsNullOrEmpty(command.Content) || string.IsNullOrEmpty(command.FileIdentifier);
    }
}
