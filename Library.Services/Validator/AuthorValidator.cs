using FluentValidation;
using Library.Domain.Models;

namespace Library.Application.Validator {
    public class AuthorValidator: AbstractValidator<Author> {
        public AuthorValidator() {
            RuleFor( author => author.Id ).NotEmpty();
            RuleFor( author => author.FirstName ).NotEmpty();
            RuleFor( author => author.LastName ).NotEmpty();
            RuleFor( author => author.Country ).NotEmpty().MinimumLength( 3 );
            RuleFor( author => author.Birthday ).NotEmpty().GreaterThan( DateTime.UtcNow.AddYears( -150 ) ).LessThan( DateTime.UtcNow.AddYears( -9 ) );
            When( author => author.Books?.Count() > 0, () => {
                RuleForEach( author => author.Books ).SetValidator( new BookValidator() );
            } );
        }
    }
}
