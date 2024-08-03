using Application.Features.Auth.Events;
using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using Core.Application.Security;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisteredResponse>
{
    public UserForRegisterDto Register { get; set; }
    public string IpAddress { get; set; }

    internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IMediator _mediator;

        public RegisterCommandHandler(IUserRepository userRepository, IAuthService authService, AuthBusinessRules authBusinessRules, IMediator mediator)
        {
            _userRepository = userRepository;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _mediator = mediator;
        }

        public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.EmailShouldBeUnique(request.Register.Email);

            HashingHelper.CreatePasswordHash(request.Register.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User newUser = new User
            {
                Email = request.Register.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Nickname = request.Register.Nickname
            };

            await _userRepository.AddAsync(newUser);

            var accessToken = _authService.CreateAccessToken(newUser);
            var refreshToken = await _authService.CreateRefreshTokenAsync(newUser, request.IpAddress);

            await _mediator.Publish(new SendEmailVerificationEvent
            {
                UserId = newUser.Id,
                Email = newUser.Email,
                Nickname = newUser.Nickname
            });

            return new RegisteredResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }
    }
}
