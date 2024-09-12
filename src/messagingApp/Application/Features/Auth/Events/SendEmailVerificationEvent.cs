using Application.Features.Auth.Rules;
using Application.Services.Mail;
using Application.Services.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Application.Features.Auth.Events;

internal class SendEmailVerificationEvent : INotification
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Nickname { get; set; }

    internal class UserRegisteredEventHandler(
        IEmailService emailService, 
        IUserRepository userRepository, 
        AuthBusinessRules authBusinessRules, 
        IConfiguration configuration) : INotificationHandler<SendEmailVerificationEvent>
    {
        public async Task Handle(SendEmailVerificationEvent notification, CancellationToken cancellationToken)
        {
            // Send email to user
            var user = await userRepository.GetAsync(u => u.Id == notification.UserId);

            authBusinessRules.UserShouldExist(user);

            var verificationCode = Guid.NewGuid().ToString().Substring(0, 6);
            user!.VerificationCode = verificationCode;
            await userRepository.UpdateAsync(user);

            string domain = configuration["Domain"];
            string httpEncodeEmail = HttpUtility.UrlEncode(user.Email);
            string verficationLink = $"{domain}/api/Auth/VerifyEmail?Email={httpEncodeEmail}&Code={verificationCode}";

            var emailBody = $"""
                            <h1>Welcome{notification.Nickname}</h1>
                            <p>Your verification code is: {verificationCode}</p>
                            <p>Click the link below to verify your email</p>
                            <a href='{verficationLink}'>Verify Email</a>
                            """;
            await emailService.SendEmailAsync(notification.Email, "Welcome to MessagingApp!", emailBody);

        }
    }
}

