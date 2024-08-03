using Application.Features.Auth.Rules;
using Application.Services.Mail;
using Application.Services.Repositories;
using MediatR;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Events;

internal class SendEmailVerificationEvent : INotification
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Nickname { get; set; }

    internal class UserRegisteredEventHandler : INotificationHandler<SendEmailVerificationEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly AuthBusinessRules _authBusinessRules;

        public UserRegisteredEventHandler(IEmailService emailService, IUserRepository userRepository, AuthBusinessRules authBusinessRules)
        {
            _emailService = emailService;
            _userRepository = userRepository;
            _authBusinessRules = authBusinessRules;
        }

        public async Task Handle(SendEmailVerificationEvent notification, CancellationToken cancellationToken)
        {
            // Send email to user
            var user = await _userRepository.GetAsync(u=>u.Id==notification.UserId);
            
            _authBusinessRules.UserShouldExist(user);

            var verificationCode = Guid.NewGuid().ToString().Substring(0,6);
            user!.VerificationCode = verificationCode;
            await _userRepository.UpdateAsync(user);

            var emailBody = $"<h1>Welcome to our platform!</h1><p>Your verification code is: {verificationCode}</p>";
            await _emailService.SendEmailAsync(notification.Email, "Welcome!", emailBody);

        }
    }
}

