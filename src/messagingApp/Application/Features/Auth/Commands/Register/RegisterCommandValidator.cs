using FluentValidation;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<UserForRegisterDto>
{
    public RegisterCommandValidator() 
    {
        RuleFor(x=>x.Email).NotEmpty().EmailAddress();
        RuleFor(x=>x.Password).NotEmpty().MinimumLength(4);
        RuleFor(x=>x.Nickname).NotEmpty().NotEmpty();
    }
}

//public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
//{
//    public RegisterCommandValidator()
//    {
//        RuleFor(x => x.Register.Email).NotEmpty().EmailAddress();
//        RuleFor(x => x.Register.Password).NotEmpty().MinimumLength(4);
//        RuleFor(x => x.Register.Nickname).NotEmpty().NotEmpty();
//    }
//}