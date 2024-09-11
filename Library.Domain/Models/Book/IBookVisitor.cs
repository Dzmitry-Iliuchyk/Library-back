namespace Library.Domain.Models.Book
{
    public interface IBookVisitor<out T>
    {
        public T Visit( FreeBook book );
        public T Visit( TakenBook book );
    }
}