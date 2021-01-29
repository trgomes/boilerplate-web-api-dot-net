using FluentValidation;
using Solution.Domain.Models;

namespace Solution.Domain.Validations
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(o => o.Name)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(2, 100).WithMessage("O nome deve ter entre 2 e 100 caracteres.");

            RuleFor(o => o.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("Email informado não é válido.");

            RuleFor(o => o.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.");
        }
    }
}
