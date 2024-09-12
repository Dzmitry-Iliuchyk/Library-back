using FluentValidation;
using FluentValidation.Validators;
using Library.Domain.Models;

namespace Library.Application.Validator {
    public class UserValidator: AbstractValidator<User> {
        public UserValidator() {
            RuleFor( user => user.Id ).NotEmpty();
            RuleFor( user => user.UserName ).NotEmpty().MinimumLength(8);
            RuleFor( user => user.Email ).EmailAddress( EmailValidationMode.AspNetCoreCompatible ).NotEmpty();
            RuleFor( user => user.PasswordHash ).NotEmpty();
            When( user => user.Books?.Count()>0, () => {
                RuleForEach( user => user.Books ).SetValidator( new BookValidator() );
            } );

        }
    }
}
