using FluentValidation;
using Library.Domain.Models.Book;

namespace Library.Application.Validator {
    public class BookValidator: AbstractValidator<Book> {
        public BookValidator() {
            RuleFor( book => book.Id ).NotEmpty();
            RuleFor( book => book.ISBN ).NotEmpty().MaximumLength( 13 ).MinimumLength( 10 );
            RuleFor( book => book.Title ).NotEmpty().MinimumLength(2);
            RuleFor( book => book.Description ).NotEmpty().MinimumLength(20);
            RuleFor( book => book.Genre ).NotEmpty().MinimumLength(1);
            RuleFor( book => book.AuthorId).NotEmpty();
            When( book => book is TakenBook, () => {
                RuleFor( ( taken ) => ( taken as TakenBook ).ClientId ).NotEmpty();
                RuleFor( ( taken ) => ( taken as TakenBook ).TakenAt ).GreaterThan( DateTime.UtcNow.AddYears( -100 ) );
                RuleFor( ( taken ) => ( taken as TakenBook ).ReturnTo ).GreaterThan( DateTime.UtcNow.AddYears( -100 ) );
                RuleFor( ( taken ) => ( taken as TakenBook ).TakenAt ).LessThan( taken => ( taken as TakenBook ).ReturnTo );
                } );
        }
    }
}
