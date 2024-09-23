namespace Library.WebAPI.Contracts.Book {
    public class BooksResponseWithCount {
        public IList<BookResponce> Books {  get; set; }
        public int Count { get; set; }

    }
}
