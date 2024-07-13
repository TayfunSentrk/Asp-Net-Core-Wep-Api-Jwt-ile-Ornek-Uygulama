using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos;
using FluentValidation;

namespace AuthServer.Api.Validations
{
    public class CreateUserDtoValidator:AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email gereklidir").EmailAddress().WithMessage("email yanlış");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password gereklidir");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username gereklidir");
        }
    }
}
