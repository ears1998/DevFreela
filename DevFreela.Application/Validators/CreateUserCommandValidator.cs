using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.Constants;
using FluentValidation;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DevFreela.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(p => p.Email)
                .EmailAddress()
                .WithMessage("Invalid e-mail");

            RuleFor(p => p.FullName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Full name is required");

            RuleFor(p => p.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("User name is required");

            RuleFor(p => p.Password)
                .Must(ValidatePassword)
                .WithMessage("The password must contain at least 8 characters, 1 uppercase letter, 1 lowercase letter and 1 special character ");

            RuleFor(p => p.Role)
                .NotNull()
                .NotEmpty()
                .Must(ValidateIfRoleExists)
                .WithMessage("Role cannot be null or empty. Role must be a valid Role");

        }

        private bool ValidatePassword(string password)
        {
            var regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            return regex.IsMatch(password);
        }

        public bool ValidateIfRoleExists(string role)
        {
            var existingRoles = GetExistingRoles();

            if (!existingRoles.Contains(role)) return false;

            return true;
        }

        private string?[] GetExistingRoles()
        {
            var existingRoles = new string?[] { };

            var typeOfRolesStaticClass = typeof(Roles);
            var fieldsInRolesStaticClass = typeOfRolesStaticClass.GetFields(BindingFlags.Static | BindingFlags.Public);

            if (!fieldsInRolesStaticClass.Any()) return existingRoles;

            existingRoles = fieldsInRolesStaticClass.Select(f => f.GetValue(null).ToString()).ToArray();

            return existingRoles;
        }

    }
}
